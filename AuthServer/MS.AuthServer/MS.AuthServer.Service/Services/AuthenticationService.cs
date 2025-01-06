using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MS.AuthServer.Core.Configuration;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Entities;
using MS.AuthServer.Core.Repositories;
using MS.AuthServer.Core.Services;
using MS.AuthServer.Core.UnitOfWork;

namespace MS.AuthServer.Service.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly List<Client> _clients;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _userRefreshRepository;

    public AuthenticationService(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshRepository, IOptions<List<Client>> clients) 
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _userRefreshRepository = userRefreshRepository;
        _clients = clients.Value;
    }

    public async Task<GenericResponse<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
        {
            return GenericResponse<TokenDto>.Fail("Email or password is wrong", HttpStatusCode.NotFound, true);
        }
        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return GenericResponse<TokenDto>.Fail("Email or password is wrong", HttpStatusCode.NotFound, true);
        }
        var token = _tokenService.CreateToken(user);

        var userRefreshToken = await _userRefreshRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

        if (userRefreshToken is null)
        {
            userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Code = token.RefreshToken
            };
            await _userRefreshRepository.AddAsync(userRefreshToken);
        }
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration.ToUniversalTime();
        }
        await _unitOfWork.SaveChangesAsync();

        return GenericResponse<TokenDto>.Success(token, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        var existRefreshToken = _userRefreshRepository.Where(x => x.Code == refreshToken).SingleOrDefault();
        if (existRefreshToken is null)
        {
            return GenericResponse<TokenDto>.Fail("Refresh token not found", HttpStatusCode.NotFound, true);
        }

        var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

        if (user is null)
        {
            return GenericResponse<TokenDto>.Fail("User not found", HttpStatusCode.NotFound, true);
        }

        var token = _tokenService.CreateToken(user);

        if (token is null)
        {
            return GenericResponse<TokenDto>.Fail("Token could not be created", HttpStatusCode.InternalServerError, true);
        }

        existRefreshToken.Code = token.RefreshToken;
        existRefreshToken.Expiration = token.RefreshTokenExpiration.ToUniversalTime();

        await _unitOfWork.SaveChangesAsync();

        return GenericResponse<TokenDto>.Success(token, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
    {
        var existRefreshToken = _userRefreshRepository.Where(x => x.Code == refreshToken).SingleOrDefault();

        if (existRefreshToken is null)
        {
            return GenericResponse<NoDataDto>.Fail("Refresh token not found", HttpStatusCode.NotFound, true);
        }

        _userRefreshRepository.Remove(existRefreshToken);
        _unitOfWork.SaveChangesAsync();

        return GenericResponse<NoDataDto>.Success(HttpStatusCode.OK);
    }

    public async Task<GenericResponse<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto)
    {
        var client = _clients.SingleOrDefault(x => x.ClientId == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
        if (client is null)
        {
            return GenericResponse<ClientTokenDto>.Fail("Client not found", HttpStatusCode.NotFound, true);
        }
        var token = _tokenService.CreateClientToken(client);
        if (token is null)
        {
            return GenericResponse<ClientTokenDto>.Fail("Token could not be created", HttpStatusCode.InternalServerError, true);
        }
        return GenericResponse<ClientTokenDto>.Success(token, HttpStatusCode.OK);
    }
}

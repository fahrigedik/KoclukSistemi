using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.AuthServer.Core.Configuration;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Entities;
using MS.AuthServer.Core.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MS.AuthServer.Service.Services;

public class TokenService : ITokenService
{
    private readonly CustomTokenOption _tokenOptions;

    private readonly UserManager<AppUser> _userManager;

    public TokenService(IOptions<CustomTokenOption> tokenOptions, UserManager<AppUser> userManager)
    {
        _tokenOptions = tokenOptions.Value;
        _userManager = userManager;
    }

    private string CreateRefreshToken()
    {
        var numberByte = new Byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);
    }

    private async Task<IEnumerable<Claim>> GetClaims(AppUser appUser, List<string> audiences)
    {
        var userRoles = await _userManager.GetRolesAsync(appUser);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Name, appUser.UserName),
            new Claim(ClaimTypes.Email, appUser.Email)
        };
        claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }


    public TokenDto CreateToken(AppUser appUser)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.RefreshTokenExpiration);

        var securityKey = SignInService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        SigningCredentials signingCredentials =
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: GetClaims(appUser, _tokenOptions.Audiences).Result,
            signingCredentials: signingCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDto
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration
        };
        return tokenDto;
    }



    private IEnumerable<Claim> GetClaimsByClient(Client client)
    {
        var claims = new List<Claim>();
        claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.ClientId));
        return claims;
    }

    public ClientTokenDto CreateClientToken(Client client)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SignInService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        SigningCredentials signingCredentials =
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: GetClaimsByClient(client),
            signingCredentials: signingCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var clientTokenDto = new ClientTokenDto
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration
        };

        return clientTokenDto;
    }
}

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
        claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        return claims;
    }


    public TokenDto CreateToken(AppUser appUser)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);

        var securityKey = SignInService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        SigningCredentials signingCredentials =
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
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
}

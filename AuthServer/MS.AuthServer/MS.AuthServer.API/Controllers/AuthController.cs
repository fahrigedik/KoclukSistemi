using Microsoft.AspNetCore.Mvc;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Services;

namespace MS.AuthServer.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : CustomBaseController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateToken(LoginDto loginDto)
    {
        var result = await _authenticationService.CreateTokenAsync(loginDto);
        return ActionResultInstance(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token);
        return ActionResultInstance(result);
    }

    [HttpPost]
    public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var result = await _authenticationService.RevokeRefreshTokenAsync(refreshTokenDto.Token);
        return ActionResultInstance(result);
    }


    [HttpPost]
    public async Task<IActionResult> CreateTokenByClient(ClientLoginDto clientLoginDto)
    {
        var result = await _authenticationService.CreateTokenByClient(clientLoginDto);
        return ActionResultInstance(result);
    }
}


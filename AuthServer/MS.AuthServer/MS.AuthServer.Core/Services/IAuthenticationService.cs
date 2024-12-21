using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.Core.Services;

public interface IAuthenticationService
{
    Task<GenericResponse<TokenDto>> CreateTokenAsync(LoginDto loginDto);

    Task<GenericResponse<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);

    Task<GenericResponse<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken);

    Task<GenericResponse<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto);

}


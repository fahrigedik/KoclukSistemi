using System.Net.Http.Headers;
using System.Net.Http.Json;
using MS.CoachSystem.Core.DTOs;

namespace MS.CoachSystem.Service.Services;
public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GenericResponse<TokenDto>> LoginAsync(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/CreateToken", loginDto);
        response.EnsureSuccessStatusCode();

        var tokenResponse = await response.Content.ReadFromJsonAsync<GenericResponse<TokenDto>>();
        if (response.IsSuccessStatusCode)
        {
            tokenResponse.IsSuccessfull = true;
        }
        else
        {
            tokenResponse.IsSuccessfull = false;
        }
        return tokenResponse;
    }

    public void SetAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

}

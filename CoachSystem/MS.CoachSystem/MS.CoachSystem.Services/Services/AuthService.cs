using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Helpers;


namespace MS.CoachSystem.Service.Services;
public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _httpClient.BaseAddress = new Uri("https://localhost:7076/");
    }
    public async Task<GenericResponse<TokenDto>> LoginAsync(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/CreateToken", loginDto);

        var tokenResponse = await response.Content.ReadFromJsonAsync<GenericResponse<TokenDto>>();
        if (response.IsSuccessStatusCode)
        {
            tokenResponse.IsSuccessfull = true;
            return tokenResponse;
        }
        else
        {
            tokenResponse.IsSuccessfull = false;
            tokenResponse = GenericResponse<TokenDto>.Fail(new ErrorDto(tokenResponse.Error.Errors,true),HttpStatusCode.NotFound);
        }
        return tokenResponse;
    }

   


}

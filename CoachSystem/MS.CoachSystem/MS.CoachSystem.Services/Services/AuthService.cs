using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MS.CoachSystem.Core.DTOs;


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
    }

    public void AddAuthorizationHeader()
    {
        var accessToken = _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
        if (!string.IsNullOrEmpty(accessToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
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

    public async Task<GenericResponse<AppUserDto>> GetUsersByIdsAsync(List<string> userIds)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/GetUsersByIds", userIds);
        var userResponse = await response.Content.ReadFromJsonAsync<GenericResponse<AppUserDto>>();
        if (response.IsSuccessStatusCode)
        {
            userResponse.IsSuccessfull = true;
            return userResponse;
        }
        else
        {
            userResponse.IsSuccessfull = false;
            userResponse = GenericResponse<AppUserDto>.Fail(new ErrorDto(userResponse.Error.Errors, true), HttpStatusCode.NotFound);
        }
        return userResponse;
    }

    public async Task<GenericResponse<CreateStudentUserResponseDto>> CreateStudentUserAsync(CreateUserDto createUserDto)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync("api/user/createstudentuser", createUserDto);

        var result = await response.Content.ReadFromJsonAsync<GenericResponse<CreateStudentUserResponseDto>>();
        return result;
    }


}

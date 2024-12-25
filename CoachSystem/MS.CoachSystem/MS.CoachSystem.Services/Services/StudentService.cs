using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Helpers;
using System.Net;
using System.Net.Http.Json;

namespace MS.CoachSystem.Service.Services;
public class StudentService
{

    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StudentService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<AppUserDto>> GetStudentsByIdsAsync(List<string> userIds)
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
        AuthHelper.AddAuthorizationHeader(_httpClient, _httpContextAccessor);
        var response = await _httpClient.PostAsJsonAsync("api/user/createstudentuser", createUserDto);

        var result = await response.Content.ReadFromJsonAsync<GenericResponse<CreateStudentUserResponseDto>>();
        return result;
    }
}


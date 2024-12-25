using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.Core.Services;
public interface IUserService
{
    Task<GenericResponse<CreateUserDto>> CreateUserAsync(CreateUserDto createUserDto);

    Task<GenericResponse<AppUserDto>> GetUserByNameAsync(string name);

    Task<GenericResponse<CreateUserDto>> CreateCoachUserAsync(CreateUserDto createUserDto);

    Task<GenericResponse<List<AppUserDto>>> GetUsersByIdsAsync(List<string> userIds);

    Task<GenericResponse<CreateStudentUserResponseDto>> CreateStudentUserAsync(CreateUserDto createUserDto);
}


using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.Core.Services;
public interface IUserService
{
    Task<GenericResponse<CreateUserDto>> CreateUserAsync(CreateUserDto createUserDto);

    Task<GenericResponse<AppUserDto>> GetUserByNameAsync(string name);
}


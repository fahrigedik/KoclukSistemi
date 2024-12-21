using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.Core.Services;
public interface IUserService
{
    Task<CreateUserDto> CreateUserAsync(CreateUserDto createUserDto);

    Task<AppUserDto> GetUserByNameAsync(string name);
}


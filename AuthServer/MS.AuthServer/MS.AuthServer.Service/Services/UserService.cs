using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Entities;
using MS.AuthServer.Core.Services;

namespace MS.AuthServer.Service.Services;
public class UserService(UserManager<AppUser> _userManager, IMapper _mapper) : IUserService
{

    public async Task<GenericResponse<CreateUserDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new AppUser()
        {
            UserName = createUserDto.UserName,
            Email = createUserDto.Email,
            BirthDate = createUserDto.BirthDate,
            Gender = createUserDto.Gender
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if(!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return GenericResponse<CreateUserDto>.Fail(new ErrorDto(errors, true), HttpStatusCode.NotFound);
        }

        var userDto = _mapper.Map<CreateUserDto>(user);

        return GenericResponse<CreateUserDto>.Success(userDto, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<AppUserDto>> GetUserByNameAsync(string name)
    {
        var user = await _userManager.FindByNameAsync(name);

        if (user is null)
        {
            return GenericResponse<AppUserDto>.Fail("User is not found", HttpStatusCode.NotFound, true);
        }

        var userDto = _mapper.Map<AppUserDto>(user);

        return GenericResponse<AppUserDto>.Success(userDto, HttpStatusCode.OK);
    }
}


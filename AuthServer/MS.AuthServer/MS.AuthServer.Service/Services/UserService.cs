﻿using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Entities;
using MS.AuthServer.Core.Enum;
using MS.AuthServer.Core.Services;

namespace MS.AuthServer.Service.Services;
public class UserService(UserManager<AppUser> _userManager, IMapper _mapper) : IUserService
{

    public async Task<GenericResponse<CreateUserDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new AppUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = createUserDto.UserName,
            Email = createUserDto.Email,
            BirthDate = createUserDto.BirthDate,
            Gender = createUserDto.Gender,
            TCKN = createUserDto.TCKN,
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
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

    public async Task<GenericResponse<CreateUserDto>> CreateCoachUserAsync(CreateUserDto createUserDto)
    {
        var user = new AppUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = createUserDto.UserName,
            Name = createUserDto.Name,
            Surname = createUserDto.Surname,
            Email = createUserDto.Email,
            BirthDate = createUserDto.BirthDate,
            Gender = createUserDto.Gender,
            TCKN = createUserDto.TCKN,
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return GenericResponse<CreateUserDto>.Fail(new ErrorDto(errors, true), HttpStatusCode.NotFound);
        }

        var AddUserRole = await _userManager.AddToRoleAsync(user, "coach");
        if (!AddUserRole.Succeeded)
        {
            var errors = AddUserRole.Errors.Select(x => x.Description).ToList();
            return GenericResponse<CreateUserDto>.Fail(new ErrorDto(errors, true), HttpStatusCode.NotFound);
        }

        var userDto = _mapper.Map<CreateUserDto>(user);
        return GenericResponse<CreateUserDto>.Success(userDto, HttpStatusCode.OK);

    }

    public async Task<GenericResponse<List<AppUserDto>>> GetUsersByIdsAsync(List<string> userIds)
    {
        var users = await _userManager.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        if (users == null || users.Count == 0)
        {
            return GenericResponse<List<AppUserDto>>.Fail("Users not found", HttpStatusCode.NotFound, true);
        }

        var userDtos = users.Select(user => new AppUserDto
        {
            UserName = user.UserName,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        }).ToList();

        return GenericResponse<List<AppUserDto>>.Success(userDtos, HttpStatusCode.OK);

    }

    public async Task<GenericResponse<CreateStudentUserResponseDto>> CreateStudentUserAsync(CreateUserDto createUserDto)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = createUserDto.UserName,
            Email = createUserDto.Email,
            Name = createUserDto.Name,
            Surname = createUserDto.Surname,
            BirthDate = createUserDto.BirthDate,
            TCKN = createUserDto.TCKN,
            Gender = createUserDto.Gender
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return GenericResponse<CreateStudentUserResponseDto>.Fail(new ErrorDto(errors, true), HttpStatusCode.BadRequest);
        }

        var AddUserRole = await _userManager.AddToRoleAsync(user, "student");
        if (!AddUserRole.Succeeded)
        {
            var errors = AddUserRole.Errors.Select(x => x.Description).ToList();
            return GenericResponse<CreateStudentUserResponseDto>.Fail(new ErrorDto(errors, true), HttpStatusCode.NotFound);
        }

        var responseDto = new CreateStudentUserResponseDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Name = user.Name,
            Surname = user.Surname,
            BirthDate = user.BirthDate ?? DateTime.MinValue,
            TCKN = user.TCKN,
            Gender = user.Gender ?? Gender.Other
        };

        return GenericResponse<CreateStudentUserResponseDto>.Success(responseDto, HttpStatusCode.OK);
    }
}


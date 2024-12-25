using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Services;

namespace MS.AuthServer.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var result = await _userService.CreateUserAsync(createUserDto);
        return ActionResultInstance(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var result = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
        return ActionResultInstance(result);
    }


    [HttpPost]
    public async Task<IActionResult> CreateCoachUser(CreateUserDto createUserDto)
    {

        var result = await _userService.CreateCoachUserAsync(createUserDto);
        return ActionResultInstance(result);
    }

    [Authorize(Roles = "coach")]
    [HttpPost]
    public async Task<IActionResult> GetUsersByIds(List<string> userIds)
    {
        var result = await _userService.GetUsersByIdsAsync(userIds);
        return ActionResultInstance(result);
    }

    [Authorize(Roles = "coach")]
    [HttpPost]
    public async Task<IActionResult> CreateStudentUser(CreateUserDto createUserDto)
    {
        var result = await _userService.CreateStudentUserAsync(createUserDto);
        return ActionResultInstance(result);
    }
}

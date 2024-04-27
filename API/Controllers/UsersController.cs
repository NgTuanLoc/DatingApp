using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        // var users = await _userRepository.GetUsersAsync();
        // var usersDto = _mapper.Map<IEnumerable<MemberDto>>(users);
        var usersDto = await _userRepository.GetMembersAsync();
        return Ok(usersDto);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUserByUsername(string username)
    {
        // var user = await _userRepository.GetUserByUsernameAsync(username);
        // var userDto = _mapper.Map<MemberDto>(user);
        var userDto = await _userRepository.GetMemberByUsernameAsync(username);
        return Ok(userDto);
    }
}
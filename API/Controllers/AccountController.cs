using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController(DataContext context) : BaseController
{
    private readonly DataContext _context = context;

    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        bool userExisted = await UserExisted(registerDto.Username, cancellationToken);
        if (userExisted) return BadRequest("Username is taken");

        using var hmac = new HMACSHA512();

        AppUser user = new()
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(user);
    }

    private async Task<bool> UserExisted(string username, CancellationToken cancellationToken)
    {
        bool userExisted = await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower(), cancellationToken);
        return userExisted;
    }
}
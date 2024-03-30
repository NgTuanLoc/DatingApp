using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUserById(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id, cancellationToken);
        return Ok(user);
    }
}
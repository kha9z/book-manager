using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using backend.Models;
using backend.Data;
using backend.DTOs;

namespace backend.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public AccountController(AppDbContext context)
    { _context = context; }


[HttpPost("register")]
public IActionResult Register([FromBody] RegisterDTO request)
{
    var userExists = _context.Users.Any(user => 
        user.Username == request.Username
        ||
        user.Email == request.Email);
        
        if (userExists)
        {
            return Conflict("Username or email already exists");
        }
        
        var user = new User
        {
            Username = request.Username,
            Email = request.Email
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _context.Users.Add(user);
        _context.SaveChanges();
        return StatusCode(201, "Account created");
}
}

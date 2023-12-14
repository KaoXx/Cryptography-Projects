using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.DTO_s;
using WebApp.Models;

namespace WebApp.Controllers;
[Authorize]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public UserController(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _context = context;
    }
    [Authorize(Roles = "Admin")]
    public IActionResult UserPasswords()
    {
        var users = _context.Users.ToList();
        var userPasswords = users.Select(u => new UserPasswordDTO
        {
            Username = u.Email,
            CaesarPassword = u.PasswordCesar,
            VigenerePassword = u.PasswordVigenere
        }).ToList();
        return View(userPasswords);
    }
}
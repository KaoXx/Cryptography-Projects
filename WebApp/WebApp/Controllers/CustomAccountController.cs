using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class CustomAccountController : Controller
{
    
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private string key = "trash";

    public CustomAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var ceasar = Utils.EncryptionService.EncryptCaesar(model.Password, 10);
            var vigenere = Utils.EncryptionService.EncryptVigenere(model.Password, key);
            var user = new AppUser() { UserName = model.Email , Email = model.Email , 
                PasswordCesar = ceasar,PasswordVigenere = vigenere,PasswordPlainText = model.Password};
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (user.Email.EndsWith("@admin.com"))
                {
                    // Check if the role "admin" exists
                    var roleExists = await _roleManager.RoleExistsAsync("admin");
    
                    if (!roleExists)
                    {
                        // If the role doesn't exist, create it
                        var createRoleResult = await _roleManager.CreateAsync(new IdentityRole("admin"));

                        if (!createRoleResult.Succeeded)
                        {
                            // Handle the error (e.g., log it or return an error response)
                            return RedirectToAction("Error", "Home");
                        }
                    }
                    // Now, add the user to the "admin" role
                    var addToRoleResult = await _userManager.AddToRoleAsync(user, "admin");
                    if (addToRoleResult.Succeeded)
                    {
                        Console.WriteLine("User added to role");
                    }
                    else
                    {
                        // Handle the error (e.g., log it or return an error response)
                        return RedirectToAction("Error", "Home");
                    }
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }
}
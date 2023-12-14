using System.Drawing;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

public class AppUser : IdentityUser
{
    public ICollection<Person>? Persons { get; set; }
    public string PasswordPlainText { get; set; }
    public string PasswordCesar { get; set; }
    public string PasswordVigenere { get; set; }
    
}
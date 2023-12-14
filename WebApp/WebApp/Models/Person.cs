using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

public class Person
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string PersonName { get; set; }
    [MaxLength(64)]
    public string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
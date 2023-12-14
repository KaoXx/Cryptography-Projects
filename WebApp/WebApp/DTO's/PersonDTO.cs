using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO_s;

public class PersonDTO
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string PersonName { get; set; }
}
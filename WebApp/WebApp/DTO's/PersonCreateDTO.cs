using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO_s;

public class PersonCreateDTO
{
    [MaxLength(128)]
    public string PersonName { get; set; }
}
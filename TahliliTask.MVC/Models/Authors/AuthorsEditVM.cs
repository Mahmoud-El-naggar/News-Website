using System.ComponentModel.DataAnnotations;

namespace TahliliTask.MVC.Models;

public class AuthorsEditVM
{
    public Guid Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;
}

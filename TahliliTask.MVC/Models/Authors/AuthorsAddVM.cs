using System.ComponentModel.DataAnnotations;

namespace TahliliTask.MVC.Models;

public class AuthorsAddVM
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

}

using System.ComponentModel.DataAnnotations;

namespace TahliliTask.MVC.Models;

public class NewsAddVM
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;

    [Required]
    public IFormFile Image { get; set; } = null!;
    [Required]
    public DateTime PublicationDate { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
}

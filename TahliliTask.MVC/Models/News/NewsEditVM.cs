using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TahliliTask.MVC.Models;

public class NewsEditVM
{

    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = null!; 
    public IFormFile? Image { get; set; }
    [Required]
    [DisplayName("Publication Date")]
    public DateTime PublicationDate { get; set; }
    [Required]
    [DisplayName("Author Name")]
    public Guid AuthorId { get; set; }
}

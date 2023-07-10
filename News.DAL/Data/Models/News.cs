using System.ComponentModel.DataAnnotations;

namespace TahliliTask.DAL;

public class News
{

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    
    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}

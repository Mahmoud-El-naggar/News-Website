using System.ComponentModel.DataAnnotations;

namespace TahliliTask.BL;

public class NewsEditDto
{
    public Guid Id { get; set; }
	[Required]
	public string Title { get; set; } = string.Empty;
	[Required]
	public string Description { get; set; } = string.Empty;
	[Required] 
	public string ImagePath { get; set; } = string.Empty;
	[Required]
    [WeekFromToday]
	public DateTime PublicationDate { get; set; }
	[Required]
	public Guid AuthorId { get; set; }
}

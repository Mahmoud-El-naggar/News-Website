namespace TahliliTask.BL;

public class NewsDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string ImagePath { get; set; } = string.Empty;
	public DateTime PublicationDate { get; set; }
    public DateTime CreationDate { get; set; }
    public string AuthorName { get; set; } = string.Empty;

}

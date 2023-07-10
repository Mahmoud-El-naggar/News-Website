namespace TahliliTask.BL;

public class AuthorDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<string>? NewsTitle { get; set; }
}

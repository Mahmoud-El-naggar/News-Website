namespace TahliliTask.DAL;

public class Author
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public ICollection<News> News { get; set; } = new HashSet<News>();
}

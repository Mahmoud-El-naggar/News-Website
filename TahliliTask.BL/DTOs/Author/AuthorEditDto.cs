using System.ComponentModel.DataAnnotations;

namespace TahliliTask.BL;

public class AuthorEditDto
{
    public Guid Id { get; set; }
	[Required]
	[MinLength(3)]
	[MaxLength(20)]
	public string Name { get; set; } = string.Empty;
}

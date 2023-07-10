using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TahliliTask.BL;

public class NewsAddDto
{
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

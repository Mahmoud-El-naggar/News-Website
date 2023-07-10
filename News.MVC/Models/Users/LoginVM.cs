using System.ComponentModel.DataAnnotations;

namespace TahliliTask.MVC.Models;

public class LoginVM
{
	[Required]
	public string UserName { get; set; } = string.Empty;
	[Required]
	public string Password { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;

namespace TahliliTask.BL;

public class WeekFromToday:ValidationAttribute
{
	public override bool IsValid(object? value)
	{
		return value is DateTime publicationDate && publicationDate >= DateTime.Now && publicationDate <= DateTime.Now.AddDays(7);	
	}
}

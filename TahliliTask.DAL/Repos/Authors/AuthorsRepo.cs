using Microsoft.EntityFrameworkCore;

namespace TahliliTask.DAL;

public class AuthorsRepo : GenericRepo<Author>, IAuthorsRepo
{
	private readonly NewsContext context;

	public AuthorsRepo(NewsContext context) : base(context)
	{
		this.context = context;
	}

	public Author? GetAuthorDetails(Guid id)
	{
		return context.Set<Author>()
						.Include(author => author.News)
						.FirstOrDefault(author => author.Id == id);
	}
}

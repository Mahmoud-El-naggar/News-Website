using Microsoft.EntityFrameworkCore;

namespace TahliliTask.DAL;

public class NewsRepo : GenericRepo<News>, INewsRepo
{
	private readonly NewsContext context;

	public NewsRepo(NewsContext context) : base(context)
	{
		this.context = context;
	}

	public News? GetNewsDetails(Guid id)
	{
		return context.Set<News>()
			.Include(news => news.Author).FirstOrDefault(news => news.Id == id);
	}
}

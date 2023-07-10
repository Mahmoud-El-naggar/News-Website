namespace TahliliTask.DAL;

public class UnitOfWork : IUnitOfWork
{
	private readonly NewsContext context;

	public IAuthorsRepo AuthorsRepo { get; }
	public INewsRepo NewsRepo { get; }
    public UnitOfWork(NewsContext context,IAuthorsRepo authorsRepo,INewsRepo newsRepo)
    {
		this.context = context;
		AuthorsRepo = authorsRepo;
		NewsRepo = newsRepo;
    }

    public int Save()
	{
		return context.SaveChanges();
	}
}

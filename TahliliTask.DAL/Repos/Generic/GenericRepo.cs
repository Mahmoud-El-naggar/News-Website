using Microsoft.EntityFrameworkCore;

namespace TahliliTask.DAL;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
	private readonly NewsContext context;

	public GenericRepo(NewsContext context)
    {
		this.context = context;
	}
    public IEnumerable<T> GetAll()
	{
		return context.Set<T>().AsNoTracking();
	}

	public T? GetById(Guid id)
	{
		return context.Set<T>().Find(id);
	}

	public void Add(T entity)
	{
		context.Set<T>().Add(entity);
	}
	public void Update(T entity)
	{
		context.Set<T>().Update(entity);
	}

	public void Delete(T entity)
	{
		context.Set<T>().Remove(entity);
	}


}

namespace TahliliTask.DAL;

public interface IGenericRepo<T> where T : class
{
	IEnumerable<T> GetAll();
	T? GetById(Guid id);
	void Add(T entity);
	void Update(T entity);
	void Delete(T entity);

}

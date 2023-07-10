namespace TahliliTask.DAL;

public interface INewsRepo:IGenericRepo<News>
{
	News? GetNewsDetails(Guid id);
}

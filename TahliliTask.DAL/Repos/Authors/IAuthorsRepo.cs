namespace TahliliTask.DAL;

public interface IAuthorsRepo : IGenericRepo<Author>
{
	Author? GetAuthorDetails(Guid id);
}

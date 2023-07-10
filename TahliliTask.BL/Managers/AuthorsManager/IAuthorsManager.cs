namespace TahliliTask.BL;

public interface IAuthorsManager
{
	IEnumerable<AuthorDisplayDto> GetAll();
	AuthorDetailsDto? GetDetails(Guid id);
	AuthorEditDto? GetEdit(Guid id);

	void Add(AuthorAddDto authorAddDto);
	void Update(AuthorEditDto authorEditDto);
	void Delete(Guid id);
}

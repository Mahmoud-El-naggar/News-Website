namespace TahliliTask.BL;

public interface INewsManager
{

	IEnumerable<NewsDisplayDto> GetAll();
	IEnumerable<NewsDisplayDto> GetAllPublish();
	NewsDetailsDto? GetDetails(Guid id);
	NewsEditDto? GetEdit(Guid id);

	void Add(NewsAddDto newsAddDto);
	void Update(NewsEditDto newsEditDto);
	void Delete(Guid id);
}

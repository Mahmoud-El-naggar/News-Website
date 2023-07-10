using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata.Ecma335;
using TahliliTask.DAL;

namespace TahliliTask.BL;

public class AuthorsManager:IAuthorsManager
{
	private readonly IUnitOfWork unitOfWork;

	public AuthorsManager(IUnitOfWork unitOfWork)
    {
		this.unitOfWork = unitOfWork;
	}


	public IEnumerable<AuthorDisplayDto> GetAll()
	{
		var authors = unitOfWork.AuthorsRepo.GetAll();
		return authors.Select(author => new AuthorDisplayDto
		{
			Id = author.Id,
			Name = author.Name,
		});
	}

	public AuthorDetailsDto? GetDetails(Guid id)
	{
		var author = unitOfWork.AuthorsRepo.GetAuthorDetails(id);
		if(author is null)
			return null;

		return new AuthorDetailsDto
		{
			Id = author.Id,
			Name = author.Name,
			NewsTitle = author.News.Select(news => news.Title).ToList()
		};
	}
	
	public void Add(AuthorAddDto authorAddDto)
	{
		unitOfWork.AuthorsRepo.Add(new Author
		{
			Id = Guid.NewGuid(),
			Name = authorAddDto.Name,
		});
		unitOfWork.Save();
	}

	public void Update(AuthorEditDto authorEditDto)
	{
		var author = unitOfWork.AuthorsRepo.GetById(authorEditDto.Id);
		if (author is null)
			return;

		author.Name = authorEditDto.Name;

		unitOfWork.AuthorsRepo.Update(author);
		unitOfWork.Save();
	}

	public void Delete(Guid id)
	{
		var author = unitOfWork.AuthorsRepo.GetById(id);
		if( author is null)
			return;

		unitOfWork.AuthorsRepo.Delete(author);
		unitOfWork.Save();
	}

	public AuthorEditDto? GetEdit(Guid id)
	{
		var author = unitOfWork.AuthorsRepo.GetById(id);
		if(author is null)
			return null;
		return new AuthorEditDto { Id = author.Id , Name = author.Name };
	}
}


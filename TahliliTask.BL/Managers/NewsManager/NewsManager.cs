
using System.Xml.Linq;
using TahliliTask.DAL;
using static System.Net.Mime.MediaTypeNames;

namespace TahliliTask.BL;

public class NewsManager : INewsManager
{
	private readonly IUnitOfWork unitOfWork;

	public NewsManager(IUnitOfWork unitOfWork)
    {
		this.unitOfWork = unitOfWork;
	}

    public void Add(NewsAddDto newsAddDto)
	{
		
		unitOfWork.NewsRepo.Add(new News
		{
			Id = Guid.NewGuid(),
			Title = newsAddDto.Title,
			AuthorId = newsAddDto.AuthorId,
			Description = newsAddDto.Description,
			Image = newsAddDto.ImagePath,
			PublicationDate = newsAddDto.PublicationDate,
		});
		unitOfWork.Save();
	}

	public void Delete(Guid id)
	{
		var news = unitOfWork.NewsRepo.GetById(id);
		if (news is null)
			return;
		unitOfWork.NewsRepo.Delete(news);
		unitOfWork.Save();
	}

	public IEnumerable<NewsDisplayDto> GetAll()
	{
		return unitOfWork.NewsRepo.GetAll().Select(news => new NewsDisplayDto
		{
			Id = news.Id,
			Title = news.Title,
			ImagePath = news.Image
		});
	}

    public IEnumerable<NewsDisplayDto> GetAllPublish()
    {
		return unitOfWork.NewsRepo.GetAll().Where(n=>n.PublicationDate.Date <= DateTime.Now.Date).Select(news => new NewsDisplayDto
        {
            Id = news.Id,
            Title = news.Title,
            ImagePath = news.Image
        });
    }

    public NewsDetailsDto? GetDetails(Guid id)
	{
		var news =  unitOfWork.NewsRepo.GetNewsDetails(id);
		if(news is null)
			return null;
		return new NewsDetailsDto
		{
			Id = news.Id,
			Title = news.Title,
			CreationDate = news.CreationDate,
			PublicationDate = news.PublicationDate,
			Description = news.Description,
			ImagePath = news.Image,
			AuthorName = news.Author.Name
		};
	}

	public NewsEditDto? GetEdit(Guid id)
	{
		var news = unitOfWork.NewsRepo.GetById(id);
		if (news is null) 
			return null;
		return new NewsEditDto 
		{
			Id = news.Id,
			Title = news.Title,
			Description = news.Description,
			ImagePath = news.Image,
			PublicationDate = news.PublicationDate,
			AuthorId = news.AuthorId
		};
	}

	public void Update(NewsEditDto newsEditDto)
	{
		var news = unitOfWork.NewsRepo.GetById(newsEditDto.Id);
		if (news is null)
			return;
		news.PublicationDate = newsEditDto.PublicationDate;
		news.Title = newsEditDto.Title;
		news.Description = newsEditDto.Description;
		news.AuthorId = newsEditDto.AuthorId;
		news.Image = newsEditDto.ImagePath;
		news.Id = newsEditDto.Id;

		unitOfWork.NewsRepo.Update(news);
		unitOfWork.Save();
	}
}

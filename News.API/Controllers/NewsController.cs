using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TahliliTask.BL;

namespace TahliliTask.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
	private readonly INewsManager newsManager;

	public NewsController(INewsManager newsManager)
	{
		this.newsManager = newsManager;
	}

    [HttpGet]
    public IActionResult GetAllNewsPublish()
    {
        return Ok(newsManager.GetAllPublish());
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
	[Route("Admin")]
    public IActionResult GetAllNews()
	{
		return Ok(newsManager.GetAll());
	}
    
    [HttpGet]
	[Route("{id}")]
	public IActionResult GetById(Guid id)
	{
		if (newsManager.GetDetails(id) is null)
			return NotFound();
		return Ok(newsManager.GetDetails(id));
	}

	[HttpPost]
    [Authorize(Policy = "Admin")]
    public IActionResult Add(NewsAddDto dto)
	{
		if(!ModelState.IsValid)
			return BadRequest("Publication Date Not Valid");

		newsManager.Add(dto);
		return NoContent();
	}

	[HttpPut]
	[Route("{id}")]
    [Authorize(Policy = "Admin")]
    public IActionResult Update(Guid id,NewsEditDto dto)
	{
		if (!ModelState.IsValid || id != dto.Id)
			return BadRequest("Publication Date Not Valid");
		
		newsManager.Update(dto);
		return NoContent();
	}

	[HttpDelete]
	[Route("{id}")]
    [Authorize(Policy = "Admin")]
    public IActionResult Delete(Guid id)
	{
		newsManager.Delete(id);
		return NoContent();
	}

}

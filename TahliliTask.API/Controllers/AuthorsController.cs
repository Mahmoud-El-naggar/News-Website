using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TahliliTask.BL;

namespace TahliliTask.API.Controllers;
[Authorize(Policy = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
	private readonly IAuthorsManager authorsManager;

	public AuthorsController(IAuthorsManager authorsManager)
	{
		this.authorsManager = authorsManager;
	}


	[HttpGet]
	public IActionResult GetAllAuthors()
	{
		return Ok(authorsManager.GetAll());
	}
	[HttpGet]
	[Route("{id}")]
	public IActionResult GetById(Guid id)
	{
		if (authorsManager.GetDetails(id) is null)
			return NotFound();

		return Ok(authorsManager.GetDetails(id));
	}

	[HttpPost]
	public IActionResult Add(AuthorAddDto dto)
	{
		if (!ModelState.IsValid)
			return BadRequest();

		authorsManager.Add(dto);
		return NoContent();
	}

	[HttpPut]
	[Route("{id}")]
	public IActionResult Update(Guid id, AuthorEditDto dto)
	{
		if (!ModelState.IsValid || id != dto.Id)
			return BadRequest();

		authorsManager.Update(dto);
		return NoContent();
	}

	[HttpDelete]
	[Route("{id}")]
	public IActionResult Delete(Guid id)
	{
		authorsManager.Delete(id);
		return NoContent();
	}
}

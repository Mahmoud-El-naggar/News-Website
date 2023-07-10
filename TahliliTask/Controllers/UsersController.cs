using Microsoft.AspNetCore.Mvc;
using TahliliTask.BL;

namespace TahliliTask.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly IUserManager userManager;

	public UsersController(IUserManager userManager)
	{
		this.userManager = userManager;
	}


	[HttpPost]
	[Route("Login")]
	public ActionResult<TokenDto> Login(LoginDto loginDTO)
	{
		var user = userManager.GetUser(loginDTO);
		if (user is null)
			return BadRequest();

		var claims = userManager.GetClaims(user);
		var token = userManager.CreateToken(claims!);

		return token is null ? BadRequest() : token;
	}

}

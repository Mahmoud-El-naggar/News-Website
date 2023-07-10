using Microsoft.AspNetCore.Mvc;
using TahliliTask.MVC.Models;
using Newtonsoft.Json.Linq;
namespace TahliliTask.MVC.Controllers;

public class AdminController : Controller
{
	
    [HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Login(LoginVM request)
	{
		HttpClient client = new HttpClient();
		HttpResponseMessage response = client.PostAsJsonAsync($"{Constants.UsersBaseUrl}/Login", request).Result;
		if (response.IsSuccessStatusCode)
		{
			string responseBody = response.Content.ReadAsStringAsync().Result;
			var token = JObject.Parse(responseBody)["token"]?.ToString();
			Response.Cookies.Append(Constants.TokenKey, token!);

            return RedirectToAction("GetAll","Authors");
        }
		ModelState.AddModelError("", "Username or Password are incorrect");
		return View();
	}

}

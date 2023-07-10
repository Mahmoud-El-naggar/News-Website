using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TahliliTask.MVC;
using TahliliTask.MVC.Models;

namespace News.MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthorsController(IHttpContextAccessor _httpContextAccessor)
        {
            httpClient = new HttpClient();
            httpContextAccessor = _httpContextAccessor;

        }
        private void GetToken()
        {
            var token = httpContextAccessor.HttpContext?.Request.Cookies[Constants.TokenKey];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Bearer, token);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            GetToken();

            HttpResponseMessage response = httpClient.GetAsync(Constants.AuthorsBaseUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var authors = JsonConvert.DeserializeObject<List<AuthorsDisplayVM>>(data);
                return View(authors);
            }
            return RedirectToAction("Login", "Admin");
        }


        [HttpGet]
        public IActionResult Get(Guid id)
        {
            GetToken();
            AuthorsDetailsVM? author = new AuthorsDetailsVM();
            HttpResponseMessage response = httpClient.GetAsync($"{Constants.AuthorsBaseUrl}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<AuthorsDetailsVM>(data);
            }
            return View(author);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AuthorsAddVM author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            GetToken();
            var response = httpClient.PostAsJsonAsync(Constants.AuthorsBaseUrl, author).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAll));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            AuthorsEditVM? author = new AuthorsEditVM();

            GetToken();
            HttpResponseMessage response = httpClient.GetAsync($"{Constants.AuthorsBaseUrl}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<AuthorsEditVM>(data);
            }
            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(AuthorsEditVM author)
        {
            if (!ModelState.IsValid)
                return View(author);
            GetToken();
            HttpResponseMessage response = httpClient.PutAsJsonAsync($"{Constants.AuthorsBaseUrl}/{author.Id}", author).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAll));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            GetToken();
            HttpResponseMessage response = httpClient.DeleteAsync($"{Constants.AuthorsBaseUrl}/{id}").Result;
            return RedirectToAction(nameof(GetAll));
        }
    }
}
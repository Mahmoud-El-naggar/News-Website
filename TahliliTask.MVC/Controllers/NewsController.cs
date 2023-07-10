using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Xml.Linq;
using TahliliTask.MVC;
using TahliliTask.MVC.Models;
using static System.Net.Mime.MediaTypeNames;

namespace News.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        
        public NewsController(IHttpContextAccessor _httpContextAccessor)
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
        public IActionResult Details(Guid id)
        {
            NewsDetailsVM? news = new NewsDetailsVM();
            HttpResponseMessage response = httpClient.GetAsync($"{Constants.NewsBaseUrl}/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                news = JsonConvert.DeserializeObject<NewsDetailsVM>(data);
            }
            return View(news);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<NewsDisplayVM>? list = new List<NewsDisplayVM>();

            HttpResponseMessage response = httpClient.GetAsync(Constants.NewsBaseUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<NewsDisplayVM>>(data);
                return View(list);
            }
            return View();

        }
        [HttpGet]
        [Route("News/Admin")]
        public IActionResult GetAllAdmin()
        {
            
            GetToken();
            HttpResponseMessage response = httpClient.GetAsync($"{Constants.NewsBaseUrl}/Admin").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var list = JsonConvert.DeserializeObject<List<NewsDisplayVM>>(data);
                return View(list);
            }
            return RedirectToAction("Login","Admin");

        }

        [HttpGet]
        public IActionResult Add()
        {
            FillAuthors();
            return View();
        }

         async Task<string> GetSavedImagePath(IFormFile image)
        {
            string fileName = Path.GetFileName(image.FileName);
            fileName = string.Concat(Guid.NewGuid().ToString(), fileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return fileName;
        }

        [HttpPost]
        public IActionResult Add(NewsAddVM newsAddVM)
        {
            if (!ModelState.IsValid)
                return View();

            if (newsAddVM.Image != null && newsAddVM.Image.Length > 0)
            {
                newsAddVM.ImagePath = GetSavedImagePath(newsAddVM.Image).Result;
            }

            GetToken();
            var response = httpClient.PostAsJsonAsync(Constants.NewsBaseUrl, newsAddVM).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllAdmin));
            }
            ModelState.AddModelError("","Publication Date Must Be Within a Week From Now");
            FillAuthors();
            return View(newsAddVM);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            NewsEditVM? news = new NewsEditVM();

            GetToken();
            HttpResponseMessage response = httpClient.GetAsync($"{Constants.NewsBaseUrl}/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                news = JsonConvert.DeserializeObject<NewsEditVM>(data);
            }
            
            FillAuthors();
            return View(news);
        }

        [HttpPost]
        public IActionResult Edit(NewsEditVM newsEdit)
        {
            if (!ModelState.IsValid)
                return View();

            if (newsEdit.Image != null && newsEdit.Image.Length > 0)
            {
                newsEdit.ImagePath = GetSavedImagePath(newsEdit.Image).Result;
            }
            
            GetToken();
            var response = httpClient.PutAsJsonAsync($"{Constants.NewsBaseUrl}/{newsEdit.Id}", newsEdit).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllAdmin));
            }
            ModelState.AddModelError("", "Publication Date Must Be Within a Week From Now");
            FillAuthors();
            return View(newsEdit);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            GetToken();
            var response = httpClient.DeleteAsync($"{Constants.NewsBaseUrl}/{id}").Result;

            return RedirectToAction(nameof(GetAll));

        }

        void FillAuthors()
        {
            GetToken();
            List<AuthorsDisplayVM>? list = new List<AuthorsDisplayVM>();

            HttpResponseMessage response = httpClient.GetAsync(Constants.AuthorsBaseUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<AuthorsDisplayVM>>(data);
            }
            ViewData[Constants.Authors] = list?.Select(a => new SelectListItem(a.Name, a.Id.ToString())).ToList();

        }
    }
}



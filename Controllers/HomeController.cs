using System.Diagnostics;
using System.Text.Json.Serialization;
using ConsumeGenericWebAPI.Models;
using ConsumeGenericWebAPI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeGenericWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl;

        public HomeController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            BaseUrl = configuration["ApiSettings:APIBaseURL"];
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<EmployeeVM> empList = new List<EmployeeVM>();
            HttpResponseMessage response = _httpClient.GetAsync(BaseUrl+ "api/Employee/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                empList = JsonConvert.DeserializeObject<List<EmployeeVM>>(data);
            }
            return View(empList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

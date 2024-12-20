using System.Diagnostics;
using System.Text;
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
            HttpResponseMessage response = _httpClient.GetAsync(BaseUrl + "Employee/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                empList = JsonConvert.DeserializeObject<List<EmployeeVM>>(data);
            }
            return View(empList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM emp)
        {
            try
            {
                string data = JsonConvert.SerializeObject(emp);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = _httpClient.PostAsync(BaseUrl + "Employee/Post", content).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["successMsg"] = "Employee Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMsg"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}

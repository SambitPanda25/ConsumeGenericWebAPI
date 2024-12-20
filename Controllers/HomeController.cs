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

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                EmployeeVM emp = new EmployeeVM();
                HttpResponseMessage httpResponse = _httpClient.GetAsync(BaseUrl + "Employee/GetById/" + Id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<EmployeeVM>(data);
                }
                return View(emp);
            }
            catch (Exception ex)
            {
                TempData["errorMsg"] = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Edit(EmployeeVM emp)
        {
            try
            {
                string data = JsonConvert.SerializeObject(emp);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = _httpClient.PutAsync(BaseUrl + "Employee/Put/" + emp.Id, content).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["successMsg"] = "Employee Updated Successfully";
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

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                EmployeeVM emp = new EmployeeVM();
                HttpResponseMessage httpResponse = _httpClient.GetAsync(BaseUrl + "Employee/GetById/" + Id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<EmployeeVM>(data);
                }
                return View(emp);
            }
            catch (Exception ex)
            {
                TempData["errorMsg"] = ex.Message;
                return View();
            }
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            try
            {
                HttpResponseMessage httpResponse = _httpClient.DeleteAsync(BaseUrl + "Employee/Delete/" + Id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["successMsg"] = "Employee Deleted Successfully";
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

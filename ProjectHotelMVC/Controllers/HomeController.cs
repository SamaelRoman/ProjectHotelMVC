using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CategoryViewModel> CategoryModel = null;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/Category");

            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                CategoryModel = JsonConvert.DeserializeObject<List<CategoryViewModel>>(result);
            }

            return View("Index",CategoryModel);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectHotelMVC.Interfaces;
using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }
        public async Task<string> GetToken(EmplpoyeeGetTokenViewModel emplpoyee)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "Token");
            requestMessage.Content = JsonContent.Create(emplpoyee);
            var response = await httpClient.SendAsync(requestMessage);
            string TokenResult = null;
            if (response.IsSuccessStatusCode)
            {
                TokenResult = await response.Content.ReadAsStringAsync();
            }
            return TokenResult;
        }

        public async Task<EmployeeViewModel> IsAuthenticated(string JWT)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "IsAuthenticated");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            var response = await httpClient.SendAsync(requestMessage);
            EmployeeViewModel employee = null;
            if (response.IsSuccessStatusCode)
            {
                employee = JsonConvert.DeserializeObject<EmployeeViewModel>(await response.Content.ReadAsStringAsync());
            }
            return employee;
        }
    }
}

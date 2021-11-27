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
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public EmployeeService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }

        public async Task<bool> AddEmployeeAsync(EmployeeRegistrationViewModel employee, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Post, "api/Employee");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requsteMassage.Content = JsonContent.Create(employee);

            var response = await httpClient.SendAsync(requsteMassage);
            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> DeleteEmployeeAsync(string ID, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Delete, $"api/Employee/{ID}");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<List<EmployeeViewModel>> GetAllEmployeesAsync(string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, "api/Employee");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            List<EmployeeViewModel> Model = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);
            }
            return Model;
        }

        public async Task<EmployeeViewModel> GetByIDEmployeeAsync(string ID, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, $"api/Employee/{ID}");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            EmployeeViewModel Model = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<EmployeeViewModel>(result);
            }
            return Model;
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeViewModel employee, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Put, "api/Employee");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requsteMassage.Content = JsonContent.Create(employee);

            var response = await httpClient.SendAsync(requsteMassage);
            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }
    }
}

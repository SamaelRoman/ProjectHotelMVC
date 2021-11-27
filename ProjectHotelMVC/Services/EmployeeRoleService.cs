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
    public class EmployeeRoleService : IEmployeeRoleService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public EmployeeRoleService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }

        public async Task<bool> AddEmployeeRoleAsync(EmployeeRoleCreateViewModel employeeRole, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Post, "api/EmployeeRole");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requsteMassage.Content = JsonContent.Create(employeeRole);

            var response = await httpClient.SendAsync(requsteMassage);
            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> DeleteEmployeeRoleAsync(string ID, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Delete, $"api/EmployeeRole/{ID}");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<List<EmployeeRoleViewModel>> GetAllEmployeeRolesAsync(string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, "api/EmployeeRole");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            List<EmployeeRoleViewModel> RoleModel = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                RoleModel = JsonConvert.DeserializeObject<List<EmployeeRoleViewModel>>(result);
            }
            return RoleModel;
        }

        public async Task<EmployeeRoleCreateViewModel> GetByIDEmployeeRolesAsync(string ID, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, $"api/EmployeeRole/{ID}");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            EmployeeRoleCreateViewModel RoleModel = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                RoleModel = JsonConvert.DeserializeObject<EmployeeRoleCreateViewModel>(result);
            }
            return RoleModel;
        }

        public async Task<bool> UpdateEmployeeRolesAsync(EmployeeRoleCreateViewModel employeeRole, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Put, $"api/EmployeeRole");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requsteMassage.Content = JsonContent.Create(employeeRole);

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

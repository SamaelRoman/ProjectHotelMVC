using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectHotelMVC.Interfaces;
using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public CustomerService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }

        public async Task<bool> DeleteCustomerAsync(string ID, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Delete, $"api/Customer/{ID}");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<List<CustomerViewModel>> GetAllCustomersAsync(string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, "api/Customer");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            List<CustomerViewModel> Model = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<List<CustomerViewModel>>(result);
            }
            return Model;
        }
    }
}

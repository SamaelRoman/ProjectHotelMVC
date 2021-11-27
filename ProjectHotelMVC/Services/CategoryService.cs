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
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public CategoryService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }

        public async Task<bool> AddCategoryAsync(CategoryViewModel category,string JWT)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Category");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requestMessage.Content = JsonContent.Create(category);

            var response = await httpClient.SendAsync(requestMessage);

            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(string ID, string JWT)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"api/Category/{ID}");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requestMessage);
            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            List<CategoryViewModel> CategoryModel = null;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/Category");

            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                CategoryModel = JsonConvert.DeserializeObject<List<CategoryViewModel>>(result);
            }
            return CategoryModel;
        }

        public async Task<CategoryViewModel> GetByIDCategoryAsync(string ID, string JWT)
        {
            CategoryViewModel CategoryModel = null;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/Category/{ID}");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                CategoryModel = JsonConvert.DeserializeObject<CategoryViewModel>(result);
            }
            return CategoryModel;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryViewModel category, string JWT)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, "api/Category");
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requestMessage.Content = JsonContent.Create(category);

            var response = await httpClient.SendAsync(requestMessage);

            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }
    }
}

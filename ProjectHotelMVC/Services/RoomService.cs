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
    public class RoomService : IRoomService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public RoomService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }

        public async Task<bool> AddRoomAsync(RoomViewModel room, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Post, "api/Room");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requsteMassage.Content = JsonContent.Create(room);

            var response = await httpClient.SendAsync(requsteMassage);

            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> DeleteRoomAsync(string ID, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Delete, $"api/Room/{ID}");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);

            var result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }

        public async Task<List<RoomViewModel>> GetAllRoomsAsync(string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, "api/Room");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            List<RoomViewModel> RoomsModel = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                RoomsModel = JsonConvert.DeserializeObject<List<RoomViewModel>>(result);
            }
            return RoomsModel;
        }

        public async Task<List<AvailbleRoomViewModel>> GetAvailbleRoomsByDateAsync(DateTime Start, DateTime End, string CategoryID = null)
        {
            string StartDate = Start.ToString("yyyy.MM.dd");
            string EndDate = End.ToString("yyyy.MM.dd");

            List<AvailbleRoomViewModel> RoomModel = null;

            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, $"api/Room/{StartDate}/{EndDate}/{CategoryID}");

            var response = await httpClient.SendAsync(requsteMassage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                RoomModel = JsonConvert.DeserializeObject<List<AvailbleRoomViewModel>>(result);
            }
            return RoomModel;
        }

        public async Task<RoomViewModel> GetByIDRoomAsync(string ID, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, $"api/Room/{ID}");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            RoomViewModel RoomModel = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                RoomModel = JsonConvert.DeserializeObject<RoomViewModel>(result);
            }
            return RoomModel;
        }

        public async Task<decimal> GetTotalPrice(DateTime Start, DateTime End, string RoomID)
        {
            string StartDate = Start.ToString("yyyy.MM.dd");
            string EndDate = End.ToString("yyyy.MM.dd");

            decimal? TotalCountResult = null;
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, $"GetTotalPrice/{StartDate}/{EndDate}/{RoomID}");

            var response = await httpClient.SendAsync(requsteMassage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                TotalCountResult = JsonConvert.DeserializeObject<decimal>(result);
            }
            return (decimal)TotalCountResult;
        }

        public async Task<bool> UpdateRoomAsync(RoomViewModel room, string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Put, "api/Room");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);
            requsteMassage.Content = JsonContent.Create(room);

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

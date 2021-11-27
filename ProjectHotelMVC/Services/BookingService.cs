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
    public class BookingService : IBookingService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public BookingService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration.GetSection("APIAddress").Value);
        }
        public async Task<BookingInfoViewModel> AddBookingAsync(BookingInfoRegViewModel bookingInfo)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/BookingInfo");
            httpRequest.Content = JsonContent.Create(bookingInfo);
            var response = await httpClient.SendAsync(httpRequest);
            BookingInfoViewModel ResultModel = null;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<BookingInfoViewModel> Result = await GetBookingInfoByPassportIDAsync(bookingInfo.Customer.PassportID);
                ResultModel = Result.Last();
            }
            return ResultModel;
        }

        public async Task<bool> CancelBooking(string ID)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"api/BookingInfo/{ID}");
            var response = await httpClient.SendAsync(httpRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<BookingInfoViewModel>> GetAllBookingInfosAsync(string JWT)
        {
            HttpRequestMessage requsteMassage = new HttpRequestMessage(HttpMethod.Get, "api/BookingInfo");
            requsteMassage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWT);

            var response = await httpClient.SendAsync(requsteMassage);
            List<BookingInfoViewModel> BookingModel = null;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                BookingModel = JsonConvert.DeserializeObject<List<BookingInfoViewModel>>(result);
            }
            return BookingModel;
        }

        public async Task<IEnumerable<BookingInfoViewModel>> GetBookingInfoByPassportIDAsync(string PassportID)
        {
            IEnumerable<BookingInfoViewModel> BookinginfoModel = null;
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/GetBookingInfo/{PassportID}");
            var response = await httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                BookinginfoModel = JsonConvert.DeserializeObject<IEnumerable<BookingInfoViewModel>>(result);
            }
            return BookinginfoModel;
        }
    }
}

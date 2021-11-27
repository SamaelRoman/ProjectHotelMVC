using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Interfaces
{
    public interface IBookingService
    {
        public Task<BookingInfoViewModel> AddBookingAsync(BookingInfoRegViewModel bookingInfo);
        public Task<IEnumerable<BookingInfoViewModel>> GetBookingInfoByPassportIDAsync(string PassportID);
        public Task<List<BookingInfoViewModel>> GetAllBookingInfosAsync(string JWT);
        public Task<bool> CancelBooking(string ID);
    }
}

using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<CustomerViewModel>> GetAllCustomersAsync(string JWT);
        public Task<bool> DeleteCustomerAsync(string ID, string JWT);
    }
}

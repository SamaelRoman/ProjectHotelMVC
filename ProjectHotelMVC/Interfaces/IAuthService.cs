using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Interfaces
{
    public interface IAuthService
    {
        public Task<string> GetToken(EmplpoyeeGetTokenViewModel emplpoyee);
        public Task<EmployeeViewModel> IsAuthenticated(string JWT);
    }
}

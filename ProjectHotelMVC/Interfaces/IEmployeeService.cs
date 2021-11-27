using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Interfaces
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeViewModel>> GetAllEmployeesAsync(string JWT);
        public Task<bool> AddEmployeeAsync(EmployeeRegistrationViewModel employee, string JWT);
        public Task<bool> DeleteEmployeeAsync(string ID, string JWT);
        public Task<EmployeeViewModel> GetByIDEmployeeAsync(string ID, string JWT);
        public Task<bool> UpdateEmployeeAsync(EmployeeViewModel employee, string JWT);
    }
}

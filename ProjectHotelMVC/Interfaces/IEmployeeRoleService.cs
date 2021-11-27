using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Interfaces
{
    public interface IEmployeeRoleService
    {
        public Task<List<EmployeeRoleViewModel>> GetAllEmployeeRolesAsync(string JWT);
        public Task<bool> AddEmployeeRoleAsync(EmployeeRoleCreateViewModel employeeRole, string JWT);
        public Task<bool> DeleteEmployeeRoleAsync(string ID, string JWT);
        public Task<EmployeeRoleCreateViewModel> GetByIDEmployeeRolesAsync(string ID, string JWT);
        public Task<bool> UpdateEmployeeRolesAsync(EmployeeRoleCreateViewModel employeeRole, string JWT);
    }
}

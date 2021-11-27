using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        public Task<bool> AddCategoryAsync(CategoryViewModel category,string JWT);
        public Task<bool> DeleteCategoryAsync(string ID, string JWT);
        public Task<CategoryViewModel> GetByIDCategoryAsync(string ID, string JWT);
        public Task<bool> UpdateCategoryAsync(CategoryViewModel category, string JWT);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectHotelMVC.Filters;
using ProjectHotelMVC.Interfaces;
using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Controllers
{
    [Authorize("Administrator", "Moderator")]
    public class AdminController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ICategoryService categoryService;
        private readonly IRoomService roomService;
        private readonly IBookingService bookingService;
        private readonly IAuthService authService;
        private readonly ICustomerService customerService;
        private readonly IEmployeeService employeeService;
        private readonly IEmployeeRoleService employeeRoleService;
        public AdminController(IConfiguration configuration,
            ICategoryService categoryService,
            IRoomService roomService,
            IBookingService bookingService,
            IAuthService authService,
            ICustomerService customerService,
            IEmployeeService employeeService,
            IEmployeeRoleService employeeRoleService
            )
        {
            this.configuration = configuration;
            this.roomService = roomService;
            this.categoryService = categoryService;
            this.bookingService = bookingService;
            this.authService = authService;
            this.customerService = customerService;
            this.employeeService = employeeService;
            this.employeeRoleService = employeeRoleService;           

        }
        public IActionResult Index()
        {
            
            return View();
        }
        //GetALL Action Methods
        #region Get ALL
        [HttpGet]
        public async Task<IActionResult> Category()
        {
            var CategoryModel = await categoryService.GetAllCategoriesAsync();
            return View("Category",CategoryModel);
        }
        [HttpGet]
        public async Task<IActionResult> Room()
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var RoomModel = await roomService.GetAllRoomsAsync(JWT);
            return View("Room",RoomModel);
        }
        [HttpGet]
        public async Task<IActionResult> Customer()
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var CustmerModel = await customerService.GetAllCustomersAsync(JWT);
            return View("Customer", CustmerModel);
        }
        [HttpGet]
        [Authorize("Administrator")]
        public async Task<IActionResult> Employee()
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var EmployeeModel = await employeeService.GetAllEmployeesAsync(JWT);
            return View("Employee",EmployeeModel);
        }
        [HttpGet]
        [Authorize("Administrator")]
        public async Task<IActionResult> EmployeeRole()
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var EmployeeRoleModel = await employeeRoleService.GetAllEmployeeRolesAsync(JWT);
            return View("EmployeeRole", EmployeeRoleModel);
        }
        [HttpGet]
        public async Task<IActionResult> BookingInfo()
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var BookingModel = await bookingService.GetAllBookingInfosAsync(JWT);
            return View("BookingInfo", BookingModel);
        }
        #endregion ADD
        //Add Action Methods
        #region Add
        [HttpGet]
        public async Task<IActionResult> CategoryAdd()
        {
            var CategoryModel = new CategoryViewModel();
            return View("CategoryAdd", CategoryModel);
        }
        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryViewModel CategoryModel,decimal Price)
        {
            CategoryModel.CategoryInfos.Add(new CategoryInfoViewModel() { Price = Price,CategoryID = CategoryModel.ID });

            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await categoryService.AddCategoryAsync(CategoryModel, JWT);

            if(result == true)
            {
                return RedirectPermanent("~/Admin/Category");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> RoomAdd()
        {
            var Room = new RoomViewModel();
            ViewBag.CategoryForRoomCreate = await categoryService.GetAllCategoriesAsync();
            return View("RoomAdd", Room);
        }
        [HttpPost]
        public async Task<IActionResult> RoomAdd(RoomViewModel room,string[] RoomImgs,string CategoryID)
        {
            for (int i = 0; i < RoomImgs.Length; i++)
            {
                room.RoomImages.Add(new RoomImageViewModel() { ImgUrl = RoomImgs[i], RoomID = room.ID });
            }

            room.CategoryID = new Guid(CategoryID);


            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await roomService.AddRoomAsync(room,JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Room");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeAdd()
        {
            var employee = new EmployeeRegistrationViewModel();
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            ViewBag.RoleForEmployeeCreate = await employeeRoleService.GetAllEmployeeRolesAsync(JWT);
            return View("EmployeeAdd", employee);
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeAdd(EmployeeRegistrationViewModel employee, string RoleID)
        {
            employee.EmployeeRoleID = new Guid(RoleID);

            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await employeeService.AddEmployeeAsync(employee,JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Employee");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeRoleAdd()
        {
            var Role = new EmployeeRoleCreateViewModel();
            return View("EmployeeRoleAdd", Role);
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeRoleAdd(EmployeeRoleCreateViewModel role)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await employeeRoleService.AddEmployeeRoleAsync(role,JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/EmployeeRole");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        #endregion
        //Delte Action Methods
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await categoryService.DeleteCategoryAsync(ID, JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Category");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoomDelete(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await roomService.DeleteRoomAsync(ID, JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Room");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerDelete(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await customerService.DeleteCustomerAsync(ID, JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Customer");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeDelete(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await employeeService.DeleteEmployeeAsync(ID, JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Employee");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeRoleDelete(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await employeeRoleService.DeleteEmployeeRoleAsync(ID,JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/EmployeeRole");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        #endregion
        //Update Action Methods
        #region Update
        [HttpGet]
        public async Task<IActionResult> EmployeeRoleEdit(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var role = await employeeRoleService.GetByIDEmployeeRolesAsync(ID,JWT);
            return View("EmployeeRoleEdit", role);
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeRoleEdit(EmployeeRoleCreateViewModel Role)
        {
            if (!ModelState.IsValid)
            {
                return RedirectPermanent("Error");
            }
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await employeeRoleService.UpdateEmployeeRolesAsync(Role, JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/EmployeeRole");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeEdit(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var employee = await employeeService.GetByIDEmployeeAsync(ID,JWT);
            ViewBag.RoleForEmployeeCreate = await employeeRoleService.GetAllEmployeeRolesAsync(JWT);
            return View("EmployeeEdit", employee);
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeEdit(EmployeeViewModel employee, string RoleID)
        {
            if (!ModelState.IsValid)
            {
                return RedirectPermanent("Error");
            }
            employee.EmployeeRoleID = new Guid(RoleID);


            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await employeeService.UpdateEmployeeAsync(employee,JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Employee");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> CategoryEdit(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var CategoryModel = await categoryService.GetByIDCategoryAsync(ID,JWT);
            return View("CategoryEdit", CategoryModel);
        }
        [HttpPost]
        public async Task<IActionResult> CategoryEdit(CategoryViewModel CategoryModel, decimal Price)
        {
            CategoryModel.CategoryInfos.Add(new CategoryInfoViewModel() { Price = Price, CategoryID = CategoryModel.ID });
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await categoryService.UpdateCategoryAsync(CategoryModel,JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Category");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> RoomEdit(string ID)
        {
            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var Room = await roomService.GetByIDRoomAsync(ID, JWT);
            ViewBag.CategoryForRoomCreate = await categoryService.GetAllCategoriesAsync();
            return View("RoomEdit", Room);
        }
        [HttpPost]
        public async Task<IActionResult> RoomEdit(RoomViewModel room, string[] RoomImgs, string CategoryID)
        {
            for (int i = 0; i < RoomImgs.Length; i++)
            {
                room.RoomImages.Add(new RoomImageViewModel() { ImgUrl = RoomImgs[i], RoomID = room.ID });
            }

            room.CategoryID = new Guid(CategoryID);


            var JWT = HttpContext.Session.GetString("ValidationJWT");
            var result = await roomService.UpdateRoomAsync(room, JWT);

            if (result == true)
            {
                return RedirectPermanent("~/Admin/Room");
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectHotelMVC.Filters;
using ProjectHotelMVC.Interfaces;
using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ICategoryService categoryService;
        private readonly IRoomService roomService;
        private readonly IBookingService bookingService;
        private readonly IAuthService authService;
        public BookingController(IConfiguration configuration,ICategoryService categoryService, IRoomService roomService, IBookingService bookingService,IAuthService authService)
        {
            this.configuration = configuration;
            this.roomService = roomService;
            this.categoryService = categoryService;
            this.bookingService = bookingService;
            this.authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]       
        public async Task<IActionResult> SearchAvailbleRooms(string CategoryID = null)
        {
            ViewBag.CategoryToSearch = await categoryService.GetAllCategoriesAsync();
            if(CategoryID != null)
            {
                ViewBag.TargetCategory = CategoryID;
            }
            return View("SearchRoom");
        }
        [HttpPost]
        public async Task<IActionResult> SearchAvailbleRooms(DateTime Start , DateTime End , string CategoryID = null)
        {
            List<CategoryViewModel> CategoryModel = await categoryService.GetAllCategoriesAsync();
            List<AvailbleRoomViewModel> RoomModel = await roomService.GetAvailbleRoomsByDateAsync(Start, End, CategoryID);

            ViewBag.StartDate = Start.ToString("yyyy-MM-dd");
            ViewBag.EndDate = End.ToString("yyyy-MM-dd");
            ViewBag.CategoryToSearch = CategoryModel;

            if (CategoryID != null)
            {
                ViewBag.TargetCategory = CategoryID;
            }

            return View("SearchRoom",RoomModel);
        }
        [HttpGet]
        public async Task<IActionResult> CreateBooking(DateTime StartDate, DateTime EndDate,string RoomID)
        {
            if(StartDate != null && EndDate != null && RoomID != null)
            {
                var BookingRegModel = new BookingInfoRegViewModel()
                {
                    StartBooking = StartDate,
                    EndBooking = EndDate,
                    RoomID = RoomID,
                    TotalPrice = await roomService.GetTotalPrice(StartDate, EndDate, RoomID),
                    Customer = new CustomerViewModel()
                };
                return View("CreateBooking", BookingRegModel);
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingInfoRegViewModel bookingInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await bookingService.AddBookingAsync(bookingInfo);
                return View("AddBookingSuccess", result);
            }
            else
            {
                return RedirectPermanent("Error");
            }
        }
        [HttpGet]
        public IActionResult BookingState()
        {
            return View("BookingInfo");
        }
        [HttpPost]
        public async Task<IActionResult> BookingState(string PassportID)
        {
            var result = await bookingService.GetBookingInfoByPassportIDAsync(PassportID);
            return View("BookingInfo", result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(string ID)
        {
            var result = await bookingService.CancelBooking(ID);
            if(result != true)
            {
                return RedirectPermanent("Error");
            }
            else
            {
                ViewBag.CancelStateTrue = true;
                return View("BookingInfo");
            }
        }
    }
}

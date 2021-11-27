using ProjectHotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Interfaces
{
    public interface IRoomService
    {
        public Task<List<AvailbleRoomViewModel>> GetAvailbleRoomsByDateAsync(DateTime Start,DateTime End,string CategoryID = null);
        public Task<List<RoomViewModel>> GetAllRoomsAsync(string JWT);
        public Task<bool> AddRoomAsync(RoomViewModel room, string JWT);
        public Task<bool> DeleteRoomAsync(string ID, string JWT);
        public Task<RoomViewModel> GetByIDRoomAsync(string ID, string JWT);
        public Task<bool> UpdateRoomAsync(RoomViewModel room, string JWT);
        public Task<decimal> GetTotalPrice(DateTime Start, DateTime End, string RoomID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Repository
{
    public interface IRoomRepository
    {
        //UPDATE WITH 4 OVERLOADS
        void Update(Room room);
        void Reserve(RoomOperation reservation);
        void Assign(RoomOperation assignment);
        void Empty(Room room);
        Room GetRoomById(int id);
        List<Room> GetAllRoomsByFilter(byte? number, RoomType? type, RoomStatus? status, bool? isActive, RoomSortBy? sortBy);
    }
}
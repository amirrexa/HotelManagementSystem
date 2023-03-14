using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelContext hotelContext;

        public RoomRepository(HotelContext hotelContext)
        {
            this.hotelContext = hotelContext;
        }

        public void Empty(Room room)
        {
            if (room.Status != RoomStatus.Unoccupied)
            {
                room.Update(RoomStatus.Unoccupied);
                room.Update(customer: null);
            }
            else
                throw new Exception("Room is already unoccupied.");
            hotelContext.SaveChanges();
        }

        public Room GetRoomById(int id)
        {
            return hotelContext.rooms.Include(r => r.Customer).FirstOrDefault(r => r.Id == id);
        }

        public List<Room> GetAllRooms()
        {
            return hotelContext.rooms.ToList();
        }

        public List<Room> GetAllRoomsByFilter(byte? number, RoomType? type, RoomStatus? status, bool? isActive, RoomSortBy? sortBy)
        {
            var query = hotelContext.rooms.AsQueryable();
            if (number.HasValue)
                query = query.Where(r => r.Number == number.Value);
            if (type.HasValue)
                query = query.Where(r => r.Type == type.Value);
            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);
            if (isActive.HasValue)
                query = query.Where(r => r.IsActive == isActive.Value);
            if (sortBy.HasValue)
            {
                if (sortBy == RoomSortBy.Number)
                    query = query.OrderBy(r => r.Number);
                if (sortBy == RoomSortBy.Type)
                    query = query.OrderBy(r => r.Type);
                if (sortBy == RoomSortBy.Status)
                    query = query.OrderBy(r => r.Status);
                if (sortBy == RoomSortBy.IsActive)
                    query = query.OrderBy(r => r.IsActive);
            }
            return query.ToList();
        }

        /*
        public void Reserve(Room room, Customer customer)
        {
            
        }
        */
        public void Reserve(RoomOperation reservation)
        {
            if (reservation.Room.Status == RoomStatus.Unoccupied &
                !string.IsNullOrEmpty(reservation.Customer.Name) &
                !string.IsNullOrEmpty(reservation.FromDate.ToString()) &
                !string.IsNullOrEmpty(reservation.ToDate.ToString()) &
                !string.IsNullOrEmpty(reservation.PaidAmount.ToString()))
            {
                reservation.Room.Update(reservation.Customer);
                reservation.Room.Update(RoomStatus.Reserved);
            }
            else
                throw new Exception($"Room is {reservation.Room.Status}");
            hotelContext.roomOperations.Add(reservation);
            hotelContext.SaveChanges();
        }

        public void Assign(RoomOperation assignment)
        {
            if (assignment.Room.Status == RoomStatus.Unoccupied &
                !string.IsNullOrEmpty(assignment.Customer.Name) &
                !string.IsNullOrEmpty(assignment.FromDate.ToString()) &
                !string.IsNullOrEmpty(assignment.ToDate.ToString()) &
                !string.IsNullOrEmpty(assignment.PaidAmount.ToString()))
            {
                assignment.Room.Update(assignment.Customer);
                assignment.Room.Update(RoomStatus.Assigned);
            }
            else
                throw new Exception($"Room is {assignment.Room.Status}");
            hotelContext.roomOperations.Add(assignment);
            hotelContext.SaveChanges();
        }

        public void Update(Room room)
        {
            hotelContext.Update(room);
            hotelContext.SaveChanges();
        }
    }
}

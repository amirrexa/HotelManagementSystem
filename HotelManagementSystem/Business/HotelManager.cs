using HotelManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelManagementSystem.Business
{
    public class HotelManager
    {
        private readonly HotelContext hotelContext;
        public HotelManager()
        {
            hotelContext = new HotelContext();
        }

        public void InitializeRooms()
        {
            var rooms = new List<Room>()
            {
            new Room(RoomType.Single, RoomStatus.Unoccupied, 1, true), //RoomList[0]
            new Room(RoomType.Single, RoomStatus.Unoccupied, 2, true), //RoomList[1]
            new Room(RoomType.Double, RoomStatus.Unoccupied, 3, true), //RoomList[2]
            new Room(RoomType.Deluxe, RoomStatus.Unoccupied, 4, true), //RoomList[3]
            new Room(RoomType.Suite, RoomStatus.Unoccupied, 5, true)   //RoomList[4]
            };

            foreach (var newRoom in rooms)
            {
                if (hotelContext.rooms.Any(r => r.Number == newRoom.Number))
                    continue;
                hotelContext.rooms.Add(newRoom);
            }
            hotelContext.SaveChanges();
        }

        //METHODS FOR CUSTOMER

        public void AddCustomer(Customer newCustomer)
        {
            hotelContext.customers.Add(newCustomer);
            hotelContext.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            hotelContext.customers.Remove(customer);
            hotelContext.SaveChanges();
        }

        public void UpdateCustomerName(Customer customer, string newCustomerName)
        {
            customer.UpdateName(newCustomerName);
            hotelContext.SaveChanges();
        }

        public void UpdateCustomerPhoneNumber(Customer customer, string newPhoneNumber)
        {
            customer.UpdatePhoneNumber(newPhoneNumber);
            hotelContext.SaveChanges();
        }
        public Customer GetCustomer(int id)
        {
            return hotelContext.customers.FirstOrDefault(c => c.Id == id);
        }
        public List<Customer> GetAllCustomers()
        {
            return hotelContext.customers.ToList();
        }
        public List<Customer> GetAllCustomersByFilter(string? name, string? phoneNumber)
        {
            var query = hotelContext.customers.AsQueryable();
            query = query.Where(c => c.Name == name);
            query = query.Where(c => c.PhoneNumber == phoneNumber);
            return query.ToList();
        }

        //METHODS FOR ROOM
        public List<Room> GetAllRooms()
        {
            return hotelContext.rooms.ToList();
        }
        public List<Room> GetAllRoomsByFilter(byte? number, RoomType? type, RoomStatus? status, bool? isActive, RoomSortBy? sortBy)
        {
            var query =  hotelContext.rooms.AsQueryable();
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
                if(sortBy == RoomSortBy.Number)
                    query = query.OrderBy(r => r.Number);
                if(sortBy == RoomSortBy.Type)
                    query = query.OrderBy(r => r.Type);
                if (sortBy == RoomSortBy.Status)
                    query = query.OrderBy(r => r.Status);
                if (sortBy == RoomSortBy.IsActive)
                    query = query.OrderBy(r => r.IsActive);
            }
                
            return query.ToList();
        }

        public List<Room> GetEmptyRooms()
        {
            return hotelContext.rooms.Where(r => r.Status == RoomStatus.Unoccupied).ToList();
        }

        public Room GetRoom(int id)
        {
            return hotelContext.rooms.Include(r => r.Customer)
                .FirstOrDefault(r => r.Id == id);
        }

        public void AddRoom(Room room)
        {
            hotelContext.rooms.Add(room);
            hotelContext.SaveChanges();
        }

        public void RemoveRoom(Room room)
        {
            hotelContext.Remove(room);
            hotelContext.SaveChanges();
        }

        public void ReserveRoom(RoomOperation reservation)
        {
            if (reservation.Room.Status == RoomStatus.Unoccupied)
            {
                reservation.Room.UpdateCustomer(reservation.Customer);
                reservation.Room.UpdateStatus(RoomStatus.Reserved);
            }
            hotelContext.roomOperations.Add(reservation);
            hotelContext.SaveChanges();
        }

        public void AssignRoom(RoomOperation assignment)
        {
            if (assignment.Room.Status == RoomStatus.Unoccupied)
            {
                assignment.Room.UpdateCustomer(assignment.Customer);
                assignment.Room.UpdateStatus(RoomStatus.Assigned);
            }
            else
                throw new Exception($"Room is {assignment.Room.Status}");
            hotelContext.roomOperations.Add(assignment);
            hotelContext.SaveChanges();
        }
        public void FreeRoom(Room room)
        {
            if (room.Status != RoomStatus.Unoccupied)
            {
                room.UpdateStatus(RoomStatus.Unoccupied);
                room.UpdateCustomer(null);
            }
            else
                throw new Exception("Room is already unoccupied.");
            hotelContext.SaveChanges();
        }


        public void UpdateRoomStatus(Room room, RoomStatus newStatus)
        {
            room.UpdateStatus(newStatus);
            hotelContext.SaveChanges();
        }
        public void UpdateRoomNumber(Room room, byte number)
        {
            room.UpdateNumber(number);
            hotelContext.SaveChanges();
        }

        public void UpdateRoomType(Room room, RoomType roomType)
        {
            room.UpdateType(roomType);
            hotelContext.SaveChanges();
        }

        public void UpdateRoomActivation(Room room, bool isActive)
        {
            room.IsActive = isActive;
            hotelContext.SaveChanges();
        }

        public void CheckOut(Room room, Customer customer)
        {
            if (room.Status != RoomStatus.Unoccupied && room.Customer == customer)
            {
                room.UpdateStatus(RoomStatus.Unoccupied);
                room.UpdateCustomer(null);
            }
            else if (room.Status == RoomStatus.Reserved)
            {
                this.CancelReservation(room, customer);
            }
            hotelContext.SaveChanges();
        }
        public void CancelReservation(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Reserved && room.Customer == customer)
            {
                room.UpdateStatus(RoomStatus.Unoccupied);
                room.UpdateCustomer(null);
                Console.WriteLine("Reservation cancelled successfully.");
            }
            hotelContext.SaveChanges();
        }
        public void CancelAllReservations()
        {
            foreach (var room in hotelContext.rooms)
            {
                if (room.Status == RoomStatus.Reserved)
                {
                    room.UpdateStatus(RoomStatus.Unoccupied);
                    room.UpdateCustomer(null);
                }
            }
            hotelContext.SaveChanges();
        }

        //OPERATION METHODS

        public List<RoomOperation> GetAllRoomOperations()
        {
            return (hotelContext.roomOperations.ToList());
        }

        public RoomOperation GetRoomOperation(int id)
        {
            return(hotelContext.roomOperations.Where(ro=>ro.Id == id).FirstOrDefault());
        }
    }
}
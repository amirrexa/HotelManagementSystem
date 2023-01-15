using HotelManagementSystem.Data;
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
            hotelContext= new HotelContext();
        }

        public void InitializeRooms()
        {
            var rooms = new List<Room>()
            {
            new Room(RoomType.Single, RoomStatus.Unoccupied, 1), //RoomList[0]
            new Room(RoomType.Single, RoomStatus.Unoccupied, 2), //RoomList[1]
            new Room(RoomType.Double, RoomStatus.Unoccupied, 3), //RoomList[2]
            new Room(RoomType.Deluxe, RoomStatus.Unoccupied, 4), //RoomList[3]
            new Room(RoomType.Suite, RoomStatus.Unoccupied, 5)   //RoomList[4]
            };

            foreach (var newRoom in rooms)
            {
                if (hotelContext.rooms.Any(r => r.Number == newRoom.Number))
                    continue;
                hotelContext.rooms.Add(newRoom);
            }
            hotelContext.SaveChanges();
        }

        public void AddCustomer(string customerName)
        {
            var addedCustomer = new Customer(customerName);
            hotelContext.customers.Add(addedCustomer);
            hotelContext.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            hotelContext.customers.Remove(customer);
            hotelContext.SaveChanges();
        }
        public List<Room> GetAll()
        {
            return hotelContext.rooms.ToList();
        }
        public List<Room> GetEmptyRooms()
        {
            return hotelContext.rooms.Where(r => r.Status == RoomStatus.Unoccupied).ToList();
        }

        public Room GetRoom(int number)
        {
            return (Room)hotelContext.rooms.Where(r => r.Number == number);
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

        public void ConfirmReservedRoom(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Reserved || room.Status == RoomStatus.Occupied)
            {
                throw new Exception($"Room is {room.Status}");
            }
            else
                room.UpdateStatus(RoomStatus.Reserved, customer);
            hotelContext.SaveChanges();
        }

        public void AssignRoom(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Unoccupied)
                room.UpdateStatus(RoomStatus.Occupied, customer);
            else
                throw new Exception($"Room is {room.Status}");
            hotelContext.SaveChanges();
        }

        public void FreeRoom(Room room)
        {
            if (room.Status != RoomStatus.Unoccupied)
                room.UpdateStatus(RoomStatus.Unoccupied, null);
            else
                throw new Exception("Room is already unoccupied.");
            hotelContext.SaveChanges();
        }

        public void CancelReservation(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Reserved && room.Customer == customer)
            {
                room.UpdateStatus(RoomStatus.Unoccupied, null);
                Console.WriteLine("Reservation cancelled successfully.");
            }
            hotelContext.SaveChanges();
        }
        public void CheckOut(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Occupied && room.Customer == customer)
            {
                room.UpdateStatus(RoomStatus.Unoccupied, null);
            }
            else if (room.Status == RoomStatus.Reserved)
            {
                this.CancelReservation(room, customer);
            }
            hotelContext.SaveChanges();
        }
        public void CancelAllReservations()
        {
            foreach (var room in hotelContext.rooms)
            {
                if (room.Status == RoomStatus.Reserved)
                {
                    room.UpdateStatus(RoomStatus.Unoccupied, null);

                }
            }
            hotelContext.SaveChanges();
        }
    }
}
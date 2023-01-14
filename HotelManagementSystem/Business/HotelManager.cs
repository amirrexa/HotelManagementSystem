using HotelManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Business
{
    public class HotelManager
    {
        private readonly List<Room> rooms;

        public HotelManager()
        {
            rooms = new List<Room>()
            {
            new Room(RoomType.Single, RoomStatus.Unoccupied, 1), //RoomList[0]
            new Room(RoomType.Single, RoomStatus.Unoccupied, 2), //RoomList[1]
            new Room(RoomType.Double, RoomStatus.Unoccupied, 3), //RoomList[2]
            new Room(RoomType.Deluxe, RoomStatus.Unoccupied, 4), //RoomList[3]
            new Room(RoomType.Suite, RoomStatus.Unoccupied, 5)   //RoomList[4]
            };
        }

        public List<Room> GetAll()
        {
            return this.rooms;
        }
        public void ShowAll()
        {
            foreach (Room room in this.rooms)
            {
                Console.WriteLine($"Room {room.Number} | {room.Type} | {room.Status} | {room.Customer}");
            }
        }
        public List<Room> GetEmptyRooms()
        {
            return this.rooms.Where(r => r.Status == RoomStatus.Unoccupied).ToList();
        }

        public Room GetRoom(int number)
        {
            return this.rooms.Single(r => r.Number == number);
        }

        public void ReserveRoom(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Reserved || room.Status == RoomStatus.Occupied)
            {
                throw new Exception($"Room is {room.Status}");
            }
            else
                room.UpdateStatus(RoomStatus.Reserved, customer);
        }

        public void AssignRoom(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Unoccupied)
                room.UpdateStatus(RoomStatus.Occupied, customer);

            else
                throw new Exception($"Room is {room.Status}");

        }

        public void FreeRoom(Room room)
        {
            if (room.Status != RoomStatus.Unoccupied)
                room.UpdateStatus(RoomStatus.Unoccupied, null);
            else
                throw new Exception("Room is already unoccupied.");
        }

        public void CancelReservation(Room room, Customer customer)
        {
            if (room.Status == RoomStatus.Reserved && room.Customer == customer)
            {
                room.UpdateStatus(RoomStatus.Unoccupied, null);
                Console.WriteLine("Reservation cancelled successfully.");
            }
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
        }
        public void CancelAllReservations()
        {
            foreach (var room in rooms)
            {
                if (room.Status == RoomStatus.Reserved)
                {
                    room.UpdateStatus(RoomStatus.Unoccupied, null);

                }
            }
        }
    }
}
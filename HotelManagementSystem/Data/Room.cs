using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data
{
    public class Room
    {
        public byte Number { get; }
        public RoomType Type { get; private set; }
        public RoomStatus Status { get; private set; }
        public Customer Customer { get; private set; }
        public int Id { get; set; }

        public Room()
        {

        }
        public Room(RoomType roomType, RoomStatus roomStatus, byte number)
        {
            Type = roomType;
            Status = roomStatus;
            Number = number;
        }

        public void UpdateStatus(RoomStatus roomStatus, Customer customer)
        {
            if (Status == RoomStatus.Occupied && roomStatus == RoomStatus.Reserved)
            {
                throw new Exception("Room is full");
            }
            else if (Status == roomStatus)
            {
                throw new Exception($"Room is {Status}");
            }
            else
            {
                Status = roomStatus;
                Customer = customer;
            }
        }
    }
}

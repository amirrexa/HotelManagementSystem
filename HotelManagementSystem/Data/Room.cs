using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data
{
    public class Room
    {
        public byte Number { get; set; }
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public Room()
        {
            Status = RoomStatus.Unoccupied;
            IsActive = true;
        }
        public Room(RoomType roomType, RoomStatus roomStatus, byte number, bool isActive)
        {
            Type = roomType;
            Status = roomStatus;
            Number = number;
            IsActive = true;
        }
        public void UpdateStatus(RoomStatus roomStatus)
        {
            Status = roomStatus;
        }

        public void UpdateCustomer(Customer customer)
        {
            Customer = customer;
        }

        public void UpdateNumber(byte number)
        {
            Number = number;
        }

        public void UpdateType(RoomType roomType)
        {
            Type = roomType;
        }
    }
}

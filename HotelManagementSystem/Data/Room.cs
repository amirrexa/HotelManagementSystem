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
        public byte Number { get; private set; }
        public RoomType Type { get; private set; }
        public RoomStatus Status { get; private set; }
        public Customer Customer { get; private set; }
        public int? CustomerId { get; private set; }
        public int Id { get; private set; }
        public bool IsActive { get; private set; }

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
        
        // Under Construction
        public void Update(byte newNumber) { Number = newNumber; }
        public void Update(RoomStatus newStatus) { Status = newStatus; }
        public void Update(RoomType newRoomType) { Type = newRoomType; }
        public void Update(bool newActivation) { IsActive = newActivation; }
        public void Update(Customer customer) { Customer = customer; }
    }
}

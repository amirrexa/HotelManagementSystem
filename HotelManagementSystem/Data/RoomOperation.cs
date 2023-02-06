using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data
{
    public class RoomOperation
    {
        public RoomOperation()
        {

        }
        public RoomOperation(Room room, Customer customer, DateTime fromDate, DateTime toDate, decimal paidAmount, RoomActionType roomActionType)
        {
            Customer = customer;
            Room = room;
            FromDate = fromDate;
            ToDate = toDate;
            PaidAmount = paidAmount;
            RoomActionType = roomActionType;
            FromDate = fromDate;
            ToDate = toDate;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal PaidAmount { get; set; }
        public RoomActionType RoomActionType { get; set; }
    }
}
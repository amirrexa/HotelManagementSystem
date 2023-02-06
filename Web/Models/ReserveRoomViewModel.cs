using HotelManagementSystem.Data;

namespace Web.Models
{
    public class ReserveRoomViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal PaidAmount { get; set; }
    }
}

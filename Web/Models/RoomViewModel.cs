using HotelManagementSystem.Data;

namespace Web.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public byte Number { get; set; }
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public bool IsActive { get; set; }
        //ta vaghti ke set haye ina private bood, bad az submite edit etefaghi nemioftad
    }
}

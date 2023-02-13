using HotelManagementSystem.Data;
namespace Web.Models

{
    public class RoomSearchViewModel
    {
        public byte? Number { get; set; }
        public RoomType? Type { get; set; }
        public RoomStatus? Status { get; set; }
        public string? IsActive { get; set; }
    }
}

using HotelManagementSystem.Data;
namespace Web.Models
{
    public class OperationSearchViewModel
    {
        public string? CustomerName { get; set; }
        public byte? RoomNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public RoomActionType? RoomActionType { get; set; }
        public OperationSortBy SortBy { get; set; }
    }
}
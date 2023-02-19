using HotelManagementSystem.Data;
namespace Web.Models
{
    public class CustomerSearchViewModel
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public CustomerSortBy? SortBy { get; set; }
    }
}

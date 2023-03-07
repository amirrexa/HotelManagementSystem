using HotelManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelManagementSystem.Business
{
    public class HotelManager
    {
        private readonly HotelContext hotelContext;
        public HotelManager()
        {
            hotelContext = new HotelContext();
        }

        public void InitializeRooms()
        {
            var rooms = new List<Room>()
            {
            new Room(RoomType.Single, RoomStatus.Unoccupied, 1, true), //RoomList[0]
            new Room(RoomType.Single, RoomStatus.Unoccupied, 2, true), //RoomList[1]
            new Room(RoomType.Double, RoomStatus.Unoccupied, 3, true), //RoomList[2]
            new Room(RoomType.Deluxe, RoomStatus.Unoccupied, 4, true), //RoomList[3]
            new Room(RoomType.Suite, RoomStatus.Unoccupied, 5, true)   //RoomList[4]
            };

            foreach (var newRoom in rooms)
            {
                if (hotelContext.rooms.Any(r => r.Number == newRoom.Number))
                    continue;
                hotelContext.rooms.Add(newRoom);
            }
            hotelContext.SaveChanges();
        }

        //METHODS FOR CUSTOMER

        public void AddCustomer(Customer newCustomer)
        {
            hotelContext.customers.Add(newCustomer);
            hotelContext.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            hotelContext.customers.Remove(customer);
            hotelContext.SaveChanges();
        }

        public void UpdateCustomerName(Customer customer, string newCustomerName)
        {
            customer.UpdateName(newCustomerName);
            hotelContext.SaveChanges();
        }

        public void UpdateCustomerPhoneNumber(Customer customer, string newPhoneNumber)
        {
            customer.UpdatePhoneNumber(newPhoneNumber);
            hotelContext.SaveChanges();
        }
        public Customer GetCustomer(int id)
        {
            return hotelContext.customers.FirstOrDefault(c => c.Id == id);
        }
        public List<Customer> GetAllCustomers()
        {
            return hotelContext.customers.ToList();
        }
        public List<Customer> GetAllCustomersByFilter(string? name, string? phoneNumber, CustomerSortBy? sortBy)
        {
            var query = hotelContext.customers.AsQueryable();
            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.Contains(name));
            if (!string.IsNullOrEmpty(phoneNumber))
                query = query.Where(c => c.PhoneNumber.Contains(phoneNumber));
            if (sortBy.HasValue)
            {
                if (sortBy == CustomerSortBy.Name)
                    query = query.OrderBy(c => c.Name);
                if (sortBy == CustomerSortBy.PhoneNumber)
                    query = query.OrderBy(c => c.PhoneNumber);
            }
            return query.ToList();
        }

        //METHODS FOR ROOM
        public List<Room> GetAllRooms()
        {
            return hotelContext.rooms.ToList();
        }
        public List<Room> GetAllRoomsByFilter(byte? number, RoomType? type, RoomStatus? status, bool? isActive, RoomSortBy? sortBy)
        {
            var query = hotelContext.rooms.AsQueryable();
            if (number.HasValue)
                query = query.Where(r => r.Number == number.Value);
            if (type.HasValue)
                query = query.Where(r => r.Type == type.Value);
            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);
            if (isActive.HasValue)
                query = query.Where(r => r.IsActive == isActive.Value);
            if (sortBy.HasValue)
            {
                if (sortBy == RoomSortBy.Number)
                    query = query.OrderBy(r => r.Number);
                if (sortBy == RoomSortBy.Type)
                    query = query.OrderBy(r => r.Type);
                if (sortBy == RoomSortBy.Status)
                    query = query.OrderBy(r => r.Status);
                if (sortBy == RoomSortBy.IsActive)
                    query = query.OrderBy(r => r.IsActive);
            }

            return query.ToList();
        }

        public List<Room> GetEmptyRooms()
        {
            return hotelContext.rooms.Where(r => r.Status == RoomStatus.Unoccupied).ToList();
        }

        public Room GetRoom(int id)
        {
            return hotelContext.rooms.Include(r => r.Customer)
                .FirstOrDefault(r => r.Id == id);
        }

        public void AddRoom(Room room)
        {
            hotelContext.rooms.Add(room);
            hotelContext.SaveChanges();
        }

        //OPERATION METHODS
        public List<RoomOperation> GetAllRoomOperations()
        {
            return (hotelContext.roomOperations.ToList());
        }

        public RoomOperation GetRoomOperation(int id)
        {
            return (hotelContext.roomOperations.Where(ro => ro.Id == id).FirstOrDefault());
        }

        public List<RoomOperation> GetAllOperationsByFilter(string? customerName, byte? roomNumber, DateTime? fromDate, DateTime? toDate, decimal? paidAmount, RoomActionType? roomActionType, OperationSortBy? sortBy)
        {
            var query = hotelContext.roomOperations.AsQueryable(); // Important
            if (!string.IsNullOrEmpty(customerName)) // Important
                query = query.Include(o => o.Customer).Where(o => o.Customer.Name.Contains(customerName)); // Important
            if (roomNumber.HasValue)
                query = query.Include(o => o.Room).Where(o => o.Room.Number == roomNumber);
            if (fromDate.HasValue)
                query = query.Where(o => o.FromDate >= fromDate); //In ghablesh == bood; moshkeli ke ijad mikard in bood ke fromdate ro vared mikardi hichi nemiavord
            if (toDate.HasValue)
                query = query.Where(o => o.ToDate <= toDate); //In ghablesh == bood; moshkeli ke ijad mikard in bood ke todate ro vared mikardi hichi nemiavord
            if (paidAmount.HasValue)
                query = query.Where(o => o.PaidAmount == paidAmount);
            if (roomActionType.HasValue)
                query = query.Where(o => o.RoomActionType == roomActionType.Value);
            if (sortBy.HasValue)
            {
                if (sortBy == OperationSortBy.RoomNumber)
                    query = query.OrderBy(o => o.Room.Number);
                if (sortBy == OperationSortBy.CustomerName)
                    query = query.OrderBy(o => o.Customer.Name);
                if (sortBy == OperationSortBy.ActionType)
                    query = query.OrderBy(o => o.RoomActionType);
                if (sortBy == OperationSortBy.FromDate)
                    query = query.OrderBy(o => o.FromDate);
                if (sortBy == OperationSortBy.ToDate)
                    query = query.OrderBy(o => o.ToDate);
            }

            return query.Include(o => o.Room).Include(o => o.Customer).ToList(); //Include haro mitooni in tah benevisi ke behtar bashe
        }
    }
}


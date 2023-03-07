using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Repository
{
    public class OperationRepository : IOperationRepository
    {
        private readonly HotelContext hotelContext;
        public OperationRepository(HotelContext hotelContext)
        {
            this.hotelContext = hotelContext;
        }
        public List<RoomOperation> GetAllOperations()
        {
            return (hotelContext.roomOperations.ToList());
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

        public RoomOperation GetRoomOperationById(int id)
        {
            return (hotelContext.roomOperations.Where(ro => ro.Id == id).FirstOrDefault());
        }
    }
}

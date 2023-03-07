using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Repository
{
    public interface IOperationRepository
    {
        public List<RoomOperation> GetAllOperations();
        public RoomOperation GetRoomOperationById(int id);
        public List<RoomOperation> GetAllOperationsByFilter(string? customerName, byte? roomNumber, DateTime? fromDate, DateTime? toDate, decimal? paidAmount, RoomActionType? roomActionType, OperationSortBy? sortBy);
    }
}

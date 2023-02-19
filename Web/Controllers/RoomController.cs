using HotelManagementSystem.Business;
using HotelManagementSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly HotelManager hotelManager;

        public RoomController()
        {
            hotelManager = new HotelManager();
        }

        // GET: RoomController
        public ActionResult Index([FromQuery] RoomSearchViewModel searchVM)
        {
            var roomToSearch = hotelManager.GetAllRoomsByFilter(searchVM.Number, searchVM.Type, searchVM.Status, searchVM.IsActive, searchVM.SortBy);
            return View((roomToSearch, searchVM));
        }

        

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            var room = hotelManager.GetRoom(id);
            return View(room);
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var room = hotelManager.GetRoom(id);
            return View(room);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room room)
        {
            var roomToUpdate = hotelManager.GetRoom(room.Id);
            hotelManager.UpdateRoomNumber(roomToUpdate, room.Number);
            hotelManager.UpdateRoomStatus(roomToUpdate, room.Status);
            hotelManager.UpdateRoomType(roomToUpdate, room.Type);
            hotelManager.UpdateRoomActivation(roomToUpdate, room.IsActive);
            return RedirectToAction("Index");
        }

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Cancel(int id)
        {
            var roomToCancel = hotelManager.GetRoom(id);
            var customerToCancel = roomToCancel.Customer;
            return View((roomToCancel, customerToCancel));
        }

        [HttpPost]
        public ActionResult Cancel(Room room)
        {
            var roomToCancelReservation = hotelManager.GetRoom(room.Id);
            hotelManager.CancelReservation(roomToCancelReservation, roomToCancelReservation.Customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Free(int id)
        {
            var roomToFree = hotelManager.GetRoom(id);
            return View(roomToFree);
        }

        [HttpPost]
        public ActionResult Free(Room room)
        {
            var roomToFree = hotelManager.GetRoom(room.Id);
            hotelManager.FreeRoom(roomToFree);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Reserve(int id)
        {
            var roomToReserve = hotelManager.GetRoom(id);
            var customers = hotelManager
                .GetAllCustomers()
                .Select(c => new DropDownViewModel(c.Id, c.Name))
                .ToList();

            var tuple = (roomToReserve, customers);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult Reserve(ReserveRoomViewModel reserve)
        {
            var roomToReserve = hotelManager.GetRoom(reserve.RoomId);
            var customerToReserve = hotelManager.GetCustomer(reserve.CustomerId);
            var reservation = new RoomOperation(roomToReserve, customerToReserve, reserve.FromDate, reserve.ToDate, reserve.PaidAmount, RoomActionType.Reservation);
            hotelManager.ReserveRoom(reservation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Assign(int id)
        {
            var roomToAssign = hotelManager.GetRoom(id);
            var customers = hotelManager
                .GetAllCustomers()
                .Select(c => new DropDownViewModel(c.Id, c.Name))
                .ToList();

            ViewBag.Id = new SelectList(customers, string.Empty);

            var tuple = (roomToAssign, customers);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult Assign(RoomOperation assign)
        {
            var roomToAssign = hotelManager.GetRoom(assign.RoomId);
            var customerToAssign = hotelManager.GetCustomer(assign.CustomerId);
            var assignment = new RoomOperation(roomToAssign, customerToAssign, DateTime.Now, assign.ToDate, assign.PaidAmount, RoomActionType.Assignment);
            hotelManager.AssignRoom(assignment);
            return RedirectToAction("Index");
        }

        //OPERATION

        [HttpGet]
        public ActionResult Operation(int id)
        {
            var roomToOperate = hotelManager.GetRoom(id);
            return View(roomToOperate);
        }

        [HttpPost]
        public ActionResult Operation(RoomActionType roomActionType)
        {
            switch (roomActionType)
            {
                case RoomActionType.Assignment:
                    return RedirectToAction("Assign");
                case RoomActionType.Reservation:
                    return RedirectToAction("Reserve");
                default:
                    return View();
            }
        }

        //SEARCH
    }
}
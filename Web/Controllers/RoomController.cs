using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;
        private readonly ICustomerRepository customerRepository;

        public RoomController(IRoomRepository roomRepository, ICustomerRepository customerRepository)
        {
            this.roomRepository = roomRepository;
            this.customerRepository = customerRepository;
        }

        // GET: RoomController
        public ActionResult Index([FromQuery] RoomSearchViewModel searchVM)
        {
            var roomToSearch = roomRepository.GetAllRoomsByFilter(searchVM.Number, searchVM.Type, searchVM.Status, searchVM.IsActive, searchVM.SortBy);
            return View((roomToSearch, searchVM));
        }

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            var room = roomRepository.GetRoomById(id); //Moshkele nullreference dadane customer az implementatione GetRoomById bood ke customer ro include nakarde boodam
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
            var roomToUpdate = roomRepository.GetRoomById(id);
            return View(roomToUpdate);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomViewModel room)
        {
            var roomToUpdate = roomRepository.GetRoomById(room.Id) ??
                throw new Microsoft.AspNetCore.Http.BadHttpRequestException("room not found");
            roomToUpdate.Update(room.Number);
            roomToUpdate.Update(room.Status);
            roomToUpdate.Update(room.Type);
            roomToUpdate.Update(room.IsActive);
            roomRepository.Update(roomToUpdate); //havaset part nashe, in updatesh fargh dare
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
        public ActionResult Empty(int id)
        {
            var roomToEmpty = roomRepository.GetRoomById(id);
            return View(roomToEmpty);
        }

        [HttpPost]
        public ActionResult Empty(RoomViewModel room) //mishe az ReserveRoomViewModel ya harchize digeii ke id-e room ro dare ham estefade kard
        {
            var roomToEmpty = roomRepository.GetRoomById(room.Id);
            roomRepository.Empty(roomToEmpty);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Reserve(int id)
        {
            var roomToReserve = roomRepository.GetRoomById(id);
            var customers = customerRepository
                .GetAllCustomers()
                .Select(c => new DropDownViewModel(c.Id, c.Name))
                .ToList();
            return View((roomToReserve, customers));
        }

        [HttpPost]
        public ActionResult Reserve(RoomReserveViewModel reserve)
        {
            var roomToReserve = roomRepository.GetRoomById(reserve.RoomId);
            var customerToReserve = customerRepository.GetCustomerById(reserve.CustomerId);
            var reservation = new RoomOperation(roomToReserve, customerToReserve, reserve.FromDate, reserve.ToDate, reserve.PaidAmount, RoomActionType.Reservation);
            roomRepository.Reserve(reservation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Assign(int id)
        {
            var roomToAssign = roomRepository.GetRoomById(id);
            var customers = customerRepository
                .GetAllCustomers()
                .Select(c => new DropDownViewModel(c.Id, c.Name))
                .ToList();
            //ViewBag.Id = new SelectList(customers, string.Empty);
            return View((roomToAssign, customers));
        }

        [HttpPost]
        public ActionResult Assign(RoomOperation assign)
        {
            var roomToAssign = roomRepository.GetRoomById(assign.RoomId);
            var customerToAssign = customerRepository.GetCustomerById(assign.CustomerId);
            var assignment = new RoomOperation(roomToAssign, customerToAssign, DateTime.Now, assign.ToDate, assign.PaidAmount, RoomActionType.Assignment);
            roomRepository.Assign(assignment);
            return RedirectToAction("Index");
        }
    }
}
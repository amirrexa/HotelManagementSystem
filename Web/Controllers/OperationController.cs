
using HotelManagementSystem.Business;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class OperationController : Controller
    {
        private readonly IOperationRepository operationRepository;
        public OperationController(IOperationRepository operationRepository)
        {
            this.operationRepository = operationRepository;
        }
        // GET: OperationsController
        [HttpGet]
        public ActionResult Index([FromQuery] OperationSearchViewModel searchVM)
        {
            var operationToSearch = operationRepository.GetAllOperationsByFilter( searchVM.CustomerName, searchVM.RoomNumber, searchVM.FromDate, searchVM.ToDate, searchVM.PaidAmount, searchVM.RoomActionType, searchVM.SortBy);
            return View((operationToSearch, searchVM));
        }
        // GET: OperationsController/Details/5
        public ActionResult Details(int id)
        {
            var operationToShow = operationRepository.GetRoomOperationById(id);
            return View(operationToShow);
        }
        // GET: OperationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperationsController/Create
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

        // GET: OperationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OperationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: OperationsController/Delete/5
        public ActionResult Delete(int id)
        {
            var roomOperationToDelete = operationRepository.GetRoomOperationById(id);
            return View(roomOperationToDelete);
        }

    }
}

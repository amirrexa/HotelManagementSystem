using HotelManagementSystem.Business;
using HotelManagementSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class OperationController : Controller
    {
        private readonly HotelManager hotelManager;
        public OperationController()
        {
            hotelManager = new HotelManager();
        }
        // GET: OperationsController
        [HttpGet]
        public ActionResult Index()
        {
            var operations = hotelManager.GetAllRoomOperations();
            return View(operations);
        }

        // GET: OperationsController/Details/5
        public ActionResult Details(int id)
        {
            var operationToShow = hotelManager.GetRoomOperation(id);
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
            var roomOperationToDelete = hotelManager.GetRoomOperation(id);
            return View(roomOperationToDelete);
        }

    }
}

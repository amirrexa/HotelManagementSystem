using HotelManagementSystem.Business;
using HotelManagementSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HotelManager hotelManager;
        public CustomerController()
        {
            hotelManager = new HotelManager();
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            var customers = hotelManager.GetAllCustomers();
            return View(customers);
        }
        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var customer = hotelManager.GetCustomer(id);
            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer newCustomer)
        {
            hotelManager.AddCustomer(newCustomer);
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        { 
            return View(hotelManager.GetCustomer(id));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            var customerToUpdate = hotelManager.GetCustomer(customer.Id);
            hotelManager.UpdateCustomerName(customerToUpdate, customer.Name);
            hotelManager.UpdateCustomerPhoneNumber(customerToUpdate, customer.PhoneNumber);
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(hotelManager.GetCustomer(id));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Customer customer)
        {
            var customerToDelete = hotelManager.GetCustomer(customer.Id);
            hotelManager.RemoveCustomer(customerToDelete);
            return RedirectToAction(nameof(Index));
        }
    }
}

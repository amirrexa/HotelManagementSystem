using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        // GET: CustomerController
        public ActionResult Index([FromQuery] CustomerSearchViewModel searchVM)
        {
            var customers = customerRepository.GetAllCustomersByFilter(searchVM.Name, searchVM.PhoneNumber, searchVM.SortBy);            return View(customers);
        }
        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var customer = customerRepository.GetCustomerById(id);
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
            customerRepository.AddCustomer(newCustomer);
            return RedirectToAction("Index");
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        { 
            return View(customerRepository.GetCustomerById(id));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            var customerToUpdate = customerRepository.GetCustomerById(customer.Id);
            customerToUpdate.UpdateName(customer.Name);
            customerToUpdate.UpdatePhoneNumber(customer.PhoneNumber);
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(customerRepository.GetCustomerById(id));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Customer customer)
        {
            var customerToDelete = customerRepository.GetCustomerById(customer.Id);
            customerRepository.RemoveCustomer(customerToDelete);
            return RedirectToAction(nameof(Index));
        }
    }
}

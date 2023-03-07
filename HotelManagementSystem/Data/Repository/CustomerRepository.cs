using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.Data;

namespace HotelManagementSystem.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HotelContext hotelContext;

        public CustomerRepository(HotelContext hotelContext)
        {
            this.hotelContext = hotelContext;
        }

        public void Update(Customer customer)
        {
            hotelContext.Update(customer);
            hotelContext.SaveChanges();
        }

        public Customer GetCustomerById(int id)
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
    }
}

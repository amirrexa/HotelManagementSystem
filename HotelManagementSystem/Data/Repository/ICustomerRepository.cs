using HotelManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Repository
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void RemoveCustomer(Customer customer);
        void Update(Customer customer);
        Customer GetCustomerById(int id);
        List<Customer> GetAllCustomers();
        List<Customer> GetAllCustomersByFilter(string? name, string? phoneNumber, CustomerSortBy? sortBy);
    }
}

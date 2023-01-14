using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data
{
    public class Customer
    {
        public string Name { get; private set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public Customer()
        {
        }
        public Customer(string name)
        {
            Name = name;
        }
    }
}

using HotelManagementSystem.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data
{
    public class Customer
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }

        public Customer()
        {

        }
        public Customer(string name)
        {
            Name = name;
        }
        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public void UpdatePhoneNumber(string newPhoneNumber)
        {
            PhoneNumber = newPhoneNumber;
        }
    }
}
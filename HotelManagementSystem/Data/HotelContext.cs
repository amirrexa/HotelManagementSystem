using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Data
{
    public class HotelContext : DbContext
    {
        public DbSet<Room> rooms { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<RoomOperation> roomOperations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=HotelDB;Trusted_Connection=True;Encrypt=False;"); //Encrypt False Ro Nazari Kar Nemikone
        }
    }
}
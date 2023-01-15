﻿using HotelManagementSystem.Business;
using HotelManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
public class Program
{
    static void Main(string[] args)
    {
        var hotelManager = new HotelManager();
        hotelManager.InitializeRooms();
        hotelManager.GetRoom(1);
        //hotelManager.AddCustomer("Amir");
        //hotelManager.UpdateCustomerName(hotelManager.GetCustomer("Amir"),"Rima");
        hotelManager.RemoveCustomer(hotelManager.GetCustomer("Rima"));

        
    }
}
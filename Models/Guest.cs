﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_HotelBooking1.Models
{
    public class Guest
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        
        public string? PhoneNumber { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Booking> Bookings { get; set; }



    }
}

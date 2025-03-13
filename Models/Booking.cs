using DB_HotelBooking1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_HotelBooking1.Models
 {
        public class Booking
        {
            public int Id { get; set; }
            public int RoomId { get; set; }
            public required Room Room { get; set; }
            public int GuestId { get; set; }
            public required Guest Guest { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
        }
    

}

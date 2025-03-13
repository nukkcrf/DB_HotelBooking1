using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_HotelBooking1.Models
{
    public class Room
    {

        public int Id { get; set; }
        public required string RoomType { get; set; }       //Single or double
        public int ExtraBeds { get; set; }      //Max 2 extra beds
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }
       
        public List<Booking> Bookings { get; set; } = new  List <Booking>();
        public Room()  {  }
 
        public Room(int id, string roomType,int extraBeds)
        {
            Id = id;
            RoomType = roomType;
            ExtraBeds = extraBeds;
        }
    }
}

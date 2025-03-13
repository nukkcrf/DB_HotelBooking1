using Microsoft.EntityFrameworkCore;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;
using System;
using System.Linq;

namespace DB_HotelBooking1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseSqlServer("Server=.;Database=DB_HotelBooking1;Trusted_Connection=True;")
                .Options;

            using (var context = new HotelContext(options)) // ✅ Transmitem `options`
            {
                var rooms = context.Rooms.ToList();
                foreach (var room in rooms)
                {
                    Console.WriteLine($"Room {room.Id} is a {room.RoomType} room with {room.ExtraBeds} extra beds.");
                }
            }

            Console.WriteLine("Hello, World!");
        }
    }
}

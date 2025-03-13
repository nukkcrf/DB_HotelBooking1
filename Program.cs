using Microsoft.EntityFrameworkCore;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;


namespace DB_HotelBooking1

{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(var context = new HotelContext())
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

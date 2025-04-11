using System;
using System.Linq;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;

namespace DB_HotelBooking1.Services
{
    public class RoomService
    {
        private readonly HotelContext _context;

        public RoomService()
        {
            _context = new HotelContext();

        }

        public void AddRoom(string roomType, int extraBeds, decimal price)
        {
            var room = new Room
            {
                RoomType = roomType,
                ExtraBeds = extraBeds,
                Price = price
            };
            _context.Rooms.Add(room);
            _context.SaveChanges();
            Console.WriteLine("Room added successfully!");
        }
        // Metoda UpdateRoom 
        public void UpdateRoom(int id, string roomType, int extraBeds, decimal price)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
            if (room != null)
            {
                room.RoomType = roomType;
                room.ExtraBeds = extraBeds;
                room.Price = price;
                _context.SaveChanges();
                Console.WriteLine("Room updated successfully!");
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        // Metoda AddBooking 
        public void AddBooking(Booking booking)
        {
            // Guest and Room attach to context
            _context.Attach(booking.Guest);
            _context.Attach(booking.Room);

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            Console.WriteLine("Booking added successfully!");
        }
    }
}

// Remastered

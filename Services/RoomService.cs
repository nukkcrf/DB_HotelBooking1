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

        // Metoda UpdateRoom actualizează un room pe baza parametrilor furnizați.
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

        // Metoda AddBooking adaugă o rezervare și se asigură că Guest și Room sunt urmărite corect.
        public void AddBooking(Booking booking)
        {
            // Asigură-te că entitățile Guest și Room sunt atașate la context
            _context.Attach(booking.Guest);
            _context.Attach(booking.Room);

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            Console.WriteLine("Booking added successfully!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;

namespace DB_HotelBooking1.Services
{
    class BookingService
    {
        private readonly HotelContext _context;
        public BookingService()
        {
            _context = new HotelContext();
        }

        // CREATE - Add a new booking
        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            Console.WriteLine("Booking added successfully!");
        }
        public void RemoveBooking(Booking booking)
        {
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            Console.WriteLine("Booking removed successfully!");
        }
        // READ - List all bookings
        public void ListBookings()
        {
            var bookings = _context.Bookings.ToList();
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking {booking.Id}: {booking.Room.RoomType}, {booking.Guest.Name}, {booking.CheckIn}, {booking.CheckOut}");
            }
        }
        // UPDATE - Update an existing booking
        public void UpdateBooking(int id, DateTime newCheckInDate, DateTime newCheckOutDate)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                booking.CheckIn = newCheckInDate;
                booking.CheckOut = newCheckOutDate;
                _context.SaveChanges();
                Console.WriteLine("Booking updated successfully!");
            }
            else
            {
                Console.WriteLine("Booking not found!");
            }
        }
        //Delete - Delete a booking
        public void DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
                Console.WriteLine("Booking deleted successfully!");
            }
            else
            {
                Console.WriteLine("Booking not found!");
            }
        }
    }
}

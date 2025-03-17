using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;
using Microsoft.EntityFrameworkCore;

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
            // Ensure Guest and Room are properly tracked
            _context.Attach(booking.Guest);
            _context.Attach(booking.Room);

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
        public void CreateInvoice(int bookingId)
        {
            var booking = _context.Bookings.Include(b => b.Room).FirstOrDefault(b => b.Id == bookingId);
            if (booking != null)
            {
                var totalAmount = CalculateTotalAmount(booking);
                var invoice = new Invoice
                {
                    BookingId = booking.Id,
                    Booking = booking,
                    TotalAmount = totalAmount,
                    Date = DateTime.Now
                };
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                Console.WriteLine("Invoice created successfully!");
            }
            else
            {
                Console.WriteLine("Booking not found!");
            }
        }
        private decimal CalculateTotalAmount(Booking booking)
        {
            // Calculate total amount based on room type and duration
            var duration = (booking.CheckOut - booking.CheckIn).Days;
            var roomRate = booking.Room.ExtraBeds > 0 ? 150 : 100;
            return duration * roomRate;
        }
    }
}

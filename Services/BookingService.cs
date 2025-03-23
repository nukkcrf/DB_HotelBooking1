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
            // Re-fetch Guest from database to ensure correct data is loaded
            var guestFromDb = _context.Guests.FirstOrDefault(g => g.Id == booking.Guest.Id);
            if (guestFromDb != null)
            {
                booking.Guest = guestFromDb;
            }
            else
            {
                // attach new guest
                _context.Attach(booking.Guest);
            }

            
            var roomFromDb = _context.Rooms.FirstOrDefault(r => r.Id == booking.Room.Id);
            if (roomFromDb != null)
            {
                booking.Room = roomFromDb;
            }
            else
            {
                _context.Attach(booking.Room);
            }

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
            var bookings = _context.Bookings.Include(b => b.Room).Include(b => b.Guest). ToList();
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking {booking.Id}: {booking.Room.RoomType}, {booking.Guest.Name}, {booking.CheckIn}, {booking.CheckOut}");
            }
        }
       
        // UPDATE - Update an existing booking and its invoice
        public void UpdateBooking(int id, DateTime newCheckInDate, DateTime newCheckOutDate)
        {
            // Include Room and Guest
            
            var booking = _context.Bookings
                                  .Include(b => b.Room)
                                  .Include(b => b.Guest)

                                  .FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                booking.CheckIn = newCheckInDate;
                booking.CheckOut = newCheckOutDate;
                _context.SaveChanges();
                Console.WriteLine("Booking updated successfully!");

                
                UpdateInvoice(booking);
                
            }
            else
            {
                Console.WriteLine("Booking not found!");
            }
        }

        // Metoda for updating invoice
        private void UpdateInvoice(Booking booking)
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.BookingId == booking.Id);
            if (invoice != null)
            {
                invoice.TotalAmount = CalculateTotalAmount(booking);
                invoice.Date = DateTime.Now;  // date updated to current date
                _context.SaveChanges();


                Console.WriteLine("Invoice updated successfully!");
                Console.WriteLine("----- Updated Invoice Details -----");
                Console.WriteLine($"Invoice ID: {invoice.Id}");
                Console.WriteLine($"Booking ID: {invoice.BookingId}");
                Console.WriteLine($"Total Amount: {invoice.TotalAmount:C}");
                Console.WriteLine($"Invoice Date: {invoice.Date}");
            }
            else
            {
                Console.WriteLine("Invoice not found for this booking. Creating new invoice...");
                var newInvoice = CreateInvoice(booking.Id);
                if (newInvoice != null)
                {
                    Console.WriteLine("----- New Invoice Details -----");
                    Console.WriteLine($"Invoice ID: {newInvoice.Id}");
                    Console.WriteLine($"Booking ID: {newInvoice.BookingId}");
                    Console.WriteLine($"Total Amount: {newInvoice.TotalAmount:C}");
                    Console.WriteLine($"Invoice Date: {newInvoice.Date}");
                }
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

        public void ListAvailableRooms()
        {
            // Select all rooms that are not booked
            var availableRooms = _context.Rooms.Where(r => !r.IsBooked).ToList();
            foreach (var room in availableRooms)
            {
                Console.WriteLine($"Room {room.Id}: {room.RoomType}, Extra Beds: {room.ExtraBeds}");
            }
        }

        public Invoice CreateInvoice(int bookingId)
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
                return invoice;
            }
            else
            {
                Console.WriteLine("Booking not found!");
                return null;    
            }
        }
        private decimal CalculateTotalAmount(Booking booking)
        {
            var duration = (booking.CheckOut - booking.CheckIn).Days;
            decimal roomRate = 0;

            // Single sau Double ?
            if (booking.Room.RoomType.Equals("Single", StringComparison.OrdinalIgnoreCase))
            {
                roomRate = 100; // Price for single room
            }
            else if (booking.Room.RoomType.Equals("Double", StringComparison.OrdinalIgnoreCase))
            {
                roomRate = 150; // Price for double room
            }
            // price for extra beds
            if (booking.Room.RoomType.Equals("Double", StringComparison.OrdinalIgnoreCase))
            {
                roomRate = 150 + (booking.Room.ExtraBeds * 20); // de ex., fiecare extras bed adaugă 20
            }

            return duration * roomRate;
        }

    }
}

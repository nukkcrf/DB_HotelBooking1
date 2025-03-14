using System;
using System.Collections.Generic;
using System.Linq;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;

namespace DB_HotelBooking1.Services
{
    public class GuestService
    {
        private readonly HotelContext _context;

        public GuestService()
        {
            _context = new HotelContext();
        }

        // CREATE - Add a new guest
        public void AddGuest(Guest guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
            Console.WriteLine("Guest added successfully!");
        }

        // READ - List all guests
        public void ListGuests()
        {
            var guests = _context.Guests.ToList();
            foreach (var guest in guests)
            {
                Console.WriteLine($"Guest {guest.Id}: {guest.Name}, {guest.Email}");
            }
        }

        // UPDATE - Update an existing guest
        public void UpdateGuest(int id, string newName, string newEmail)
        {
            var guest = _context.Guests.Find(id);
            if (guest != null)
            {
                guest.Name = newName;
                guest.Email = newEmail;
                _context.SaveChanges();
                Console.WriteLine("Guest updated successfully!");
            }
            else
            {
                Console.WriteLine("Guest not found!");
            }
        }

        // DELETE - Delete a guest
        public void DeleteGuest(int id)
        {
            var guest = _context.Guests.Find(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                _context.SaveChanges();
                Console.WriteLine("Guest deleted successfully!");
            }
            else
            {
                Console.WriteLine("Guest not found!");
            }
        }
    }
}

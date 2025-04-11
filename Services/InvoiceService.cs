using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;

namespace DB_HotelBooking1.Services
{
    public class InvoiceService
    {
        private readonly HotelContext _context;

        public InvoiceService()
        {
            _context = new HotelContext();
        }

        // READ - List all invoices
        public void ListInvoices()
        {
            var invoices = _context.Invoices
                .Include(i => i.Booking)
                    .ThenInclude(b => b.Room)
                .Include(i => i.Booking)
                    .ThenInclude(b => b.Guest)
                .ToList();

            foreach (var invoice in invoices)
            {
                Console.WriteLine($"Invoice {invoice.Id}: Booking {invoice.BookingId}, Total Amount: {invoice.TotalAmount:C}, Date: {invoice.Date}");
            }
        }
    }
}

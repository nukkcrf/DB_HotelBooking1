﻿using System;
using DB_HotelBooking1.Services;
using DB_HotelBooking1.Models;
using DB_HotelBooking1.Data;
using Microsoft.EntityFrameworkCore;

namespace DB_HotelBooking1
{
    internal class Program
    {
        // Instanțierea serviciilor existente
        static BookingService bookingService = new BookingService();
        static GuestService guestService = new GuestService();
        static InvoiceService invoiceService = new InvoiceService();
        static HotelContext _hotelContext = new HotelContext();
        static RoomService _roomService = new RoomService();

        static void Main(string[] args)
        {
            _hotelContext.Database.Migrate();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("         HOTEL MANAGEMENT SYSTEM              ");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. New booking");
                Console.WriteLine("2. Update booking");
                Console.WriteLine("3. Cancel booking");
                Console.WriteLine("4. See available rooms");
                Console.WriteLine("5. See bokings");
                Console.WriteLine("6. Show invoices");
                Console.WriteLine("7. Exit");
                Console.WriteLine("8. Add room");


                Console.Write("Choose an option: ");
                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // NEW BOOKING: Redirect to available rooms and guest selection
                        Console.WriteLine("Available rooms:");
                        bookingService.ListAvailableRooms();

                        Console.Write("Enter the ID of the room you want to book: ");
                        string? roomIdInput = Console.ReadLine();
                        if (!int.TryParse(roomIdInput, out int roomId))
                        {
                            Console.WriteLine("Invalid room ID.");
                            Pause();
                            break;
                        }

                        // Ask if the guest is new or registered
                        Console.Write("Is the guest new or registered? (Enter N for new, R for registered): ");
                        string? guestType = Console.ReadLine();
                        int guestId = 0;
                        if (guestType?.ToUpper() == "N")
                        {
                            // New guest: use your existing GuestService method
                            Console.Write("Enter guest name: ");
                            string? guestName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(guestName))
                            {
                                Console.WriteLine("Guest name cannot be empty.");
                                Pause();
                                break;
                            }
                            Console.Write("Enter guest email: ");
                            string? guestEmail = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(guestEmail))
                            {
                                Console.WriteLine("Guest email cannot be empty.");
                                Pause();
                                break;
                            }

                            // Create and add the new guest using your GuestService
                            Guest newGuest = new Guest { Name = guestName, Email = guestEmail };
                            guestService.AddGuest(newGuest);

                            // Assume the new guest's ID is set after insertion
                            guestId = newGuest.Id;
                        }
                        else if (guestType?.ToUpper() == "R")
                        {
                            // Registered guest: list guests and ask for guest ID
                            guestService.ListGuests();
                            Console.Write("Enter your guest ID: ");
                            string? guestIdInput = Console.ReadLine();
                            if (!int.TryParse(guestIdInput, out guestId))
                            {
                                Console.WriteLine("Invalid guest ID.");
                                Pause();
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid option for guest type.");
                            Pause();
                            break;
                        }

                        // Get check-in and check-out dates
                        Console.Write("Enter check-in date (yyyy-MM-dd): ");
                        string? checkInInput = Console.ReadLine();
                        if (!DateTime.TryParse(checkInInput, out DateTime checkIn))
                        {
                            Console.WriteLine("Invalid date format.");
                            Pause();
                            break;
                        }
                        Console.Write("Enter check-out date (yyyy-MM-dd): ");
                        string? checkOutInput = Console.ReadLine();
                        if (!DateTime.TryParse(checkOutInput, out DateTime checkOut))
                        {
                            Console.WriteLine("Invalid date format.");
                            Pause();
                            break;
                        }

                        // Create the booking object
                        if (!IsBookingDateValid(checkIn, checkOut))
                        {
                            Pause(); 
                            break;

                        }
                        Booking newBooking = new Booking()  
                        {
                            RoomId = roomId,
                            GuestId = guestId,
                            CheckIn = checkIn,
                            CheckOut = checkOut,
                            Room = new Room { Id = roomId },
                            Guest = new Guest { Id = guestId ,Name = "....",Email = " mail@ example.com"}
                        };

                        // Add the booking using BookingService
                        
                        bookingService.AddBooking(newBooking);
                        Console.WriteLine("Booking created successfully!");
                        // Generate invoice after booking creation.
                        var invoice = bookingService.CreateInvoice(newBooking.Id);
                        if (invoice != null)
                        {
                            Console.WriteLine("============================");
                            Console.WriteLine("----- Invoice Details -----");
                            Console.WriteLine($"Invoice ID: {invoice.Id}");
                            Console.WriteLine($"Booking ID: {invoice.BookingId}");
                            Console.WriteLine($"Total Amount: {invoice.TotalAmount:C}");
                            Console.WriteLine($"Invoice Date: {invoice.Date}");
                            Console.WriteLine("=============================");
                        }
                        Pause();
                        break;

                    case "2":
                        // Update booking logic here
                        Console.Write("Enter the booking ID to update: ");
                        string? updateIdInput = Console.ReadLine();
                        if (!int.TryParse(updateIdInput, out int updateId))
                        {
                            Console.WriteLine("Invalid booking ID.");
                            Pause();
                            break;
                        }
                        Console.Write("Enter new check-in date (yyyy-MM-dd): ");
                        string? newCheckInInput = Console.ReadLine();
                        if (!DateTime.TryParse(newCheckInInput, out DateTime newCheckIn))
                        {
                            Console.WriteLine("Invalid date format.");
                            Pause();
                            break;
                        }
                        Console.Write("Enter new check-out date (yyyy-MM-dd): ");
                        string? newCheckOutInput = Console.ReadLine();
                        if (!DateTime.TryParse(newCheckOutInput, out DateTime newCheckOut))
                        {
                            Console.WriteLine("Invalid date format.");
                            Pause();
                            break;
                        }
                        if (!IsBookingDateValid(newCheckIn, newCheckOut))
                        { 
                            Pause();
                            break; 
                        }
                        bookingService.UpdateBooking(updateId, newCheckIn, newCheckOut);
                        Console.WriteLine("Booking updated.");
                        Pause();
                        break;

                    case "3":
                        // Cancel booking logic here
                        Console.Write("Enter the booking ID to cancel: ");
                        string? cancelIdInput = Console.ReadLine();
                        if (!int.TryParse(cancelIdInput, out int cancelId))
                        {
                            Console.WriteLine("Invalid booking ID.");
                            Pause();
                            break;
                        }
                        Console.Write("Are you sure you want to cancel the booking? (Y/N): ");
                        string? confirmCancel = Console.ReadLine();
                        if (confirmCancel?.ToUpper() == "Y")
                        {
                            bookingService.DeleteBooking(cancelId);
                            Console.WriteLine("Booking cancelled.");
                        }
                        else
                        {
                            Console.WriteLine("Cancellation aborted.");
                        }
                        Pause();
                        break;

                    case "4":
                        // See available rooms
                        bookingService.ListAvailableRooms();
                        Pause();
                        break;
                    case "5":
                        // See bookings
                        bookingService.ListBookings();
                        Pause();
                        break;

                    case "6":
                        invoiceService.ListInvoices();
                        Pause();
                        break;


                    case "7":
                        exit = true;
                        break;

                    case "8":
                       
                        AddRoom();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        Pause();
                        break;
                }
            }
            Console.WriteLine("Exiting the application. Goodbye!");
        }

        public static bool IsBookingDateValid(DateTime checkInDate, DateTime checkOutDate)
        {
            // Get today's date without the time (e.g., 2025-04-11 00:00:00)
            DateTime today = DateTime.Today;

            // 1. Check if check-in date is in the past
            if (checkInDate < today)
            {
                Console.WriteLine("Check-in date cannot be in the past.");
                return false;
            }

            // 2. Check if check-out date is before or same as check-in date
            if (checkOutDate <= checkInDate)
            {
                Console.WriteLine("Check-out date must be after check-in date.");
                return false;
            }

            // If both conditions are OK
            return true;
        }
        public static void AddRoom()
        {
            // Room Type
            Console.Write("Enter room type (Single/Double): ");
            string roomType = Console.ReadLine();

            // Extra Beds
            int extraBeds;
            do
            {
                Console.Write("Enter number of extra beds (0 to 2): ");
            }
            while (!int.TryParse(Console.ReadLine(), out extraBeds) || extraBeds < 0 || extraBeds > 2);

            // Price
            decimal price;
            do
            {
                Console.Write("Enter price: ");
            }
            while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0);

            // IsBooked
            Console.Write("Is the room booked? (yes/no): ");
            string bookedInput = Console.ReadLine()?.ToLower();
            bool IsBooked = bookedInput == "yes" || bookedInput == "y";

            _roomService.AddRoom(roomType, extraBeds, price);
        }

        static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

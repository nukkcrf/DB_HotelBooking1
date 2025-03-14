using Microsoft.EntityFrameworkCore;
using DB_HotelBooking1.Data;
using DB_HotelBooking1.Models;
using System;
using System.Linq;
using DB_HotelBooking1.Services;

namespace DB_HotelBooking1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseSqlServer("Server=GABRIEL_U;Database=DB_HotelBooking1;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            // Testing connection: list all rooms from the context
            using (var context = new HotelContext(options))
            {
                var rooms = context.Rooms.ToList();
                foreach (var room in rooms)
                {
                    Console.WriteLine($"Room {room.Id} is a {room.RoomType} room with {room.ExtraBeds} extra beds.");
                }
            }

            Console.WriteLine("Testing");

           
            var hotelService = new HotelServices();
            bool exit = false;

            while (!exit)
            {      // Menu options for Guest and Booking management

                Console.WriteLine("\n --- Hotel Management System ---");
                Console.WriteLine("1. Add a room");
                Console.WriteLine("2. List all rooms");
                Console.WriteLine("3. Update a room");
                Console.WriteLine("4. Delete a room");
                Console.WriteLine("5. Add a guest");
                Console.WriteLine("6. List all guests");
                Console.WriteLine("7. Update a guest");
                Console.WriteLine("8. Delete a guest");
                Console.WriteLine("9. Add a booking");
                Console.WriteLine("10. List all bookings");
                Console.WriteLine("11. Update a booking");
                Console.WriteLine("12. Cancel a booking");
                Console.WriteLine("13. Exit");
                Console.Write("Choose an option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Enter room type: ");
                        string? roomType = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(roomType))
                        {
                            Console.WriteLine("Room type cannot be empty.");
                            break;
                        }

                        Console.Write("Enter number of extra beds: ");
                        string? extraBedsInput = Console.ReadLine();
                        if (!int.TryParse(extraBedsInput, out int extraBeds))
                        {
                            Console.WriteLine("Invalid number for extra beds.");
                            break;
                        }

                       
                        Room room = new Room(roomType, extraBeds);
                        hotelService.AddRoom(room);
                        break;

                    case "2":
                        hotelService.ListRooms();
                        break;

                    case "3":
                        Console.Write("Enter the room ID to update: ");
                        string? idInput = Console.ReadLine();
                        if (!int.TryParse(idInput, out int id))
                        {
                            Console.WriteLine("Invalid room ID.");
                            break;
                        }
                        Console.Write("Enter new room type: ");
                        string? newRoomType = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newRoomType))
                        {
                            Console.WriteLine("New room type cannot be empty.");
                            break;
                        }
                        Console.Write("Enter new number of extra beds: ");
                        string? newExtraBedsInput = Console.ReadLine();
                        if (!int.TryParse(newExtraBedsInput, out int newExtraBeds))
                        {
                            Console.WriteLine("Invalid number for extra beds.");
                            break;
                        }
                        hotelService.UpdateRoom(id, newRoomType, newExtraBeds);
                        break;

                    case "4":
                        Console.Write("Enter the room ID to delete: ");
                        string? deleteIdInput = Console.ReadLine();
                        if (!int.TryParse(deleteIdInput, out int deleteId))
                        {
                            Console.WriteLine("Invalid room ID.");
                            break;
                        }
                        hotelService.DeleteRoom(deleteId);
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Exiting the Application. Goodbye!");
        }
    }
}

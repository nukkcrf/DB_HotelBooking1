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
                .UseSqlServer("Server=GABRIEL_U;Database=DB_HotelBooking1;Trusted_Connection=True;")
                .Options;

            using (var context = new HotelContext(options)) // ✅ Transmitem `options`
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
            {
                Console.WriteLine("\n --- Hotel Managment System ---");
                Console.WriteLine("1. Add a room");
                Console.WriteLine("2. List all rooms");
                Console.WriteLine("3. Update a room");
                Console.WriteLine("4. Delete a room");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)

                {
                    case "1":
                        Console.WriteLine("Enter room type: ");
                        string roomType = Console.ReadLine();
                        Console.WriteLine("Enter number of extra beds: ");
                        int extraBeds = int.Parse(Console.ReadLine());
                        Room room = new Room(roomType, extraBeds);
                        hotelService.AddRoom(room);
                        break;
                    case "2":
                        hotelService.ListRooms();
                        break;
                    case "3":
                        Console.WriteLine("Enter the room ID to update");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new room type: ");
                        string newRoomType = Console.ReadLine();
                        Console.WriteLine("Enter new number of extra beds: ");
                        int newExtraBeds = int.Parse(Console.ReadLine());
                        hotelService.UpdateRoom(id, newRoomType, newExtraBeds);
                        break;
                    case "4":
                        Console.WriteLine("Enter the room ID to delete");
                        int deleteId = int.Parse(Console.ReadLine());
                        hotelService.DeleteRoom(deleteId);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. ");
                        break;


                }
                Console.WriteLine("Exiting the Application. Goodye! ");

             }



         }
    }
}


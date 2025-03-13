using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB_HotelBooking1.Models;

using Microsoft.EntityFrameworkCore;
using DB_HotelBooking1.Data;


namespace DB_HotelBooking1.Services
{
    public class HotelServices
    {
        private readonly HotelContext _context;
        public HotelServices()
        {
            _context = new HotelContext();
        }

        //Create- Ad a new room
        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            Console.WriteLine("Room addded succesfully. ");
        }
        //Read- Get all rooms

        public void ListRooms()
        {
            var rooms = _context.Rooms.ToList();
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room {room.Id} is a {room.RoomType} room with {room.ExtraBeds} extra beds.");
            }
        }
        //Update- Update a room

        public void UpdateRoom(Room room)
        {
            _ = _context.Rooms.Find(room.Id);
            if (room != null)
            {
                _context.Rooms.Update(room);
                _context.SaveChanges();
                Console.WriteLine("Room updated succesfully. ");
            }
            else
            {
                Console.WriteLine("Room not found. ");
            }

        }
        //Delete- Delete a room
        public void DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
                Console.WriteLine("Room deleted succesfully. ");
            }
            else
            {
                Console.WriteLine("Room not found. ");
            }
        }


    }
}

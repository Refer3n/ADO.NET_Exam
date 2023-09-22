using HotelApp.DAL.Entities;
using HotelApp.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HotelApp.Providers
{
    public class RoomProvider
    {
        private readonly IRepository<Room> _repository;

        public RoomProvider(IRepository<Room> repository)
        {
            _repository = repository;
        }

        public void AddRooms(List<Room> rooms)
        {
            rooms.ForEach(room => AddRoom(room));
        }

        public void AddRoom(Room room)
        {
            _repository.Add(room);
        }

        public void RemoveRooms(List<Room> rooms)
        {
            rooms.ForEach(room => RemoveRoom(room));
        }

        public void RemoveRoom(Room room)
        {
            _repository.Remove(room);
        }

        public void RemoveRoom(int Id)
        {
            var room = GetRoom(Id);
            _repository.Remove(room);
        }

        public Room GetRoom(int Id)
        {
            return _repository.Get(Id);
        }

        public Room GetRoomByNumber(string roomNumber)
        {
            return _repository.GetAll().FirstOrDefault(room => room.RoomNumber == roomNumber);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _repository.GetAll();
        }
    }
}
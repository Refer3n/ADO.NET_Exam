using HotelApp.DAL.Entities;
using HotelApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            rooms.ForEach(room => AddReservation(room));
        }

        public void AddReservation(Room room)
        {
            _repository.Add(room);
        }

        public void RemoveReservations(List<Room> rooms)
        {
            rooms.ForEach(room => RemoveReservation(room));
        }

        public void RemoveReservation(Room room)
        {
            _repository.Remove(room);
        }

        public void RemoveReservation(int Id)
        {
            var room = GetReservation(Id);
            _repository.Remove(room);
        }

        public Room GetReservation(int Id)
        {
            return _repository.Get(Id);
        }

        public IEnumerable<Room> GetReservations()
        {
            return _repository.GetAll();
        }
    }
}
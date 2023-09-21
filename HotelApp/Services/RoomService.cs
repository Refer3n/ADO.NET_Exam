using AutoMapper;
using HotelApp.HotelDtos;
using HotelApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    public class RoomService
    {
        private RoomProvider _provider;
        private IMapper _mapper;

        public RoomService()
        {
            CreateProvider();
            CreateMapper();
        }

        public List<RoomDto> GetRooms()
        {
            var rooms = _provider.GetRooms();
            var roomDtos = _mapper.Map<List<RoomDto>>(rooms);
            return roomDtos;
        }

        public List<RoomDto> GetRoomsByCriteria(string sortingCriteria)
        {
            var rooms = GetRooms();

            switch (sortingCriteria)
            {
                case "Room Number":
                    rooms = rooms.OrderBy(r => r.RoomNumber).ToList();
                    break;
                case "Class":
                    rooms = rooms.OrderBy(r => r.Class).ToList();
                    break;
                case "Price Per Night":
                    rooms = rooms.OrderBy(r => r.PricePerNight).ToList();
                    break;
            }

            return rooms;
        }



        private void CreateProvider()
        {
            var context = new DAL.HotelContext();
            var repository = new DAL.Repositories.Repository<DAL.Entities.Room>(context);
            _provider = new RoomProvider(repository);
        }

        private void CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DAL.Entities.Room, RoomDto>();
            });

            _mapper = config.CreateMapper();
        }
    }
}

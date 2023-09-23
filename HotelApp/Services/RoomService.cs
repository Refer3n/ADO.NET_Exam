using AutoMapper;
using HotelApp.DAL.Entities;
using HotelApp.HotelDtos;
using HotelApp.Providers;
using System.Collections.Generic;
using System.Linq;

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

        public List<RoomDto> GetAvailableRoomsByCriteria(string sortingCriteria)
        {
            var availableRooms = GetAvailableRooms();

            switch (sortingCriteria)
            {
                case "Room Number":
                    availableRooms = availableRooms.OrderBy(r => r.RoomNumber).ToList();
                    break;
                case "Class":
                    availableRooms = availableRooms.OrderBy(r => r.PricePerNight).ToList();
                    break;
                case "Price Per Night":
                    availableRooms = availableRooms.OrderBy(r => r.PricePerNight).ToList();
                    break;
            }

            return availableRooms;
        }

        public List<RoomDto> GetAvailableRooms()
        {
            var allRooms = GetRooms();

            var availableRooms = allRooms.Where(r => r.Status == true).ToList();

            return availableRooms;
        }


        public RoomDto GetRoomByNumber(string roomNumber)
        {
            var room = _provider.GetRoomByNumber(roomNumber);

            return _mapper.Map<RoomDto>(room);
        }

        public void UpdateRoomStatus(RoomDto roomDto, bool newStatus)
        {
            var room = GetRoomById(roomDto.Id);

            room.Status = newStatus;

            _provider.UpdateRoom(room);
        }

        private Room GetRoomById(int Id)
        {
            return _provider.GetRoom(Id);
        }

        private void CreateProvider()
        {
            var context = new DAL.HotelContext();
            var repository = new DAL.Repositories.Repository<Room>(context);
            _provider = new RoomProvider(repository);
        }
        private void CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Room, RoomDto>();
                cfg.CreateMap<RoomDto, Room>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore());
            });

            _mapper = config.CreateMapper();
        }
    }
}

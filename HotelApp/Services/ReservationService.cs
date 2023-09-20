using HotelApp.HotelDtos;
using HotelApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    public class ReservationService
    {
        private ReservartionProvider _provider;

        public ReservationService()
        {
            var context = new DAL.HotelContext();
            var repository = new DAL.Repositories.Repository<DAL.Entities.Reservation>(context);
            _provider = new ReservartionProvider(repository);
        }

        public List<RoomDto> GetRooms()
        {
            var rooms = new List<RoomDto>
            {
                new RoomDto { RoomNumber = "101", Class = "Standard", PricePerNight = 100, Status = true, Description = "Standard Room 101" },
                new RoomDto { RoomNumber = "201", Class = "Deluxe", PricePerNight = 150, Status = true, Description = "Deluxe Room 201" },
                new RoomDto { RoomNumber = "301", Class = "Suite", PricePerNight = 200, Status = true, Description = "Suite Room 301" },
                new RoomDto { RoomNumber = "102", Class = "Standard", PricePerNight = 100, Status = true, Description = "Standard Room 102" },
                new RoomDto { RoomNumber = "202", Class = "Deluxe", PricePerNight = 150, Status = true, Description = "Deluxe Room 202" },
                new RoomDto { RoomNumber = "302", Class = "Suite", PricePerNight = 200, Status = true, Description = "Suite Room 302" },
            };

            return rooms;
        }
    }
}

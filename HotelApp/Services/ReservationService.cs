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

        public List<RoomDto> GetReservartions()
        {
            return null;
        }


    }
}

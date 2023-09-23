using AutoMapper;
using HotelApp.DAL.Entities;
using HotelApp.HotelDtos;
using HotelApp.Providers;

namespace HotelApp.Services
{
    public class ReservationService
    {
        private ReservationProvider _provider;
        private IMapper _mapper;

        public ReservationService()
        {
            CreateProvider();
            CreateMapper();
        }

        public void AddReservation(ReservationDto reservationDto)
        {
            var reservationEntity = _mapper.Map<Reservation>(reservationDto);

            _provider.AddReservation(reservationEntity);
        }

        private void CreateProvider()
        {
            var context = new DAL.HotelContext();
            var repository = new DAL.Repositories.Repository<Reservation>(context);
            _provider = new ReservationProvider(repository);
        }
        private void CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<ReservationDto, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<CustomerDto, Customer>();
                cfg.CreateMap<RoomDto, Room>();
            });

            _mapper = config.CreateMapper();
        }

    }
}

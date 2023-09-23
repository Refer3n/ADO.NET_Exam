using AutoMapper;
using HotelApp.DAL.Entities;
using HotelApp.HotelDtos;
using HotelApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelApp.Services
{
    public class ReservationService
    {
        private ReservationProvider _reservationProvider;
        private IMapper _mapper;

        public ReservationService()
        {
            CreateProvider();
            CreateMapper();
        }

        public void AddReservation(ReservationDto reservationDto)
        {
            var reservationEntity = _mapper.Map<Reservation>(reservationDto);

            _reservationProvider.AddReservation(reservationEntity);
        }

        public string GetStatistics(DateTime startDate, DateTime endDate)
        {
            var reservations = GetReservationsInDateRange(startDate, endDate);

            int totalBookings = reservations.Count();
            decimal totalRevenue = reservations.Sum(r => r.Price);

            int totalCustomers = reservations.Select(r => r.CustomerId).Distinct().Count();
            int totalRoomsReserved = reservations.Select(r => r.RoomId).Distinct().Count();

            decimal averageRevenuePerBooking = totalRevenue / totalBookings;

            var mostCommonRoomClass = reservations
                .GroupBy(r => r.Room.Class)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            string statistics = $"Statistics from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}:\n" +
                               $"Total Bookings: {totalBookings}\n" +
                               $"Total Revenue: ${totalRevenue}\n" +
                               $"Total Customers: {totalCustomers}\n" +
                               $"Total Rooms Reserved: {totalRoomsReserved}\n" +
                               $"Average Revenue Per Booking: ${averageRevenuePerBooking:F2}\n" +
                               $"Most Common Room Class Booked: {mostCommonRoomClass}\n";

            return statistics;
        }

        public List<Reservation> GetReservationsInDateRange(DateTime startDate, DateTime endDate)
        {
            var reservations = _reservationProvider.GetReservationsIncluding(r => r.Customer, r => r.Room);

            var reservationsInDateRange = reservations
                .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
                .ToList();

            return reservationsInDateRange;
        }

        //public List<RoomDto> GetUnoccupiedRoomsInDateRange(DateTime startDate, DateTime endDate)
        //{
        //    var allRooms = _roomProvider.GetRooms();

        //    var reservations = GetReservationsInDateRange(startDate, endDate);

        //    var occupiedRoomIds = reservations.Select(r => r.RoomId).Distinct().ToList();

        //    var unoccupiedRooms = allRooms.Where(room => !occupiedRoomIds.Contains(room.Id)).ToList();

        //    var roomDtos = _mapper.Map<List<RoomDto>>(unoccupiedRooms);

        //    return roomDtos;
        //}

        private void CreateProvider()
        {
            var context = new DAL.HotelContext();
            var repository = new DAL.Repositories.Repository<Reservation>(context);
            _reservationProvider = new ReservationProvider(repository);
        }

        private void CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Reservation, ReservationDto>();
                cfg.CreateMap<ReservationDto, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<CustomerDto, Customer>();
                cfg.CreateMap<RoomDto, Room>();
            });

            _mapper = config.CreateMapper();
        }

    }
}

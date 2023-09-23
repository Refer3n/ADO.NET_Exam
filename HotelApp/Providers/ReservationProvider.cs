using HotelApp.DAL.Entities;
using HotelApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HotelApp.Providers
{
    public class ReservationProvider
    {
        private readonly IRepository<Reservation> _repository;

        public ReservationProvider(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public void AddReservations(List<Reservation> reservations)
        {
            reservations.ForEach(reservation => AddReservation(reservation));
        }

        public void AddReservation(Reservation reservation)
        {
            _repository.Add(reservation);
        }

        public void RemoveReservations(List<Reservation> reservations)
        {
            reservations.ForEach(reservation => RemoveReservation(reservation));
        }

        public void RemoveReservation(Reservation reservation)
        {
            _repository.Remove(reservation);
        }

        public void RemoveReservation(int Id)
        {
            var reservation = GetReservation(Id);
            _repository.Remove(reservation);
        }

        public Reservation GetReservation(int Id)
        {
            return _repository.Get(Id);
        }

        public IEnumerable<Reservation> GetReservations()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Reservation> GetReservationsIncluding(params Expression<Func<Reservation, object>>[] includeProperties)
        {
            return _repository.GetAllIncluding(includeProperties);
        }

    }
}

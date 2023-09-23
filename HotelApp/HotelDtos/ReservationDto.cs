using System;

namespace HotelApp.HotelDtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public RoomDto Room { get; set; }
    }
}

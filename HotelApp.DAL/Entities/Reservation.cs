using System.ComponentModel.DataAnnotations;

namespace HotelApp.DAL.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }

}

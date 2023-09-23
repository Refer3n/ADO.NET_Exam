using System.ComponentModel.DataAnnotations;

namespace HotelApp.DAL.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

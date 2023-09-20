using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DAL.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string RoomNumber { get; set; }

        public string Class { get; set; }

        public decimal PricePerNight { get; set; }

        public bool Status { get; set; } 

        public string Description { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}

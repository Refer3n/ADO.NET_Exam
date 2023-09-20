using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [ForeignKey("Id")]
        public Customer Customer { get; set; }

        [ForeignKey("Id")]
        public Room Room { get; set; }

        public virtual Finance Finance { get; set; }
    }
}

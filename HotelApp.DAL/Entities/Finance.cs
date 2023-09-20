using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DAL.Entities
{
    public class Finance
    {
        [Key]
        public int Id { get; set; }

        public string TransactionDescription { get; set; }

        public DateTime TransactionDate { get; set; }

        [ForeignKey("Id")]
        public Reservation Reservation { get; set; }
    }
}
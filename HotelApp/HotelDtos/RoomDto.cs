using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.HotelDtos
{
    public class RoomDto
    {
        public string RoomNumber { get; set; }
        public string Class { get; set; }
        public decimal PricePerNight { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
    }
}

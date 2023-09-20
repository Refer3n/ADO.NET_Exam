using HotelApp.HotelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Interface
{
    public class RoomViewModel
    {
        public string RoomNumber { get; set; }
        public string Class { get; set; }
        public decimal PricePerNight { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }

        public RoomViewModel(RoomDto roomDto)
        {
            RoomNumber = roomDto.RoomNumber;
            Class = roomDto.Class;
            PricePerNight = roomDto.PricePerNight;
            Status = roomDto.Status;
            Description = roomDto.Description;
        }
    }

}

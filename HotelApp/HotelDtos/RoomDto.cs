namespace HotelApp.HotelDtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Class { get; set; }
        public decimal PricePerNight { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
    }
}

namespace Infohotel.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int MaxGuests { get; set; }
        public decimal PricePerNight { get; set; }
    }
}

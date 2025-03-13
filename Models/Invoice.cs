namespace DB_HotelBooking1.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public required Booking Booking { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
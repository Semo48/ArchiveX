namespace LibrarySystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsActive { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}

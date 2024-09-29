namespace LibrarySystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string ?Title { get; set; }
        public string ?Author { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }

        public Category ?Category { get; set; }
        public Publisher ?Publisher { get; set; }
        public ICollection<BorrowProcess> ?BorrowingTransactions { get; set; }
        public ICollection<Reservation> ?Reservations { get; set; }
        public ICollection<Review> ?Reviews { get; set; }
    }
}

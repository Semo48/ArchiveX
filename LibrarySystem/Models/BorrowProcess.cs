namespace LibrarySystem.Models
{
    public class BorrowProcess
    {
        public int BorrowProcessId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}

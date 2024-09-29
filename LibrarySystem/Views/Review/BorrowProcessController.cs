using LibrarySystem.DBContext;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Views.Review
{
    public class BorrowProcessController : Controller
    {
        private readonly AppDBContext _context;

        public BorrowProcessController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int userId)
        {
            var borrowedBooks = _context.BorrowProcess.Include(b => b.Book).Where(b => b.UserId == userId && !b.IsReturned).ToList();
            return View(borrowedBooks);
        }

        public IActionResult Borrow(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book != null && book.AvailableCopies > 0)
            {
                book.AvailableCopies--;
                _context.BorrowProcess.Add(new BorrowProcess { BookId = bookId, UserId = 1, BorrowDate = DateTime.Now, IsReturned = false }); // Example userId = 1
                _context.SaveChanges();
                return RedirectToAction("Index", new { userId = 1 }); // Example userId = 1
            }
            return View("Error"); // Handle the case where the book is not available
        }

        public IActionResult Return(int id)
        {
            var borrowProcess = _context.BorrowProcess.Find(id);
            if (borrowProcess != null)
            {
               // borrowProcess.IsReturned = true;
                borrowProcess.ReturnDate = DateTime.Now;

                var book = _context.Books.Find(borrowProcess.BookId);
                if (book != null)
                {
                    book.AvailableCopies++;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { userId = 1 }); // Example userId = 1
        }
    }
}

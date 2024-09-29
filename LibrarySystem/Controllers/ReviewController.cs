using LibrarySystem.DBContext;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDBContext _context;

        public ReviewController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int bookId)
        {
            var reviews = _context.Reviews.Include(r => r.User).Where(r => r.BookId == bookId).ToList();
            ViewBag.BookId = bookId;
            return View(reviews);
        }

        public IActionResult Add(int bookId)
        {
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction("Index", new { bookId = review.BookId });
            }
            return View(review);
        }
    }
}

using LibrarySystem.DBContext;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibrarySystem.Controllers
{
    public class BookController: Controller
    {

        private readonly AppDBContext _context;

        public BookController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            // If the model state is invalid, reload categories and publishers
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            // Send categories and publishers to the view
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var book = _context.Books.FirstOrDefault(b => b.BookId == id);

    if (book == null)
    {
        return NotFound();
    }

    return View(book);
}

[HttpPost, ActionName("DeleteConfirmed")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    var book = _context.Books.Find(id);

    if (book == null)
    {
        return NotFound();
    }

    _context.Books.Remove(book);
    _context.SaveChanges();
    
    return RedirectToAction(nameof(Index));
}
        public IActionResult Search(string query)
        {
            var result = _context.Books
                .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
                .ToList();
            return View("Index", result);
        }
    }
}

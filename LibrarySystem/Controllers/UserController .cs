using LibrarySystem.DBContext;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }

        // Registration Page (GET)
        public IActionResult Register()
        {
            return View();
        }

        // Registration (POST)
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Login Page (GET)
        public IActionResult Login()
        {
            return View();
        }

        // Login (POST)
        [HttpPost]
        public IActionResult Login(User loginModel)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            if (user != null)
            {
                // Store user in session or authenticate user
                return RedirectToAction("Profile", new { id = user.UserId });
            }
            ModelState.AddModelError("", "Invalid login credentials");
            return View();
        }

        // Profile (GET)
        public IActionResult Profile(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Edit Profile (GET)
        public IActionResult Edit(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Edit Profile (POST)
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Profile", new { id = user.UserId });
            }
            return View(user);
        }

        // List all users (Admin-only)
        [Authorize(Roles = "Admin")]
        public IActionResult ListUsers()
		{
			var users = _context.Users.ToList();
			return View(users);
		}

	}
}

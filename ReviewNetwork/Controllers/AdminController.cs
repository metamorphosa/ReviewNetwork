using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReviewNetwork.Data;
using ReviewNetwork.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNetwork.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private static string _userId;

        public AdminController(
            ILogger<AdminController> logger, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = context;
        }
        public async Task<IActionResult> Index()
        {
            AdminViewModel adminViewModel = new();
            adminViewModel.Users = await _db.Users.ToListAsync();
            return View(adminViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var selectList = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.SelectItems = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel reviewView)
        {
            reviewView.Review.ApplicationUser = await _db.Users.FindAsync(_userId);
            var tag = new Tag { Name = reviewView.Name };
            reviewView.Review.Tags.Add(tag);
            reviewView.Tag = tag;
            reviewView.Tag.Reviews.Add(reviewView.Review);
            _db.Reviews.Add(reviewView.Review);
            _db.Tags.Add(reviewView.Tag);
            await _db.SaveChangesAsync();
            return RedirectToAction("UserPage", "Admin", new { id = _userId });
        }

        public async Task<IActionResult> UserPage(string id)
        {
            _userId = id;
            AdminViewModel adminViewModel = new();
            adminViewModel.User = await _db.Users.FindAsync(id);
            return View(adminViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

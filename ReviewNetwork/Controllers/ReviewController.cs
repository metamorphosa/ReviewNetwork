using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReviewNetwork.Data;
using ReviewNetwork.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ReviewNetwork.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ILogger<ReviewController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
        {
            _logger = logger;
            _userManager = userManager;
            _db = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult Index()
        {
            SelectList selectList = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.SelectItems = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Review review)
        {
            review.ApplicationUser = await GetCurrentUserAsync();
            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Account/Manage/Review", new { area = "Identity" }); 
        }

        public async Task<IActionResult> Browse()
        {
            var currentUser = await GetCurrentUserAsync();
            var appUser = await _db.ApplicationUsers.Include(x => x.Reviews).SingleOrDefaultAsync(y => y == currentUser);
            return View(appUser.Reviews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

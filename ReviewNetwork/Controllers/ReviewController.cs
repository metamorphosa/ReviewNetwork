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

        public ReviewController(ILogger<ReviewController> logger, 
                                ApplicationDbContext context, 
                                UserManager<ApplicationUser> userManager) 
        {
            _logger = logger;
            _userManager = userManager;
            _db = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public IActionResult Index()
        {
            var selectList = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.SelectItems = selectList;
            return View();
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReviewViewModel reviewView)
        {
            reviewView.Review.ApplicationUser = await GetCurrentUserAsync();         
            var tag = new Tag { Name = reviewView.Name };
            reviewView.Review.Tags.Add(tag);
            reviewView.Tag = tag;
            reviewView.Tag.Reviews.Add(reviewView.Review);
            _db.Reviews.Add(reviewView.Review);
            _db.Tags.Add(reviewView.Tag);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Account/Manage/Review", new { area = "Identity" }); 
        }

        public IActionResult Detail(int id)
        {
            ReviewViewModel reviewView = new();
            reviewView.Review = _db.Reviews.Find(id);
            return View(reviewView);
        }
        
        public IActionResult Edit(int id)
        {
            ReviewViewModel reviewView = new();
            reviewView.Review = _db.Reviews.Include(x => x.ApplicationUser)
                                           .Where(x => x.ReviewId == id)
                                           .FirstOrDefault();
            var selectList = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.SelectItems = selectList;
            var tags = _db.Tags.Include(x => x.Reviews).Where(y=>y.Reviews.Contains(reviewView.Review)).ToList();
            reviewView.Review.Tags = tags;
            return View(reviewView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReviewViewModel reviewView)
        {
            var tag = new Tag { Name = reviewView.Name };
            reviewView.Review.Tags.Add(tag);
            reviewView.Tag = tag;
            reviewView.Tag.Reviews.Add(reviewView.Review);
            _db.Tags.Update(reviewView.Tag);
            _db.Reviews.Update(reviewView.Review);
            await _db.SaveChangesAsync();
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToPage("/Account/Manage/Review", new { area = "Identity" });
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var term = HttpContext.Request.Query["term"].ToString();
            var tags = await _db.Tags.Where(x => x.Name.Contains(term)).Select(x => x.Name).ToListAsync();
            return Ok(tags);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReviewNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewNetwork.Areas.Identity.Pages.Account.Manage
{
    public class ReviewModel : PageModel
    {
        private readonly ILogger<ReviewModel> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewModel(ILogger<ReviewModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = context;
            _userManager = userManager;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public IList<Review> Reviews { get; private set; }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> OnGetAsync(string sortOrder)
        {
            var currentUser = await GetCurrentUserAsync();
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            IQueryable<Review> review = _db.Reviews.Where(x => x.ApplicationUser == currentUser);
            switch (sortOrder)
            {
                case "name_desc":
                    review = review.OrderByDescending(x => x.Title);
                    break;
                case "date_asc":
                    review = review.OrderBy(x => x.CreateDate);
                    break;
                case "date_desc":
                    review = review.OrderByDescending(x => x.CreateDate);
                    break;
                default:
                    review = review.OrderBy(x => x.Title);
                    break;
            }
            Reviews = await review.AsNoTracking().ToListAsync();
            foreach (var item in Reviews)
            {
                item.Tags = _db.Tags.Where(x => x.Reviews.Contains(item)).ToList();
            }
            return Page();        
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var review = await _db.Reviews.FindAsync(id);
            if (review != null)
            {
                _db.Reviews.Remove(review);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}

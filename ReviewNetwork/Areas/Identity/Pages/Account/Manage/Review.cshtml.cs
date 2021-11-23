using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReviewNetwork.Data;
using System;
using System.Collections.Generic;
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
        public ICollection<Review> Reviews { get; private set; }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await GetCurrentUserAsync();
            var appUser = await _db.ApplicationUsers.Include(x => x.Reviews).SingleOrDefaultAsync(y => y == currentUser);
            foreach (var item in appUser.Reviews)
            {
                var review = await _db.Reviews.Include(x => x.Tags).SingleOrDefaultAsync(y => y == item);               
                item.Tags = review.Tags;
            }
            Reviews = appUser.Reviews;
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReviewNetwork.Data;
using ReviewNetwork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Korzh.EasyQuery.Linq;

namespace ReviewNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private static int _reviewId;


        public HomeController(ILogger<HomeController> logger, 
                                ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Index()
        {
            LikeViewModel likeViewModel = new();
            likeViewModel.Reviews = await _db.Reviews.ToListAsync();     
            return View(likeViewModel);
        }
        public async Task<IActionResult> Detail(int id)
        {
            _reviewId = id;
            LikeViewModel likeViewModel = new();
            likeViewModel.Review = await _db.Reviews.FindAsync(id);
            if (User.Identity.IsAuthenticated)
            {
                likeViewModel.CurrentUser = await GetCurrentUserAsync();
                var like = _db.Likes
                .Where(x => x.ReviewId == id && x.ApplicationUserId == likeViewModel.CurrentUser.Id)
                .FirstOrDefault();
                if (like == null)
                {
                    like = new()
                    {
                        ReviewId = id,
                        ApplicationUserId = likeViewModel.CurrentUser.Id,
                        IsLiked = false
                    };
                    await _db.Likes.AddAsync(like);
                    await _db.SaveChangesAsync();
                }
                likeViewModel.Like = await _db.Likes.FindAsync(like.LikeId);
            }
            return View(likeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(LikeViewModel likeViewModel)
        {
            ApplicationUser currentUser = await GetCurrentUserAsync();
            var like = _db.Likes
                .Where(x => x.ReviewId == _reviewId && x.ApplicationUserId == currentUser.Id)
                .FirstOrDefault();
            likeViewModel.Review = await _db.Reviews.FindAsync(_reviewId);
            likeViewModel.Like = await _db.Likes.FindAsync(like.LikeId);
            var currentLike = likeViewModel.Like;
            var review = likeViewModel.Review;
            if (currentLike.IsLiked == true)
            {
                currentLike.IsLiked = false;
                review.LikeCount--;
                _db.Likes.Update(currentLike);
                await _db.SaveChangesAsync();
            }
            else if (currentLike.IsLiked == false)
            {
                currentLike.IsLiked = true;
                review.LikeCount++;
                _db.Likes.Update(currentLike);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Detail", "Home");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)}
            );
            return LocalRedirect(returnUrl);
        }

        public IActionResult Search(string searchString)
        {
            SearchViewModel searchViewModel = new();
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                searchViewModel.Reviews = _db.Reviews.FullTextSearchQuery(searchString).ToList();
            }
            return View(searchViewModel);
        }

        public IActionResult Test()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

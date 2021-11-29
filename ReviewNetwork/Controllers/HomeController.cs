using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

namespace ReviewNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


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
            LikeViewModel likeViewModel = new();
            likeViewModel.CurrentUser = await GetCurrentUserAsync();
            likeViewModel.Review = await _db.Reviews.FindAsync(id);
            var like = _db.Likes
                .Where(x => x.ReviewId == id && x.ApplicationUserId == likeViewModel.CurrentUser.Id)
                .FirstOrDefault();       
            likeViewModel.Like = await _db.Likes.FindAsync(like.LikeId);
            return View(likeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(LikeViewModel likeViewModel)
        {
            ApplicationUser currentUser = await GetCurrentUserAsync();
            var review = await _db.Reviews.FindAsync(likeViewModel.ReviewId);
            var like = _db.Likes.Include(x => x.LikeId)
                .Where(y => y.ApplicationUserId == currentUser.Id && y.ReviewId == review.ReviewId);
            var currentLike = _db.Likes.Find(like);
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
            else
            {
                likeViewModel.Like.ReviewId = review.ReviewId;
                likeViewModel.Like.ApplicationUserId = currentUser.Id;
                likeViewModel.Like.IsLiked = true;
                review.LikeCount++;
                await _db.Likes.AddAsync(likeViewModel.Like);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
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

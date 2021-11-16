using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        private ApplicationDbContext _db;
        public ReviewController(ILogger<ReviewController> logger, ApplicationDbContext context) 
        {
            _logger = logger;
            _db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Categories.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

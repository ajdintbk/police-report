using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using police_alert.Data;
using police_alert.Domain;
using police_alert.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace police_alert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Report> model = _context.Reports.Include(i=>i.User).ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Add(string name, string message)
        {
            var u = _context.Users.FirstOrDefault(w => w.UserName == User.Identity.Name);
            Report r = new Report
            {
                User = u,
                Message = message,
                CreatedAt = DateTime.Now
            };
            _context.Reports.Add(r);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DeleteAll()
        {
            _context.Reports.RemoveRange(_context.Reports.ToList());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

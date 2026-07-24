using BloodBank.EF;
using BloodBank.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Controllers
{

    public class DonationController : Controller
    {
        BloodBankDbContext db;
        public DonationController(BloodBankDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Donors = db.Donors.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Donation donation)
        {
            ModelState.Remove("Donor");
            if (ModelState.IsValid)
            {
                db.Donations.Add(donation);
                var res = db.SaveChanges();
                if (res > 0)
                {
                    return RedirectToAction("List");
                }
            }
            ViewBag.Donors = db.Donors.ToList();
            return View(donation);
        }

        public IActionResult List()
        {
            var donations = db.Donations.Include(item => item.Donor).ToList();
            return View(donations);
        }

        public IActionResult TotalBloodVolume()
        {
            var totalVolume = db.Donations.Sum(item => item.VolumeMl);
            ViewBag.TotalVolume = totalVolume;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

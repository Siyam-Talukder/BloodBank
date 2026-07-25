using System;
using BloodBank.EF;
using BloodBank.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Controllers
{
    public class DonorController : Controller
    {
        BloodBankDbContext db;
        public DonorController(BloodBankDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Donor d)
        {
            if (ModelState.IsValid)
            {
                db.Donors.Add(d);
                var res = db.SaveChanges();
                if (res > 0)
                {
                    return RedirectToAction("List");
                }
            }
            return View(d);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var donor = db.Donors.Find(id);
            return View(donor);
        }
        
        [HttpPost]
        public IActionResult Edit(Donor d)
        {
            if (d.LastDonationDate >= DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("LastDonationDate", "The last donation date must be a past date.");
            }
            if (ModelState.IsValid)
            {
                db.Donors.Update(d);
                var res = db.SaveChanges();
                if (res > 0)
                {
                    return RedirectToAction("List");
                }
            }
            return View(d);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var donor = db.Donors.Find(id);
            return View(donor);
        }

        [HttpPost]
        public IActionResult Delete(Donor d)
        {
            var relatedDonations = (from item in db.Donations
                                    where item.DonorId == d.DonorId
                                    select item).ToList();

            if (relatedDonations.Count > 0)
            {
                db.Donations.RemoveRange(relatedDonations);
            }
            db.Donors.Remove(d);
            var res = db.SaveChanges();
            if (res > 0)
            {
                return RedirectToAction("List");
            }
            return View(d);
        }

        public IActionResult List()
        {
            var data = db.Donors.ToList();
            return View(data);
        }

        public IActionResult Filter(string bloodGroup)
        {
            ViewBag.SelectedGroup = bloodGroup;
            List<Donor> donors;

            if (string.IsNullOrEmpty(bloodGroup))
            {
                donors = (from item in db.Donors
                         select item ).ToList();
            }
            else
            {
                donors = (from item in db.Donors
                         where item.BloodGroup == bloodGroup
                         select item ).ToList();
            }
            return View(donors);
        }

        public IActionResult Sort()
        {
            var data = ( from item in db.Donors
                                orderby item.LastDonationDate descending
                                select item).ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult Count()
        {
            var donors = (from d in db.Donors.Include(item => item.Donations)
                          select d).ToList();

            return View(donors);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

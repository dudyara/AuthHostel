using AuthHostel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthHostel.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        Context db = new Context();

        public ActionResult Money()
        {
            return View(db.MoneyHistories);
        }

        public ActionResult History()
        {
            return View(db.Universals);
        }

        public ActionResult Journal()
        {
            return View(db.Journals);
        }

        public ActionResult Rooms()
        {
            return View(db.ActualRooms);
        }

        [HttpGet]
        public ActionResult EditJournal(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            CareJournal jour = db.CareJournals.Find(id);
            if (jour != null)
            {
                SelectList employs = new SelectList(db.Employs, "ID", "FIO");
                ViewBag.Employs = employs;
                SelectList animals = new SelectList(db.Animals, "ID", "Name");
                ViewBag.Animals = animals;
                SelectList cares = new SelectList(db.Cares, "ID", "Name");
                ViewBag.Cares = cares;
                return View(jour);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditJournal(CareJournal jour)
        {
            db.Entry(jour).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Journal");
        }

        [HttpGet]
        public ActionResult CreateJournal()
        {
            // Формируем список команд для передачи в представление
            SelectList employs  = new SelectList(db.Employs, "ID", "FIO");
            ViewBag.Employs = employs;
            SelectList animals = new SelectList(db.Animals, "ID", "Name");
            ViewBag.Animals = animals;
            SelectList cares = new SelectList(db.Cares, "ID", "Name");
            ViewBag.Cares = cares;
            DateTime time = DateTime.Now;
            ViewBag.Time = time;
            return View();
        }
        [HttpPost]
        public ActionResult CreateJournal(CareJournal jour)
        {
            db.CareJournals.Add(jour);
            db.SaveChanges();

            return RedirectToAction("Journal");
        }
        public ActionResult DeleteJournal(int id)
        {
            CareJournal b = new CareJournal { ID = id };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Journal");
        }
        public ActionResult DeleteConfirmed(int id)
        {
            CareJournal b = db.CareJournals.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.CareJournals.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Journal");
        }

    }
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
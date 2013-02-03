using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alf.Models;

namespace Alf.Controllers
{
    public class DanceClassController : Controller
    {
        private RegistrationContext db = new RegistrationContext();

        //
        // GET: /DanceClass/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        //
        // GET: /DanceClass/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            DanceClass danceclass = db.Classes.Find(id);
            ViewBag.Participants = db.Participants.Where(p => p.DanceClass.Id == id);
            if (danceclass == null)
            {
                return HttpNotFound();
            }
            return View(danceclass);
        }

        //
        // GET: /DanceClass/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DanceClass/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(DanceClass danceclass)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(danceclass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danceclass);
        }

        //
        // GET: /DanceClass/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            DanceClass danceclass = db.Classes.Find(id);
            if (danceclass == null)
            {
                return HttpNotFound();
            }
            return View(danceclass);
        }

        //
        // POST: /DanceClass/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(DanceClass danceclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danceclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danceclass);
        }

        //
        // GET: /DanceClass/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            DanceClass danceclass = db.Classes.Find(id);
            if (danceclass == null)
            {
                return HttpNotFound();
            }
            return View(danceclass);
        }

        //
        // POST: /DanceClass/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DanceClass danceclass = db.Classes.Find(id);
            db.Classes.Remove(danceclass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
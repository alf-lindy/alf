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
    public class ParticipantController : Controller
    {
        private RegistrationContext db = new RegistrationContext();

        //
        // GET: /Participant/

        public ActionResult Index()
        {
            return View(db.Participants.ToList());
        }

        //
        // GET: /Participant/Details/5

        public ActionResult Details(int id = 0)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        //
        // GET: /Participant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Participant/Create

        [HttpPost]
        public ActionResult Create(Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Participants.Add(participant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(participant);
        }

        //
        // GET: /Participant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        //
        // POST: /Participant/Edit/5

        [HttpPost]
        public ActionResult Edit(Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(participant);
        }

        //
        // GET: /Participant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        //
        // POST: /Participant/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Participant participant = db.Participants.Find(id);
            db.Participants.Remove(participant);
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
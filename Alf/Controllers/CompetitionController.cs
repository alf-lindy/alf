using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alf.Models;
using Alf.ViewModels;
using System.Web.Security;

namespace Alf.Controllers
{
    public class CompetitionController : Controller
    {
        private RegistrationContext db = new RegistrationContext();

        //
        // GET: /Competition/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Competitions.ToList());
        }

        //
        // GET: /Competition/SignUp/5
        public ActionResult SignUp(string id)
        {
            var term = Guid.Parse(id);
            var participant = db.Participants.First(p => p.Guid == term);
            var competitions = db.Competitions.ToList();

            int signup = Request.QueryString.HasKeys() ? int.Parse(Request.QueryString["Competition"]): 0;

            if (signup != 0)
            {
                db.Competitions.First(c => c.Id == signup).Participants.Add(participant);
                db.SaveChanges();    
            }

            var data = new Alf.ViewModels.CompetitionSignup { 
                Available = competitions.Except(participant.Competitions),
                SignedUp = participant.Competitions,// competitions.Where(c => c.Participants.Count > 0),
                Guid = term.ToString(),
                Username = participant.Name
            };


            /*Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }*/
            return View(data);
        }

        //
        // GET: /Competition/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        //
        // GET: /Competition/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Competition/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Competition competition)
        {
            if (ModelState.IsValid)
            {
                db.Competitions.Add(competition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(competition);
        }

        //
        // GET: /Competition/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        //
        // POST: /Competition/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Competition competition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(competition);
        }

        //
        // GET: /Competition/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Competition competition = db.Competitions.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        //
        // POST: /Competition/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Competition competition = db.Competitions.Find(id);
            db.Competitions.Remove(competition);
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
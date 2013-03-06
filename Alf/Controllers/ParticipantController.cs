using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alf.Models;
using AppHarbor.Web.Mvc;
using Services;
using Data;

namespace Alf.Controllers
{
    public class ParticipantController : Controller
    {
        private RegistrationContext db = new RegistrationContext();

        //
        // GET: /Participant/
        [Authorize]
        public ActionResult Index()
        {
            var ret = db.Participants.ToList();
            ret.ForEach(p => p.DanceClass = db.Classes.FirstOrDefault(c => c.Id == p.DanceClassId));
            return View(ret);
        }

        //
        // GET: /Participant/
        [Authorize]
        public ActionResult MailLists()
        {
            ViewBag.IkkeBetalt = db.Participants.Where(p => p.Paid == false).ToList();
            ViewBag.Betalt = db.Participants.Where(p => p.Paid == true).ToList();

            return View();
        }

        [Authorize]
        public ActionResult SendConfirmationMail()
        {
            db.Participants.ToList().ForEach(p => MailService.SendRegistrationConfirmed(p.Guid.ToString(), p.Mail));
            return RedirectToAction("Index");
        }

        //
        // GET: /Participant/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult SendMailTilIkkeBetalte()
        {
            var tekst = Request["mailtekst"] as string;
            var tittel = Request["tittel"] as string;
            var personer = db.Participants.Where(p => p.Paid == false).Select(p => new MailPerson { Adresse = p.Mail, Navn = p.Name, Id = p.Guid }).ToList();

            ViewBag.Status = MailService.SendMail(tittel, tekst, personer);

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult SendMailTilBetalte()
        {
            var tekst = Request["mailtekst"] as string;
            var tittel = Request["tittel"] as string;
            var personer = db.Participants.Where(p => p.Paid == true).Select(p => new MailPerson { Adresse = p.Mail, Navn = p.Name, Id = p.Guid }).ToList();

            ViewBag.Status = MailService.SendMail(tittel, tekst, personer);

            return View();
        }


        //
        // GET: /Participant/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.DanceClasses = db.Classes.ToList();
            return View();
        }

        //
        // POST: /Participant/Create

        protected override ITempDataProvider CreateTempDataProvider()
        {
            return new CookieTempDataProvider();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(Participant participant)
        {
            ViewBag.DanceClasses = db.Classes.ToList();
            var danceclassId = Convert.ToInt32(Request["danceclass"]);
            try
            {
                participant.DanceClass = db.Classes.Single(c => c.Id == danceclassId);

                participant.Paid = false; // Ensure noone hijacks this variable
                participant.Guid = Guid.NewGuid();

                if (ClassHasSpace(participant))
                {
                    participant.Status = ParticipantStatus.AwaitingPayment;
                    TempData["Message"] = "Successfully registered participant :) Please check your inbox";
                    MailService.SendRegistrationConfirmed(participant.Guid.ToString(), participant.Mail);
                }
                else
                {
                    participant.Status = ParticipantStatus.PutInWaitingList;
                    TempData["Message"] = "The selected class was full, you've been put in the waiting list.";
                }

                TempData["Message"] += "<p>Go <a href=\"/Competition/SignUp/" + participant.Guid.ToString()  + "\">here</a> to sign up for competitions</p>";

                db.Participants.Add(participant);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Message"] = "Something went wrong, did you fill the form correctly?";
            }

            return RedirectToAction("RegistryComplete");
        }

        private bool ClassHasSpace(Participant participant)
        {
            var registered = db.Participants.Count(p => p.DanceClass.Id == participant.DanceClass.Id);
            var limit = db.Classes.Single(c => c.Id == participant.DanceClass.Id).Limit;
            return registered < limit;
        }
        
        [AllowAnonymous]
        public ActionResult RegistryComplete()
        {
            return View();
        }

        //
        // GET: /Participant/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        [Authorize]
        public ActionResult RegisterPaid(int id = 0)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            participant.Paid = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Participant/Delete/5
        [Authorize]
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
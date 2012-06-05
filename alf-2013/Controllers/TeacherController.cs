using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using alf_2013.Models;

namespace alf_2013.Controllers
{ 
    public class TeacherController : Controller
    {
        private WorkshopContext db = new WorkshopContext();

        //
        // GET: /Teacher/

        public ViewResult Index()
        {
            return View(db.Teachers.ToList());
        }

        //
        // GET: /Teacher/Details/5

        public ViewResult Details(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            return View(teacher);
        }

        //
        // GET: /Teacher/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Teacher/Create

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(teacher);
        }
        
        //
        // GET: /Teacher/Edit/5
 
        public ActionResult Edit(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            return View(teacher);
        }

        //
        // POST: /Teacher/Edit/5

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        //
        // GET: /Teacher/Delete/5
 
        public ActionResult Delete(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            return View(teacher);
        }

        //
        // POST: /Teacher/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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
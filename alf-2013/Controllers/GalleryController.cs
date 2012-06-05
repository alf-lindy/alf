using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Management;
using System.IO;
using alf_services.Storage;

namespace alf_2013.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                ViewBag.Files = Storage.GetAll();
            } 
            catch (Exception error) 
            {
                new LogEvent(error);
                ViewBag.Files = new List<string>();
            }
            return View();
        }

        public ImageResult Picture(string filename)
        {
            ImageResult image = null;
            try
            {
                image = new ImageResult(Storage.Get(filename), "image/jpg");
            }
            catch (Exception error)
            {
                new LogEvent(error);
            }
            return image;
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && file.ContentLength < 100000)
            {
                var filename = Path.GetFileName(file.FileName);
                var stream = new MemoryStream();
             
                file.InputStream.CopyTo(stream);
                try
                {
                    Storage.Store(filename, stream);
                }
                catch (Exception error)
                {
                    new LogEvent(error);
                }
            }

            return RedirectToAction("Index");
        }

    }


}

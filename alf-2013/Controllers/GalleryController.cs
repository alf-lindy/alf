using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using alf_services.Storage;

namespace alf_2013.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            ViewBag.Files = Item.GetAll();
            return View();
        }

        public ImageResult Picture(string filename)
        {
            return new ImageResult(Item.Get(filename), "image/jpg");
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var stream = new MemoryStream();
             
                file.InputStream.CopyTo(stream);
                Item.Store(filename, stream);
            }

            return RedirectToAction("Index");
        }

    }

    public class ImageResult : ActionResult
    {
        public ImageResult(Stream imageStream, string contentType)
        {
            if (imageStream == null)
                throw new ArgumentNullException("imageStream");
            if (contentType == null)
                throw new ArgumentNullException("contentType");
     
           this.ImageStream = imageStream;
           this.ContentType = contentType;
       }
    
       public Stream ImageStream { get; private set; }
       public string ContentType { get; private set; }
    
       public override void ExecuteResult(ControllerContext context)
       {
           if (context == null)
               throw new ArgumentNullException("context");
    
           HttpResponseBase response = context.HttpContext.Response;
    
           response.ContentType = this.ContentType;
           
           byte[] buffer = new byte[4096];
           while (true)
           {
               int read = this.ImageStream.Read(buffer, 0, buffer.Length);
               if (read == 0)
                   break;
    
               response.OutputStream.Write(buffer, 0, read);
           }
    
           response.End();
       }
   }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.KendoUISample.ViewModels;

namespace RnD.KendoUISample.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }

        //Basic
        [HttpGet]
        public ViewResult Basic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Basic(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {

                var test = GetFileInfo(files);
                ViewBag.UploadedFiles = GetFileInfo(files);

            }

            return RedirectToAction("Basic");
        }

        [HttpGet]
        public ActionResult Async()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AsyncShow()
        {
            var pictureViewModel = new PictureViewModel();
            return View(pictureViewModel);
        }

        [HttpGet]
        public ActionResult AsyncShowSingle()
        {
            var pictureViewModel = new PictureViewModel();
            return View(pictureViewModel);
        }

        [HttpGet]
        public ActionResult OtherFile()
        {
            var pictureViewModel = new PictureViewModel();
            return View(pictureViewModel);
        }


        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            var pictureViewModel = new PictureViewModel();
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images"), fileName);

                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);

                    string pictureUrl = @"../../Images/" + fileName;

                    pictureViewModel.PictureId = 1;
                    pictureViewModel.PictureName = fileName;
                    pictureViewModel.PictureUrl = pictureUrl;
                    pictureViewModel.Status = true;
                }
            }

            // Return an empty string to signify success
            //return Content("");
            //return Content(Boolean.TrueString);
            //return Json(Boolean.TrueString, JsonRequestBehavior.AllowGet);
            //return Json(Boolean.TrueString, "text/plain");
            //return Json(pictureViewModel, "text/plain"); //ok
            return Json(pictureViewModel, JsonRequestBehavior.AllowGet); //ok
        }

        public ActionResult Remove(string[] fileNames)
        {
            var pictureViewModel = new PictureViewModel();

            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);

                        string pictureUrl = @"../../Content/Images/App/loading.gif";

                        pictureViewModel.PictureId = 1;
                        pictureViewModel.PictureName = fileName;
                        pictureViewModel.PictureUrl = pictureUrl;
                        pictureViewModel.Status = false;
                    }
                }
            }

            // Return an empty string to signify success
            //return Content("");
            //return Content(Boolean.TrueString);
            //return Json(Boolean.TrueString, JsonRequestBehavior.AllowGet);
            //return Json(Boolean.TrueString, "text/plain");
            //return Json(pictureViewModel, "text/plain"); //ok
            return Json(pictureViewModel, JsonRequestBehavior.AllowGet); //ok
        }

        public ActionResult AsyncSave(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images"), fileName);

                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult AsyncRemove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }
    }
}

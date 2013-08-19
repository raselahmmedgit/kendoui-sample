using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.KendoUISample.Models;
using RnD.KendoUISample.Helpers;
using RnD.KendoUISample.ViewModels;
using System.Data;

namespace RnD.KendoUISample.Controllers
{
    public class DemoController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Demo/

        public ActionResult Index()
        {
            return View();
        }

        #region CRUD Action For Custom PopUp

        //
        // GET: /Category/Details/By ID

        public ActionResult Details(int id)
        {
            Category category = _db.Categories.Find(id);
            //return View(category);
            return PartialView("_Details", category);
        }

        //
        // GET: /Category/Add

        public ActionResult Add()
        {
            //return View();
            return PartialView("_Add");
        }

        //
        // POST: /Category/Add

        [HttpPost]
        public ActionResult Add(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Categories.Add(category);
                    _db.SaveChanges();

                    //return RedirectToAction("Index");
                    return Content(Boolean.TrueString);
                }

                //return View(category);
                //return PartialView("_Add", category);
                return Content("Please review your form.");
            }
            catch (Exception ex)
            {
                return Content("Error Occured!");
            }
        }

        //
        // GET: /Category/Edit/By ID

        public ActionResult Edit(int id)
        {
            Category category = _db.Categories.Find(id);

            if (category != null)
            //if (category == null)
            {
                //return View(category);
                return PartialView("_Edit", category);
            }
            else
            {
                return PartialView("_Error");
            }

        }

        //
        // POST: /Category/Edit/By ID

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(category).State = EntityState.Modified;
                    _db.SaveChanges();

                    //return RedirectToAction("Index");
                    return Content(Boolean.TrueString);
                }

                //return View(category);
                //return PartialView("_Edit", category);
                return Content("Please review your form.");
            }
            catch (Exception ex)
            {
                return Content("Error Occured!");
            }
        }

        //
        // GET: /Category/Delete/By ID

        public ActionResult Delete(int id)
        {
            Category category = _db.Categories.Find(id);

            //return View(category);
            return PartialView("_Delete", category);
        }

        //
        // POST: /Category/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Category category = _db.Categories.Find(id);
                if (category != null)
                {
                    _db.Categories.Remove(category);
                    _db.SaveChanges();

                    //return RedirectToAction("Index");
                    return Content(Boolean.TrueString);
                }
                return Content("Please review your form.");
            }
            catch (Exception ex)
            {
                return Content("Error Occured!");
            }
        }

        #endregion
    }
}

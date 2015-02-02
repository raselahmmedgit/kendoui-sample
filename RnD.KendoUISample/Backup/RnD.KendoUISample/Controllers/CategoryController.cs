using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.KendoUISample.Helpers;
using RnD.KendoUISample.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Data.Linq;
using RnD.KendoUISample.ViewModels;

namespace RnD.KendoUISample.Controllers
{
    public class CategoryController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        public ViewResult Index()
        {
            //return View(GetCategorys());
            return View();
        }

        //Basic
        public ViewResult Basic()
        {
            //return View(GetCategorys());
            return View();
        }

        //InCell
        public ViewResult InCell()
        {
            //return View(GetCategorys());
            return View();
        }

        //PopUp
        public ViewResult PopUp()
        {
            //return View(GetCategorys());
            return View();
        }

        //CustomPopUp
        public ViewResult Dialog()
        {
            //return View(GetCategorys());
            return View();
        }

        //CustomPopUp By Kendo UI
        public ViewResult Window()
        {
            //return View(GetCategorys());
            return View();
        }

        //CustomPopUp By Kendo UI
        public ViewResult WindowLink()
        {
            //return View(GetCategorys());
            return View();
        }

        //CustomPopUp By Kendo UI
        public ViewResult WindowGlobal()
        {
            //return View(GetCategorys());
            return View();
        }

        //SelectSingleRow
        [HttpGet]
        public ViewResult SelectSingleItems()
        {
            //return View(GetCategorys());
            return View();
        }

        //SelectMultiRow
        [HttpGet]
        public ViewResult SelectMultiItems()
        {
            //return View(GetCategorys());
            return View();
        }

        //Toolbar
        public ViewResult Toolbar()
        {
            //return View(GetCategorys());
            return View();
        }

        //CascadingDropdownlist
        public ViewResult CascadingDropdownlist()
        {
            //return View(GetCategorys());
            return View();
        }

        //GridInnerDetailsListView
        public ViewResult GridInnerDetailsListView()
        {
            var result = new DataSourceResult();
            result = GetDataSourceResultCategorys();
            //return View(GetCategorys());
            return View(result.Data);
        }

        //ListViewInnerDetailsListView
        public ViewResult ListViewInnerDetailsListView()
        {
            var result = new DataSourceResult();
            result = GetDataSourceResultCategorys();
            //return View(GetCategorys());
            return View(result.Data);
        }

        //Get
        [HttpGet]
        public ActionResult SelectList()
        {
            return PartialView("_SelectList");
        }

        //Get
        [HttpGet]
        public ActionResult SelectSingleList()
        {
            return PartialView("_SelectSingleList");
        }

        //Get
        public ActionResult SelectListByModel(int id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.CategoryId == id);
            var products = _db.Products.Where(x => x.CategoryId == id);

            var categoryViewModel = new CategoryViewModel { CategoryId = category.CategoryId, Name = category.Name, Products = products };

            return PartialView("_SelectListByModel", categoryViewModel);
        }

        [HttpPost]
        public JsonResult MasterDetailsSave(Category model, List<Product> modelList)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (model != null)
                    {
                        if (modelList.Any())
                        {
                            //Save MasterDetails Data

                            //Save MasterDetails Data

                            return Json(new { msg = "Master details saved successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //Details Data Null Message
                            return Json(new { msg = "Details data could not found.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //Master Data Null Message
                        return Json(new { msg = "Master data could not found.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                }

                //ModelState Validation
                return Json(new { msg = ExceptionHelper.ModelStateErrorFormat(ModelState), status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex, log: false), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CategorysRead([DataSourceRequest] DataSourceRequest request)
        {
            var dataList = GetCategorys();

            var models = GetCategorys().ToDataSourceResult(request);

            return Json(models);
        }

        public ActionResult CategoryWithProductsRead([DataSourceRequest] DataSourceRequest request)
        {
            var dataList = GetCategoryWithProducts();

            DataSourceResult result = GetCategoryWithProducts().ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductRead([DataSourceRequest] DataSourceRequest request, int categoryId)
        {
            var dataList = GetProductsByCategoryId(categoryId);

            var models = GetProductsByCategoryId(categoryId).ToDataSourceResult(request);

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        #region Add Multipule Category List

        [HttpPost]
        public ActionResult AddList(IList<Category> categoryList)
        {
            try
            {
                foreach (var category in categoryList)
                {
                    //_db.Categories.Add(category);
                    //_db.SaveChanges();

                    //return RedirectToAction("Index");
                    //return Content(Boolean.TrueString);
                    return Json(Boolean.TrueString);
                }

                //return View(category);
                //return PartialView("_Add", category);
                //return Content("Please review your form.");
                return Json("Please review your form.");
            }
            catch (Exception ex)
            {
                //return Content("Error Occured!");
                return Json("Error Occured!");
            }
        }

        #endregion

        private IEnumerable<Category> GetCategorys()
        {
            var categories = _db.Categories.ToList();

            return categories;
        }

        private IEnumerable<LvCategoryViewModel> GetCategoryWithProducts()
        {
            var categories = _db.Categories.ToList().Select(c => new LvCategoryViewModel { CategoryId = c.CategoryId, CategoryName = c.Name, ProductViewModels = _db.Products.ToList().Where(x => x.CategoryId == c.CategoryId).Select(p => new LvProductViewModel { ProductId = p.ProductId, ProductName = p.Name, ProductPrice = p.Price, CategoryId = p.CategoryId, CategoryName = p.Category != null ? p.Category.Name : "" }) });

            return categories;
        }

        private IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _db.Products.ToList().Where(x => x.CategoryId == categoryId);

            return products;
        }

        private DataSourceResult GetDataSourceResultCategorys()
        {
            DataSourceResult result = new DataSourceResult();

            var categories = _db.Categories.ToList();

            result.Data = categories;

            return result;
        }

        public JsonResult GetCascadeCategories()
        {
            var categories = _db.Categories.ToList();

            return Json(categories.Select(c => new { CategoryId = c.CategoryId, CategoryName = c.Name }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeProducts(string categories)
        {
            var products = _db.Products.ToList();

            if (!string.IsNullOrEmpty(categories))
            {
                products = products.Where(p => p.CategoryId.ToString() == categories).ToList();
            }

            return Json(products.Select(p => new { ProductID = p.ProductId, ProductName = p.Name }), JsonRequestBehavior.AllowGet);
        }

        #region CRUD Action For InCell By List

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CategoryCreate([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Category> categorys)
        {
            var results = new List<Category>();

            if (categorys != null && ModelState.IsValid)
            {
                foreach (var category in categorys)
                {
                    _db.Categories.Add(category);
                    _db.SaveChanges();
                    results.Add(category);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CategoryUpdate([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Category> categorys)
        {
            if (categorys != null && ModelState.IsValid)
            {
                foreach (var category in categorys)
                {
                    var target = _db.Categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
                    if (target != null)
                    {
                        target.Name = category.Name;

                        _db.Entry(target).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CategoryDestroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Category> categorys)
        {
            if (categorys != null && categorys.Any())
            {
                foreach (var category in categorys)
                {
                    _db.Categories.Remove(category);
                    _db.SaveChanges();
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }


        #endregion

        #region CRUD Action For PopUp

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("PopUp");
            }

            return View("PopUp");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("PopUp");
            }

            return View("PopUp");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy(int categoryID)
        {
            Category category = _db.Categories.Find(categoryID);

            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("PopUp");
        }

        #endregion

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
        // GET: /Category/Create

        public ActionResult Create()
        {
            //return View();
            return PartialView("_Create");
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

            //return View(category);
            return PartialView("_Edit", category);
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

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
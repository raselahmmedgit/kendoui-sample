using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using RnD.KendoUISample.Helpers;
using RnD.KendoUISample.Models;
using RnD.KendoUISample.ViewModels;

namespace RnD.KendoUISample.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

        //AddToGrid
        public ViewResult AddToGrid()
        {
            return View();
        }

        public ActionResult ProductsRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetProducts().ToDataSourceResult(request));
        }

        public ActionResult ProductViewModelsRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetProductViewModels().ToDataSourceResult(request));
        }

        public ActionResult ClientProductViewModelsRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetClientProductViewModels().ToDataSourceResult(request));
        }

        private IEnumerable<Product> GetProducts()
        {
            var products = _db.Products.ToList();

            var models =
                products.Select(
                    x =>
                    new Product
                        {
                            ProductId = x.ProductId,
                            Name = x.Name,
                            Price = x.Price,
                            CategoryId = x.CategoryId,
                            Category = new Category { CategoryId = x.Category.CategoryId, Name = x.Category.Name }
                        }).ToList();

            return models;
        }

        private IEnumerable<ProductViewModel> GetProductViewModels()
        {
            //var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name").ToList();
            //var categories = _db.Categories.ToList().Select(x => new Category { CategoryId = x.CategoryId, Name = x.Name }).ToList();
            var productViewModels = _db.Products.Select(x => new ProductViewModel
                                                                 {
                                                                     ProductId = x.ProductId,
                                                                     Name = x.Name,
                                                                     Price = x.Price,
                                                                     CategoryId = x.CategoryId,
                                                                     CategoryName = x.Category != null ? x.Category.Name : null,
                                                                     //Categories = categories
                                                                 }).ToList();

            return productViewModels;
        }

        private IEnumerable<ClientProductViewModel> GetClientProductViewModels()
        {
            var clientProductViewModels = _db.Products.Select(x => new ClientProductViewModel { ProductId = x.ProductId, Name = x.Name, Price = x.Price, CategoryId = x.CategoryId, CategoryName = x.Category != null ? x.Category.Name : null, Category = new ClientCategoryViewModel { CategoryId = x.Category != null ? x.Category.CategoryId : 0, Name = x.Category != null ? x.Category.Name : null } }).ToList();

            return clientProductViewModels;
        }

        //GroundTotal
        [HttpGet]
        public ActionResult GroundTotal()
        {
            var productOrderViewModel = new ProductOrderViewModel();

            return View(productOrderViewModel);
        }

        //MasterDetails
        [HttpGet]
        public ActionResult MasterDetails()
        {
            var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name").ToList();

            var productViewModel = new ProductViewModel()
            {
                ddlCategories = categories
            };

            return View(productViewModel);
        }

        //EditMasterDetails
        [HttpGet]
        public ActionResult EditMasterDetails(int id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.CategoryId == id);

            var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name").ToList();

            if (category != null)
            {
                var products = _db.Products.Where(x => x.CategoryId == category.CategoryId).ToList();

                var productViewModel = new ProductViewModel()
                                           {
                                               CategoryId = category.CategoryId,
                                               CategoryName = category.Name,
                                               ddlCategories = categories,
                                               Products = products
                                           };

                return View(productViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }

        //DuplicateDetails
        [HttpGet]
        public ActionResult DuplicateDetails()
        {
            var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name").ToList();

            var productViewModel = new Product2ViewModel()
            {
                ddlCategories = categories
            };

            return View(productViewModel);
        }

        //[HttpPost]
        //public ActionResult MasterDetails(int id)
        //{
        //    return Content(Boolean.TrueString);
        //}

        //DataExport
        [HttpGet]
        public ActionResult DataExport()
        {
            return View();
        }

        public FileResult ExportToCsv([DataSourceRequest]DataSourceRequest request)
        {
            var products = _db.Products.ToDataSourceResult(request).Data;

            //Export To CSV 
            MemoryStream output = new MemoryStream();
            StreamWriter writer = new StreamWriter(output, Encoding.UTF8);

            writer.Write("ProductId,");
            writer.Write("ProductName,");
            writer.Write("ProductPrice,");
            writer.Write("CategoryId,");
            writer.Write("CategoryName");
            writer.WriteLine();
            foreach (Product product in products)
            {
                writer.Write(product.ProductId);
                writer.Write(",");
                writer.Write("\"");
                writer.Write(product.Name);
                writer.Write("\"");
                writer.Write(",");
                writer.Write("\"");
                writer.Write(product.Price);
                writer.Write("\"");
                writer.Write(",");
                writer.Write(product.Category != null ? product.CategoryId : 0);
                writer.Write(",");
                writer.Write("\"");
                if (product.Category != null) writer.Write(product.Category != null ? product.Category.Name : string.Empty);
                writer.Write("\"");
                writer.WriteLine();
            }
            writer.Flush();
            output.Position = 0;

            return File(output, "text/comma-separated-values", "Products.csv");
        }

        public FileResult ExportToXls([DataSourceRequest]DataSourceRequest request)
        {
            //Get the data representing the current grid state - page, sort and filter
            IEnumerable products = _db.Products.ToDataSourceResult(request).Data;

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 50 * 256);
            sheet.SetColumnWidth(2, 50 * 256);
            sheet.SetColumnWidth(3, 50 * 256);
            sheet.SetColumnWidth(4, 50 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("ProductId");
            headerRow.CreateCell(1).SetCellValue("ProductName");
            headerRow.CreateCell(2).SetCellValue("ProductPrice");
            headerRow.CreateCell(3).SetCellValue("CategoryId");
            headerRow.CreateCell(4).SetCellValue("CategoryName");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (Product product in products)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(product.ProductId);
                row.CreateCell(1).SetCellValue(product.Name);
                row.CreateCell(2).SetCellValue(product.Price.ToString());
                if (product.Category != null)
                    row.CreateCell(3).SetCellValue(product.Category != null ? product.CategoryId : 0);
                if (product.Category != null)
                    row.CreateCell(4).SetCellValue(product.Category != null ? product.Category.Name : string.Empty);
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "Products.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user

        }

        [HttpPost]
        public ActionResult ExportToCsvOnClick([DataSourceRequest]DataSourceRequest request)
        {
            var products = _db.Products.ToDataSourceResult(request).Data;

            //Export To CSV 
            MemoryStream output = new MemoryStream();
            StreamWriter writer = new StreamWriter(output, Encoding.UTF8);

            writer.Write("ProductId,");
            writer.Write("ProductName,");
            writer.Write("ProductPrice,");
            writer.Write("CategoryId,");
            writer.Write("CategoryName");
            writer.WriteLine();
            foreach (Product product in products)
            {
                writer.Write(product.ProductId);
                writer.Write(",");
                writer.Write("\"");
                writer.Write(product.Name);
                writer.Write("\"");
                writer.Write(",");
                writer.Write("\"");
                writer.Write(product.Price);
                writer.Write("\"");
                writer.Write(",");
                writer.Write(product.Category != null ? product.CategoryId : 0);
                writer.Write(",");
                writer.Write("\"");
                if (product.Category != null) writer.Write(product.Category != null ? product.Category.Name : string.Empty);
                writer.Write("\"");
                writer.WriteLine();
            }
            writer.Flush();
            output.Position = 0;

            File(output, "text/comma-separated-values", "Products.csv");

            return Json(new { msg = "Export to CSV successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportToXlsOnClick([DataSourceRequest]DataSourceRequest request)
        {
            //Get the data representing the current grid state - page, sort and filter
            IEnumerable products = _db.Products.ToDataSourceResult(request).Data;

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 50 * 256);
            sheet.SetColumnWidth(2, 50 * 256);
            sheet.SetColumnWidth(3, 50 * 256);
            sheet.SetColumnWidth(4, 50 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("ProductId");
            headerRow.CreateCell(1).SetCellValue("ProductName");
            headerRow.CreateCell(2).SetCellValue("ProductPrice");
            headerRow.CreateCell(3).SetCellValue("CategoryId");
            headerRow.CreateCell(4).SetCellValue("CategoryName");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (Product product in products)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(product.ProductId);
                row.CreateCell(1).SetCellValue(product.Name);
                row.CreateCell(2).SetCellValue(product.Price.ToString());
                if (product.Category != null)
                    row.CreateCell(3).SetCellValue(product.Category != null ? product.CategoryId : 0);
                if (product.Category != null)
                    row.CreateCell(4).SetCellValue(product.Category != null ? product.Category.Name : string.Empty);
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "Products.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            return Json(new { msg = "Export to EXCEL successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);

        }

        //DataImport
        [HttpGet]
        public ActionResult DataImport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ImportFromCsv()
        {
            return PartialView("_ImportFromCsv");
        }

        [HttpPost]
        //public ActionResult ImportFromCsv(HttpPostedFileBase ImportCsv)
        public ActionResult ImportFromCsv(ImportFileViewModel model)
        {
            try
            {
                if (model.ImportFile != null)
                {
                    var file = model.ImportFile;

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, file.ContentLength);
                        //do stuff with the bytes
                        string fileName = file.FileName;
                        string filePath = Path.Combine(Request.PhysicalApplicationPath, "Files\\", fileName);

                        System.IO.File.WriteAllBytes(filePath, fileBytes);

                        //File Uploaded
                        //using (var reader = new StreamReader(ImportCsv.InputStream))
                        //using (var reader = new StreamReader(model.ImportFile.InputStream))
                        using (var reader = new StreamReader(filePath))
                        using (var csvReader = new CsvReader(reader))
                        {
                            csvReader.Read();
                            //Read CSV File
                            var products = csvReader.GetRecords<ProductCsvModel>().ToList();

                        }

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        return RedirectToAction("DataImport", "Product");
                        //return Content(Boolean.TrueString);
                        //return Json(new { msg = "Product data uploaded successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        //return Content("Sorry! Could not found this file.");
                        return RedirectToAction("DataImport", "Product");
                    }

                }
                else
                {
                    //Upload file Null Message
                    return RedirectToAction("DataImport", "Product");
                    //return Content("Upload file could not found.");
                    //return Json(new { msg = "Upload file could not found.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("DataImport", "Product");
                //return Content("Oop! Error.");
                //return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex, log: false), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ImportFromXls()
        {
            return PartialView("_ImportFromXls");
        }

        [HttpPost]
        //public ActionResult ImportFromXls(HttpPostedFileBase ImportExcel)
        public ActionResult ImportFromXls(ImportFileViewModel model)
        {
            try
            {
                if (model.ImportFile != null)
                {
                    var file = model.ImportFile;

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, file.ContentLength);
                        //do stuff with the bytes
                        string fileName = file.FileName;
                        string filePath = Path.Combine(Request.PhysicalApplicationPath, "Files\\", fileName);

                        System.IO.File.WriteAllBytes(filePath, fileBytes);

                        //File Uploaded
                        HSSFWorkbook hssfWorkbook;

                        string filefullpath = filePath;

                        //StreamReader streamReader = new StreamReader(model.ImportFile.InputStream);

                        using (FileStream fileStream = new FileStream(filefullpath, FileMode.Open, FileAccess.Read))
                        {
                            hssfWorkbook = new HSSFWorkbook(fileStream);
                            //hssfWorkbook = new HSSFWorkbook();
                        }

                        var products = new List<ProductXlsModel>();

                        //the columns
                        var properties = new string[] { "ProductId", "ProductName", "ProductPrice", "CategoryId", "CategoryName" };

                        ISheet sheet = hssfWorkbook.GetSheet("Products");
                        for (int row = 1; row <= sheet.LastRowNum; row++)
                        {
                            if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                            {

                                int productId = Convert.ToInt32(sheet.GetRow(row).GetCell(GetColumnIndex(properties, "ProductId")).NumericCellValue);

                                string productName = sheet.GetRow(row).GetCell(GetColumnIndex(properties, "ProductName")).StringCellValue;
                                decimal productPrice = Convert.ToDecimal(sheet.GetRow(row).GetCell(GetColumnIndex(properties, "ProductPrice")).StringCellValue);
                                int categoryId = Convert.ToInt32(sheet.GetRow(row).GetCell(GetColumnIndex(properties, "CategoryId")).NumericCellValue);
                                string categoryName = sheet.GetRow(row).GetCell(GetColumnIndex(properties, "CategoryName")).StringCellValue;

                                var product = new ProductXlsModel { ProductId = productId, ProductName = productName, ProductPrice = productPrice, CategoryId = categoryId, CategoryName = categoryName };

                                products.Add(product);

                            }
                        }

                        if (System.IO.File.Exists(filefullpath))
                        {
                            System.IO.File.Delete(filefullpath);
                        }

                        return RedirectToAction("DataImport", "Product");
                        //return Content(Boolean.TrueString);
                        //return Json(new { msg = "Product data uploaded successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //return Content("Sorry! Could not found this file.");
                        return RedirectToAction("DataImport", "Product");
                    }

                }
                else
                {
                    //Upload file Null Message
                    return RedirectToAction("DataImport", "Product");
                    //return Content("Upload file could not found.");
                    //return Json(new { msg = "Upload file could not found.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("DataImport", "Product");
                //return Content("Oop! Error.");
                //return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex, log: false), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        protected virtual int GetColumnIndex(string[] properties, string columnName)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");

            if (columnName == null)
                throw new ArgumentNullException("columnName");

            for (int i = 0; i < properties.Length; i++)
                if (properties[i].Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    //return i + 1; //excel indexes start from 1
                    return i; //excel indexes start from 0
            return 0;
        }

        //CellCalculate
        [HttpGet]
        public ActionResult CellCalculate(int id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.CategoryId == id);
            var productViewModels = _db.Products.Where(x => x.CategoryId == id).Select(x => new ProductViewModel { ProductId = x.ProductId, Name = x.Name, Price = x.Price, Quantity = 1, Total = x.Price * 1, CategoryId = x.CategoryId });

            var categoryViewModel = new CategoryViewModel { CategoryId = category != null ? category.CategoryId : 0, Name = category != null ? category.Name : "", ProductViewModels = productViewModels };

            return View(categoryViewModel);
        }

        [HttpPost]
        public JsonResult CellCalculate(List<ProductViewModel> productViewModelList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (productViewModelList.Any())
                    {
                        //Save MasterDetails Data

                        return Json(new { msg = "Master details saved successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //Details Data Null Message
                        return Json(new { msg = "Details data could not found.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
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

        //RowCustom
        [HttpGet]
        public ActionResult RowCustom()
        {
            //var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name").ToList();
            //var categories = _db.Categories.ToList().Select(x => new Category { CategoryId = x.CategoryId, Name = x.Name }).ToList();
            //var categories = _db.Categories.ToList().Select(x => new Category { CategoryId = x.CategoryId, Name = x.Name });
            var categories = _db.Categories.Select(x => new CategoryViewModelNew { CategoryId = x.CategoryId, CategoryName = x.Name });
            //var categories = _db.Categories.ToList().Select(x => new ClientCategoryViewModel { CategoryId = x.CategoryId, Name = x.Name }).ToList();
            ViewData["categories"] = categories;
            //ViewBag.Categories = categories;
            return View();
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

        //Add Product
        public JsonResult Add(ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(Boolean.TrueString, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = ExceptionHelper.ModelStateErrorFormat(ModelState), status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex, log: false), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Product/Details/by ID

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            //var categories = _db.Categories.ToList<Category>().PopulateDropdownList("CategoryId", "Name").ToList();
            var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name").ToList();
            //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");

            var model = new ProductViewModel()
            {
                ddlCategories = categories
            };

            //return View();
            //return PartialView("_Create");
            //return View("_Create");
            return View("_Create", model);
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public JsonResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Products.Add(product);
                    _db.SaveChanges();

                    //return RedirectToAction("Index");
                    //return Content(Boolean.TrueString);
                    //return Json("Success", JsonRequestBehavior.AllowGet);
                    return Json(new { msg = "Product saved successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                }

                //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);

                //return View(product);
                //return View("_Create", product);
                //return Content("Please review your form.");
                //return Json("Success", JsonRequestBehavior.AllowGet);

                return Json(new { msg = ExceptionHelper.ModelStateErrorFormat(ModelState), status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //return Content("Error Occured!");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex, log: true), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Product/Edit/by ID

        public ActionResult Edit(int id)
        {
            Product product = _db.Products.Find(id);

            var categories = _db.Categories.ToList<Category>().PopulateDropdownList("CategoryId", "Name", isEdit: true).ToList();
            //var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name", isEdit: true).ToList();
            //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);

            var model = new ProductViewModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ddlCategories = categories
            };

            //return View(product);
            //return PartialView("_Edit", product);
            //return View("_Edit", product);
            return View("_Edit", model);
        }

        //
        // POST: /Product/Edit/By ID

        [HttpPost]
        public JsonResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _db.Products.Find(product.ProductId);

                    if (model != null)
                    {
                        model.Name = product.Name;
                        model.Price = product.Price;
                        model.CategoryId = product.CategoryId;

                        _db.Entry(model).State = EntityState.Modified;
                        _db.SaveChanges();

                        //return RedirectToAction("Index");
                        //return Content(Boolean.TrueString);
                        //return Json("Success", JsonRequestBehavior.AllowGet);
                        return Json(new { msg = "Product updated successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { msg = "Product is null.", status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);

                }

                //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);

                //return View(product);
                //return View("_Edit", product);
                //return Content("Please review your form.");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ModelStateErrorFormat(ModelState), status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Content("Error Occured!");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Product/Delete/By ID

        public ActionResult Delete(int id)
        {
            Product product = _db.Products.Find(id);

            var model = new ProductViewModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            //return View(product);
            //return PartialView("_Delete", product);
            //return View("_Delete", product);
            return View("_Delete", model);
        }

        //
        // POST: /Product/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = _db.Products.Find(id);
                if (product != null)
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();

                    //return RedirectToAction("Index");
                    //return Content(Boolean.TrueString);
                    //return Json("Success", JsonRequestBehavior.AllowGet);
                    return Json(new { msg = "Product deleted successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = "Product is null.", status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Content("Error Occured!");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.KendoUISample.Models;

namespace RnD.KendoUISample.ViewModels
{
    public class ProductViewModel : BaseModel
    {
        [Key]
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }
        [DisplayName("Product Price")]
        [Required(ErrorMessage = "Product Price is required.")]
        public float Price { get; set; }
        public float Quantity { get; set; }
        public float Total { get; set; }
        [Required(ErrorMessage = "Select one category.")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<SelectListItem> ddlCategories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class ProductCsvModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProductXlsModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string CategoryName { get; set; }
    }

    public class ImportFileViewModel
    {
        [DisplayName("Upload File")]
        [Required(ErrorMessage = "File is required.")]
        public HttpPostedFileBase ImportFile { get; set; }
    }

    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public IEnumerable<SelectListItem> ddlCategories { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
        public virtual IEnumerable<ProductViewModel> ProductViewModels { get; set; }
    }

    public class PictureViewModel
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public string PictureUrl { get; set; }
        public bool Status { get; set; }
    }
}
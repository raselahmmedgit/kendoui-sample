using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SelectListItem> ddlCategories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class Product2ViewModel : BaseModel
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

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SelectListItem> ddlCategories { get; set; }
        public IEnumerable<ProductViewModel> ProductViewModels { get; set; }
    }

    public class ProductOrderViewModel : BaseModel
    {
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float AITRate { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float VATRate { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float AITRateTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float VATRateTotal { get; set; }

        [Key]
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }
        [DisplayName("Product Price")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Product Price is required.")]
        public float Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float Total { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float GrossTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float GroundTotal { get; set; }
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
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class ImportFileViewModel
    {
        [DisplayName("Upload File")]
        [Required(ErrorMessage = "File is required.")]
        public HttpPostedFileBase ImportFile { get; set; }
    }

    public class LvCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual IEnumerable<LvProductViewModel> ProductViewModels { get; set; }
    }

    public class LvProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }

    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public IEnumerable<SelectListItem> ddlCategories { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
        public virtual IEnumerable<ProductViewModel> ProductViewModels { get; set; }
    }

    public class CategoryViewModelNew
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class PictureViewModel
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public string PictureUrl { get; set; }
        public bool Status { get; set; }
    }

    public class ClientProductViewModel : BaseModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public float Total { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [UIHint("CategoryList"), Required]
        public ClientCategoryViewModel Category { get; set; }
    }

    public class ClientCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class MenuItemViewModel : BaseModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuText { get; set; }
        public string MenuImageUrl { get; set; }
        public string MenuUrl { get; set; }
        public string MenuSpriteCssClass { get; set; }
        public string MenuActionName { get; set; }
        public string MenuUrlTarget { get; set; }

        public IEnumerable<SubMenuItemViewModel> SubMenuItemViewModels { get; set; }
    }

    public class SubMenuItemViewModel : BaseModel
    {
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuText { get; set; }
        public string SubMenuImageUrl { get; set; }
        public string SubMenuUrl { get; set; }
        public string SubMenuSpriteCssClass { get; set; }
        public string SubMenuActionName { get; set; }
        public string SubMenuUrlTarget { get; set; }

        public int MenuId { get; set; }
        public MenuItem MenuItem { get; set; }
    }

    public class MainMenuViewModel : BaseModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuText { get; set; }
        public string MenuImageUrl { get; set; }
        public string MenuUrl { get; set; }
        public string MenuSpriteCssClass { get; set; }
        public string MenuActionName { get; set; }
        public string MenuUrlTarget { get; set; }

        public IEnumerable<MainMenuViewModel> SubMenuViewModels { get; set; }
    }

    public class MenuViewModel : BaseModel
    {
        public MenuViewModel()
        {
            SubMenuViewModels = new List<MenuViewModel>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuText { get; set; }
        public string MenuImageUrl { get; set; }
        public string MenuUrl { get; set; }
        public string MenuSpriteCssClass { get; set; }
        public string MenuUrlTarget { get; set; }
        public int MenuSortOrder { get; set; }
        public bool IsSelected { get; set; }

        public int ParentMenuId { get; set; }
        public virtual MenuViewModel ParentMenuViewModel { get; set; }

        //public IEnumerable<MenuViewModel> SubMenuViewModels { get; set; }
        public List<MenuViewModel> SubMenuViewModels { get; set; }

    }

    public class JsMenuViewModel
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public IEnumerable<JsMenuItemViewModel> JsMenuItemViewModels { get; set; }
    }

    public class JsMenuItemViewModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class dsMenuViewModel
    {
        public string text { get; set; }
        public string value { get; set; }

        public IEnumerable<dsMenuViewModel> items { get; set; }
    }

}
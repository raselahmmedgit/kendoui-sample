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
}
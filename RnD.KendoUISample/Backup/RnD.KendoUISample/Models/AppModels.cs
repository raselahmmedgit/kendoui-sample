using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using RnD.KendoUISample.Helpers;

namespace RnD.KendoUISample.Models
{
    public class BaseModel
    {
        [NotMapped]
        public virtual int TempId { get; set; }

        [NotMapped]
        public virtual string KendoWindow { get; set; }

        [NotMapped]
        public virtual string ActionLink { get; set; }

        [NotMapped]
        public virtual bool HasCreate { get; set; }

        [NotMapped]
        public virtual bool HasUpdate { get; set; }

        [NotMapped]
        public virtual bool HasDelete { get; set; }
    }

    public class Category : BaseModel
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }
    }

    public class Product : BaseModel
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

        [Required(ErrorMessage = "Select one category.")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public virtual Category Category { get; set; }
    }

    public class Logger
    {
        [Key]
        public int LoggerId { get; set; }

        [MaxLength(200)]
        public string Summery { get; set; }

        [MaxLength(200)]
        public string Details { get; set; }

        [MaxLength(200)]
        public string FilePath { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        public int LoggerTypeId { get; set; }
        [ForeignKey("LoggerTypeId")]
        public virtual LoggerType LoggerType { get; set; }
    }

    public class LoggerType
    {
        [Key]
        public int LoggerTypeId { get; set; }

        [MaxLength(200)]
        public string LoggerTypeName { get; set; }
    }

    public class MenuItem : BaseModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuText { get; set; }
        public string MenuImageUrl { get; set; }
        public string MenuUrl { get; set; }
        public string MenuSpriteCssClass { get; set; }
        public string MenuUrlTarget { get; set; }
    }

    public class SubMenuItem : BaseModel
    {
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuText { get; set; }
        public string SubMenuImageUrl { get; set; }
        public string SubMenuUrl { get; set; }
        public string SubMenuSpriteCssClass { get; set; }
        public string SubMenuUrlTarget { get; set; }

        public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual MenuItem MenuItem { get; set; }
    }

    public class Menu : BaseModel
    {
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
        //[ForeignKey("ParentMenuId")]
        public virtual Menu ParentMenu { get; set; }

    }

    public class CustomMenu : BaseModel
    {
        [Key]
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
        //[ForeignKey("ParentMenuId")]
        public virtual CustomMenu ParentMenu { get; set; }

        public virtual IEnumerable<CustomMenu> CustomMenus { get; set; }

    }

    public class ApplicationEntity
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }

        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "End Date must be greater than Start Date")]
        public DateTime EndDate { get; set; }

        [BirthDate(ErrorMessage = "Birth date must be less then today")]
        public DateTime DateOfBirth { get; set; }

        [LessDate(ErrorMessage = "Date must be less then today")]
        public DateTime PreviousDate { get; set; }

        [GraterDate(ErrorMessage = "Date must be grater then today")]
        public DateTime NextDate { get; set; }
    }
}
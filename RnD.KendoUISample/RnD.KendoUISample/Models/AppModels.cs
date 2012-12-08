using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace RnD.KendoUISample.Models
{
    public class BaseModel
    {
        [NotMapped]
        public virtual bool KendoWindow { get; set; }
    }

    public class Category : BaseModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class Product : BaseModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
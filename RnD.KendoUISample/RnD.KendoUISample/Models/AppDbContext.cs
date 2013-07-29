using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RnD.KendoUISample.Models
{
    public class AppDbContext : DbContext
    {
            
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<CustomMenu> CustomMenus { get; set; }

        //For Logger
        public DbSet<Logger> Loggers { get; set; }
        public DbSet<LoggerType> LoggerTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }

    #region Initial data

    // Change the base class as follows if you want to drop and create the database during development:
    //public class DBInitializer : DropCreateDatabaseAlways<AppDbContext>
    //public class DBInitializer : CreateDatabaseIfNotExists<AppDbContext>
    public class DBInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Create default categories.
            var categories = new List<Category>
                            {
                                new Category { CategoryId=1, Name = "Fruit"},
                                new Category {CategoryId=2, Name = "Cloth"},
                                new Category {CategoryId=3, Name = "Car"},
                                new Category {CategoryId=4, Name = "Book"},
                                new Category {CategoryId=5, Name = "Shoe"},
                                new Category {CategoryId=6, Name = "Wiper"},
                                new Category {CategoryId=7, Name = "Laptop"},
                                new Category {CategoryId=8, Name = "Desktop"},
                                new Category {CategoryId=9, Name = "Notebook"},
                                new Category {CategoryId=10, Name = "AC"}

                            };

            categories.ForEach(c => context.Categories.Add(c));

            context.SaveChanges();

            // Create some products.
            var products = new List<Product> 
                        {
                            new Product {ProductId=1, Name="Apple", Price=15, CategoryId=1},
                            new Product {ProductId=2, Name="Mango", Price=20, CategoryId=1},
                            new Product {ProductId=3, Name="Orange", Price=15, CategoryId=1},
                            new Product {ProductId=4, Name="Banana", Price=20, CategoryId=1},
                            new Product {ProductId=5, Name="Licho", Price=15, CategoryId=1},
                            new Product {ProductId=6, Name="Jack Fruit", Price=20, CategoryId=1},

                            new Product {ProductId=7, Name="Toyota", Price=15000, CategoryId=2},
                            new Product {ProductId=8, Name="Nissan", Price=20000, CategoryId=2},
                            new Product {ProductId=9, Name="Tata", Price=50000, CategoryId=2},
                            new Product {ProductId=10, Name="Honda", Price=20000, CategoryId=2},
                            new Product {ProductId=11, Name="Kimi", Price=50000, CategoryId=2},
                            new Product {ProductId=12, Name="Suzuki", Price=20000, CategoryId=2},
                            new Product {ProductId=13, Name="Ferrari", Price=50000, CategoryId=2},

                            new Product {ProductId=14, Name="T Shirt", Price=20000, CategoryId=3},
                            new Product {ProductId=15, Name="Polo Shirt", Price=50000, CategoryId=3},
                            new Product {ProductId=16, Name="Shirt", Price=200, CategoryId=3},
                            new Product {ProductId=17, Name="Panjabi", Price=500, CategoryId=3},
                            new Product {ProductId=18, Name="Fotuya", Price=200, CategoryId=3},
                            new Product {ProductId=19, Name="Shari", Price=500, CategoryId=3},
                            new Product {ProductId=19, Name="Kamij", Price=400, CategoryId=3},

                            new Product {ProductId=20, Name="History", Price=20000, CategoryId=4},
                            new Product {ProductId=21, Name="Fire Out", Price=50000, CategoryId=4},
                            new Product {ProductId=22, Name="Apex", Price=200, CategoryId=5},
                            new Product {ProductId=23, Name="Bata", Price=500, CategoryId=5},
                            new Product {ProductId=24, Name="Acer", Price=200, CategoryId=8},
                            new Product {ProductId=25, Name="Dell", Price=500, CategoryId=8},
                            new Product {ProductId=26, Name="HP", Price=400, CategoryId=8}

                        };

            products.ForEach(p => context.Products.Add(p));

            context.SaveChanges();

            // Create some Menus.
            var menus = new List<Menu> 
                        {
                            new Menu {MenuId=1, MenuName="Home", MenuText="Home", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 1},
                            new Menu {MenuId=2, MenuName="Admin", MenuText="Admin", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 2}, //Admin
                            new Menu {MenuId=3, MenuName="Account", MenuText="Account", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 3}, //Account
                            new Menu {MenuId=4, MenuName="Sales", MenuText="Sales", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 4}, //Sales
                            new Menu {MenuId=5, MenuName="Settings", MenuText="Settings", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 5, ParentMenuId = 3}, //Settings
                            new Menu {MenuId=6, MenuName="Add Settings", MenuText="Add Settings", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 6, ParentMenuId = 5}, //Add Settings
                            new Menu {MenuId=7, MenuName="Edit Settings", MenuText="Edit Settings", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 7, ParentMenuId = 5}, //Edit Settings
                            new Menu {MenuId=8, MenuName="Product", MenuText="Product", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 8, ParentMenuId = 4}, //Product
                            new Menu {MenuId=9, MenuName="Add Product", MenuText="Add Product", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 9, ParentMenuId = 8}, //Add Product
                            new Menu {MenuId=10, MenuName="Edit Product", MenuText="Edit Product", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 10, ParentMenuId = 8}, //Edit Product
                            new Menu {MenuId=11, MenuName="Order", MenuText="Order", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 11, ParentMenuId = 4}, //Order
                            new Menu {MenuId=12, MenuName="Add Order", MenuText="Add Order", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 12, ParentMenuId = 11}, //Add Order
                            new Menu {MenuId=13, MenuName="Edit Order", MenuText="Edit Order", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 13, ParentMenuId = 11}, //Edit Order
                            new Menu {MenuId=14, MenuName="Task", MenuText="Task", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 14, ParentMenuId = 2}, //task
                            new Menu {MenuId=15, MenuName="Schedule", MenuText="Schedule", MenuImageUrl="/Content/Images/menu.png" , MenuUrl="/Home/Index", MenuSpriteCssClass="menu-sprite", MenuUrlTarget="_black", MenuSortOrder = 15, ParentMenuId = 14} //Schedule
                        };

            menus.ForEach(m => context.Menus.Add(m));

            context.SaveChanges();

            List<CustomMenu> _customMenuList = new List<CustomMenu>();

            CustomMenu a = new CustomMenu();
            a.MenuId = 1;
            a.MenuName = "A";
            a.ParentMenuId = 0;
            _customMenuList.Add(a);

            CustomMenu b = new CustomMenu();
            b.MenuId = 2;
            b.MenuName = "B";
            b.ParentMenuId = 0;
            _customMenuList.Add(b);

            CustomMenu cc = new CustomMenu();
            cc.MenuId = 3;
            cc.MenuName = "C";
            cc.ParentMenuId = 0;
            _customMenuList.Add(cc);
            //End of main menus

            #region Child menu for A

            List<CustomMenu> lstA = new List<CustomMenu>();

            CustomMenu a1 = new CustomMenu();
            a1.MenuId = 4;
            a1.MenuName = "A 1";
            a1.ParentMenuId = 1;
            a1.ParentMenu = a;
            lstA.Add(a1);

            CustomMenu a2 = new CustomMenu();
            a2.MenuId = 5;
            a2.MenuName = "A 2";
            a2.ParentMenuId = 1;
            a2.ParentMenu = a;
            lstA.Add(a2);
            a.CustomMenus = lstA;

            #endregion Child menu for A

            #region Child menu for B

            List<CustomMenu> lstB = new List<CustomMenu>();
            CustomMenu b1 = new CustomMenu();
            b1.MenuId = 6;
            b1.MenuName = "B 1";
            b1.ParentMenuId = 1;
            lstB.Add(b1);
            b.CustomMenus = lstB;

            #region Child for B under B

            List<CustomMenu> lstB_B = new List<CustomMenu>();
            CustomMenu bb1 = new CustomMenu();
            bb1.MenuId = 7;
            bb1.MenuName = "BB 1";
            bb1.ParentMenuId = 6;
            lstB_B.Add(bb1);

            b1.CustomMenus = lstB_B;

            #endregion Child for B under B

            #endregion Child menu for B

            _customMenuList.ForEach(cm => context.CustomMenus.Add(cm));

            context.SaveChanges();

        }
    }

    #endregion
}

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
            // Create default catrgories.
            var categories = new List<Category>
                            {
                                new Category { CategoryId=1, Name = "Fruit"},
                                new Category {CategoryId=2, Name = "Cloth"},
                                new Category {CategoryId=3, Name = "Car"},
                                new Category {CategoryId=4, Name = "Book"},
                                new Category {CategoryId=5, Name = "Shoe"},
                                new Category {CategoryId=6, Name = "Wipper"},
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

        }
    }

    #endregion
}
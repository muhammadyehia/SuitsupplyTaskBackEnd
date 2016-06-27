using System.Data.Entity.Infrastructure.Annotations;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.Infrastructure
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductContext : DbContext
    {
        public ProductContext()
            : base("name=ProductDataBase")
        {
            Database.SetInitializer<ProductContext>(new CreateDatabaseIfNotExists<ProductContext>());
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Photo> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(c => c.Id);
            modelBuilder.Entity<Product>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>()
                .Property(c => c.Name)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true }));
            modelBuilder.Entity<Product>().Property(c => c.LastUpdated).HasColumnType("datetime2");
            modelBuilder.Entity<Product>()
                .Property(e => e.Price).IsRequired()
                .HasPrecision(19, 4);
            modelBuilder.Entity<Product>()
                .HasOptional(s => s.Photo)
                .WithRequired(ad => ad.Product)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Photo>().HasKey(c => c.PhotoId);
            modelBuilder.Entity<Photo>().Property(c => c.PhotoName).IsRequired().HasMaxLength(225);
            modelBuilder.Entity<Photo>().Property(c => c.Content).IsRequired();
            modelBuilder.Entity<Photo>().Property(c => c.ContentType).IsRequired().HasMaxLength(100);
        }
    }
}

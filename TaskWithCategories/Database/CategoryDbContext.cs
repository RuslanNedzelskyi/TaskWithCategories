using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskWithCategories.Models;

namespace TaskWithCategories.Database
{
    public class ManagerGoodsDbContext : DbContext
    {
        public ManagerGoodsDbContext(DbContextOptions<ManagerGoodsDbContext> options)
            : base(options)
        { }

        public DbSet<Goods> Goods { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(i => i.SubCategories)
                .HasForeignKey(k => k.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>()
                .HasMany(e => e.SubCategories)
                .WithOne(i => i.ParentCategory)
                .HasForeignKey(k => k.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Goods>()
            //    .HasOne(e => e.SubCategory)
            //    .WithMany(g => g.Goods)
            //    .HasForeignKey(k => k.SubCategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Category>()
            //    .HasMany(e => e.Goods)
            //    .WithOne(i => i.SubCategory)
            //    .HasForeignKey(k => k.SubCategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);

            foreach (var relationships in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationships.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}

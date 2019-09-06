using Microsoft.EntityFrameworkCore;
using TaskWithCategories.Models;
using TaskWithCategories.Repositories.Consts;

namespace TaskWithCategories.Database
{
    public class ManagerGoodsDbContext : DbContext
    {
        public DbSet<Goods> Goods { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(PathToDB.PATH_TO_DB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(i => i.SubCategories)
                .HasForeignKey(k => k.ParentCategoryId);
        }
    }
}

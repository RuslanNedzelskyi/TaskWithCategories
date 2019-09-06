using Microsoft.EntityFrameworkCore;
using TaskWithCategories.Models;

namespace TaskWithCategories.Database
{
    public class ManagerGoodsDbContext : DbContext
    {
        //public ManagerGoodsDbContext(DbContextOptions<ManagerGoodsDbContext> options)
        //    : base(options)
        //{ }

        public DbSet<Goods> Goods { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ManagerGoodsAppData;Integrated Security=True;");
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

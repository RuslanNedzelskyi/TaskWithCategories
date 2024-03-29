﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskWithCategories.Database;

namespace TaskWithCategories.Migrations
{
    [DbContext(typeof(ManagerGoodsDbContext))]
    partial class ManagerGoodsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskWithCategories.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("ID");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TaskWithCategories.Models.Goods", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("GoodsName");

                    b.Property<double>("Price");

                    b.Property<int>("SubCategoryId");

                    b.HasKey("ID");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("TaskWithCategories.Models.Category", b =>
                {
                    b.HasOne("TaskWithCategories.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("TaskWithCategories.Models.Goods", b =>
                {
                    b.HasOne("TaskWithCategories.Models.Category", "SubCategory")
                        .WithMany("Goods")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

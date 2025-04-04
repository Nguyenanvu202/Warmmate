﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20250307123043_ModifyConfigImg")]
    partial class ModifyConfigImg
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ParentProductCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentProductCategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Core.Entities.ProductItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("Core.Entities.ProductItemImg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductItemId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductItemId");

                    b.ToTable("ProductItemImgs");
                });

            modelBuilder.Entity("Core.Entities.Variation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Variations");
                });

            modelBuilder.Entity("Core.Entities.VariationOpt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("VariationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VariationId");

                    b.ToTable("VariationOpts");
                });

            modelBuilder.Entity("ProductItemVariationOpt", b =>
                {
                    b.Property<int>("ItemForeignKey")
                        .HasColumnType("int");

                    b.Property<int>("OptForeignKey")
                        .HasColumnType("int");

                    b.HasKey("ItemForeignKey", "OptForeignKey");

                    b.HasIndex("OptForeignKey");

                    b.ToTable("ProductItemVariationOpt");
                });

            modelBuilder.Entity("Core.Entities.ProductCategory", b =>
                {
                    b.HasOne("Core.Entities.ProductCategory", "ParentProductCategory")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ParentProductCategoryId");

                    b.Navigation("ParentProductCategory");
                });

            modelBuilder.Entity("Core.Entities.ProductItem", b =>
                {
                    b.HasOne("Core.Entities.ProductCategory", "ProductCategory")
                        .WithMany("ProductItems")
                        .HasForeignKey("ProductCategoryId");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Core.Entities.ProductItemImg", b =>
                {
                    b.HasOne("Core.Entities.ProductItem", "ProductItem")
                        .WithMany("ProductItemImgs")
                        .HasForeignKey("ProductItemId");

                    b.Navigation("ProductItem");
                });

            modelBuilder.Entity("Core.Entities.Variation", b =>
                {
                    b.HasOne("Core.Entities.ProductCategory", "ProductCategory")
                        .WithMany("Variations")
                        .HasForeignKey("ProductCategoryId");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Core.Entities.VariationOpt", b =>
                {
                    b.HasOne("Core.Entities.Variation", "Variation")
                        .WithMany("VariationOpts")
                        .HasForeignKey("VariationId");

                    b.Navigation("Variation");
                });

            modelBuilder.Entity("ProductItemVariationOpt", b =>
                {
                    b.HasOne("Core.Entities.ProductItem", null)
                        .WithMany()
                        .HasForeignKey("ItemForeignKey")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Entities.VariationOpt", null)
                        .WithMany()
                        .HasForeignKey("OptForeignKey")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.ProductCategory", b =>
                {
                    b.Navigation("ProductCategories");

                    b.Navigation("ProductItems");

                    b.Navigation("Variations");
                });

            modelBuilder.Entity("Core.Entities.ProductItem", b =>
                {
                    b.Navigation("ProductItemImgs");
                });

            modelBuilder.Entity("Core.Entities.Variation", b =>
                {
                    b.Navigation("VariationOpts");
                });
#pragma warning restore 612, 618
        }
    }
}

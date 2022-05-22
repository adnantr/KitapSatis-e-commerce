﻿// <auto-generated />
using System;
using KitapSatis.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KitapSatis.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class AplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("KitapSatis.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("KitapSatis.Models.CategoryProduct", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CategoryProducts");
                });

            modelBuilder.Entity("KitapSatis.Models.Communication", b =>
                {
                    b.Property<int>("CommunicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommunicationId");

                    b.ToTable("Communications");
                });

            modelBuilder.Entity("KitapSatis.Models.CreditCard", b =>
                {
                    b.Property<int>("CreditCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CardExpMo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardExpYr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CreditCardId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("KitapSatis.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CommunicationId")
                        .HasColumnType("int");

                    b.Property<int>("CreditCardId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirtName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CommunicationId")
                        .IsUnique();

                    b.HasIndex("CreditCardId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("KitapSatis.Models.Favorite", b =>
                {
                    b.Property<int>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("KitapSatis.Models.Kind", b =>
                {
                    b.Property<int>("KindId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("KindName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KindId");

                    b.ToTable("Kinds");
                });

            modelBuilder.Entity("KitapSatis.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("KitapSatis.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("OrderDetailId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("KitapSatis.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("KindId")
                        .HasColumnType("int");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("KitapSatis.Models.ProductKind", b =>
                {
                    b.Property<int>("KindId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("KindId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductKind");
                });

            modelBuilder.Entity("KitapSatis.Models.CategoryProduct", b =>
                {
                    b.HasOne("KitapSatis.Models.Category", "Category")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KitapSatis.Models.Product", "Product")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("KitapSatis.Models.Customer", b =>
                {
                    b.HasOne("KitapSatis.Models.Communication", "Communication")
                        .WithOne("Customer")
                        .HasForeignKey("KitapSatis.Models.Customer", "CommunicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KitapSatis.Models.CreditCard", "CreditCard")
                        .WithOne("Customer")
                        .HasForeignKey("KitapSatis.Models.Customer", "CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Communication");

                    b.Navigation("CreditCard");
                });

            modelBuilder.Entity("KitapSatis.Models.Favorite", b =>
                {
                    b.HasOne("KitapSatis.Models.Customer", "Customer")
                        .WithMany("Favorite")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("KitapSatis.Models.ProductKind", b =>
                {
                    b.HasOne("KitapSatis.Models.Kind", "Kind")
                        .WithMany("ProductKinds")
                        .HasForeignKey("KindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KitapSatis.Models.Product", "Product")
                        .WithMany("ProductKinds")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kind");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("KitapSatis.Models.Category", b =>
                {
                    b.Navigation("CategoryProducts");
                });

            modelBuilder.Entity("KitapSatis.Models.Communication", b =>
                {
                    b.Navigation("Customer");
                });

            modelBuilder.Entity("KitapSatis.Models.CreditCard", b =>
                {
                    b.Navigation("Customer");
                });

            modelBuilder.Entity("KitapSatis.Models.Customer", b =>
                {
                    b.Navigation("Favorite");
                });

            modelBuilder.Entity("KitapSatis.Models.Kind", b =>
                {
                    b.Navigation("ProductKinds");
                });

            modelBuilder.Entity("KitapSatis.Models.Product", b =>
                {
                    b.Navigation("CategoryProducts");

                    b.Navigation("ProductKinds");
                });
#pragma warning restore 612, 618
        }
    }
}
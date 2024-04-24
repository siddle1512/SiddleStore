﻿// <auto-generated />
using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(SiddleSroteDbContext))]
    [Migration("20240424114409_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObject.CustomerObject", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerFullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Customer", (string)null);

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "NOXH An Phu Thinh Block B",
                            Balance = 1000000000m,
                            CustomerFullName = "Le Minh Vuong",
                            CustomerPhone = "0918955649",
                            NationalId = "054202001111",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("BusinessObject.DailyRevenueObject", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalOrder")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalRevenue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Date", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("DailyRevenue", (string)null);
                });

            modelBuilder.Entity("BusinessObject.OrderDetailObject", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("BusinessObject.OrderObject", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StoreId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("BusinessObject.ProductObject", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<int>("InStock")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Discount = 0.20000000000000001,
                            InStock = 100,
                            Price = 48000000m,
                            ProductName = "RTX4090",
                            Status = 0
                        });
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Manager"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Customer"
                        });
                });

            modelBuilder.Entity("BusinessObject.StoreObject", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreId");

                    b.ToTable("Store", (string)null);

                    b.HasData(
                        new
                        {
                            StoreId = 1,
                            Address = "5/30 Le Hong Phong",
                            Status = 0,
                            StoreName = "Siddle01"
                        },
                        new
                        {
                            StoreId = 2,
                            Address = "6/30 Le Hong Phong",
                            Status = 1,
                            StoreName = "Siddle02"
                        });
                });

            modelBuilder.Entity("BusinessObject.UserObject", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StoreId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            PasswordHashed = "binhbo22",
                            RoleId = 1,
                            Status = 1,
                            StoreId = 1,
                            UserName = "Binh"
                        });
                });

            modelBuilder.Entity("BusinessObject.CustomerObject", b =>
                {
                    b.HasOne("BusinessObject.UserObject", "User")
                        .WithOne("Customer")
                        .HasForeignKey("BusinessObject.CustomerObject", "UserId")
                        .HasConstraintName("FK_Customer_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.DailyRevenueObject", b =>
                {
                    b.HasOne("BusinessObject.StoreObject", "Store")
                        .WithMany("DailyRevenues")
                        .HasForeignKey("StoreId")
                        .IsRequired()
                        .HasConstraintName("FK_DailyRevenue_Store");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("BusinessObject.OrderDetailObject", b =>
                {
                    b.HasOne("BusinessObject.OrderObject", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Order");

                    b.HasOne("BusinessObject.ProductObject", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Product");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BusinessObject.OrderObject", b =>
                {
                    b.HasOne("BusinessObject.CustomerObject", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Order_Customer");

                    b.HasOne("BusinessObject.StoreObject", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Store");

                    b.Navigation("Customer");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("BusinessObject.UserObject", b =>
                {
                    b.HasOne("BusinessObject.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_User_Role");

                    b.HasOne("BusinessObject.StoreObject", "Store")
                        .WithMany("Users")
                        .HasForeignKey("StoreId")
                        .IsRequired()
                        .HasConstraintName("FK_User_Store");

                    b.Navigation("Role");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("BusinessObject.CustomerObject", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObject.OrderObject", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObject.ProductObject", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObject.StoreObject", b =>
                {
                    b.Navigation("DailyRevenues");

                    b.Navigation("Orders");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObject.UserObject", b =>
                {
                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}

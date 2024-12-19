﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(modelsContext))]
    partial class modelsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication1.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountCustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasoneRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountCustomerId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("WebApplication1.Models.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AlbumID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlbumId"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int")
                        .HasColumnName("ArtistID");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("GenreID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.HasKey("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("WebApplication1.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ArtistID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistId"), 1L, 1);

                    b.Property<string>("Biography")
                        .HasColumnType("text");

                    b.Property<string>("Nicneym")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("ArtistId");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("bit");

                    b.Property<string>("AddressCustomer")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Address_Customer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameSurname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Name_Surname");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WebApplication1.Models.Disc", b =>
                {
                    b.Property<int>("DiscId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DiscID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscId"), 1L, 1);

                    b.Property<int>("AlbumId")
                        .HasColumnType("int")
                        .HasColumnName("AlbumID");

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("GenreID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("DiscId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("GenreId");

                    b.ToTable("Discs");
                });

            modelBuilder.Entity("WebApplication1.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GenreID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("DescriptionGenre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameGenre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("WebApplication1.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date");

                    b.Property<bool>("StatusOrder")
                        .HasColumnType("bit");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApplication1.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderItemID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                    b.Property<int>("DiscId")
                        .HasColumnType("int")
                        .HasColumnName("DiscID");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("DiscId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("WebApplication1.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.HasKey("PaymentId");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("WebApplication1.Models.Review", b =>
                {
                    b.Property<int>("ReviewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReviewsID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewsId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("Rev1iewDate")
                        .HasColumnType("date");

                    b.Property<string>("TextReviews")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ReviewsId")
                        .HasName("PK__Reviews__64C7C08DF49C87AC");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("WebApplication1.Models.Shipping", b =>
                {
                    b.Property<int>("ShippingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ShippingID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShippingId"), 1L, 1);

                    b.Property<string>("DeliveryServise")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<DateTime>("ShippingDate")
                        .HasColumnType("date");

                    b.Property<string>("TrecerShipping")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ShippingId");

                    b.HasIndex("OrderId");

                    b.ToTable("Shipping", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Entities.RefreshToken", b =>
                {
                    b.HasOne("WebApplication1.Models.Customer", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebApplication1.Models.Album", b =>
                {
                    b.HasOne("WebApplication1.Models.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .IsRequired()
                        .HasConstraintName("FK__Albums__ArtistID__3B75D760");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("WebApplication1.Models.Disc", b =>
                {
                    b.HasOne("WebApplication1.Models.Album", "Album")
                        .WithMany("Discs")
                        .HasForeignKey("AlbumId")
                        .IsRequired()
                        .HasConstraintName("FK__Discs__AlbumID__403A8C7D");

                    b.HasOne("WebApplication1.Models.Genre", "Genre")
                        .WithMany("Discs")
                        .HasForeignKey("GenreId")
                        .IsRequired()
                        .HasConstraintName("FK__Discs__GenreID__412EB0B6");

                    b.Navigation("Album");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("WebApplication1.Models.Order", b =>
                {
                    b.HasOne("WebApplication1.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Orders__StatusOr__440B1D61");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebApplication1.Models.OrderItem", b =>
                {
                    b.HasOne("WebApplication1.Models.Disc", "Disc")
                        .WithMany("OrderItems")
                        .HasForeignKey("DiscId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderItem__DiscI__47DBAE45");

                    b.HasOne("WebApplication1.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderItem__Order__46E78A0C");

                    b.Navigation("Disc");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApplication1.Models.Payment", b =>
                {
                    b.HasOne("WebApplication1.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__Payments__Amount__4AB81AF0");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApplication1.Models.Review", b =>
                {
                    b.HasOne("WebApplication1.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Reviews__Custome__5165187F");

                    b.HasOne("WebApplication1.Models.Order", "Order")
                        .WithMany("Reviews")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__Reviews__OrderID__5070F446");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApplication1.Models.Shipping", b =>
                {
                    b.HasOne("WebApplication1.Models.Order", "Order")
                        .WithMany("Shippings")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__Shipping__Trecer__4D94879B");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApplication1.Models.Album", b =>
                {
                    b.Navigation("Discs");
                });

            modelBuilder.Entity("WebApplication1.Models.Artist", b =>
                {
                    b.Navigation("Albums");
                });

            modelBuilder.Entity("WebApplication1.Models.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("WebApplication1.Models.Disc", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("WebApplication1.Models.Genre", b =>
                {
                    b.Navigation("Discs");
                });

            modelBuilder.Entity("WebApplication1.Models.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payments");

                    b.Navigation("Reviews");

                    b.Navigation("Shippings");
                });
#pragma warning restore 612, 618
        }
    }
}

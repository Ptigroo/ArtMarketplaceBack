﻿// <auto-generated />
using System;
using ArtMarketplace.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace data.Migrations
{
    [DbContext(typeof(ArtMarketplaceDbContext))]
    partial class ArtMarketplaceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtMarketplace.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ArtMarketplace.Domain.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtisanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DeliveryStatus")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductStatus")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ReviewId")
                        .IsUnique()
                        .HasFilter("[ReviewId] IS NOT NULL");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ArtMarketplace.Domain.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ArtMarketplace.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArtMarketplace.Domain.Models.Product", b =>
                {
                    b.HasOne("ArtMarketplace.Domain.Models.User", "Artisan")
                        .WithMany("ArtisanProducts")
                        .HasForeignKey("ArtisanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ArtMarketplace.Domain.Models.User", "Buyer")
                        .WithMany("BuyerProducts")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ArtMarketplace.Domain.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtMarketplace.Domain.Models.Review", "Review")
                        .WithOne("Product")
                        .HasForeignKey("ArtMarketplace.Domain.Models.Product", "ReviewId");

                    b.Navigation("Artisan");

                    b.Navigation("Buyer");

                    b.Navigation("Category");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("ArtMarketplace.Domain.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ArtMarketplace.Domain.Models.Review", b =>
                {
                    b.Navigation("Product")
                        .IsRequired();
                });

            modelBuilder.Entity("ArtMarketplace.Domain.Models.User", b =>
                {
                    b.Navigation("ArtisanProducts");

                    b.Navigation("BuyerProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using BookLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookLibrary.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20250608142037_InitBooks")]
    partial class InitBooks
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("Models.Media", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Books");

                    b.HasDiscriminator<string>("Type").HasValue("Media");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Ebook", b =>
                {
                    b.HasBaseType("Models.Media");

                    b.HasDiscriminator().HasValue("Ebook");
                });

            modelBuilder.Entity("PaperBook", b =>
                {
                    b.HasBaseType("Models.Media");

                    b.HasDiscriminator().HasValue("PaperBook");
                });
#pragma warning restore 612, 618
        }
    }
}

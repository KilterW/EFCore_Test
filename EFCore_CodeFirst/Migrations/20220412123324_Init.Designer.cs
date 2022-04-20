﻿// <auto-generated />
using System;
using EFCore_CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore_CodeFirst.Migrations
{
    [DbContext(typeof(CodeFContext))]
    [Migration("20220412123324_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EFCore_CodeFirst.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("AuthorName")
                        .HasColumnType("longtext")
                        .HasColumnName("AuthorName");

                    b.Property<double?>("Price")
                        .HasColumnType("double")
                        .HasColumnName("Price");

                    b.Property<DateTime?>("PubTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("PubTime");

                    b.Property<string>("Title")
                        .HasColumnType("longtext")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("EFCore_CodeFirst.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("CommentStr")
                        .HasColumnType("longtext")
                        .HasColumnName("CommentStr");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Title")
                        .HasColumnType("longtext")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Comment");
                });
#pragma warning restore 612, 618
        }
    }
}

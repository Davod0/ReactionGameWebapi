﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240228220148_ChangingPropertyOfHighscoreClass")]
    partial class ChangingPropertyOfHighscoreClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("WebApi.Highscore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Highscore");
                });
#pragma warning restore 612, 618
        }
    }
}

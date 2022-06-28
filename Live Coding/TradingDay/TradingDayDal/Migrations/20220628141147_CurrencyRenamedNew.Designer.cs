﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradingDayDal;

#nullable disable

namespace TradingDayDal.Migrations
{
    [DbContext(typeof(TradingDayContext))]
    [Migration("20220628141147_CurrencyRenamedNew")]
    partial class CurrencyRenamedNew
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TradingDayDal.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TradingDayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TradingDayId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("TradingDayDal.TradingDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("TradingLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TradingDays");
                });

            modelBuilder.Entity("TradingDayDal.Currency", b =>
                {
                    b.HasOne("TradingDayDal.TradingDay", "TradingDay")
                        .WithMany("Currencies")
                        .HasForeignKey("TradingDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TradingDay");
                });

            modelBuilder.Entity("TradingDayDal.TradingDay", b =>
                {
                    b.Navigation("Currencies");
                });
#pragma warning restore 612, 618
        }
    }
}

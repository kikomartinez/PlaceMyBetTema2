﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlaceMyBet.Models;

namespace PlaceMyBet.Migrations
{
    [DbContext(typeof(PlaceMyBetContext))]
    partial class PlaceMyBetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PlaceMyBet.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bank")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("CurrentMoney")
                        .HasColumnType("float");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("AccountID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PlaceMyBet.Models.Bet", b =>
                {
                    b.Property<int>("BetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("BetMoney")
                        .HasColumnType("float");

                    b.Property<string>("Date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("MarketID")
                        .HasColumnType("int");

                    b.Property<float>("Odd")
                        .HasColumnType("float");

                    b.Property<string>("TypeOfBet")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("BetID");

                    b.HasIndex("MarketID");

                    b.HasIndex("UserID");

                    b.ToTable("Bet");
                });

            modelBuilder.Entity("PlaceMyBet.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LocalTeam")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("VisitorTeam")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("EventID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("PlaceMyBet.Models.Market", b =>
                {
                    b.Property<int>("MarketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<float>("OverMoney")
                        .HasColumnType("float");

                    b.Property<float>("OverOdds")
                        .HasColumnType("float");

                    b.Property<float>("Type")
                        .HasColumnType("float");

                    b.Property<float>("UnderMoney")
                        .HasColumnType("float");

                    b.Property<float>("UnderOdds")
                        .HasColumnType("float");

                    b.HasKey("MarketID");

                    b.HasIndex("EventID");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("PlaceMyBet.Models.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlaceMyBet.Models.Account", b =>
                {
                    b.HasOne("PlaceMyBet.Models.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("PlaceMyBet.Models.Account", "UserID");
                });

            modelBuilder.Entity("PlaceMyBet.Models.Bet", b =>
                {
                    b.HasOne("PlaceMyBet.Models.Market", "Market")
                        .WithMany("Bets")
                        .HasForeignKey("MarketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceMyBet.Models.User", "User")
                        .WithMany("Bets")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("PlaceMyBet.Models.Market", b =>
                {
                    b.HasOne("PlaceMyBet.Models.Event", "Event")
                        .WithMany("Markets")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
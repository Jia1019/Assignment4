﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication.Persistence;

namespace WebApplication.Migrations
{
    [DbContext(typeof(FamilyContext))]
    [Migration("20201124162941_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Adult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EyeColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("HairColor")
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .HasColumnType("TEXT");

                    b.Property<float>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Adults");
                });

            modelBuilder.Entity("Models.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EyeColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("HairColor")
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .HasColumnType("TEXT");

                    b.Property<float>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("Models.ChildInterest", b =>
                {
                    b.Property<int>("ChildID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InterestType")
                        .HasColumnType("TEXT");

                    b.HasKey("ChildID", "InterestType");

                    b.HasIndex("InterestType");

                    b.ToTable("ChildInterest");
                });

            modelBuilder.Entity("Models.Family", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("Models.FamilyAdult", b =>
                {
                    b.Property<int>("FamilyID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AdultID")
                        .HasColumnType("INTEGER");

                    b.HasKey("FamilyID", "AdultID");

                    b.HasIndex("AdultID");

                    b.ToTable("FamilyAdult");
                });

            modelBuilder.Entity("Models.FamilyChild", b =>
                {
                    b.Property<int>("FamilyID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChildID")
                        .HasColumnType("INTEGER");

                    b.HasKey("FamilyID", "ChildID");

                    b.HasIndex("ChildID");

                    b.ToTable("FamilyChild");
                });

            modelBuilder.Entity("Models.Interest", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Type");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("Models.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FamilyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.HasIndex("FamilyId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.ChildInterest", b =>
                {
                    b.HasOne("Models.Child", "Child")
                        .WithMany("ChildInterests")
                        .HasForeignKey("ChildID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Interest", "Interest")
                        .WithMany("ChildInterests")
                        .HasForeignKey("InterestType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Interest");
                });

            modelBuilder.Entity("Models.FamilyAdult", b =>
                {
                    b.HasOne("Models.Adult", "Adult")
                        .WithMany("FamilyAdults")
                        .HasForeignKey("AdultID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Family", "Family")
                        .WithMany("Adults")
                        .HasForeignKey("FamilyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adult");

                    b.Navigation("Family");
                });

            modelBuilder.Entity("Models.FamilyChild", b =>
                {
                    b.HasOne("Models.Child", "Child")
                        .WithMany("FamilyChildren")
                        .HasForeignKey("ChildID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Family", "Family")
                        .WithMany("Children")
                        .HasForeignKey("FamilyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Family");
                });

            modelBuilder.Entity("Models.Pet", b =>
                {
                    b.HasOne("Models.Child", null)
                        .WithMany("Pets")
                        .HasForeignKey("ChildId");

                    b.HasOne("Models.Family", null)
                        .WithMany("Pets")
                        .HasForeignKey("FamilyId");
                });

            modelBuilder.Entity("Models.Adult", b =>
                {
                    b.Navigation("FamilyAdults");
                });

            modelBuilder.Entity("Models.Child", b =>
                {
                    b.Navigation("ChildInterests");

                    b.Navigation("FamilyChildren");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Models.Family", b =>
                {
                    b.Navigation("Adults");

                    b.Navigation("Children");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Models.Interest", b =>
                {
                    b.Navigation("ChildInterests");
                });
#pragma warning restore 612, 618
        }
    }
}

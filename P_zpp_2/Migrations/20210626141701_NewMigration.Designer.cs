﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P_zpp_2.Data;

namespace P_zpp_2.Migrations
{
    [DbContext(typeof(P_zpp_2DbContext))]
    [Migration("20210626141701_NewMigration")]
    partial class NewMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("P_zpp_2.Data.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoordinatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HangingDaysInJSON")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastScheduleDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("ScheduleInJSON")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScheduleInstructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScheduleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.ToTable("schedules");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BossIdId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.HasIndex("BossIdId");

                    b.ToTable("company");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Departures", b =>
                {
                    b.Property<int>("DeprtureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("DepartureName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Shifts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupervisorIdId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DeprtureId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SupervisorIdId");

                    b.ToTable("departures");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Leaves", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IddepartuersDeprtureId")
                        .HasColumnType("int");

                    b.Property<string>("IduseraId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeaveDayRange")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Leavesname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status_odrzucone")
                        .HasColumnType("bit");

                    b.Property<bool>("Status_zaakceptopwane")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IddepartuersDeprtureId");

                    b.HasIndex("IduseraId");

                    b.ToTable("leaves");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Messages", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReciverId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isRead")
                        .HasColumnType("bit");

                    b.HasKey("MessageId");

                    b.HasIndex("ReciverId");

                    b.HasIndex("SenderId");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.ScheduleInstructions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoordinatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ListOfShistsInJSON")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.ToTable("ScheduleInstructions");
                });

            modelBuilder.Entity("P_zpp_2.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int?>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rola")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Schedule")
                        .HasColumnType("nvarchar(900)");

                    b.Property<string>("SpecialInfo")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("DeptId");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P_zpp_2.Data.Schedule", b =>
                {
                    b.HasOne("P_zpp_2.Models.ApplicationUser", "Coordinaor")
                        .WithMany()
                        .HasForeignKey("CoordinatorId");

                    b.Navigation("Coordinaor");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Company", b =>
                {
                    b.HasOne("P_zpp_2.Models.ApplicationUser", "BossId")
                        .WithMany()
                        .HasForeignKey("BossIdId");

                    b.Navigation("BossId");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Departures", b =>
                {
                    b.HasOne("P_zpp_2.Models.MyCustomLittleDatabase.Company", "CompanyID")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("P_zpp_2.Models.ApplicationUser", "SupervisorId")
                        .WithMany()
                        .HasForeignKey("SupervisorIdId");

                    b.Navigation("CompanyID");

                    b.Navigation("SupervisorId");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Leaves", b =>
                {
                    b.HasOne("P_zpp_2.Models.MyCustomLittleDatabase.Departures", "Iddepartuers")
                        .WithMany()
                        .HasForeignKey("IddepartuersDeprtureId");

                    b.HasOne("P_zpp_2.Models.ApplicationUser", "Idusera")
                        .WithMany()
                        .HasForeignKey("IduseraId");

                    b.Navigation("Iddepartuers");

                    b.Navigation("Idusera");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Messages", b =>
                {
                    b.HasOne("P_zpp_2.Models.ApplicationUser", "Reciver")
                        .WithMany()
                        .HasForeignKey("ReciverId");

                    b.HasOne("P_zpp_2.Models.ApplicationUser", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");

                    b.Navigation("Reciver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.ScheduleInstructions", b =>
                {
                    b.HasOne("P_zpp_2.Models.ApplicationUser", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId");

                    b.Navigation("Coordinator");
                });

            modelBuilder.Entity("P_zpp_2.Models.ApplicationUser", b =>
                {
                    b.HasOne("P_zpp_2.Models.MyCustomLittleDatabase.Departures", "departure")
                        .WithMany("MyUsers")
                        .HasForeignKey("DeptId");

                    b.Navigation("departure");
                });

            modelBuilder.Entity("P_zpp_2.Models.MyCustomLittleDatabase.Departures", b =>
                {
                    b.Navigation("MyUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
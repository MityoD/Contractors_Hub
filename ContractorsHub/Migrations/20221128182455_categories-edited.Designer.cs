﻿// <auto-generated />
using System;
using ContractorsHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContractorsHub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221128182455_categories-edited")]
    partial class categoriesedited
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContractorsHub.Data.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ContractorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("bit");

                    b.Property<int>("JobStatusId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OwnerName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("JobStatusId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.JobCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("JobsCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Heating & Plumbing"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Electrical & Lighting"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Outdoor & Gardening"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Heavy machinery"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Decorating"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Other.."
                        });
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.JobOffer", b =>
                {
                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("JobId", "OfferId");

                    b.HasIndex("OfferId");

                    b.ToTable("JobOffer");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.JobStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JobStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Declined"
                        });
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("JobCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("JobCategoryId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.ToolCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ToolsCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Hand tools"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Power tool accessories"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Power tools"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Measuring tools"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Testing equipment"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Tool storage"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Other.."
                        });
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsContractor")
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

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

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

                    b.HasIndex("OwnerId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "dea12856-c198-4129-b3f3-b893d8395082",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "12522f86-b95e-435d-9c6d-b8c871d94736",
                            Email = "contractor@mail.com",
                            EmailConfirmed = false,
                            IsContractor = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "CONTRACTOR@MAIL.COM",
                            NormalizedUserName = "CONTRACTOR",
                            PasswordHash = "AQAAAAEAACcQAAAAEDQnZ+n4H6+8CPXG9pYuPnqeddVBy1RUU5YFq599Lkqdy6HZQ19FAyhBiT20KH4t2Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d8b36bd3-5a13-4041-a67e-5897eef2b684",
                            TwoFactorEnabled = false,
                            UserName = "contractor"
                        },
                        new
                        {
                            Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "47efa9c8-25ef-46c9-9781-78aa20e50c3e",
                            Email = "guest@mail.com",
                            EmailConfirmed = false,
                            IsContractor = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GUEST@MAIL.COM",
                            NormalizedUserName = "GUEST",
                            PasswordHash = "AQAAAAEAACcQAAAAEJD6vWld9KLbI5u1cHPCb5SX2WBB03YVV4pikNVzJmnVaCWilQrZX+syBPsTmcaywQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "aee12acf-6fb0-4f3c-8e8f-62c59474397b",
                            TwoFactorEnabled = false,
                            UserName = "guest"
                        },
                        new
                        {
                            Id = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "00ac0ec2-ea35-456f-836a-46b613b9f8d7",
                            Email = "admin@mail.com",
                            EmailConfirmed = false,
                            IsContractor = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEDisp9J5jatnpNrvHzDYnFpK3Bj7YTFM1K07G04Ixp+5pTN0I198a4vpx87s7Ngb+g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2eefb30a-c8eb-4180-a8be-f570b771d041",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

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

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1e62f853-4a41-4652-b9a9-8e8b236e24c7",
                            ConcurrencyStamp = "1fa54d33-a8ba-47b1-be64-02d6d4836152",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "5d937746-9833-4886-83d1-3c125ad5294c",
                            ConcurrencyStamp = "80196750-3beb-42a1-b321-0aa9c9f41275",
                            Name = "Guest",
                            NormalizedName = "GUEST"
                        },
                        new
                        {
                            Id = "c8a8cf93-46b1-4e79-871a-1f4742a0db83",
                            ConcurrencyStamp = "292f8d0e-c368-4cf1-bb0a-b8cda6096ea9",
                            Name = "Contractor",
                            NormalizedName = "CONTRACTOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
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

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                            RoleId = "1e62f853-4a41-4652-b9a9-8e8b236e24c7"
                        },
                        new
                        {
                            UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                            RoleId = "5d937746-9833-4886-83d1-3c125ad5294c"
                        },
                        new
                        {
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                            RoleId = "c8a8cf93-46b1-4e79-871a-1f4742a0db83"
                        });
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

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.Job", b =>
                {
                    b.HasOne("ContractorsHub.Data.Models.JobCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContractorsHub.Data.Models.JobStatus", "JobStatus")
                        .WithMany("Jobs")
                        .HasForeignKey("JobStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContractorsHub.Data.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("JobStatus");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.JobOffer", b =>
                {
                    b.HasOne("ContractorsHub.Data.Models.Job", "Job")
                        .WithMany("JobsOffers")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContractorsHub.Data.Models.Offer", "Offer")
                        .WithMany("JobsOffers")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.Tool", b =>
                {
                    b.HasOne("ContractorsHub.Data.Models.ToolCategory", "Category")
                        .WithMany("Tools")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContractorsHub.Data.Models.JobCategory", null)
                        .WithMany("Tools")
                        .HasForeignKey("JobCategoryId");

                    b.HasOne("ContractorsHub.Data.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.User", b =>
                {
                    b.HasOne("ContractorsHub.Data.Models.Offer", null)
                        .WithMany("Owner")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);
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
                    b.HasOne("ContractorsHub.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ContractorsHub.Data.Models.User", null)
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

                    b.HasOne("ContractorsHub.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ContractorsHub.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.Job", b =>
                {
                    b.Navigation("JobsOffers");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.JobCategory", b =>
                {
                    b.Navigation("Tools");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.JobStatus", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.Offer", b =>
                {
                    b.Navigation("JobsOffers");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ContractorsHub.Data.Models.ToolCategory", b =>
                {
                    b.Navigation("Tools");
                });
#pragma warning restore 612, 618
        }
    }
}
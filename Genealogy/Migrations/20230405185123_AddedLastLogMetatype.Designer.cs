﻿// <auto-generated />
using System;
using Genealogy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Genealogy.Migrations
{
    [DbContext(typeof(GenealogyContext))]
    [Migration("20230405185123_AddedLastLogMetatype")]
    partial class AddedLastLogMetatype
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Genealogy.Metatype", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Metatypes");
                });

            modelBuilder.Entity("Genealogy.Models.BusinessObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<DateTime>("FinishDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<Guid>("MetatypeId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MetatypeId");

                    b.HasIndex("UserId");

                    b.ToTable("BusinessObjects");
                });

            modelBuilder.Entity("Genealogy.Models.Cemetery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CountyId");

                    b.Property<string>("Name");

                    b.Property<bool>("isRemoved");

                    b.HasKey("Id");

                    b.HasIndex("CountyId");

                    b.ToTable("Cemeteries");
                });

            modelBuilder.Entity("Genealogy.Models.County", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Coords");

                    b.Property<string>("Name");

                    b.Property<bool>("isRemoved");

                    b.HasKey("Id");

                    b.ToTable("County");
                });

            modelBuilder.Entity("Genealogy.Models.Link", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<int>("Order");

                    b.Property<Guid>("PageId");

                    b.Property<Guid>("TargetPageId");

                    b.HasKey("Id");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("Genealogy.Models.LinkRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FinishDate");

                    b.Property<Guid>("Metatype");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("LinkRelations");
                });

            modelBuilder.Entity("Genealogy.Models.Page", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.Property<bool>("isRemoved");

                    b.Property<bool>("isSection");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Genealogy.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CemeteryId");

                    b.Property<string>("Comment");

                    b.Property<string>("FinishDate");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("Patronymic");

                    b.Property<Guid?>("PersonGroupId");

                    b.Property<string>("Source");

                    b.Property<string>("StartDate");

                    b.Property<bool>("isRemoved");

                    b.HasKey("Id");

                    b.HasIndex("CemeteryId");

                    b.HasIndex("PersonGroupId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Genealogy.Models.PersonGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("PersonGroups");
                });

            modelBuilder.Entity("Genealogy.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Genealogy.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<DateTime>("FinishDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<Guid?>("RoleId");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Genealogy.ObjectRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BusinessObjectId");

                    b.Property<DateTime>("FinishDate");

                    b.Property<Guid>("LinkRelationId");

                    b.Property<Guid>("MetatypeId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("ObjectRelations");
                });

            modelBuilder.Entity("Genealogy.Models.BusinessObject", b =>
                {
                    b.HasOne("Genealogy.Metatype", "Metatype")
                        .WithMany()
                        .HasForeignKey("MetatypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Genealogy.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Genealogy.Models.Cemetery", b =>
                {
                    b.HasOne("Genealogy.Models.County", "County")
                        .WithMany()
                        .HasForeignKey("CountyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Genealogy.Models.Person", b =>
                {
                    b.HasOne("Genealogy.Models.Cemetery", "Cemetery")
                        .WithMany()
                        .HasForeignKey("CemeteryId");

                    b.HasOne("Genealogy.Models.PersonGroup", "PersonGroup")
                        .WithMany()
                        .HasForeignKey("PersonGroupId");
                });

            modelBuilder.Entity("Genealogy.Models.User", b =>
                {
                    b.HasOne("Genealogy.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}

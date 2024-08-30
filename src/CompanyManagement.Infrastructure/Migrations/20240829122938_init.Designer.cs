﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CompanyManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyManagement.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240829122938_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CompanyManagement.Domain.Companies.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "CompanyManagement.Domain.Companies.Company.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("name_value");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Number", "CompanyManagement.Domain.Companies.Company.Number#Number", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("number_value");
                        });

                    b.HasKey("Id")
                        .HasName("pk_companies");

                    b.ToTable("companies", (string)null);
                });

            modelBuilder.Entity("CompanyManagement.Domain.Companies.Employees.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasColumnName("company_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<int>("EmployeeStatus")
                        .HasColumnType("integer")
                        .HasColumnName("employee_status");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "CompanyManagement.Domain.Companies.Employees.Employee.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("email_value");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "CompanyManagement.Domain.Companies.Employees.Employee.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_last_name");

                            b1.Property<string>("Patronymic")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_patronymic");
                        });

                    b.HasKey("Id")
                        .HasName("pk_employees");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_employees_company_id");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("CompanyManagement.Domain.Companies.Company", b =>
                {
                    b.OwnsOne("CompanyManagement.Domain.Companies.ImagesCollection", "Images", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uuid");

                            b1.HasKey("CompanyId");

                            b1.ToTable("companies");

                            b1.ToJson("image_collection");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId")
                                .HasConstraintName("fk_companies_companies_id");

                            b1.OwnsMany("CompanyManagement.Domain.Companies.Image", "Images", b2 =>
                                {
                                    b2.Property<Guid>("ImagesCollectionCompanyId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Alt")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Url")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ImagesCollectionCompanyId", "Id");

                                    b2.ToTable("companies");

                                    b2.ToJson("image_collection");

                                    b2.WithOwner()
                                        .HasForeignKey("ImagesCollectionCompanyId")
                                        .HasConstraintName("fk_companies_companies_images_collection_company_id");
                                });

                            b1.Navigation("Images");
                        });

                    b.Navigation("Images")
                        .IsRequired();
                });

            modelBuilder.Entity("CompanyManagement.Domain.Companies.Employees.Employee", b =>
                {
                    b.HasOne("CompanyManagement.Domain.Companies.Company", null)
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employees_companies_company_id");

                    b.OwnsOne("CompanyManagement.Domain.Companies.Employees.ProjectCollection", "Projects", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uuid");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("employees");

                            b1.ToJson("project_collection");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId")
                                .HasConstraintName("fk_employees_employees_id");

                            b1.OwnsMany("CompanyManagement.Domain.Companies.Employees.Project", "Projects", b2 =>
                                {
                                    b2.Property<Guid>("ProjectCollectionEmployeeId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<DateOnly>("EndDate")
                                        .HasColumnType("date");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Position")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<DateOnly>("StartDate")
                                        .HasColumnType("date");

                                    b2.HasKey("ProjectCollectionEmployeeId", "Id");

                                    b2.ToTable("employees");

                                    b2.ToJson("project_collection");

                                    b2.WithOwner()
                                        .HasForeignKey("ProjectCollectionEmployeeId")
                                        .HasConstraintName("fk_employees_employees_project_collection_employee_id");
                                });

                            b1.Navigation("Projects");
                        });

                    b.Navigation("Projects")
                        .IsRequired();
                });

            modelBuilder.Entity("CompanyManagement.Domain.Companies.Company", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}

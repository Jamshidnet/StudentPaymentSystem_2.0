﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StudentPaymentSystem.Infrustructure.Persistence;

#nullable disable

namespace StudentPaymentSystem.Infrustructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StudentsId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamptz");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("timestamptz");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Fee")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("CourseID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamptz");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("StudentID")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("StudentID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamptz");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("StudentPaymentSystem.Domein.Entities.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPaymentSystem.Domein.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Course", b =>
                {
                    b.HasOne("StudentPaymentSystem.Domein.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Invoice", b =>
                {
                    b.HasOne("StudentPaymentSystem.Domein.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Payment", b =>
                {
                    b.HasOne("StudentPaymentSystem.Domein.Entities.Invoice", "Invoice")
                        .WithMany("Payments")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPaymentSystem.Domein.Entities.Student", "Student")
                        .WithMany("Payments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Invoice", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Student", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("StudentPaymentSystem.Domein.Entities.Teacher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}

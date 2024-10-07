﻿// <auto-generated />
using System;
using Accapt.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    [DbContext(typeof(AccaptFContext))]
    partial class AccaptFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Accapt.DataLayer.Entities.BankT", b =>
                {
                    b.Property<int>("BankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BankId"));

                    b.Property<string>("BankAddress")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("BankBranch")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("BankNumber")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<bool>("HaseCheck")
                        .HasColumnType("bit");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Billan", b =>
                {
                    b.Property<int>("BillanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillanId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndDateShow")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("NegtiveBillan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PlusBillan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StartDateShow")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("TotoalBillan")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BillanId");

                    b.HasIndex("Id");

                    b.ToTable("Billans");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Chek", b =>
                {
                    b.Property<int>("CheckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CheckId"));

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<string>("CheckNumber")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<decimal>("ChekPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ChekcBankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CurrentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentDateSearch")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DueDateSearch")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatuceOfPaid")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TypeOfChek")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CheckId");

                    b.HasIndex("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("Cheks");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.DebtorCreditor", b =>
                {
                    b.Property<int>("DebtorCreditorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DebtorCreditorID"));

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOfPurchaseForShow")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Desctiptions")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<double>("PriceOfDebtorCreditor")
                        .HasColumnType("float");

                    b.Property<string>("Statuce")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DebtorCreditorID");

                    b.HasIndex("UserId");

                    b.ToTable("DebtorCreditors");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.EmployeeDeatails", b =>
                {
                    b.Property<int>("EmployeeDeatailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeDeatailsId"));

                    b.Property<int>("EmployeeDeatailsCount")
                        .HasMaxLength(200)
                        .HasColumnType("int");

                    b.Property<string>("EmployeeDeatailsName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("EpmloyeeId")
                        .HasColumnType("int");

                    b.Property<double>("PriceOfEmployeDeatails")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EmployeeDeatailsId");

                    b.HasIndex("EpmloyeeId");

                    b.HasIndex("UsersId");

                    b.ToTable("EmployeeDeatails");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Epmloyee", b =>
                {
                    b.Property<int>("EpmloyeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EpmloyeeId"));

                    b.Property<DateTime>("DateOfRegister")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOfRegisterShow")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EmployeeRoll")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EpmloyeeName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EpmloyeeId");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceId"));

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("CreditorStatuce")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateOfSubmitInvoice")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InvoiceName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("NextDateVisit")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TypeOfInvoice")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.InvoiceDetails", b =>
                {
                    b.Property<int>("InvoiceDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceDetailsId"));

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.Property<decimal>("ProductTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceDetailsId");

                    b.HasIndex("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Pepole", b =>
                {
                    b.Property<string>("PepoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PepoCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PepoName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PepoType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PepoId");

                    b.HasIndex("UserId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.ProviderServiceList", b =>
                {
                    b.Property<int>("ProviderWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProviderWorkId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("DateOfService")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOfServiceForShow")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsDone")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ServiceProviderId")
                        .HasColumnType("int");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProviderWorkId");

                    b.HasIndex("ServiceProviderId");

                    b.HasIndex("UserId");

                    b.ToTable("ProviderServiceLists");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.SallaryAndCosts", b =>
                {
                    b.Property<int>("SallaryAndCostsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SallaryAndCostsId"));

                    b.Property<DateTime>("DateOfSubmit")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOfSubmitForShow")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<double>("PriceOfSallaryAndCosts")
                        .HasColumnType("float");

                    b.Property<string>("SallaryAndCostsName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SallaryAndCostsId");

                    b.HasIndex("UserId");

                    b.ToTable("SallaryAndCosts");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.ServiceProvider", b =>
                {
                    b.Property<int>("ServiceProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceProviderId"));

                    b.Property<double>("AmountOfCreditor")
                        .HasMaxLength(250)
                        .HasColumnType("float");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IsCreditor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ProviderPassword")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ServiceProviderId");

                    b.HasIndex("Id");

                    b.ToTable("ServiceProviders");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Users", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("ExpireAccessDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RealFullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("VerifyCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.VisibleService", b =>
                {
                    b.Property<int>("VisibleServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisibleServiceId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("DateOfService")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProviderWorkId")
                        .HasColumnType("int");

                    b.Property<string>("SrviceName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Statuce")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("VisibleServiceId");

                    b.HasIndex("ProviderWorkId");

                    b.HasIndex("UserId");

                    b.ToTable("VisibleService");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.BankT", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "User")
                        .WithMany("Banks")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Billan", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "Users")
                        .WithMany("Billans")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Chek", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.BankT", "Bank")
                        .WithMany("Cheks")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accapt.DataLayer.Entities.Users", "User")
                        .WithMany("Cheks")
                        .HasForeignKey("UserId");

                    b.Navigation("Bank");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.DebtorCreditor", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "User")
                        .WithMany("DebtorCreditors")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.EmployeeDeatails", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Epmloyee", "Epmloyee")
                        .WithMany("EmployeeDeatails")
                        .HasForeignKey("EpmloyeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accapt.DataLayer.Entities.Users", null)
                        .WithMany("EmployeeDeatails")
                        .HasForeignKey("UsersId");

                    b.Navigation("Epmloyee");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Epmloyee", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "Users")
                        .WithMany("Employees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Invoice", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "Users")
                        .WithMany("Invoices")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.InvoiceDetails", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "Users")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accapt.DataLayer.Entities.Invoice", "Invoices")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoices");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Pepole", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "User")
                        .WithMany("Pepoles")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Product", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "Users")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.ProviderServiceList", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.ServiceProvider", "ServiceProvider")
                        .WithMany("ProviderServiceLists")
                        .HasForeignKey("ServiceProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accapt.DataLayer.Entities.Users", "User")
                        .WithMany("ProviderServiceLists")
                        .HasForeignKey("UserId");

                    b.Navigation("ServiceProvider");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.SallaryAndCosts", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "User")
                        .WithMany("SallaryAndCosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.ServiceProvider", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.Users", "Users")
                        .WithMany("ServiceProviders")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.VisibleService", b =>
                {
                    b.HasOne("Accapt.DataLayer.Entities.ProviderServiceList", "ProviderServiceList")
                        .WithMany("VisibleServices")
                        .HasForeignKey("ProviderWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accapt.DataLayer.Entities.Users", "User")
                        .WithMany("VisibleServices")
                        .HasForeignKey("UserId");

                    b.Navigation("ProviderServiceList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.BankT", b =>
                {
                    b.Navigation("Cheks");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Epmloyee", b =>
                {
                    b.Navigation("EmployeeDeatails");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.ProviderServiceList", b =>
                {
                    b.Navigation("VisibleServices");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.ServiceProvider", b =>
                {
                    b.Navigation("ProviderServiceLists");
                });

            modelBuilder.Entity("Accapt.DataLayer.Entities.Users", b =>
                {
                    b.Navigation("Banks");

                    b.Navigation("Billans");

                    b.Navigation("Cheks");

                    b.Navigation("DebtorCreditors");

                    b.Navigation("EmployeeDeatails");

                    b.Navigation("Employees");

                    b.Navigation("InvoiceDetails");

                    b.Navigation("Invoices");

                    b.Navigation("Pepoles");

                    b.Navigation("Products");

                    b.Navigation("ProviderServiceLists");

                    b.Navigation("SallaryAndCosts");

                    b.Navigation("ServiceProviders");

                    b.Navigation("VisibleServices");
                });
#pragma warning restore 612, 618
        }
    }
}

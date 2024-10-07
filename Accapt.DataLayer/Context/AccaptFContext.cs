using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Context
{
    public class AccaptFContext : DbContext
    {
        public AccaptFContext(DbContextOptions<AccaptFContext> context) : base(context) 
        {
            
        }

        #region User Table

        public DbSet<Users> Users { get; set; }
        public DbSet<Product> products { get; set; }
        #endregion

        #region Inovices Tabels

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }

        #endregion

        #region Pepole Table

        public DbSet<Pepole> People { get; set; }

        #endregion

        #region Bank Table

        public DbSet<BankT> Banks { get; set; }

        #endregion

        #region Check Table

        public DbSet<Chek> Cheks { get; set; }

        #endregion

        #region Billan Table

        public DbSet<Billan> Billans { get; set; }

        #endregion

        #region DateOfPurchaseTable

        public DbSet<DebtorCreditor> DebtorCreditors { get; set; }

        #endregion

        #region EmployeesYables

        public DbSet<Epmloyee> Employees { get; set; }
        public DbSet<EmployeeDeatails> EmployeeDeatails { get; set; }

        #endregion

        #region SallaryAndCosts

        public DbSet<SallaryAndCosts> SallaryAndCosts { get; set; }

        #endregion

        #region Provider Service

        public DbSet<ProviderServiceList> ProviderServiceLists { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<VisibleService> VisibleServices { get; set; }

        #endregion
    }
}

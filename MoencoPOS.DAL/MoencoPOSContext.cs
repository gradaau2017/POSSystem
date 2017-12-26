using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoencoPOS.Models;

namespace MoencoPOS.DAL
{
    public class MoencoPOSContext : DbContext
    {
        public MoencoPOSContext() : base("MoencoPOSContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Productc> Productcs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
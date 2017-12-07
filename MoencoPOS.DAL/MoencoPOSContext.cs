using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoencoPOS.Models;

namespace MoencoPOS.DAL
{
    public class MoencoPOSContext : DbContext
    {
        public MoencoPOSContext() : base("NcdContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
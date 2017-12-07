using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using NCD.Models;

namespace NationalCriminalsDatabase.DAL
{
    public class NcdContext : DbContext
    {
        public NcdContext() : base("NcdContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseOutcome> CaseOutcomes { get; set; }
        public DbSet<IncidentReport> IncidentReports { get; set; }
        public DbSet<InvestigationOfficer> InvestigationOfficers { get; set; }
        public DbSet<InvestigationOfficerAddress> InvestigationOfficerAddresses { get; set; }
        public DbSet<Petitioner> Petitioners { get; set; }
        public DbSet<PetitionerAddress> PetitionerAddresses { get; set; }
        public DbSet<Suspect> Suspects { get; set; }
        public DbSet<SuspectAddress> SuspectAddresses { get; set; }
        public DbSet<Victim> Victims { get; set; }
        public DbSet<VictimAddress> VictimAddresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
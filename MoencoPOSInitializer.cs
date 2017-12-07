using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NCD.Models;
namespace NCD.DAL
{
    public class NcdInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<NcdContext>
    {
        protected override void Seed(NcdContext context)
        {
            var addresses = new List<Address>
            {
                new Address{AddressId=1,LandLineNo="+251111123456",CellPhoneNo="+251911123456",State="Afar",City="Semera",StreetNo="12345",HouseNo="123"},
                new Address{AddressId=2,LandLineNo="+555555555555",CellPhoneNo="+666666666666",State="Amhara",City="Bahirdar",StreetNo="44444",HouseNo="555"},
            };
            addresses.ForEach(s => context.Addresses.Add(s));
            context.SaveChanges();

            var investigationOfficers = new List<InvestigationOfficer>
            {
                new InvestigationOfficer{InvestigationOfficerId=1,FirstName="Nathnael",MiddleName="Getahun",LastName="Woldemariam",Gender="Male",
                    DateOfBirth =DateTime.Parse("2016-10-09"),Rank="Sergeant",Shift="Day", Salary=10000.00},
                new InvestigationOfficer{InvestigationOfficerId=2,FirstName="Abebech",MiddleName="Kebede",LastName="Bekele",Gender="Female",
                    DateOfBirth =DateTime.Parse("2016-10-09"),Rank="Detective",Shift="Night", Salary=8000.00},
            };
            investigationOfficers.ForEach(s => context.InvestigationOfficers.Add(s));
            context.SaveChanges();

            var investigationOfficerAddresses = new List<InvestigationOfficerAddress>
            {
                new InvestigationOfficerAddress { InvestigationOfficerAddressId=1, InvestigationOfficerId=1, AddressId=1},
                new InvestigationOfficerAddress { InvestigationOfficerAddressId=2, InvestigationOfficerId=2, AddressId=2},
            };
            investigationOfficerAddresses.ForEach(s => context.InvestigationOfficerAddresses.Add(s));
            context.SaveChanges();

            var petitioners = new List<Petitioner>
            {
                new Petitioner { PetitionerId=1, FirstName="Haile", MiddleName="Legend", LastName="Gebresellassie", Gender="Male",
                    DateOfBirth =DateTime.Parse("2016-10-09") },
                new Petitioner { PetitionerId=2, FirstName="Tirunesh", MiddleName="Legend", LastName="Dibaba", Gender="Female",
                    DateOfBirth =DateTime.Parse("2016-10-09") }
            };
            petitioners.ForEach(s => context.Petitioners.Add(s));
            context.SaveChanges();

            var petitionerAddresses = new List<PetitionerAddress>
            {
                new PetitionerAddress { PetitionerAddressId=1, PetitionerId=1, AddressId=1},
                new PetitionerAddress { PetitionerAddressId=2, PetitionerId=2, AddressId=2},
            };
            petitionerAddresses.ForEach(s => context.PetitionerAddresses.Add(s));
            context.SaveChanges();

            var suspects = new List<Suspect>
            {
                new Suspect { SuspectId=1, FirstName="Haile", MiddleName="Legend", LastName="Gebresellassie", Gender="Male", Status="Innocent", Photo="image1.jpg",
                    DateOfBirth =DateTime.Parse("2016-10-09") },
                new Suspect { SuspectId=2, FirstName="Tirunesh", MiddleName="Legend", LastName="Dibaba", Gender="Female", Status="Innocent", Photo="image2.jpg",
                    DateOfBirth =DateTime.Parse("2016-10-09") },
            };
            suspects.ForEach(s => context.Suspects.Add(s));
            context.SaveChanges();

            var suspectAddresses = new List<SuspectAddress>
            {
                new SuspectAddress { SuspectAddressId=1, SuspectId=1, AddressId=1},
                new SuspectAddress { SuspectAddressId=2, SuspectId=2, AddressId=2},
            };
            suspectAddresses.ForEach(s => context.SuspectAddresses.Add(s));
            context.SaveChanges();

            var victims = new List<Victim>
            {
                new Victim { VictimId=1, FirstName="Haile", MiddleName="Legend", LastName="Gebresellassie", Gender="Male",
                    DateOfBirth =DateTime.Parse("2016-10-09") },
                new Victim { VictimId=2, FirstName="Tirunesh", MiddleName="Legend", LastName="Dibaba", Gender="Female", 
                    DateOfBirth =DateTime.Parse("2016-10-09") },
            };
            suspects.ForEach(s => context.Suspects.Add(s));
            context.SaveChanges();

            var victimAddresses = new List<VictimAddress>
            {
                new VictimAddress { VictimAddressId=1, VictimId=1, AddressId=1},
                new VictimAddress { VictimAddressId=2, VictimId=2, AddressId=2},
            };
            victimAddresses.ForEach(s => context.VictimAddresses.Add(s));
            context.SaveChanges();

            var incidentReports = new List<IncidentReport>
            {
                new IncidentReport { IncidentReportId=1,PetitionerId=1,VictimId=1,WhenReported=DateTime.Parse("2016-10-09"),
                    WhenHappened =DateTime.Parse("2016-10-09")},
                new IncidentReport { IncidentReportId=2,PetitionerId=2,VictimId=2,WhenReported=DateTime.Parse("2016-10-09"),
                    WhenHappened =DateTime.Parse("2016-10-09")}
            };
            incidentReports.ForEach(s => context.IncidentReports.Add(s));
            context.SaveChanges();

            var cases = new List<Case>
            {
                new Case{ CaseId=1,InvestigationOfficerId=1,IncidentReportId=1,StartDate=DateTime.Parse("2016-10-09"),EndDate=DateTime.Parse("2016-10-09"),
                    IoReport ="Sample Report", Remark="Sample Remark"},
                new Case{ CaseId=1,InvestigationOfficerId=1,IncidentReportId=1,StartDate=DateTime.Parse("2016-10-09"),EndDate=DateTime.Parse("2016-10-09"),
                    IoReport ="Sample Report", Remark="Sample Remark"},
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var caseOutcomes = new List<CaseOutcome>
            {
                new CaseOutcome { CaseOutcomeId=1,CaseId=1,SuspectId=1,Sentence="Three years without parol",Remark},
                new CaseOutcome { CaseOutcomeId=2,CaseId=2,SuspectId=1,Sentence="Ten years with possible parol",Remark},
            };
            caseOutcomes.ForEach(s=>context.CaseOutcomes.Add(s));
            context.SaveChanges();
        }
    }
}
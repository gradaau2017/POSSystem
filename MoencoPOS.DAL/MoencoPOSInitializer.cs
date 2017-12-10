using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MoencoPOS.Models;

namespace MoencoPOS.DAL
{
    public class MoencoPOSInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MoencoPOSContext>
    {
        protected override void Seed(MoencoPOSContext context)
        {
            var addresses = new List<Address>
            {
                new Address{AddressId=1,LandLineNo="+251111123456",CellPhoneNo="+251911123456",State="Afar",City="Semera",StreetNo="12345",HouseNo="123"},
                new Address{AddressId=2,LandLineNo="+555555555555",CellPhoneNo="+666666666666",State="Amhara",City="Bahirdar",StreetNo="44444",HouseNo="555"},
            };
            addresses.ForEach(s => context.Addresses.Add(s));
            context.SaveChanges();
            
            var salesInvoices = new List<SalesInvoice>
            {
                new SalesInvoice { SalesInvoiceId=1,CustomerId=1,BranchId=1,SalesType=1,UserId=1,DateSold=DateTime.Parse("2016-10-09")},
                new SalesInvoice { SalesInvoiceId=2,CustomerId=2,BranchId=2,SalesType=2,UserId=2,DateSold=DateTime.Parse("2016-10-09")}
            };
            salesInvoices.ForEach(s => context.SalesInvoices.Add(s));
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category { CategoryName="Car",CategoryDescription="Toyota Cars."},
                new Category {CategoryName="Tyres",CategoryDescription="Tyre for diffrent model Toyota Cars."}
            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            var branches = new List<Branch>
            {
                new Branch { BranchName="Main",BranchLocation="Addis Ababa", BranchDescription="Country Main Branch"},
                new Branch {BranchName="South Branch",BranchLocation="Awassa", BranchDescription="SNNP Branch in Awassa"}
            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

        }
    }
}
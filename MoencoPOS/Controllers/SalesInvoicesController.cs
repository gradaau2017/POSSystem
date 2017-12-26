using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoencoPOS.DAL;
using MoencoPOS.Models;
using MoencoPos.Sales.Services;
using MoencoPOS.Models.ViewModels;
using MoencoPOS.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MoencoPos.Product.Services;

namespace MoencoPOS.Controllers
{
    public class SalesInvoicesController : Controller
    {
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        private readonly IProductService _productService;
        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;

        public SalesInvoicesController(ISalesInvoiceService salesInvoiceService, ICustomerService customerService,
            IBranchService branchService, IProductService productService)
        {
            _salesInvoiceService = salesInvoiceService;
            _customerService = customerService;
            _branchService = branchService;
            _productService = productService;

            MyIdentityDbContext db = new MyIdentityDbContext();

            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);

            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
        }

        // GET: SalesInvoices
        public ActionResult Index()
        {
            var salesInvoices = _salesInvoiceService.GetAllSalesInvoices();
            var salesInvoiceViewModels = new List<SalesInvoiceViewModel>();
            foreach (var salesInvoice in salesInvoices)
            {
                var salesInvoiceViewModel = new SalesInvoiceViewModel()
                {
                    BranchId = salesInvoice.BranchId,
                    CustomerId = salesInvoice.CustomerId,
                    CustomerName = salesInvoice.Customer.FirstName + " " + salesInvoice.Customer.LastName,
                    BranchName = salesInvoice.Branch.BranchName,
                    DateSold = salesInvoice.DateSold,
                    ReferenceNo = salesInvoice.ReferenceNo,
                    SalesInvoiceId = salesInvoice.SalesInvoiceId,
                    SalesType = salesInvoice.SalesType,
                    UserId = salesInvoice.UserId,
                    UserName = userManager.FindById(salesInvoice.UserId).FullName
                };
                salesInvoiceViewModels.Add(salesInvoiceViewModel);
            }
            return View(salesInvoiceViewModels);
        }

        // GET: SalesInvoices/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesInvoice salesInvoice = _salesInvoiceService.FindById(id);
            if (salesInvoice == null)
            {
                return HttpNotFound();
            }
            return View(salesInvoice);
        }

        // GET: SalesInvoices/Create
        public ActionResult Create(int? id)
        {
            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

            if (id!=null && id!=0)
            {
                var salesInvoice = _salesInvoiceService.Get(t => t.SalesInvoiceId == id, null, "SalesLineItems").FirstOrDefault();
                var salesInvoiceViewModel = new SalesInvoiceViewModel()
                {
                    BranchId = salesInvoice.BranchId,
                    CustomerId = salesInvoice.CustomerId,
                    CustomerName = salesInvoice.Customer.FirstName + " " + salesInvoice.Customer.LastName,
                    BranchName = salesInvoice.Branch.BranchName,
                    DateSold = salesInvoice.DateSold,
                    ReferenceNo = salesInvoice.ReferenceNo,
                    SalesInvoiceId = salesInvoice.SalesInvoiceId,
                    SalesType = salesInvoice.SalesType,
                    UserId = salesInvoice.UserId,
                    UserName = userManager.FindById(salesInvoice.UserId).FullName
                };
                ViewBag.UserName = user.FullName;
                ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
                ViewBag.CustomerList = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName");
                ViewBag.SalesInvoiceId = salesInvoice.SalesInvoiceId;
                var salesLineItemviewModels = new List<SalesInvoiceLineItemViewModel>();
                foreach (var salesLineItem in salesInvoice.SalesLineItems)
                {
                    var salesLineItemviewModel = new SalesInvoiceLineItemViewModel()
                    {
                        ProductId = salesLineItem.ProductId,
                        Productname = _productService.FindById(salesLineItem.ProductId).ProductName,
                        Quantity = salesLineItem.Quantity,
                        UnitPrice = salesLineItem.UnitPrice
                    };
                    salesLineItemviewModels.Add(salesLineItemviewModel);
                }
                ViewData["CustomerList"] = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName", salesInvoice.CustomerId);
                ViewData["ProductList"] = new SelectList(_productService.GetAllProducts(), "ProductcId", "ProductName", salesInvoice.CustomerId);
                ViewBag.SalesLineItemViewModels = salesLineItemviewModels;
                ViewBag.UserName = user.FullName;
                ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
                ViewBag.SalesType = salesInvoice.SalesType;
                return View(salesInvoiceViewModel);
            }

            
            ViewBag.UserName = user.FullName;
            ViewBag.BranchName = _branchService.FindById(user.BranchId).BranchName;
            ViewBag.CustomerList = new SelectList(_customerService.GetAllCustomers(), "CustomerId", "FirstName");
            return View();
        }

        // POST: SalesInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,SalesType,ReferenceNo")] SalesInvoiceViewModel salesInvoiceViewModel, int CustomerList, int SalesTypeList)
        {
            if (ModelState.IsValid)
            {
                var exists = _salesInvoiceService.Get(t => t.ReferenceNo == salesInvoiceViewModel.ReferenceNo).FirstOrDefault();
                if (exists != null)
                    return View(salesInvoiceViewModel);

                MyIdentityDbContext db = new MyIdentityDbContext();
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
                UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

                var salesInvoice = new SalesInvoice() {
                    BranchId = user.BranchId,
                    CustomerId = CustomerList,
                    ReferenceNo = salesInvoiceViewModel.ReferenceNo,
                    SalesType = SalesTypeList,
                    UserId = user.Id,
                    DateSold = DateTime.Now
                };
                
                _salesInvoiceService.AddSalesInvoice(salesInvoice);
                
                return RedirectToAction("Create", "SalesInvoices", new { id = salesInvoice.SalesInvoiceId });
            }
            
            return View(salesInvoiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLineItem([Bind(Include = "SalesInvoiceId,Quantity,ProductId")] SalesInvoiceLineItemViewModel salesInvoiceLineItemViewModel)
        {
            var salesInvoice = _salesInvoiceService.Get(t => t.SalesInvoiceId == salesInvoiceLineItemViewModel.SalesInvoiceId,null, "SalesLineItems").FirstOrDefault();
            int exists = salesInvoice.SalesLineItems.Where(t => t.ProductId == salesInvoiceLineItemViewModel.ProductId).ToList().Count;
            if(exists > 0)
            {
                return RedirectToAction("Create", "SalesInvoices", new { id = salesInvoice.SalesInvoiceId });
            }
            var salesInvoiceLineItem = new SalesLineItem()
            {
                ProductId = salesInvoiceLineItemViewModel.ProductId,
                Quantity = salesInvoiceLineItemViewModel.Quantity,
                SalesInvoiceId = salesInvoiceLineItemViewModel.SalesInvoiceId,
                UnitPrice = _productService.FindById(salesInvoiceLineItemViewModel.ProductId).UnitPrice
            };
            salesInvoice.SalesLineItems.Add(salesInvoiceLineItem);
            _salesInvoiceService.EditSalesInvoice(salesInvoice);
            return RedirectToAction("Create", "SalesInvoices", new { id = salesInvoice.SalesInvoiceId });
        }

        // GET: SalesInvoices/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesInvoice salesInvoice = _salesInvoiceService.FindById(id);
            if (salesInvoice == null)
            {
                return HttpNotFound();
            }
            return View(salesInvoice);
        }

        // POST: SalesInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesInvoiceId,CustomerId,BranchId,SalesType,UserId,DateSold")] SalesInvoice salesInvoice)
        {
            if (ModelState.IsValid)
            {
                _salesInvoiceService.EditSalesInvoice(salesInvoice);
                return RedirectToAction("Index");
            }
            return View(salesInvoice);
        }

        // POST: SalesInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesInvoice salesInvoice = _salesInvoiceService.FindById(id);
            _salesInvoiceService.DeleteSalesInvoice(salesInvoice);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var salesInvoice = _salesInvoiceService.FindById(id);
            _salesInvoiceService.DeleteSalesInvoice(salesInvoice);
            return RedirectToAction("Index", "SalesInvoices");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salesInvoiceService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

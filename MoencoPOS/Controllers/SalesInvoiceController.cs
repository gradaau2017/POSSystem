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
using MoencoPOS.Services;

namespace MoencoPOS.Controllers
{
    public class SalesController : Controller
    {
        private MoencoPOSContext db = new MoencoPOSContext();

        private readonly ISalesInvoiceService _salesService;

        public SalesController(ISalesInvoiceService salesService)
        {
            _salesService = salesService;
        }

        // GET: Sales
        public ActionResult Index()
        {
            return View(_salesService.GetAllSalesInvoices().ToList());
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

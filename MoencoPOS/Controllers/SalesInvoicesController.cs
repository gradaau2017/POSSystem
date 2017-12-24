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

namespace MoencoPOS.Controllers
{
    public class SalesInvoicesController : Controller
    {
        private MoencoPOSContext db = new MoencoPOSContext();

        // GET: SalesInvoices
        public ActionResult Index()
        {
            return View(db.SalesInvoices.ToList());
        }

        // GET: SalesInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesInvoice salesInvoice = db.SalesInvoices.Find(id);
            if (salesInvoice == null)
            {
                return HttpNotFound();
            }
            return View(salesInvoice);
        }

        // GET: SalesInvoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesInvoiceId,CustomerId,BranchId,SalesType,UserId,DateSold")] SalesInvoice salesInvoice)
        {
            if (ModelState.IsValid)
            {
                db.SalesInvoices.Add(salesInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salesInvoice);
        }

        // GET: SalesInvoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesInvoice salesInvoice = db.SalesInvoices.Find(id);
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
                db.Entry(salesInvoice).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesInvoice);
        }

        // GET: SalesInvoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesInvoice salesInvoice = db.SalesInvoices.Find(id);
            if (salesInvoice == null)
            {
                return HttpNotFound();
            }
            return View(salesInvoice);
        }

        // POST: SalesInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesInvoice salesInvoice = db.SalesInvoices.Find(id);
            db.SalesInvoices.Remove(salesInvoice);
            db.SaveChanges();
            return RedirectToAction("Index");
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

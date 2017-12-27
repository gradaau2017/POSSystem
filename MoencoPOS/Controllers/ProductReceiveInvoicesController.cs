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
    public class ProductReceiveInvoicesController : Controller
    {
        private MoencoPOSContext db = new MoencoPOSContext();

        // GET: ProductReceiveInvoices
        public ActionResult Index()
        {
            return View(db.ProductReceiveInvoices.ToList());
        }

        // GET: ProductReceiveInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReceiveInvoice productReceiveInvoice = db.ProductReceiveInvoices.Find(id);
            if (productReceiveInvoice == null)
            {
                return HttpNotFound();
            }
            return View(productReceiveInvoice);
        }

        // GET: ProductReceiveInvoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductReceiveInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductReceiveInvoiceId,DateReceived,BranchId,UserId")] ProductReceiveInvoice productReceiveInvoice)
        {
            if (ModelState.IsValid)
            {
                db.ProductReceiveInvoices.Add(productReceiveInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productReceiveInvoice);
        }

        // GET: ProductReceiveInvoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReceiveInvoice productReceiveInvoice = db.ProductReceiveInvoices.Find(id);
            if (productReceiveInvoice == null)
            {
                return HttpNotFound();
            }
            return View(productReceiveInvoice);
        }

        // POST: ProductReceiveInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductReceiveInvoiceId,DateReceived,BranchId,UserId")] ProductReceiveInvoice productReceiveInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productReceiveInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productReceiveInvoice);
        }

        // GET: ProductReceiveInvoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReceiveInvoice productReceiveInvoice = db.ProductReceiveInvoices.Find(id);
            if (productReceiveInvoice == null)
            {
                return HttpNotFound();
            }
            return View(productReceiveInvoice);
        }

        // POST: ProductReceiveInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductReceiveInvoice productReceiveInvoice = db.ProductReceiveInvoices.Find(id);
            db.ProductReceiveInvoices.Remove(productReceiveInvoice);
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

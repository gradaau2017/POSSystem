﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoencoPOS.Models;
using MoencoPos.Product.Services;

namespace MoencoPOS.Controllers
{
    public class ProductcsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductcsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Productcs
        public ActionResult Index()
        {
            return View(_productService.GetAllProducts().ToList());
        }

        // GET: Productcs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productc productc = _productService.FindById(id.Value);
            if (productc == null)
            {
                return HttpNotFound();
            }
            return View(productc);
        }

        // GET: Productcs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Productcs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductName,ProductDescription,Model,UnitOfMeasure,UnitCost,UnitPrice,CategoryId")] Productc productc)
        {
            if (ModelState.IsValid)
            {
                if(_productService.AddProduct(productc))
                    return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories(), "CategoryId", "CategoryName", productc.CategoryId);
            return View(productc);
        }

        // GET: Productcs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productc productc =_productService.FindById(id.Value);
            if (productc == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories(), "CategoryId", "CategoryName", productc.CategoryId);
            return View(productc);
        }

        // POST: Productcs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductcId,ProductName,ProductDescription,Model,UnitOfMeasure,UnitCost,UnitPrice,CategoryId")] Productc productc)
        {
            if (ModelState.IsValid)
            {
                if(_productService.EditProduct(productc))
                    return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories(), "CategoryId", "CategoryName", productc.CategoryId);
            return View(productc);
        }

        // GET: Productcs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productc productc = _productService.FindById(id.Value);
            if (productc == null)
            {
                return HttpNotFound();
            }
            return View(productc);
        }

        // POST: Productcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productc productc = _productService.FindById(id);
            _productService.DeleteProduct(productc);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _categoryService.Dispose();
                _productService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyManage.Models;

namespace EasyManage.Controllers
{
    public class Order1Controller : Controller
    {
        private ecomManageEntities db = new ecomManageEntities();

        // GET: Order1
        public ActionResult Index()
        {
            var order1 = db.Order1.Include(o => o.Invoice).Include(o => o.Product);
            return View(order1.ToList());
        }

        // GET: Order1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order1 order1 = db.Order1.Find(id);
            if (order1 == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderId = id.Value;

            var comments = db.Comments.Where(d => d.OrderId.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.Comments.Where(d => d.OrderId.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }


            return View(order1);
        }

        // GET: Order1/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "ID");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: Order1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InvoiceID,ProductID,O_Date,Quantity,O_bill,O_UnitPrice")] Order1 order1)
        {
            if (ModelState.IsValid)
            {
                db.Order1.Add(order1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "ID", order1.InvoiceID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order1.ProductID);
            return View(order1);
        }

        // GET: Order1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order1 order1 = db.Order1.Find(id);
            if (order1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "ID", order1.InvoiceID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order1.ProductID);
            return View(order1);
        }

        // POST: Order1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InvoiceID,ProductID,O_Date,Quantity,O_bill,O_UnitPrice")] Order1 order1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "ID", order1.InvoiceID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order1.ProductID);
            return View(order1);
        }

        // GET: Order1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order1 order1 = db.Order1.Find(id);
            if (order1 == null)
            {
                return HttpNotFound();
            }
            return View(order1);
        }

        // POST: Order1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order1 order1 = db.Order1.Find(id);
            db.Order1.Remove(order1);
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

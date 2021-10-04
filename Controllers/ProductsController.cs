using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyManage.Models;

namespace EasyManage.Controllers
{
    public class ProductsController : Controller
    {
        private ecomManageEntities db = new ecomManageEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductType);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductPrice,ProductDetails,ProductTypeId,productImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeID", "ProductTypeName", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeID", "ProductTypeName", product.ProductTypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Product imageModel)
        {
            

            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.productImage = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);


            if (ModelState.IsValid)
            {
                db.Entry(imageModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeID", "ProductTypeName", imageModel.ProductTypeId);
            return View(imageModel);
        }


        //public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductPrice,ProductDetails,ProductTypeId,productImage")] Product product)
        //{
        //    Product imageModel;

        //    string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
        //    string extension = Path.GetExtension(imageModel.ImageFile.FileName);
        //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //    imageModel.productImage = "~/Image/" + fileName;
        //    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
        //    imageModel.ImageFile.SaveAs(fileName);


        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeID", "ProductTypeName", product.ProductTypeId);
        //    return View(product);
        //}

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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



        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeID", "ProductTypeName");
            return View();
            //return View();
        }

        [HttpPost]
        public ActionResult Add(Product imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.productImage = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);


            if (ModelState.IsValid)
            {
                db.Products.Add(imageModel);
                db.SaveChanges();
                //  return RedirectToAction("Index");
            }
            ModelState.Clear();
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeID", "ProductTypeName", imageModel.ProductTypeId);
            return View(imageModel);


            //  ModelState.Clear();
            // return View();
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            Product imageModel = new Product();
            imageModel = db.Products.Where(x => x.ProductID == id).FirstOrDefault();

            if (imageModel == null)
            {
                return HttpNotFound();
            }

            return View(imageModel);
        }
    }
}






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
    public class AdminsController : Controller
    {
        private ecomManageEntities db = new ecomManageEntities();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminID,AdminEmail,Password,ConfirmPassword")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminID,AdminEmail,Password,ConfirmPassword")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(TempUser tmpuser)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Admins.Where(u => u.AdminEmail.Equals(tmpuser.UserEmail) && u.Password.Equals(tmpuser.UserPassword)).FirstOrDefault();

                if (admin != null)
                {

                    Session["admin_id"] = admin.AdminID;
                     //return RedirectToAction("DashBoard" , new { ID = admin.AdminID });

                    return RedirectToAction("DashBoard");
                }
                else
                {

                    ViewBag.msg = "User not found or Password mismatched";

                    return View();
                }
            }

            return View();
        }

       

        //[HttpGet]
        public ActionResult Dashboard()
        {
            int ID = (int)Session["admin_id"];

            var admin = db.Admins.Where(a => a.AdminID.Equals(ID)).FirstOrDefault();

            ViewBag.Admins = admin;
            return View(admin);
        }


        [HttpGet]
        public ActionResult ManageProduct()
        {

            return View();
        }

        public ActionResult Logout() {
            Session.Abandon();
            return RedirectToAction("Login","Admins");
        }

        public ActionResult Orders() {

            return View(db.Order1.ToList());
        }

    }
}

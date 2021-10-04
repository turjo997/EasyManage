using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyManage.Models;


namespace EasyManage.Controllers
{
    public class UsersController : Controller
    {
        private ecomManageEntities db = new ecomManageEntities();

        private List<ManageProduct> manageProducts = new List<ManageProduct>();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserEmail,UserPassword,ConfirmPassword,UserName,UserAddress,UserContact")] User user)
        {

            var isExist = IsEmailExist(user.UserEmail);
            if (isExist)
            {
                ViewBag.Createmsg = "Email already exist";

                //ModelState.AddModelError("EmailExist", "Email already exist");
                return View(user);
            }

            user.UserPassword = Crypto.Hash(user.UserPassword);
            user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);



            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                ViewBag.Createmsg = "Successfully Created Account";
                return RedirectToAction("Login");
            }

            return View(user);
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (ecomManageEntities dc = new ecomManageEntities())
            {
                var v = dc.Users.Where(a => a.UserEmail == emailID).FirstOrDefault();
                return v != null;
            }
        }


        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserEmail,UserPassword,ConfirmPassword,UserName,UserAddress,UserContact")] User user)
        {
            user.UserPassword = Crypto.Hash(user.UserPassword);
            user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);



            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.editmsg = "Successfully Updated";

                return RedirectToAction("Edit");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        public ActionResult Login(Userlogin login)
        {
            var user = db.Users.Where(u => u.UserEmail.Equals(login.UserEmail)).FirstOrDefault();

            if (user != null) {

                if (string.Compare(Crypto.Hash(login.UserPassword), user.UserPassword) == 0)
                {
                    Session["user_id"] = user.UserID;
                    // Session["order_id"] = Order1.Id;
                    return RedirectToAction("Index1");
                }
                else {

                    ViewBag.msg = "User not found or Password mismatched";
                    return View();
                }


            }
            else {
                ViewBag.msg = "User not found or Password mismatched";
                return View();
            }


            //  int pass = string.Compare(Crypto.Hash(login.UserPassword), v.UserPassword);


            /*
                if (ModelState.IsValid)
               {
                var user = db.Users.Where(u => u.UserEmail.Equals(login.UserEmail) && u.UserPassword.Equals(login.UserPassword)).FirstOrDefault();

                if (user != null)
                {
                    Session["user_id"] = user.UserID;
                   // Session["order_id"] = Order1.Id;
                    return RedirectToAction("Index1");
                   
                }
                else
                {
                    ViewBag.msg = "User not found or Password mismatched";
                    return View();
                }*/


            // }

            return View();
        }

        public ActionResult UserProfile(int ID)
        {
            var user = db.Users.Where(a => a.UserID.Equals(ID)).FirstOrDefault();
            return View(user);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Users");
        }


        //[HttpGet]
        //public ActionResult Forgotpass(ForgotPass forgot)
        //{
        //    var isExist = IsEmailExist(forgot.username);

        //    if (!isExist)
        //    {
        //        // ViewBag.forgotmsg = "Email doesn't exist";

        //        return HttpNotFound();
        //    }

        //    User user = db.Users.Find(forgot.username);

        //    int id = user.UserID;

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user1 = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //  ///  User user;

        //    return View();
        //}
        // [HttpPost]
        //[NonAction]
        [HttpPost]
        public ActionResult Forgotpass(ForgotPass forgot , User user)
        {
            var isExist = IsEmailExist(forgot.username);

            ViewBag.forgotmsg = "";

            if (!isExist)
            {
                ViewBag.forgotmsg = "Email doesn't exist";

                //ModelState.AddModelError("EmailExist", "Email already exist");
                return View();
            }

            string pass = forgot.password;
            string conpass = forgot.confirmpassword;


            user.UserPassword = Crypto.Hash(pass);
            user.ConfirmPassword = Crypto.Hash(conpass);



          //  var data = db.Users.Where(d => d.UserEmail.Equals(forgot.username)).FirstOrDefault();


            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.forgotmsg = "Successfully Updated Password";

                
                return RedirectToAction("Login");
            }
            

            return View();

        }

        public ActionResult ProductView()
        {

            var products = db.Products.Include(p => p.ProductType);
            return View(products.ToList());
        }

        public ActionResult Main() {
            return View(db.Products.OrderByDescending(x => x.ProductID).ToList());
        }

        public ActionResult Index1()
        {
            //Session["u_id"] = 2;
            int ID = (int)Session["user_id"];

            var user = db.Users.Where(a => a.UserID.Equals(ID)).FirstOrDefault();

       //     var inv = db.Invoices.Where(a => a.UserID.Equals(ID)).FirstOrDefault();

         //   int ID1 = inv.ID;

            var order = db.Order1.Where(a => a.ID.Equals(db.Order1.Max(o=>o.ID))).FirstOrDefault();

            ViewBag.order = order;


            //List<User> users = db.Users.Where(u => u.ProductTypeId == product.ProductTypeId).Take(10).ToList<Product>();

            ViewBag.Users = user;


            if (TempData["cart"] != null)
            {
                float x = 0;
                List<ManageProduct> li2 = TempData["cart"] as List<ManageProduct>;
                foreach (var item in li2)
                {
                    x += item.bill;

                }

                TempData["total"] = x;
            }

            TempData.Keep();
            return View(db.Products.OrderByDescending(x => x.ProductID).ToList());
        }

        public ActionResult Adtocart(int? Id)
        {

            Product p = db.Products.Where(x => x.ProductID == Id).SingleOrDefault();
            return View(p);
        }

        List<ManageProduct> li = new List<ManageProduct>();
        [HttpPost]
        public ActionResult Adtocart(Product pi, string qty, int Id)
        {
            Product p = db.Products.Where(x => x.ProductID == Id).SingleOrDefault();

            ManageProduct c = new ManageProduct();
            c.productid = p.ProductID;
            c.price = (float)p.ProductPrice;
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            c.productname = p.ProductName;


            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;

            }
            else
            {
                List<ManageProduct> li2 = TempData["cart"] as List<ManageProduct>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.productid == c.productid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;

                    }

                }
                if (flag == 0)
                {
                    li2.Add(c);
                }

                TempData["cart"] = li2;
            }

            TempData.Keep();

            return RedirectToAction("Index1");
        }
        public ActionResult checkout()
        {
            TempData.Keep();
            return View();
        }
        [HttpPost]

        public ActionResult checkout(Order1 o)
        {
            int ID = (int)Session["user_id"];
          //  Session["u_id"] = 1;
            List<ManageProduct> li = TempData["cart"] as List<ManageProduct>;
            Invoice inv = new Invoice();
            inv.UserID = Convert.ToInt32(ID.ToString());
            inv.Date = System.DateTime.Now;
            inv.Totalbill = (float)(TempData["total"]);
            db.Invoices.Add(inv);
            db.SaveChanges();
            Order1 od = new Order1();
            foreach (var item in li)
            {
                //Order1 od = new Order1();
                od.ProductID = item.productid;
                od.InvoiceID = inv.ID;
                od.O_Date = System.DateTime.Now;
                od.Quantity = (short)item.qty;
                od.O_UnitPrice = (int)item.price;
                od.O_bill = item.bill;

                db.Order1.Add(od);
                db.SaveChanges();
            }
            TempData.Remove("total");
            TempData.Remove("cart");

           // var productName = od.ProductID;
           // var price = od.O_bill;

  

            TempData["msg"] = "Transaction has been completed";
            TempData.Keep();
            return RedirectToAction("Index1");
        }

        
        [HttpGet]
        public ActionResult Remove(int? id) 
        {

            li = TempData["cart"] as List<ManageProduct>;
            ManageProduct c = li.Where(x => x.productid == id).SingleOrDefault();
            li.Remove(c);

            float h = 0;
            foreach (var item in li)
            {
                h += item.bill;

            }
            TempData["total"] = h;
            TempData.Keep();
            return RedirectToAction("checkout");

        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserInformationController : Controller
    {
        private PoleInfoEntities db = new PoleInfoEntities();

        // GET: /UserInformation/
        public ActionResult Index()
        {
            var userinformations = db.UserInformations.Include(u => u.AspNetRole).Include(u => u.AspNetUser);
            return View(userinformations.ToList());
        }

        // GET: /UserInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userinformation = db.UserInformations.Find(id);
            if (userinformation == null)
            {
                return HttpNotFound();
            }
            return View(userinformation);
        }

        // GET: /UserInformation/Create
        public ActionResult Create()
        {
            ViewBag.AspNetRoleID = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: /UserInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInformation userinformation)
        {
            if (ModelState.IsValid)
            {
                db.UserInformations.Add(userinformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetRoleID = new SelectList(db.AspNetRoles, "Id", "Name", userinformation.AspNetRoleID);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", userinformation.AspNetUserId);
            return View(userinformation);
        }

        // GET: /UserInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userinformation = db.UserInformations.Find(id);
            if (userinformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetRoleID = new SelectList(db.AspNetRoles, "Id", "Name", userinformation.AspNetRoleID);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", userinformation.AspNetUserId);
            return View(userinformation);
        }

        // POST: /UserInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,AspNetUserId,AspNetRoleID,UserName,Email,PhoneNumber,InsertedOn,UpdatedOn,IsActive")] UserInformation userinformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userinformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetRoleID = new SelectList(db.AspNetRoles, "Id", "Name", userinformation.AspNetRoleID);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", userinformation.AspNetUserId);
            return View(userinformation);
        }

        // GET: /UserInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInformation userinformation = db.UserInformations.Find(id);
            if (userinformation == null)
            {
                return HttpNotFound();
            }
            return View(userinformation);
        }

        // POST: /UserInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInformation userinformation = db.UserInformations.Find(id);
            db.UserInformations.Remove(userinformation);
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


        /////Method
        public int  Save(UserInformation userinformation)
        {
            int id = 0;
            if (ModelState.IsValid)
            {
                db.UserInformations.Add(userinformation);
                id=db.SaveChanges();
            }
            return id;
        }


        public IEnumerable<UserInformation> GetDropdownData()
        {
            return db.UserInformations.Where(p =>p.IsActive == true || p.IsActive == null).ToList();
        }

        public UserInformation GetUserById(int Id)
        {
            return db.UserInformations.Where(p => p.Id == Id).FirstOrDefault();
        }

        public UserInformation GetUserByAspNetUserId(string AspNetUserId)
        {
            return db.UserInformations.Where(p => p.AspNetUserId == AspNetUserId).FirstOrDefault();
        }

        public IEnumerable<UserInformation> GetDropdownData(string AspNetUserId)
        {
            var userInfo = new UserInformationController().GetUserByAspNetUserId(AspNetUserId);
            //db.UserInformations.Where(p =>p.IsActive == true || p.IsActive == null).ToList()
            var data = (from p in db.UserInformations
                        where (userInfo.UserTypeID == 1 || p.Id == userInfo.Id)
                        select p).ToList();

            return data;
        }


    }
}

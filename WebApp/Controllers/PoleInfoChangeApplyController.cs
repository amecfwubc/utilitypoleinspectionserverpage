using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using Microsoft.AspNet.Identity;
namespace WebApp.Controllers
{
    public class PoleInfoChangeApplyController : Controller
    {
        private UserInformation userInfo { get; set; }
        //
        // GET: /PoleInfoChangeApply/
        public ActionResult Index()
        {
            userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            var data = GetSearchData();
            return View(data);
        }

        //
        // GET: /PoleInfoChangeApply/Details/5
        public ActionResult Details(int id)
        {
            PoleInfoEntities objE = new PoleInfoEntities();
            var data = (from p in objE.PoleInfoChangeApplies.Where(p => p.ID == id)
                        join t in objE.PoleTypes on p.TypeID equals t.ID
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TypeName = t.TypeName,
                            TaskAddeddate = p.TaskAddeddate,
                            TaskPerformeddate = p.TaskPerformeddate,
                            ImageMapPath = p.ImageMapPath,
                            AdjacentPoleHeight = p.AdjacentPoleHeight,
                            TransFormerLoading = p.TransFormerLoading,
                            Notes = p.Notes,
                            ImagesTakenpath = p.ImagesTakenpath
                        }).SingleOrDefault();
            return View(data);
        }

        //
        // GET: /PoleInfoChangeApply/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PoleInfoChangeApply/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PoleInfoChangeApply/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PoleInfoChangeApply/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PoleInfoChangeApply/Delete/5
        public ActionResult Delete(int id)
        {
            PoleInfoEntities objE = new PoleInfoEntities();
            var data = (from p in objE.PoleInfoChangeApplies.Where(p => p.ID == id)
                        join t in objE.PoleTypes on p.TypeID equals t.ID
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TypeName = t.TypeName,
                            TaskAddeddate = p.TaskAddeddate,
                            TaskPerformeddate = p.TaskPerformeddate,
                            ImageMapPath = p.ImageMapPath,
                            AdjacentPoleHeight = p.AdjacentPoleHeight,
                            TransFormerLoading = p.TransFormerLoading,
                            Notes = p.Notes,
                            ImagesTakenpath = p.ImagesTakenpath
                        }).SingleOrDefault();
            return View(data);
        }

        //
        // POST: /PoleInfoChangeApply/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here


                PoleInfoEntities objE = new PoleInfoEntities();


                var objPoleInfo = objE.PoleInfoChangeApplies.Find(id);
                objE.PoleInfoChangeApplies.Remove(objPoleInfo);
                objE.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable GetSearchData()
        {
            //.Where(p => p.IsChanged == null || p.IsChanged == false)
            PoleInfoEntities objE = new PoleInfoEntities();
            var data = (from p in objE.PoleInfoChangeApplies
                        join t in objE.PoleTypes on p.TypeID equals t.ID into typegroup
                        join u in objE.UserInformations on p.TaskAssainUserID equals u.Id into asinGroup
                        from type in typegroup.DefaultIfEmpty()
                        from asain in asinGroup.DefaultIfEmpty()
                        where (userInfo.UserTypeID == 1 || (p.UserID == userInfo.Id))
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TypeName = type.TypeName,
                            TaskAssainUserID = p.TaskAssainUserID,
                            UserFullName = asain.UserFullName,
                            TaskAddeddate = p.TaskAddeddate,
                            TaskPerformeddate = p.TaskPerformeddate,
                            ImageMapPath = p.ImageMapPath,
                            AdjacentPoleHeight = p.AdjacentPoleHeight,
                            TransFormerLoading = p.TransFormerLoading,
                            Notes = p.Notes,
                            ImagesTakenpath = p.ImagesTakenpath
                        }).ToList().OrderByDescending(p => p.ID);
            //PoleInfoViewModel
            return data;
        }
    }
}

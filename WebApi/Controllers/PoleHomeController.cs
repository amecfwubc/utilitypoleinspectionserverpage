using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class PoleHomeController : Controller
    {
        // GET: PoleHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: PoleHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PoleHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoleHome/Create
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

        // GET: PoleHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PoleHome/Edit/5
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

        // GET: PoleHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PoleHome/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

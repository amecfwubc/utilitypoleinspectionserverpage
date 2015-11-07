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
using Microsoft.AspNet.Identity;
using System.IO;
using System.Drawing;

namespace WebApp.Controllers
{
    public class PoleImageController : Controller
    {
        private PoleInfoEntities db = new PoleInfoEntities();
        private UserInformation userInfo { get; set; }
        // GET: /PoleImage/
        public ActionResult Index()
        {
            //TempData["userInfo"] = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            //userInfo = TempData["userInfo"] as UserInformation ;
            userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            //var poleimages = db.PoleImages.Include(p => p.PoleInfo).Include(p => p.UserInformation);
            List<PoleImageViewModel> poleimglist = new List<PoleImageViewModel>();
            //foreach(var itm in poleimages)
            //{
            //    PoleImageViewModel obj = new PoleImageViewModel();

            //}

            var data = (from p in db.PoleImages
                        join pt in db.PoleInfoes on p.PoleInfoID equals pt.ID
                        where (userInfo.UserTypeID == 1 || p.UserID == userInfo.Id)
                        select new PoleImageViewModel
                        {
                            ID = p.ID,
                            PoleInfoID=(int)p.PoleInfoID,
                            ImageMapPath=p.ImageMapPath,
                            //ImageBase64=PoleClass.ImageToBase64(p.ImageMapPath),
                            Notes=p.Notes,
                            PoleID = pt.PoleID

                        }).ToList().OrderByDescending(p => p.ID);

          

            return View(data);
        }

        // GET: /PoleImage/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoleImage poleimage = db.PoleImages.Find(id);
            if (poleimage == null)
            {
                return HttpNotFound();
            }
            return View(poleimage);
        }

        // GET: /PoleImage/Create
        public ActionResult Create()
        {

            ViewBag.PoleInfoID = new SelectList(GetPoleIDList(), "ID", "PoleID");
            ViewBag.UserID = new SelectList(db.UserInformations, "Id", "UserFullName");
            return View();
        }

        // POST: /PoleImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PoleImage poleimage, FormCollection collection)
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
            }
            //, IEnumerable files
            //var userbasic = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            userInfo = TempData["userInfo"] as UserInformation;
            if (ModelState.IsValid)
            {
                poleimage.UserID = userInfo.Id;
                //db.PoleImages.Add(poleimage);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PoleInfoID = new SelectList(GetPoleIDList(), "ID", "PoleID", poleimage.PoleInfoID);
            ViewBag.UserID = new SelectList(db.UserInformations, "Id", "UserFullName", poleimage.UserID);
            return View(poleimage);
        }

        // GET: /PoleImage/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoleImage poleimage = db.PoleImages.Find(id);
            if (poleimage == null)
            {
                return HttpNotFound();
            }
            ViewBag.PoleInfoID = new SelectList(db.PoleInfoes, "ID", "PoleID", poleimage.PoleInfoID);
            ViewBag.UserID = new SelectList(db.UserInformations, "Id", "UserFullName", poleimage.UserID);
            return View(poleimage);
        }

        // POST: /PoleImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PoleInfoID,ImageMapPath,UserID,Notes,CreateDate,UpdateDate")] PoleImage poleimage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poleimage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PoleInfoID = new SelectList(db.PoleInfoes, "ID", "PoleID", poleimage.PoleInfoID);
            ViewBag.UserID = new SelectList(db.UserInformations, "Id", "UserFullName", poleimage.UserID);
            return View(poleimage);
        }

        // GET: /PoleImage/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoleImage poleimage = db.PoleImages.Find(id);
            if (poleimage == null)
            {
                return HttpNotFound();
            }
            return View(poleimage);
        }

        // POST: /PoleImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PoleImage poleimage = db.PoleImages.Find(id);
            db.PoleImages.Remove(poleimage);
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

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable files, PoleImage poleimage)
        {
            string TempPath = Server.MapPath("~/Content/TempFile/");
            userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            //List<PoleImage> objlist = new List<PoleImage>();
            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Path.Combine(TempPath, file.FileName);
                //System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));

                //PoleImage obj = new PoleImage();
                poleimage.UserID = userInfo.Id;
                //db.PoleImages.Add(poleimage);
                //db.SaveChanges();
            }

            return Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        public ActionResult SaveUploadedFile()        
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = Guid.NewGuid().ToString(); //file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new System.IO.DirectoryInfo(string.Format("{0}Images\\uploaded", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                        var fileName1 = System.IO.Path.GetFileName(file.FileName);
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        var path = string.Format("{0}\\{1}", pathString, file.FileName);

                        //if (System.IO.File.Exists(path))
                        //{

                        //    System.IO.File.Delete(path);
                        //}

                        //file.SaveAs(path);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
                return Json(new { Message = fName });
            else
                return Json(new { Message = "Error in saving file" });
        }

        public ActionResult SaveUploadedPoleImage(List<PoleImageList> obj)
        {
            try
            {
                if(obj==null)
                {
                    return Json(new { Success = 3, ID = 0, ex = "" });
                }

                string PoleID="";
                PoleInfoEntities objE = new PoleInfoEntities();
                var FileSavePathList = objE.FileSavePaths.ToList();
                string _savepath = FileSavePathList.SingleOrDefault(p => p.FileFor == "PoleImageMapPath").FilePath;
                userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
                foreach (var itm in obj)
                {
                    var id = itm.ID;
                    PoleID = itm.PoleID;
                    var Imgae64 = itm.Image64;

                    Image image = PoleClass.Base64ToImage(Imgae64);

                    string ImageMapPath = _savepath + itm.PoleID.ToString().Trim() + "_MapImage"+itm.ID.ToString() + ".jpg";

                    //string imgname = PoleID+"_img_"+id.ToString()+".jpg";
                    image.Save(ImageMapPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    PoleImage poleimage = new PoleImage();
                    poleimage.UserID = userInfo.Id;
                    poleimage.PoleInfoID = itm.PoleInfoID;
                    poleimage.ImageMapPath = ImageMapPath;
                    poleimage.CreateDate = DateTime.Now;

                    db.PoleImages.Add(poleimage);
                    db.SaveChanges();

                }
                //var msg = "Saved Successfully Completed";
                //return Content(msg, "text/html");
                return Json(new { Success = 1, ID = PoleID, ex = "" });
                 
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, ex = ex.Message.ToString() });
                //throw new HttpException(500, ex.Message);
            }

            //return Json(new { Success = 0, ex = new Exception("Unable to save").Message.ToString() });
        }



        private List<PoleInfo> GetPoleIDList()
        {
            userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            var data = (from p in db.PoleInfoes
                        where (userInfo.UserTypeID == 1 || p.TaskAssainUserID == userInfo.Id)
                        select  p).ToList();

            return data;
        }






    }
}

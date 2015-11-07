using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using WebApp.Models;
using Microsoft.AspNet.Identity;
namespace WebApp.Controllers
{
    
    public class PoleInfoController : Controller
    {
        PoleInfoEntities objE = new PoleInfoEntities();
        private UserInformation userInfo { get; set; }

        //
        // GET: /PoleInfo/
        public ActionResult Index()
        {
            userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            var data = GetSearchData(userInfo);
            
            return View(data);
        }

        //
        // GET: /PoleInfo/Details/5
        public ActionResult Details(int id)
        {
            PoleInfoEntities objE = new PoleInfoEntities();
            var data = (from p in objE.PoleInfoes.Where(p => p.ID == id)
                        join t in objE.PoleTypes on p.TypeID equals t.ID
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TypeName=t.TypeName,
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
        // GET: /PoleInfo/Create
        public ActionResult Create()
        {
            //PoleInfoEntities objE = new PoleInfoEntities();
            ViewBag.PoleType = new SelectList(objE.PoleTypes.ToList(), "ID", "TypeName");
            string aID = User.Identity.GetUserId();
            ViewBag.TaskAssainUser = new SelectList(new UserInformationController().GetDropdownData(aID), "ID", "UserFullName");
            return View();
        }

        //
        // POST: /PoleInfo/Create
        [HttpPost]
        public ActionResult Create(PoleInfoViewModel PoleInfo, HttpPostedFileBase file)
        {
            string ImageMapPath = "";
            string ImagesTakenpath = "";
            try
            {
                // TODO: Add insert logic here

                PoleInfo obj = new PoleInfo();
                obj.PoleID = PoleInfo.PoleID;
                obj.TypeID = PoleInfo.TypeID;
                obj.TaskAddeddate = PoleInfo.TaskAddeddate;
                obj.TaskPerformeddate = PoleInfo.TaskPerformeddate;

                obj.AdjacentPoleHeight = PoleInfo.AdjacentPoleHeight;
                obj.TransFormerLoading = PoleInfo.TransFormerLoading;
                obj.Notes = PoleInfo.Notes;

                //obj.UserID = User.Identity.GetUserId();
                //PoleInfoEntities objE = new PoleInfoEntities();
                var FileSavePathList = objE.FileSavePaths.ToList();

                if (Request.Files["ImageMapPathfile"].ContentLength > 0)
                {
                    var _file = Request.Files["ImageMapPathfile"];
                    string _savepath = FileSavePathList.SingleOrDefault(p => p.FileFor == "PoleImageMapPath").FilePath;
                    string fileExtension = System.IO.Path.GetExtension(_file.FileName);
                    ImageMapPath = _savepath + PoleInfo.PoleID.ToString().Trim() + "_MapImage" + fileExtension;

                    if (!System.IO.Directory.Exists(_savepath))
                    {
                        System.IO.Directory.CreateDirectory(_savepath);
                    }
                    _file.SaveAs(ImageMapPath);
                }

                if (Request.Files["ImagesTakenpathfile"].ContentLength > 0)
                {
                    var _file = Request.Files["ImagesTakenpathfile"];
                    string _savepath = FileSavePathList.SingleOrDefault(p => p.FileFor == "PoleImagesTakenpath").FilePath;
                    string fileExtension = System.IO.Path.GetExtension(_file.FileName);
                    ImagesTakenpath = _savepath + PoleInfo.PoleID.ToString().Trim() + "_TakenImages" + fileExtension;
                    if (!System.IO.Directory.Exists(_savepath))
                    {
                        System.IO.Directory.CreateDirectory(_savepath);
                    }
                    _file.SaveAs(ImagesTakenpath);
                }
                obj.ImageMapPath = ImageMapPath;
                obj.ImagesTakenpath = ImagesTakenpath;
                obj.TaskAssainUserID = PoleInfo.TaskAssainUserID;
                var userbasic = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
                obj.UserID = userbasic.Id;
                
                
                obj.IsActive = true;
                obj.CreateDate = DateTime.Now;

                int Id = Save(obj);

                return RedirectToAction("Index");
            }
            catch
            {
                DeleteFile(ImageMapPath);
                DeleteFile(ImagesTakenpath);
                return View();
            }
        }

        //
        // GET: /PoleInfo/Edit/5
        public ActionResult Edit(int id)
        {

            //PoleInfoEntities objE = new PoleInfoEntities();
            var data = (from p in objE.PoleInfoes.Where(p=>p.ID==id).ToList()
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TaskAddeddate = p.TaskAddeddate,
                            TaskPerformeddate = p.TaskPerformeddate,
                            ImageMapPath = p.ImageMapPath,
                            AdjacentPoleHeight = p.AdjacentPoleHeight,
                            TransFormerLoading=p.TransFormerLoading,
                            Notes = p.Notes,
                            ImagesTakenpath = p.ImagesTakenpath,

                            TaskAssainUserID=p.TaskAssainUserID,
                            UserID=p.UserID,

                        }).SingleOrDefault();

            ViewBag.PoleType = new SelectList(objE.PoleTypes.ToList(), "ID", "TypeName", data.TypeID);
            string aID = User.Identity.GetUserId();
            ViewBag.TaskAssainUser = new SelectList(new UserInformationController().GetDropdownData(aID), "ID", "UserFullName", data.TaskAssainUserID);
            return View(data);
        }

        //
        // POST: /PoleInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PoleInfoViewModel PoleInfo)
        {
            try
            {
                // TODO: Add update logic here

                PoleInfo obj = new PoleInfo();
                obj.PoleID = PoleInfo.PoleID;
                obj.TypeID = PoleInfo.TypeID;
                obj.TaskAddeddate = PoleInfo.TaskAddeddate;
                obj.TaskPerformeddate = PoleInfo.TaskPerformeddate;
                obj.ImageMapPath = PoleInfo.ImageMapPath;
                obj.AdjacentPoleHeight = PoleInfo.AdjacentPoleHeight;
                obj.TransFormerLoading = PoleInfo.TransFormerLoading;
                obj.Notes = PoleInfo.Notes;
                obj.ImagesTakenpath = PoleInfo.ImagesTakenpath;
                obj.TaskAssainUserID = PoleInfo.TaskAssainUserID;
                obj.UpdateDate = DateTime.Now;

                //obj.UserID = User.Identity.GetUserId();
                //PoleInfoEntities objE = new PoleInfoEntities();
                var FileSavePathList = objE.FileSavePaths.ToList();

                if (Request.Files["ImageMapPathfile"].ContentLength > 0)
                {
                    var _file = Request.Files["ImageMapPathfile"];
                    string _savepath = FileSavePathList.SingleOrDefault(p => p.FileFor == "PoleImageMapPath").FilePath;
                    string fileExtension = System.IO.Path.GetExtension(_file.FileName);
                    string ImageMapPath = _savepath + PoleInfo.PoleID.ToString().Trim() + "_MapImage" + fileExtension;
                    _file.SaveAs(ImageMapPath);
                    obj.ImageMapPath = ImageMapPath;
                }

                if (Request.Files["ImagesTakenpathfile"].ContentLength > 0)
                {
                    var _file = Request.Files["ImagesTakenpathfile"];
                    string _savepath = FileSavePathList.SingleOrDefault(p => p.FileFor == "PoleImagesTakenpath").FilePath;
                    string fileExtension = System.IO.Path.GetExtension(_file.FileName);
                    string ImagesTakenpath = _savepath + PoleInfo.PoleID.ToString().Trim() + "_TakenImages" + fileExtension;
                    _file.SaveAs(ImagesTakenpath);
                    obj.ImagesTakenpath = ImagesTakenpath;
                }
                
                
                int Id = Save(obj);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PoleInfo/Delete/5
        public ActionResult Delete(int id)
        {
            PoleInfoEntities objE = new PoleInfoEntities();
            var data = (from p in objE.PoleInfoes.Where(p => p.ID == id)
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
        // POST: /PoleInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                // TODO: Add delete logic here

                //PoleInfoEntities objE = new PoleInfoEntities();


                var objPoleInfo = objE.PoleInfoes.Find(id);
                objE.PoleInfoes.Remove(objPoleInfo);
                objE.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        ///Import:
        ///
        //
        // GET: /PoleInfo/Import
        public ActionResult Import()
        {
            string aID = User.Identity.GetUserId();
            ViewBag.TaskAssainUser = new SelectList(new UserInformationController().GetDropdownData(aID), "ID", "UserFullName");
            return View();
        }

        //
        // POST: /PoleInfo/Import
        [HttpPost]
        public ActionResult Import(PoleInfoViewModel PoleInfo,HttpPostedFileBase file)
        {
            try
            {
                //return RedirectToAction("Index");
                // TODO: Add insert logic here
                //ViewBag.TaskAssainUser = new SelectList(new UserInformationController().GetDropdownData(), "ID", "UserFullName");
                DataSet ds = new DataSet();
                if (Request.Files["file"].ContentLength > 0)
                {

                    string fileExtension =System.IO.Path.GetExtension(Request.Files["file"].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                        
                        if (System.IO.File.Exists(fileLocation))
                        {

                            System.IO.File.Delete(fileLocation);
                        }
                        
                        Request.Files["file"].SaveAs(fileLocation);
                        
                        //string _UserID = User.Identity.GetUserId();
                        userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
                        
                        DataTable tbl1 = SQLHelper.GetExcelData(fileLocation, "PoleData");
                        var drs = tbl1.Select("isnull(PoleID,'')=''");
                        
                        foreach (DataRow row in drs)
                        {
                            tbl1.Rows.Remove(row);
                        }
                        
                        foreach (DataRow dr in tbl1.Rows)
                        {

                            PoleInfo obj = new PoleInfo();
                            obj.PoleID = dr["PoleID"].ToString();
                            obj.TypeID = GetPolTypeID(dr["PoleType"].ToString());

                            DateTime _outdatetime;
                            DateTime? TaskAddeddate = null;
                            if (DateTime.TryParse(dr["TaskAddeddate"].ToString(), out _outdatetime))
                            {
                                TaskAddeddate = Convert.ToDateTime(dr["TaskAddeddate"].ToString());
                            }

                            DateTime? TaskPerformeddate = null;
                            if (DateTime.TryParse(dr["TaskPerformeddate"].ToString(), out _outdatetime))
                            {
                                TaskPerformeddate = Convert.ToDateTime(dr["TaskPerformeddate"].ToString());
                            }

                            obj.TaskAddeddate = TaskAddeddate;
                            obj.TaskPerformeddate = TaskPerformeddate;

                            obj.ImageMapPath = dr["ImageMapPath"].ToString();

                            double _outval;
                            double AdjacentPoleHeight = (double.TryParse(dr["AdjacentPoleHeight"].ToString(), out _outval) == false ? default(double) : Convert.ToDouble(dr["AdjacentPoleHeight"].ToString()));

                            obj.AdjacentPoleHeight = AdjacentPoleHeight;
                            obj.TransFormerLoading = dr["TransFormerLoading"].ToString();
                            obj.Notes = dr["Notes"].ToString();
                            obj.ImagesTakenpath = dr["ImagesTakenpath"].ToString();
                            obj.TaskAssainUserID = PoleInfo.TaskAssainUserID;
                            obj.UserID = userInfo.Id;
                            obj.IsActive = true;

                            try
                            {
                                int Id = Save(obj);
                            }
                            catch (Exception ex)
                            {
                                string errormsg = ex.Message;
                            }
                        }


                    }

                }


                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                string dd = ex.Message;
                return View(ex.Message);
            }
        }

        private int GetPolTypeID(string TypeName)
        {
            int TypeID = 0;
            string conn = SQLHelper.GetConnectionString();
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                var connection = con;
                var transaction = connection.BeginTransaction();

                Hashtable htbl = new Hashtable();
                htbl.Add("@TypeName", TypeName);
                htbl.Add("@UserID", userInfo.Id);
                try
                {
                    var dset = SQLHelper.ExecuteProcedureAsFromDataAdapter("SPPoleType", htbl, connection, transaction);
                    transaction.Commit();
                    TypeID = Convert.ToInt32(dset.Tables[0].Rows[0][0].ToString());
                }
                catch (Exception ex)
                {
                    string errormsg = ex.Message;
                    transaction.Rollback();
                }
            }
            return TypeID;
        }
        private int Save(PoleInfo obj)
        {
            int ID = 0;
            string conn = SQLHelper.GetConnectionString();
            using (SqlConnection con = new SqlConnection(conn))
            {
                    con.Open();
                    var connection = con;
                    var transaction = connection.BeginTransaction();

                    Hashtable htbl = new Hashtable();
                    htbl.Add("@PoleID", obj.PoleID);
                    htbl.Add("@TypeID", obj.TypeID);
                    htbl.Add("@TaskAddeddate", obj.TaskAddeddate);
                    htbl.Add("@TaskPerformeddate", obj.TaskPerformeddate);
                    htbl.Add("@ImageMapPath", obj.ImageMapPath);
                    htbl.Add("@AdjacentPoleHeight", obj.AdjacentPoleHeight);
                    htbl.Add("@TransFormerLoading", obj.TransFormerLoading);
                    htbl.Add("@Notes", obj.Notes);
                    htbl.Add("@ImagesTakenpath", obj.ImagesTakenpath);
                    htbl.Add("@TaskAssainUserID", obj.TaskAssainUserID);
                    htbl.Add("@UserID", obj.UserID);
                    htbl.Add("@IsActive", obj.IsActive);
                    htbl.Add("@TransactionDate", DateTime.Now);
                    try
                    {
                        var dset = SQLHelper.ExecuteProcedureAsFromDataAdapter("SPPoleInfo", htbl, connection, transaction);
                        transaction.Commit();
                        ID = Convert.ToInt32(dset.Tables[0].Rows[0][0].ToString());
                    }
                    catch (Exception ex)
                    {
                        string errormsg = ex.Message;
                        transaction.Rollback();
                    }
                    con.Close();

            }
            return ID;
        }

        private bool DeleteFile(string FilePath)
        {
            try
            {
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                string errormsg = ex.Message;
                return false;
            }
            
        }


        private IEnumerable GetSearchData(UserInformation User)
        {
            
            var data = (from p in objE.PoleInfoes
                        join t in objE.PoleTypes on p.TypeID equals t.ID into typegroup
                        join u in objE.UserInformations on p.TaskAssainUserID equals u.Id
                        from type in typegroup.DefaultIfEmpty()
                        where (userInfo.UserTypeID == 1 || p.TaskAssainUserID==userInfo.Id)
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TypeName = type.TypeName,
                            TaskAssainUserID=p.TaskAssainUserID,
                            UserFullName = u.UserFullName,
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

        private IEnumerable GetPoleIDList()
        {
            userInfo = new UserInformationController().GetUserByAspNetUserId(User.Identity.GetUserId());
            var data = (from p in objE.PoleInfoes
                        where (userInfo.UserTypeID == 1 || p.UserID == userInfo.Id)
                        select new PoleImageViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID

                        }).ToList();

            return data;
        }

    }
}

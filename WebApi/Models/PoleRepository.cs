using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using System.Collections;


namespace WebApi.Models
{
    public class PoleRepository : IPoleRepository
    {
        //private List<PoleInfo> products = new List<PoleInfo>();
        private PoleInfoEntities db = new PoleInfoEntities();

        public PoleRepository()
        {           
        }

        public IEnumerable<PoleInfoViewModel> GetAll()
        {
            var data = (from p in db.PoleInfoes
                        join t in db.PoleTypes on p.TypeID equals t.ID
                        join u in db.UserInformations on p.TaskAssainUserID equals u.Id
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TypeName = t.TypeName,
                            TaskAssainUserID = p.TaskAssainUserID,
                            UserFullName = u.UserFullName,
                            TaskAddeddate = p.TaskAddeddate,
                            TaskPerformeddate = p.TaskPerformeddate,
                            ImageMapPath = p.ImageMapPath,
                            AdjacentPoleHeight = p.AdjacentPoleHeight,
                            TransFormerLoading = p.TransFormerLoading,
                            Notes = p.Notes,
                            ImagesTakenpath = p.ImagesTakenpath
                        }).ToList();


            return data;
        }
        public IEnumerable<PoleInfoViewModel> GetAll(string UserName)
        {
            var data = (from p in db.PoleInfoes
                        join t in db.PoleTypes on p.TypeID equals t.ID
                        join u in db.UserInformations on p.TaskAssainUserID equals u.Id
                        join a in db.AspNetUsers.Where(p => p.UserName == UserName) on u.AspNetUserId equals a.Id
                        select new PoleInfoViewModel
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TypeName = t.TypeName,
                            TaskAssainUserID = p.TaskAssainUserID,
                            UserFullName = u.UserFullName,
                            UserName=a.UserName,
                            TaskAddeddate = p.TaskAddeddate,
                            TaskPerformeddate = p.TaskPerformeddate,
                            ImageMapPath = p.ImageMapPath,
                            AdjacentPoleHeight = p.AdjacentPoleHeight,
                            TransFormerLoading = p.TransFormerLoading,
                            Notes = p.Notes,
                            ImagesTakenpath = p.ImagesTakenpath
                        }).ToList();


            return data;
        }

        public PoleInfo Get(int id)
        {

            var data = (from p in db.PoleInfoes.ToList()
                        where p.ID==id
                        select new PoleInfo
                        {
                            ID = p.ID,
                            PoleID = p.PoleID,
                            TypeID = p.TypeID,
                            TaskAddeddate = p.TaskAddeddate,
                            TaskPerformeddate = p.TaskPerformeddate,
                            ImageMapPath = p.ImageMapPath,
                            AdjacentPoleHeight = p.AdjacentPoleHeight,
                            TransFormerLoading = p.TransFormerLoading,
                            Notes = p.Notes,
                            ImagesTakenpath = p.ImagesTakenpath
                        }).FirstOrDefault();


            return data;
        }

        public PoleInfo Add(PoleInfo poleinfo)
        {

            //var data = db.PoleInfoChangeApplies.Where(p => p.PoleID == poleinfo.PoleID).SingleOrDefault();
            //if (data == null)
            //{
                var data = new PoleInfoChangeApply();
                data.PoleID = poleinfo.PoleID;
                data.TaskAddeddate = poleinfo.TaskAddeddate;
                data.TaskPerformeddate = poleinfo.TaskPerformeddate;
                data.TypeID = poleinfo.TypeID;
                data.TaskAssainUserID = poleinfo.TaskAssainUserID;
                data.AdjacentPoleHeight = poleinfo.AdjacentPoleHeight;
                data.TransFormerLoading = poleinfo.TransFormerLoading;
                data.Notes = poleinfo.Notes;
                data.UserID = poleinfo.UserID;

                db.Entry(data).State = EntityState.Added;
            //}
            //else if (data!=null)
            //{
            //    data.TaskAddeddate = poleinfo.TaskAddeddate;
            //    data.TaskPerformeddate = poleinfo.TaskPerformeddate;
            //    data.TypeID = poleinfo.TypeID;
            //    data.TaskAssainUserID = poleinfo.TaskAssainUserID;
            //    data.AdjacentPoleHeight = poleinfo.AdjacentPoleHeight;
            //    data.TransFormerLoading = poleinfo.TransFormerLoading;
            //    data.Notes = poleinfo.Notes;

            //    db.Entry(data).State = EntityState.Modified;
            //}




            try
            {
                db.SaveChanges();
                
            }
                //DbUpdateConcurrencyException
            catch (Exception ex)
            {
                string mm = ex.Message;
                    //throw;

            }

            return poleinfo;
        }

        public bool Update(PoleInfo poleinfo)
        {
            db.Entry(poleinfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;

            }
            return true;
        }

        public bool Delete(int id)
        {
            PoleInfo poleinfo = db.PoleInfoes.Find(id);
            if (poleinfo == null)
            {
                return false;
            }

            db.PoleInfoes.Remove(poleinfo);
            db.SaveChanges();

            return true;
        }
    }
}
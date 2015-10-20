using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PoleController : ApiController
    {
        //private WebApiContext db = new WebApiContext();

        private PoleInfoEntities db = new PoleInfoEntities();
        // GET api/Pole

        public IQueryable<PoleInfo> GetPoleInfoes()
        {
            
            return db.PoleInfoes;
        }

        // GET api/Pole/5
        [ResponseType(typeof(PoleInfo))]
        public IHttpActionResult GetPoleInfo(int id)
        {
            PoleInfo poleinfo = db.PoleInfoes.Find(id);
            if (poleinfo == null)
            {
                return NotFound();
            }

            return Ok(poleinfo);
        }

        // PUT api/Pole/5
        public IHttpActionResult PutPoleInfo(int id, PoleInfo poleinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poleinfo.ID)
            {
                return BadRequest();
            }

            db.Entry(poleinfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoleInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Pole
        [ResponseType(typeof(PoleInfo))]
        public IHttpActionResult PostPoleInfo(PoleInfo poleinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PoleInfoes.Add(poleinfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = poleinfo.ID }, poleinfo);
        }

        // DELETE api/Pole/5
        [ResponseType(typeof(PoleInfo))]
        public IHttpActionResult DeletePoleInfo(int id)
        {
            PoleInfo poleinfo = db.PoleInfoes.Find(id);
            if (poleinfo == null)
            {
                return NotFound();
            }

            db.PoleInfoes.Remove(poleinfo);
            db.SaveChanges();

            return Ok(poleinfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PoleInfoExists(int id)
        {
            return db.PoleInfoes.Count(e => e.ID == id) > 0;
        }



    }
}
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
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PoleTestController : ApiController
    {
        private PoleInfoEntities db = new PoleInfoEntities();

        // GET api/PoleTest
        public IQueryable<PoleInfo> GetPoleInfoes()
        {
            return db.PoleInfoes;
        }

        // GET api/PoleTest/5
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

        // PUT api/PoleTest/5
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

        // POST api/PoleTest
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

        // DELETE api/PoleTest/5
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
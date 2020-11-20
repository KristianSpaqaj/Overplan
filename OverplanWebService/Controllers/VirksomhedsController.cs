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
using OverplanWebService;

namespace OverplanWebService.Controllers
{
    public class VirksomhedsController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/Virksomheds
        public IQueryable<Virksomhed> GetVirksomheds()
        {
            return db.Virksomheds;
        }

        // GET: api/Virksomheds/5
        [ResponseType(typeof(Virksomhed))]
        public IHttpActionResult GetVirksomhed(int id)
        {
            Virksomhed virksomhed = db.Virksomheds.Find(id);
            if (virksomhed == null)
            {
                return NotFound();
            }

            return Ok(virksomhed);
        }

        // PUT: api/Virksomheds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVirksomhed(int id, Virksomhed virksomhed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != virksomhed.VirksomhedsID)
            {
                return BadRequest();
            }

            db.Entry(virksomhed).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VirksomhedExists(id))
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

        // POST: api/Virksomheds
        [ResponseType(typeof(Virksomhed))]
        public IHttpActionResult PostVirksomhed(Virksomhed virksomhed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Virksomheds.Add(virksomhed);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = virksomhed.VirksomhedsID }, virksomhed);
        }

        // DELETE: api/Virksomheds/5
        [ResponseType(typeof(Virksomhed))]
        public IHttpActionResult DeleteVirksomhed(int id)
        {
            Virksomhed virksomhed = db.Virksomheds.Find(id);
            if (virksomhed == null)
            {
                return NotFound();
            }

            db.Virksomheds.Remove(virksomhed);
            db.SaveChanges();

            return Ok(virksomhed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VirksomhedExists(int id)
        {
            return db.Virksomheds.Count(e => e.VirksomhedsID == id) > 0;
        }
    }
}
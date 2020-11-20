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
    public class AfdelingsplansController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/Afdelingsplans
        public IQueryable<Afdelingsplan> GetAfdelingsplans()
        {
            return db.Afdelingsplans;
        }

        // GET: api/Afdelingsplans/5
        [ResponseType(typeof(Afdelingsplan))]
        public IHttpActionResult GetAfdelingsplan(int id)
        {
            Afdelingsplan afdelingsplan = db.Afdelingsplans.Find(id);
            if (afdelingsplan == null)
            {
                return NotFound();
            }

            return Ok(afdelingsplan);
        }

        // PUT: api/Afdelingsplans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAfdelingsplan(int id, Afdelingsplan afdelingsplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != afdelingsplan.MedarbejderID)
            {
                return BadRequest();
            }

            db.Entry(afdelingsplan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AfdelingsplanExists(id))
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

        // POST: api/Afdelingsplans
        [ResponseType(typeof(Afdelingsplan))]
        public IHttpActionResult PostAfdelingsplan(Afdelingsplan afdelingsplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Afdelingsplans.Add(afdelingsplan);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AfdelingsplanExists(afdelingsplan.MedarbejderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = afdelingsplan.MedarbejderID }, afdelingsplan);
        }

        // DELETE: api/Afdelingsplans/5
        [ResponseType(typeof(Afdelingsplan))]
        public IHttpActionResult DeleteAfdelingsplan(int id)
        {
            Afdelingsplan afdelingsplan = db.Afdelingsplans.Find(id);
            if (afdelingsplan == null)
            {
                return NotFound();
            }

            db.Afdelingsplans.Remove(afdelingsplan);
            db.SaveChanges();

            return Ok(afdelingsplan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AfdelingsplanExists(int id)
        {
            return db.Afdelingsplans.Count(e => e.MedarbejderID == id) > 0;
        }
    }
}
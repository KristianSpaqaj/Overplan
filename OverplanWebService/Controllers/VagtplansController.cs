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
    public class VagtplansController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/Vagtplans
        public IQueryable<Vagtplan> GetVagtplans()
        {
            return db.Vagtplans;
        }

        // GET: api/Vagtplans/5
        [ResponseType(typeof(Vagtplan))]
        public IHttpActionResult GetVagtplan(int id)
        {
            Vagtplan vagtplan = db.Vagtplans.Find(id);
            if (vagtplan == null)
            {
                return NotFound();
            }

            return Ok(vagtplan);
        }

        // PUT: api/Vagtplans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVagtplan(int id, Vagtplan vagtplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vagtplan.VagtID)
            {
                return BadRequest();
            }

            db.Entry(vagtplan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VagtplanExists(id))
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

        // POST: api/Vagtplans
        [ResponseType(typeof(Vagtplan))]
        public IHttpActionResult PostVagtplan(Vagtplan vagtplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vagtplans.Add(vagtplan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vagtplan.VagtID }, vagtplan);
        }

        // DELETE: api/Vagtplans/5
        [ResponseType(typeof(Vagtplan))]
        public IHttpActionResult DeleteVagtplan(int id)
        {
            Vagtplan vagtplan = db.Vagtplans.Find(id);
            if (vagtplan == null)
            {
                return NotFound();
            }

            db.Vagtplans.Remove(vagtplan);
            db.SaveChanges();

            return Ok(vagtplan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VagtplanExists(int id)
        {
            return db.Vagtplans.Count(e => e.VagtID == id) > 0;
        }
    }
}
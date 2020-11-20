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
    public class MedarbejdersplansController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/Medarbejdersplans
        public IQueryable<Medarbejdersplan> GetMedarbejdersplans()
        {
            return db.Medarbejdersplans;
        }

        // GET: api/Medarbejdersplans/5
        [ResponseType(typeof(Medarbejdersplan))]
        public IHttpActionResult GetMedarbejdersplan(int id)
        {
            Medarbejdersplan medarbejdersplan = db.Medarbejdersplans.Find(id);
            if (medarbejdersplan == null)
            {
                return NotFound();
            }

            return Ok(medarbejdersplan);
        }

        // PUT: api/Medarbejdersplans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMedarbejdersplan(int id, Medarbejdersplan medarbejdersplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medarbejdersplan.MedarbejderID)
            {
                return BadRequest();
            }

            db.Entry(medarbejdersplan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedarbejdersplanExists(id))
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

        // POST: api/Medarbejdersplans
        [ResponseType(typeof(Medarbejdersplan))]
        public IHttpActionResult PostMedarbejdersplan(Medarbejdersplan medarbejdersplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medarbejdersplans.Add(medarbejdersplan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medarbejdersplan.MedarbejderID }, medarbejdersplan);
        }

        // DELETE: api/Medarbejdersplans/5
        [ResponseType(typeof(Medarbejdersplan))]
        public IHttpActionResult DeleteMedarbejdersplan(int id)
        {
            Medarbejdersplan medarbejdersplan = db.Medarbejdersplans.Find(id);
            if (medarbejdersplan == null)
            {
                return NotFound();
            }

            db.Medarbejdersplans.Remove(medarbejdersplan);
            db.SaveChanges();

            return Ok(medarbejdersplan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedarbejdersplanExists(int id)
        {
            return db.Medarbejdersplans.Count(e => e.MedarbejderID == id) > 0;
        }
    }
}
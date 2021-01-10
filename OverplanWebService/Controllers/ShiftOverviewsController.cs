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
    public class ShiftOverviewsController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/ShiftOverviews
        public IQueryable<ShiftOverview> GetShiftOverviews()
        {
            return db.ShiftOverviews;
        }

        // GET: api/ShiftOverviews/5
        [ResponseType(typeof(ShiftOverview))]
        public IHttpActionResult GetShiftOverview(int id)
        {
            ShiftOverview shiftOverview = db.ShiftOverviews.Find(id);
            if (shiftOverview == null)
            {
                return NotFound();
            }

            return Ok(shiftOverview);
        }

        // PUT: api/ShiftOverviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShiftOverview(int id, ShiftOverview shiftOverview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shiftOverview.ShiftID)
            {
                return BadRequest();
            }

            db.Entry(shiftOverview).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftOverviewExists(id))
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

        // POST: api/ShiftOverviews
        [ResponseType(typeof(ShiftOverview))]
        public IHttpActionResult PostShiftOverview(ShiftOverview shiftOverview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShiftOverviews.Add(shiftOverview);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shiftOverview.ShiftID }, shiftOverview);
        }

        // DELETE: api/ShiftOverviews/5
        [ResponseType(typeof(ShiftOverview))]
        public IHttpActionResult DeleteShiftOverview(int id)
        {
            ShiftOverview shiftOverview = db.ShiftOverviews.Find(id);
            if (shiftOverview == null)
            {
                return NotFound();
            }

            db.ShiftOverviews.Remove(shiftOverview);
            db.SaveChanges();

            return Ok(shiftOverview);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShiftOverviewExists(int id)
        {
            return db.ShiftOverviews.Count(e => e.ShiftID == id) > 0;
        }
    }
}
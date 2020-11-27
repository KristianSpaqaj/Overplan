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
    public class RostersController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/Rosters
        public IQueryable<Roster> GetRoster()
        {
            return db.Roster;
        }

        // GET: api/Rosters/5
        [ResponseType(typeof(Roster))]
        public IHttpActionResult GetRoster(int id)
        {
            Roster roster = db.Roster.Find(id);
            if (roster == null)
            {
                return NotFound();
            }

            return Ok(roster);
        }

        // PUT: api/Rosters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoster(int id, Roster roster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roster.ShiftID)
            {
                return BadRequest();
            }

            db.Entry(roster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RosterExists(id))
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

        // POST: api/Rosters
        [ResponseType(typeof(Roster))]
        public IHttpActionResult PostRoster(Roster roster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roster.Add(roster);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RosterExists(roster.ShiftID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = roster.ShiftID }, roster);
        }

        // DELETE: api/Rosters/5
        [ResponseType(typeof(Roster))]
        public IHttpActionResult DeleteRoster(int id)
        {
            Roster roster = db.Roster.Find(id);
            if (roster == null)
            {
                return NotFound();
            }

            db.Roster.Remove(roster);
            db.SaveChanges();

            return Ok(roster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RosterExists(int id)
        {
            return db.Roster.Count(e => e.ShiftID == id) > 0;
        }
    }
}
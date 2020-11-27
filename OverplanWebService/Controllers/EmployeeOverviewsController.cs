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
    public class EmployeeOverviewsController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/EmployeeOverviews
        public IQueryable<EmployeeOverview> GetEmployeeOverview()
        {
            return db.EmployeeOverview;
        }

        // GET: api/EmployeeOverviews/5
        [ResponseType(typeof(EmployeeOverview))]
        public IHttpActionResult GetEmployeeOverview(int id)
        {
            EmployeeOverview employeeOverview = db.EmployeeOverview.Find(id);
            if (employeeOverview == null)
            {
                return NotFound();
            }

            return Ok(employeeOverview);
        }

        // PUT: api/EmployeeOverviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeOverview(int id, EmployeeOverview employeeOverview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeOverview.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employeeOverview).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeOverviewExists(id))
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

        // POST: api/EmployeeOverviews
        [ResponseType(typeof(EmployeeOverview))]
        public IHttpActionResult PostEmployeeOverview(EmployeeOverview employeeOverview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeOverview.Add(employeeOverview);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeOverview.EmployeeID }, employeeOverview);
        }

        // DELETE: api/EmployeeOverviews/5
        [ResponseType(typeof(EmployeeOverview))]
        public IHttpActionResult DeleteEmployeeOverview(int id)
        {
            EmployeeOverview employeeOverview = db.EmployeeOverview.Find(id);
            if (employeeOverview == null)
            {
                return NotFound();
            }

            db.EmployeeOverview.Remove(employeeOverview);
            db.SaveChanges();

            return Ok(employeeOverview);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeOverviewExists(int id)
        {
            return db.EmployeeOverview.Count(e => e.EmployeeID == id) > 0;
        }
    }
}
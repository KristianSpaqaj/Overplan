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
    public class LoginOverviewsController : ApiController
    {
        private OverplanContext db = new OverplanContext();

        // GET: api/LoginOverviews
        public IQueryable<LoginOverview> GetLoginOverviews()
        {
            return db.LoginOverviews;
        }

        // GET: api/LoginOverviews/5
        [ResponseType(typeof(LoginOverview))]
        public IHttpActionResult GetLoginOverview(string id)
        {
            LoginOverview loginOverview = db.LoginOverviews.Find(id);
            if (loginOverview == null)
            {
                return NotFound();
            }

            return Ok(loginOverview);
        }

        // PUT: api/LoginOverviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLoginOverview(string id, LoginOverview loginOverview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginOverview.Username)
            {
                return BadRequest();
            }

            db.Entry(loginOverview).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginOverviewExists(id))
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

        // POST: api/LoginOverviews
        [ResponseType(typeof(LoginOverview))]
        public IHttpActionResult PostLoginOverview(LoginOverview loginOverview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoginOverviews.Add(loginOverview);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LoginOverviewExists(loginOverview.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = loginOverview.Username }, loginOverview);
        }

        // DELETE: api/LoginOverviews/5
        [ResponseType(typeof(LoginOverview))]
        public IHttpActionResult DeleteLoginOverview(string id)
        {
            LoginOverview loginOverview = db.LoginOverviews.Find(id);
            if (loginOverview == null)
            {
                return NotFound();
            }

            db.LoginOverviews.Remove(loginOverview);
            db.SaveChanges();

            return Ok(loginOverview);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginOverviewExists(string id)
        {
            return db.LoginOverviews.Count(e => e.Username == id) > 0;
        }
    }
}
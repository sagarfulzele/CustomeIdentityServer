using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using PsiMprODataApi.Models;

namespace PsiMprODataApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using PsiMprODataApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<msInitial>("msInitials");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msInitialsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msInitials
        [EnableQuery]
        public IQueryable<msInitial> GetmsInitials()
        {
            return db.msInitials;
        }

        // GET: odata/msInitials(5)
        [EnableQuery]
        public SingleResult<msInitial> GetmsInitial([FromODataUri] int key)
        {
            return SingleResult.Create(db.msInitials.Where(msInitial => msInitial.ID == key));
        }

        // PUT: odata/msInitials(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msInitial> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msInitial msInitial = await db.msInitials.FindAsync(key);
            if (msInitial == null)
            {
                return NotFound();
            }

            patch.Put(msInitial);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msInitialExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msInitial);
        }

        // POST: odata/msInitials
        public async Task<IHttpActionResult> Post(msInitial msInitial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msInitials.Add(msInitial);
            await db.SaveChangesAsync();

            return Created(msInitial);
        }

        // PATCH: odata/msInitials(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msInitial> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msInitial msInitial = await db.msInitials.FindAsync(key);
            if (msInitial == null)
            {
                return NotFound();
            }

            patch.Patch(msInitial);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msInitialExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msInitial);
        }

        // DELETE: odata/msInitials(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msInitial msInitial = await db.msInitials.FindAsync(key);
            if (msInitial == null)
            {
                return NotFound();
            }

            db.msInitials.Remove(msInitial);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msInitialExists(int key)
        {
            return db.msInitials.Count(e => e.ID == key) > 0;
        }
    }
}

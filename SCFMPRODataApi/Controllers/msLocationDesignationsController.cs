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
    builder.EntitySet<msLocationDesignation>("msLocationDesignations");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msLocationDesignationsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msLocationDesignations
        [EnableQuery]
        public IQueryable<msLocationDesignation> GetmsLocationDesignations()
        {
            return db.msLocationDesignations;
        }

        // GET: odata/msLocationDesignations(5)
        [EnableQuery]
        public SingleResult<msLocationDesignation> GetmsLocationDesignation([FromODataUri] int key)
        {
            return SingleResult.Create(db.msLocationDesignations.Where(msLocationDesignation => msLocationDesignation.ID == key));
        }

        // PUT: odata/msLocationDesignations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msLocationDesignation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msLocationDesignation msLocationDesignation = await db.msLocationDesignations.FindAsync(key);
            if (msLocationDesignation == null)
            {
                return NotFound();
            }

            patch.Put(msLocationDesignation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msLocationDesignationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msLocationDesignation);
        }

        // POST: odata/msLocationDesignations
        public async Task<IHttpActionResult> Post(msLocationDesignation msLocationDesignation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msLocationDesignations.Add(msLocationDesignation);
            await db.SaveChangesAsync();

            return Created(msLocationDesignation);
        }

        // PATCH: odata/msLocationDesignations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msLocationDesignation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msLocationDesignation msLocationDesignation = await db.msLocationDesignations.FindAsync(key);
            if (msLocationDesignation == null)
            {
                return NotFound();
            }

            patch.Patch(msLocationDesignation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msLocationDesignationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msLocationDesignation);
        }

        // DELETE: odata/msLocationDesignations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msLocationDesignation msLocationDesignation = await db.msLocationDesignations.FindAsync(key);
            if (msLocationDesignation == null)
            {
                return NotFound();
            }

            db.msLocationDesignations.Remove(msLocationDesignation);
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

        private bool msLocationDesignationExists(int key)
        {
            return db.msLocationDesignations.Count(e => e.ID == key) > 0;
        }
    }
}

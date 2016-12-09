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
    builder.EntitySet<mdWinding>("mdWindings");
    builder.EntitySet<mdVoltageTransformer>("mdVoltageTransformers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdWindingsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdWindings
        [EnableQuery]
        public IQueryable<mdWinding> GetmdWindings()
        {
            return db.mdWindings;
        }

        // GET: odata/mdWindings(5)
        [EnableQuery]
        public SingleResult<mdWinding> GetmdWinding([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdWindings.Where(mdWinding => mdWinding.WidingID == key));
        }

        // PUT: odata/mdWindings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdWinding> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdWinding mdWinding = await db.mdWindings.FindAsync(key);
            if (mdWinding == null)
            {
                return NotFound();
            }

            patch.Put(mdWinding);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdWindingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdWinding);
        }

        // POST: odata/mdWindings
        public async Task<IHttpActionResult> Post(mdWinding mdWinding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdWindings.Add(mdWinding);
            await db.SaveChangesAsync();

            return Created(mdWinding);
        }

        // PATCH: odata/mdWindings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdWinding> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdWinding mdWinding = await db.mdWindings.FindAsync(key);
            if (mdWinding == null)
            {
                return NotFound();
            }

            patch.Patch(mdWinding);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdWindingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdWinding);
        }

        // DELETE: odata/mdWindings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdWinding mdWinding = await db.mdWindings.FindAsync(key);
            if (mdWinding == null)
            {
                return NotFound();
            }

            db.mdWindings.Remove(mdWinding);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdWindings(5)/mdVoltageTransformer
        [EnableQuery]
        public SingleResult<mdVoltageTransformer> GetmdVoltageTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdWindings.Where(m => m.WidingID == key).Select(m => m.mdVoltageTransformer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdWindingExists(int key)
        {
            return db.mdWindings.Count(e => e.WidingID == key) > 0;
        }
    }
}

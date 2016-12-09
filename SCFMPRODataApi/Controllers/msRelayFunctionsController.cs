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
    builder.EntitySet<msRelayFunction>("msRelayFunctions");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msRelayFunctionsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msRelayFunctions
        [EnableQuery]
        public IQueryable<msRelayFunction> GetmsRelayFunctions()
        {
            return db.msRelayFunctions;
        }

        // GET: odata/msRelayFunctions(5)
        [EnableQuery]
        public SingleResult<msRelayFunction> GetmsRelayFunction([FromODataUri] int key)
        {
            return SingleResult.Create(db.msRelayFunctions.Where(msRelayFunction => msRelayFunction.ID == key));
        }

        // PUT: odata/msRelayFunctions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msRelayFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msRelayFunction msRelayFunction = await db.msRelayFunctions.FindAsync(key);
            if (msRelayFunction == null)
            {
                return NotFound();
            }

            patch.Put(msRelayFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msRelayFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msRelayFunction);
        }

        // POST: odata/msRelayFunctions
        public async Task<IHttpActionResult> Post(msRelayFunction msRelayFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msRelayFunctions.Add(msRelayFunction);
            await db.SaveChangesAsync();

            return Created(msRelayFunction);
        }

        // PATCH: odata/msRelayFunctions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msRelayFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msRelayFunction msRelayFunction = await db.msRelayFunctions.FindAsync(key);
            if (msRelayFunction == null)
            {
                return NotFound();
            }

            patch.Patch(msRelayFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msRelayFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msRelayFunction);
        }

        // DELETE: odata/msRelayFunctions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msRelayFunction msRelayFunction = await db.msRelayFunctions.FindAsync(key);
            if (msRelayFunction == null)
            {
                return NotFound();
            }

            db.msRelayFunctions.Remove(msRelayFunction);
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

        private bool msRelayFunctionExists(int key)
        {
            return db.msRelayFunctions.Count(e => e.ID == key) > 0;
        }
    }
}

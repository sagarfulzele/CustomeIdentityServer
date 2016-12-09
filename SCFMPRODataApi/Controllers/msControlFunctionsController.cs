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
    builder.EntitySet<msControlFunction>("msControlFunctions");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msControlFunctionsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msControlFunctions
        [EnableQuery]
        public IQueryable<msControlFunction> GetmsControlFunctions()
        {
            return db.msControlFunctions;
        }

        // GET: odata/msControlFunctions(5)
        [EnableQuery]
        public SingleResult<msControlFunction> GetmsControlFunction([FromODataUri] int key)
        {
            return SingleResult.Create(db.msControlFunctions.Where(msControlFunction => msControlFunction.ID == key));
        }

        // PUT: odata/msControlFunctions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msControlFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msControlFunction msControlFunction = await db.msControlFunctions.FindAsync(key);
            if (msControlFunction == null)
            {
                return NotFound();
            }

            patch.Put(msControlFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msControlFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msControlFunction);
        }

        // POST: odata/msControlFunctions
        public async Task<IHttpActionResult> Post(msControlFunction msControlFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msControlFunctions.Add(msControlFunction);
            await db.SaveChangesAsync();

            return Created(msControlFunction);
        }

        // PATCH: odata/msControlFunctions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msControlFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msControlFunction msControlFunction = await db.msControlFunctions.FindAsync(key);
            if (msControlFunction == null)
            {
                return NotFound();
            }

            patch.Patch(msControlFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msControlFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msControlFunction);
        }

        // DELETE: odata/msControlFunctions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msControlFunction msControlFunction = await db.msControlFunctions.FindAsync(key);
            if (msControlFunction == null)
            {
                return NotFound();
            }

            db.msControlFunctions.Remove(msControlFunction);
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

        private bool msControlFunctionExists(int key)
        {
            return db.msControlFunctions.Count(e => e.ID == key) > 0;
        }
    }
}

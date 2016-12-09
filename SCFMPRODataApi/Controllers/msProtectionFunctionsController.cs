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
    builder.EntitySet<msProtectionFunction>("msProtectionFunctions");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msProtectionFunctionsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msProtectionFunctions
        [EnableQuery]
        public IQueryable<msProtectionFunction> GetmsProtectionFunctions()
        {
            return db.msProtectionFunctions;
        }

        // GET: odata/msProtectionFunctions(5)
        [EnableQuery]
        public SingleResult<msProtectionFunction> GetmsProtectionFunction([FromODataUri] int key)
        {
            return SingleResult.Create(db.msProtectionFunctions.Where(msProtectionFunction => msProtectionFunction.ID == key));
        }

        // PUT: odata/msProtectionFunctions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msProtectionFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msProtectionFunction msProtectionFunction = await db.msProtectionFunctions.FindAsync(key);
            if (msProtectionFunction == null)
            {
                return NotFound();
            }

            patch.Put(msProtectionFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msProtectionFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msProtectionFunction);
        }

        // POST: odata/msProtectionFunctions
        public async Task<IHttpActionResult> Post(msProtectionFunction msProtectionFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msProtectionFunctions.Add(msProtectionFunction);
            await db.SaveChangesAsync();

            return Created(msProtectionFunction);
        }

        // PATCH: odata/msProtectionFunctions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msProtectionFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msProtectionFunction msProtectionFunction = await db.msProtectionFunctions.FindAsync(key);
            if (msProtectionFunction == null)
            {
                return NotFound();
            }

            patch.Patch(msProtectionFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msProtectionFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msProtectionFunction);
        }

        // DELETE: odata/msProtectionFunctions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msProtectionFunction msProtectionFunction = await db.msProtectionFunctions.FindAsync(key);
            if (msProtectionFunction == null)
            {
                return NotFound();
            }

            db.msProtectionFunctions.Remove(msProtectionFunction);
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

        private bool msProtectionFunctionExists(int key)
        {
            return db.msProtectionFunctions.Count(e => e.ID == key) > 0;
        }
    }
}

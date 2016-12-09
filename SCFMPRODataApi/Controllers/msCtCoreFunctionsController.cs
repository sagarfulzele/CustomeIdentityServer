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
    builder.EntitySet<msCtCoreFunction>("msCtCoreFunctions");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msCtCoreFunctionsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msCtCoreFunctions
        [EnableQuery]
        public IQueryable<msCtCoreFunction> GetmsCtCoreFunctions()
        {
            return db.msCtCoreFunctions;
        }

        // GET: odata/msCtCoreFunctions(5)
        [EnableQuery]
        public SingleResult<msCtCoreFunction> GetmsCtCoreFunction([FromODataUri] int key)
        {
            return SingleResult.Create(db.msCtCoreFunctions.Where(msCtCoreFunction => msCtCoreFunction.ID == key));
        }

        // PUT: odata/msCtCoreFunctions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msCtCoreFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCtCoreFunction msCtCoreFunction = await db.msCtCoreFunctions.FindAsync(key);
            if (msCtCoreFunction == null)
            {
                return NotFound();
            }

            patch.Put(msCtCoreFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCtCoreFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCtCoreFunction);
        }

        // POST: odata/msCtCoreFunctions
        public async Task<IHttpActionResult> Post(msCtCoreFunction msCtCoreFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msCtCoreFunctions.Add(msCtCoreFunction);
            await db.SaveChangesAsync();

            return Created(msCtCoreFunction);
        }

        // PATCH: odata/msCtCoreFunctions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msCtCoreFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCtCoreFunction msCtCoreFunction = await db.msCtCoreFunctions.FindAsync(key);
            if (msCtCoreFunction == null)
            {
                return NotFound();
            }

            patch.Patch(msCtCoreFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCtCoreFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCtCoreFunction);
        }

        // DELETE: odata/msCtCoreFunctions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msCtCoreFunction msCtCoreFunction = await db.msCtCoreFunctions.FindAsync(key);
            if (msCtCoreFunction == null)
            {
                return NotFound();
            }

            db.msCtCoreFunctions.Remove(msCtCoreFunction);
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

        private bool msCtCoreFunctionExists(int key)
        {
            return db.msCtCoreFunctions.Count(e => e.ID == key) > 0;
        }
    }
}

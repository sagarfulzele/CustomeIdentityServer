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
    builder.EntitySet<msDrawingFunction>("msDrawingFunctions");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msDrawingFunctionsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msDrawingFunctions
        [EnableQuery]
        public IQueryable<msDrawingFunction> GetmsDrawingFunctions()
        {
            return db.msDrawingFunctions;
        }

        // GET: odata/msDrawingFunctions(5)
        [EnableQuery]
        public SingleResult<msDrawingFunction> GetmsDrawingFunction([FromODataUri] int key)
        {
            return SingleResult.Create(db.msDrawingFunctions.Where(msDrawingFunction => msDrawingFunction.ID == key));
        }

        // PUT: odata/msDrawingFunctions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msDrawingFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msDrawingFunction msDrawingFunction = await db.msDrawingFunctions.FindAsync(key);
            if (msDrawingFunction == null)
            {
                return NotFound();
            }

            patch.Put(msDrawingFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msDrawingFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msDrawingFunction);
        }

        // POST: odata/msDrawingFunctions
        public async Task<IHttpActionResult> Post(msDrawingFunction msDrawingFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msDrawingFunctions.Add(msDrawingFunction);
            await db.SaveChangesAsync();

            return Created(msDrawingFunction);
        }

        // PATCH: odata/msDrawingFunctions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msDrawingFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msDrawingFunction msDrawingFunction = await db.msDrawingFunctions.FindAsync(key);
            if (msDrawingFunction == null)
            {
                return NotFound();
            }

            patch.Patch(msDrawingFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msDrawingFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msDrawingFunction);
        }

        // DELETE: odata/msDrawingFunctions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msDrawingFunction msDrawingFunction = await db.msDrawingFunctions.FindAsync(key);
            if (msDrawingFunction == null)
            {
                return NotFound();
            }

            db.msDrawingFunctions.Remove(msDrawingFunction);
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

        private bool msDrawingFunctionExists(int key)
        {
            return db.msDrawingFunctions.Count(e => e.ID == key) > 0;
        }
    }
}

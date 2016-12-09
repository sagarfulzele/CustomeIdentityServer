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
    builder.EntitySet<msBayRelayFunction>("msBayRelayFunctions");
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msBayRelayFunctionsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msBayRelayFunctions
        [EnableQuery]
        public IQueryable<msBayRelayFunction> GetmsBayRelayFunctions()
        {
            return db.msBayRelayFunctions;
        }

        // GET: odata/msBayRelayFunctions(5)
        [EnableQuery]
        public SingleResult<msBayRelayFunction> GetmsBayRelayFunction([FromODataUri] int key)
        {
            return SingleResult.Create(db.msBayRelayFunctions.Where(msBayRelayFunction => msBayRelayFunction.ID == key));
        }

        // PUT: odata/msBayRelayFunctions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msBayRelayFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msBayRelayFunction msBayRelayFunction = await db.msBayRelayFunctions.FindAsync(key);
            if (msBayRelayFunction == null)
            {
                return NotFound();
            }

            patch.Put(msBayRelayFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msBayRelayFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msBayRelayFunction);
        }

        // POST: odata/msBayRelayFunctions
        public async Task<IHttpActionResult> Post(msBayRelayFunction msBayRelayFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msBayRelayFunctions.Add(msBayRelayFunction);
            await db.SaveChangesAsync();

            return Created(msBayRelayFunction);
        }

        // PATCH: odata/msBayRelayFunctions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msBayRelayFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msBayRelayFunction msBayRelayFunction = await db.msBayRelayFunctions.FindAsync(key);
            if (msBayRelayFunction == null)
            {
                return NotFound();
            }

            patch.Patch(msBayRelayFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msBayRelayFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msBayRelayFunction);
        }

        // DELETE: odata/msBayRelayFunctions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msBayRelayFunction msBayRelayFunction = await db.msBayRelayFunctions.FindAsync(key);
            if (msBayRelayFunction == null)
            {
                return NotFound();
            }

            db.msBayRelayFunctions.Remove(msBayRelayFunction);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msBayRelayFunctions(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.msBayRelayFunctions.Where(m => m.ID == key).Select(m => m.msSubstationStructure));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msBayRelayFunctionExists(int key)
        {
            return db.msBayRelayFunctions.Count(e => e.ID == key) > 0;
        }
    }
}

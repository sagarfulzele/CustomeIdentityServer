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
    builder.EntitySet<msConsumable>("msConsumables");
    builder.EntitySet<msRequiredQuantity>("msRequiredQuantities"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msConsumablesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msConsumables
        [EnableQuery]
        public IQueryable<msConsumable> GetmsConsumables()
        {
            return db.msConsumables;
        }

        // GET: odata/msConsumables(5)
        [EnableQuery]
        public SingleResult<msConsumable> GetmsConsumable([FromODataUri] int key)
        {
            return SingleResult.Create(db.msConsumables.Where(msConsumable => msConsumable.ConsumableID == key));
        }

        // PUT: odata/msConsumables(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msConsumable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msConsumable msConsumable = await db.msConsumables.FindAsync(key);
            if (msConsumable == null)
            {
                return NotFound();
            }

            patch.Put(msConsumable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msConsumableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msConsumable);
        }

        // POST: odata/msConsumables
        public async Task<IHttpActionResult> Post(msConsumable msConsumable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msConsumables.Add(msConsumable);
            await db.SaveChangesAsync();

            return Created(msConsumable);
        }

        // PATCH: odata/msConsumables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msConsumable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msConsumable msConsumable = await db.msConsumables.FindAsync(key);
            if (msConsumable == null)
            {
                return NotFound();
            }

            patch.Patch(msConsumable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msConsumableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msConsumable);
        }

        // DELETE: odata/msConsumables(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msConsumable msConsumable = await db.msConsumables.FindAsync(key);
            if (msConsumable == null)
            {
                return NotFound();
            }

            db.msConsumables.Remove(msConsumable);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msConsumables(5)/msRequiredQuantities
        [EnableQuery]
        public IQueryable<msRequiredQuantity> GetmsRequiredQuantities([FromODataUri] int key)
        {
            return db.msConsumables.Where(m => m.ConsumableID == key).SelectMany(m => m.msRequiredQuantities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msConsumableExists(int key)
        {
            return db.msConsumables.Count(e => e.ConsumableID == key) > 0;
        }
    }
}

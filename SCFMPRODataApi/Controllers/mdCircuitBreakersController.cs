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
    builder.EntitySet<mdCircuitBreaker>("mdCircuitBreakers");
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdCircuitBreakersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdCircuitBreakers
        [EnableQuery]
        public IQueryable<mdCircuitBreaker> GetmdCircuitBreakers()
        {
            return db.mdCircuitBreakers;
        }

        // GET: odata/mdCircuitBreakers(5)
        [EnableQuery]
        public SingleResult<mdCircuitBreaker> GetmdCircuitBreaker([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCircuitBreakers.Where(mdCircuitBreaker => mdCircuitBreaker.PrimaryEquipmentId == key));
        }

        // PUT: odata/mdCircuitBreakers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdCircuitBreaker> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCircuitBreaker mdCircuitBreaker = await db.mdCircuitBreakers.FindAsync(key);
            if (mdCircuitBreaker == null)
            {
                return NotFound();
            }

            patch.Put(mdCircuitBreaker);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCircuitBreakerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCircuitBreaker);
        }

        // POST: odata/mdCircuitBreakers
        public async Task<IHttpActionResult> Post(mdCircuitBreaker mdCircuitBreaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdCircuitBreakers.Add(mdCircuitBreaker);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdCircuitBreakerExists(mdCircuitBreaker.PrimaryEquipmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdCircuitBreaker);
        }

        // PATCH: odata/mdCircuitBreakers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdCircuitBreaker> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCircuitBreaker mdCircuitBreaker = await db.mdCircuitBreakers.FindAsync(key);
            if (mdCircuitBreaker == null)
            {
                return NotFound();
            }

            patch.Patch(mdCircuitBreaker);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCircuitBreakerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCircuitBreaker);
        }

        // DELETE: odata/mdCircuitBreakers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdCircuitBreaker mdCircuitBreaker = await db.mdCircuitBreakers.FindAsync(key);
            if (mdCircuitBreaker == null)
            {
                return NotFound();
            }

            db.mdCircuitBreakers.Remove(mdCircuitBreaker);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdCircuitBreakers(5)/mdPrimaryEquipment
        [EnableQuery]
        public SingleResult<mdPrimaryEquipment> GetmdPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCircuitBreakers.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdPrimaryEquipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdCircuitBreakerExists(int key)
        {
            return db.mdCircuitBreakers.Count(e => e.PrimaryEquipmentId == key) > 0;
        }
    }
}

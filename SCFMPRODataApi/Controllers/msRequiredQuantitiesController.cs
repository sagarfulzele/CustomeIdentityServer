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
    builder.EntitySet<msRequiredQuantity>("msRequiredQuantities");
    builder.EntitySet<msConsumable>("msConsumables"); 
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msRequiredQuantitiesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msRequiredQuantities
        [EnableQuery]
        public IQueryable<msRequiredQuantity> GetmsRequiredQuantities()
        {
            return db.msRequiredQuantities;
        }

        // GET: odata/msRequiredQuantities(5)
        [EnableQuery]
        public SingleResult<msRequiredQuantity> GetmsRequiredQuantity([FromODataUri] int key)
        {
            return SingleResult.Create(db.msRequiredQuantities.Where(msRequiredQuantity => msRequiredQuantity.ID == key));
        }

        // PUT: odata/msRequiredQuantities(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msRequiredQuantity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msRequiredQuantity msRequiredQuantity = await db.msRequiredQuantities.FindAsync(key);
            if (msRequiredQuantity == null)
            {
                return NotFound();
            }

            patch.Put(msRequiredQuantity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msRequiredQuantityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msRequiredQuantity);
        }

        // POST: odata/msRequiredQuantities
        public async Task<IHttpActionResult> Post(msRequiredQuantity msRequiredQuantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msRequiredQuantities.Add(msRequiredQuantity);
            await db.SaveChangesAsync();

            return Created(msRequiredQuantity);
        }

        // PATCH: odata/msRequiredQuantities(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msRequiredQuantity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msRequiredQuantity msRequiredQuantity = await db.msRequiredQuantities.FindAsync(key);
            if (msRequiredQuantity == null)
            {
                return NotFound();
            }

            patch.Patch(msRequiredQuantity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msRequiredQuantityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msRequiredQuantity);
        }

        // DELETE: odata/msRequiredQuantities(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msRequiredQuantity msRequiredQuantity = await db.msRequiredQuantities.FindAsync(key);
            if (msRequiredQuantity == null)
            {
                return NotFound();
            }

            db.msRequiredQuantities.Remove(msRequiredQuantity);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msRequiredQuantities(5)/msConsumable
        [EnableQuery]
        public SingleResult<msConsumable> GetmsConsumable([FromODataUri] int key)
        {
            return SingleResult.Create(db.msRequiredQuantities.Where(m => m.ID == key).Select(m => m.msConsumable));
        }

        // GET: odata/msRequiredQuantities(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.msRequiredQuantities.Where(m => m.ID == key).Select(m => m.msSubstationStructure));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msRequiredQuantityExists(int key)
        {
            return db.msRequiredQuantities.Count(e => e.ID == key) > 0;
        }
    }
}

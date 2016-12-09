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
    builder.EntitySet<mdPowerTransformer>("mdPowerTransformers");
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPowerTransformersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPowerTransformers
        [EnableQuery]
        public IQueryable<mdPowerTransformer> GetmdPowerTransformers()
        {
            return db.mdPowerTransformers;
        }

        // GET: odata/mdPowerTransformers(5)
        [EnableQuery]
        public SingleResult<mdPowerTransformer> GetmdPowerTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPowerTransformers.Where(mdPowerTransformer => mdPowerTransformer.PrimaryEquipmentId == key));
        }

        // PUT: odata/mdPowerTransformers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPowerTransformer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPowerTransformer mdPowerTransformer = await db.mdPowerTransformers.FindAsync(key);
            if (mdPowerTransformer == null)
            {
                return NotFound();
            }

            patch.Put(mdPowerTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPowerTransformerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPowerTransformer);
        }

        // POST: odata/mdPowerTransformers
        public async Task<IHttpActionResult> Post(mdPowerTransformer mdPowerTransformer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPowerTransformers.Add(mdPowerTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdPowerTransformerExists(mdPowerTransformer.PrimaryEquipmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdPowerTransformer);
        }

        // PATCH: odata/mdPowerTransformers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPowerTransformer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPowerTransformer mdPowerTransformer = await db.mdPowerTransformers.FindAsync(key);
            if (mdPowerTransformer == null)
            {
                return NotFound();
            }

            patch.Patch(mdPowerTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPowerTransformerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPowerTransformer);
        }

        // DELETE: odata/mdPowerTransformers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPowerTransformer mdPowerTransformer = await db.mdPowerTransformers.FindAsync(key);
            if (mdPowerTransformer == null)
            {
                return NotFound();
            }

            db.mdPowerTransformers.Remove(mdPowerTransformer);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPowerTransformers(5)/mdPrimaryEquipment
        [EnableQuery]
        public SingleResult<mdPrimaryEquipment> GetmdPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPowerTransformers.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdPrimaryEquipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPowerTransformerExists(int key)
        {
            return db.mdPowerTransformers.Count(e => e.PrimaryEquipmentId == key) > 0;
        }
    }
}

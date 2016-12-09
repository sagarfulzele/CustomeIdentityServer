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
    builder.EntitySet<mdCurrentTransformer>("mdCurrentTransformers");
    builder.EntitySet<mdCore>("mdCores"); 
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdCurrentTransformersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdCurrentTransformers
        [EnableQuery]
        public IQueryable<mdCurrentTransformer> GetmdCurrentTransformers()
        {
            return db.mdCurrentTransformers;
        }

        // GET: odata/mdCurrentTransformers(5)
        [EnableQuery]
        public SingleResult<mdCurrentTransformer> GetmdCurrentTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCurrentTransformers.Where(mdCurrentTransformer => mdCurrentTransformer.PrimaryEquipmentId == key));
        }

        // PUT: odata/mdCurrentTransformers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdCurrentTransformer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCurrentTransformer mdCurrentTransformer = await db.mdCurrentTransformers.FindAsync(key);
            if (mdCurrentTransformer == null)
            {
                return NotFound();
            }

            patch.Put(mdCurrentTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCurrentTransformerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCurrentTransformer);
        }

        // POST: odata/mdCurrentTransformers
        public async Task<IHttpActionResult> Post(mdCurrentTransformer mdCurrentTransformer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdCurrentTransformers.Add(mdCurrentTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdCurrentTransformerExists(mdCurrentTransformer.PrimaryEquipmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdCurrentTransformer);
        }

        // PATCH: odata/mdCurrentTransformers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdCurrentTransformer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCurrentTransformer mdCurrentTransformer = await db.mdCurrentTransformers.FindAsync(key);
            if (mdCurrentTransformer == null)
            {
                return NotFound();
            }

            patch.Patch(mdCurrentTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCurrentTransformerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCurrentTransformer);
        }

        // DELETE: odata/mdCurrentTransformers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdCurrentTransformer mdCurrentTransformer = await db.mdCurrentTransformers.FindAsync(key);
            if (mdCurrentTransformer == null)
            {
                return NotFound();
            }

            db.mdCurrentTransformers.Remove(mdCurrentTransformer);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdCurrentTransformers(5)/mdCores
        [EnableQuery]
        public IQueryable<mdCore> GetmdCores([FromODataUri] int key)
        {
            return db.mdCurrentTransformers.Where(m => m.PrimaryEquipmentId == key).SelectMany(m => m.mdCores);
        }

        // GET: odata/mdCurrentTransformers(5)/mdPrimaryEquipment
        [EnableQuery]
        public SingleResult<mdPrimaryEquipment> GetmdPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCurrentTransformers.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdPrimaryEquipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdCurrentTransformerExists(int key)
        {
            return db.mdCurrentTransformers.Count(e => e.PrimaryEquipmentId == key) > 0;
        }
    }
}

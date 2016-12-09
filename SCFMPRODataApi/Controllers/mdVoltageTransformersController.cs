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
    builder.EntitySet<mdVoltageTransformer>("mdVoltageTransformers");
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments"); 
    builder.EntitySet<mdWinding>("mdWindings"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdVoltageTransformersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdVoltageTransformers
        [EnableQuery]
        public IQueryable<mdVoltageTransformer> GetmdVoltageTransformers()
        {
            return db.mdVoltageTransformers;
        }

        // GET: odata/mdVoltageTransformers(5)
        [EnableQuery]
        public SingleResult<mdVoltageTransformer> GetmdVoltageTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageTransformers.Where(mdVoltageTransformer => mdVoltageTransformer.PrimaryEquipmentId == key));
        }

        // PUT: odata/mdVoltageTransformers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdVoltageTransformer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdVoltageTransformer mdVoltageTransformer = await db.mdVoltageTransformers.FindAsync(key);
            if (mdVoltageTransformer == null)
            {
                return NotFound();
            }

            patch.Put(mdVoltageTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdVoltageTransformerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdVoltageTransformer);
        }

        // POST: odata/mdVoltageTransformers
        public async Task<IHttpActionResult> Post(mdVoltageTransformer mdVoltageTransformer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdVoltageTransformers.Add(mdVoltageTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdVoltageTransformerExists(mdVoltageTransformer.PrimaryEquipmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdVoltageTransformer);
        }

        // PATCH: odata/mdVoltageTransformers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdVoltageTransformer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdVoltageTransformer mdVoltageTransformer = await db.mdVoltageTransformers.FindAsync(key);
            if (mdVoltageTransformer == null)
            {
                return NotFound();
            }

            patch.Patch(mdVoltageTransformer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdVoltageTransformerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdVoltageTransformer);
        }

        // DELETE: odata/mdVoltageTransformers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdVoltageTransformer mdVoltageTransformer = await db.mdVoltageTransformers.FindAsync(key);
            if (mdVoltageTransformer == null)
            {
                return NotFound();
            }

            db.mdVoltageTransformers.Remove(mdVoltageTransformer);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdVoltageTransformers(5)/mdPrimaryEquipment
        [EnableQuery]
        public SingleResult<mdPrimaryEquipment> GetmdPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageTransformers.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdPrimaryEquipment));
        }

        // GET: odata/mdVoltageTransformers(5)/mdWindings
        [EnableQuery]
        public IQueryable<mdWinding> GetmdWindings([FromODataUri] int key)
        {
            return db.mdVoltageTransformers.Where(m => m.PrimaryEquipmentId == key).SelectMany(m => m.mdWindings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdVoltageTransformerExists(int key)
        {
            return db.mdVoltageTransformers.Count(e => e.PrimaryEquipmentId == key) > 0;
        }
    }
}

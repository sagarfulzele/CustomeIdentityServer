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
    builder.EntitySet<mdIsolator>("mdIsolators");
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdIsolatorsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdIsolators
        [EnableQuery]
        public IQueryable<mdIsolator> GetmdIsolators()
        {
            return db.mdIsolators;
        }

        // GET: odata/mdIsolators(5)
        [EnableQuery]
        public SingleResult<mdIsolator> GetmdIsolator([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdIsolators.Where(mdIsolator => mdIsolator.PrimaryEquipmentId == key));
        }

        // PUT: odata/mdIsolators(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdIsolator> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdIsolator mdIsolator = await db.mdIsolators.FindAsync(key);
            if (mdIsolator == null)
            {
                return NotFound();
            }

            patch.Put(mdIsolator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdIsolatorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdIsolator);
        }

        // POST: odata/mdIsolators
        public async Task<IHttpActionResult> Post(mdIsolator mdIsolator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdIsolators.Add(mdIsolator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdIsolatorExists(mdIsolator.PrimaryEquipmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdIsolator);
        }

        // PATCH: odata/mdIsolators(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdIsolator> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdIsolator mdIsolator = await db.mdIsolators.FindAsync(key);
            if (mdIsolator == null)
            {
                return NotFound();
            }

            patch.Patch(mdIsolator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdIsolatorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdIsolator);
        }

        // DELETE: odata/mdIsolators(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdIsolator mdIsolator = await db.mdIsolators.FindAsync(key);
            if (mdIsolator == null)
            {
                return NotFound();
            }

            db.mdIsolators.Remove(mdIsolator);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdIsolators(5)/mdPrimaryEquipment
        [EnableQuery]
        public SingleResult<mdPrimaryEquipment> GetmdPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdIsolators.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdPrimaryEquipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdIsolatorExists(int key)
        {
            return db.mdIsolators.Count(e => e.PrimaryEquipmentId == key) > 0;
        }
    }
}

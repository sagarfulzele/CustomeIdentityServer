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
    builder.EntitySet<mdMeter>("mdMeters");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdMetersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdMeters
        [EnableQuery]
        public IQueryable<mdMeter> GetmdMeters()
        {
            return db.mdMeters;
        }

        // GET: odata/mdMeters(5)
        [EnableQuery]
        public SingleResult<mdMeter> GetmdMeter([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdMeters.Where(mdMeter => mdMeter.ID == key));
        }

        // PUT: odata/mdMeters(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdMeter> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdMeter mdMeter = await db.mdMeters.FindAsync(key);
            if (mdMeter == null)
            {
                return NotFound();
            }

            patch.Put(mdMeter);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdMeterExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdMeter);
        }

        // POST: odata/mdMeters
        public async Task<IHttpActionResult> Post(mdMeter mdMeter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdMeters.Add(mdMeter);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdMeterExists(mdMeter.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdMeter);
        }

        // PATCH: odata/mdMeters(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdMeter> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdMeter mdMeter = await db.mdMeters.FindAsync(key);
            if (mdMeter == null)
            {
                return NotFound();
            }

            patch.Patch(mdMeter);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdMeterExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdMeter);
        }

        // DELETE: odata/mdMeters(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdMeter mdMeter = await db.mdMeters.FindAsync(key);
            if (mdMeter == null)
            {
                return NotFound();
            }

            db.mdMeters.Remove(mdMeter);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdMeters(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdMeters.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdMeterExists(int key)
        {
            return db.mdMeters.Count(e => e.ID == key) > 0;
        }
    }
}

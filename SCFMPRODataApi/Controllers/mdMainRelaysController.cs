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
    builder.EntitySet<mdMainRelay>("mdMainRelays");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdMainRelaysController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdMainRelays
        [EnableQuery]
        public IQueryable<mdMainRelay> GetmdMainRelays()
        {
            return db.mdMainRelays;
        }

        // GET: odata/mdMainRelays(5)
        [EnableQuery]
        public SingleResult<mdMainRelay> GetmdMainRelay([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdMainRelays.Where(mdMainRelay => mdMainRelay.ID == key));
        }

        // PUT: odata/mdMainRelays(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdMainRelay> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdMainRelay mdMainRelay = await db.mdMainRelays.FindAsync(key);
            if (mdMainRelay == null)
            {
                return NotFound();
            }

            patch.Put(mdMainRelay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdMainRelayExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdMainRelay);
        }

        // POST: odata/mdMainRelays
        public async Task<IHttpActionResult> Post(mdMainRelay mdMainRelay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdMainRelays.Add(mdMainRelay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdMainRelayExists(mdMainRelay.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdMainRelay);
        }

        // PATCH: odata/mdMainRelays(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdMainRelay> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdMainRelay mdMainRelay = await db.mdMainRelays.FindAsync(key);
            if (mdMainRelay == null)
            {
                return NotFound();
            }

            patch.Patch(mdMainRelay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdMainRelayExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdMainRelay);
        }

        // DELETE: odata/mdMainRelays(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdMainRelay mdMainRelay = await db.mdMainRelays.FindAsync(key);
            if (mdMainRelay == null)
            {
                return NotFound();
            }

            db.mdMainRelays.Remove(mdMainRelay);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdMainRelays(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdMainRelays.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdMainRelayExists(int key)
        {
            return db.mdMainRelays.Count(e => e.ID == key) > 0;
        }
    }
}

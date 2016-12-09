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
    builder.EntitySet<mdAuxiliaryRelay>("mdAuxiliaryRelays");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdAuxiliaryRelaysController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdAuxiliaryRelays
        [EnableQuery]
        public IQueryable<mdAuxiliaryRelay> GetmdAuxiliaryRelays()
        {
            return db.mdAuxiliaryRelays;
        }

        // GET: odata/mdAuxiliaryRelays(5)
        [EnableQuery]
        public SingleResult<mdAuxiliaryRelay> GetmdAuxiliaryRelay([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAuxiliaryRelays.Where(mdAuxiliaryRelay => mdAuxiliaryRelay.ID == key));
        }

        // PUT: odata/mdAuxiliaryRelays(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdAuxiliaryRelay> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAuxiliaryRelay mdAuxiliaryRelay = await db.mdAuxiliaryRelays.FindAsync(key);
            if (mdAuxiliaryRelay == null)
            {
                return NotFound();
            }

            patch.Put(mdAuxiliaryRelay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAuxiliaryRelayExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAuxiliaryRelay);
        }

        // POST: odata/mdAuxiliaryRelays
        public async Task<IHttpActionResult> Post(mdAuxiliaryRelay mdAuxiliaryRelay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdAuxiliaryRelays.Add(mdAuxiliaryRelay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdAuxiliaryRelayExists(mdAuxiliaryRelay.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdAuxiliaryRelay);
        }

        // PATCH: odata/mdAuxiliaryRelays(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdAuxiliaryRelay> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAuxiliaryRelay mdAuxiliaryRelay = await db.mdAuxiliaryRelays.FindAsync(key);
            if (mdAuxiliaryRelay == null)
            {
                return NotFound();
            }

            patch.Patch(mdAuxiliaryRelay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAuxiliaryRelayExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAuxiliaryRelay);
        }

        // DELETE: odata/mdAuxiliaryRelays(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdAuxiliaryRelay mdAuxiliaryRelay = await db.mdAuxiliaryRelays.FindAsync(key);
            if (mdAuxiliaryRelay == null)
            {
                return NotFound();
            }

            db.mdAuxiliaryRelays.Remove(mdAuxiliaryRelay);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdAuxiliaryRelays(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAuxiliaryRelays.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdAuxiliaryRelayExists(int key)
        {
            return db.mdAuxiliaryRelays.Count(e => e.ID == key) > 0;
        }
    }
}

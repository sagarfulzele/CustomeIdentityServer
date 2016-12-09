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
    builder.EntitySet<mdEarthSwitch>("mdEarthSwitches");
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdEarthSwitchesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdEarthSwitches
        [EnableQuery]
        public IQueryable<mdEarthSwitch> GetmdEarthSwitches()
        {
            return db.mdEarthSwitches;
        }

        // GET: odata/mdEarthSwitches(5)
        [EnableQuery]
        public SingleResult<mdEarthSwitch> GetmdEarthSwitch([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdEarthSwitches.Where(mdEarthSwitch => mdEarthSwitch.PrimaryEquipmentId == key));
        }

        // PUT: odata/mdEarthSwitches(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdEarthSwitch> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdEarthSwitch mdEarthSwitch = await db.mdEarthSwitches.FindAsync(key);
            if (mdEarthSwitch == null)
            {
                return NotFound();
            }

            patch.Put(mdEarthSwitch);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdEarthSwitchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdEarthSwitch);
        }

        // POST: odata/mdEarthSwitches
        public async Task<IHttpActionResult> Post(mdEarthSwitch mdEarthSwitch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdEarthSwitches.Add(mdEarthSwitch);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdEarthSwitchExists(mdEarthSwitch.PrimaryEquipmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdEarthSwitch);
        }

        // PATCH: odata/mdEarthSwitches(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdEarthSwitch> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdEarthSwitch mdEarthSwitch = await db.mdEarthSwitches.FindAsync(key);
            if (mdEarthSwitch == null)
            {
                return NotFound();
            }

            patch.Patch(mdEarthSwitch);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdEarthSwitchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdEarthSwitch);
        }

        // DELETE: odata/mdEarthSwitches(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdEarthSwitch mdEarthSwitch = await db.mdEarthSwitches.FindAsync(key);
            if (mdEarthSwitch == null)
            {
                return NotFound();
            }

            db.mdEarthSwitches.Remove(mdEarthSwitch);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdEarthSwitches(5)/mdPrimaryEquipment
        [EnableQuery]
        public SingleResult<mdPrimaryEquipment> GetmdPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdEarthSwitches.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdPrimaryEquipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdEarthSwitchExists(int key)
        {
            return db.mdEarthSwitches.Count(e => e.PrimaryEquipmentId == key) > 0;
        }
    }
}

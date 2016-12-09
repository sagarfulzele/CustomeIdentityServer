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
    builder.EntitySet<mdBay>("mdBays");
    builder.EntitySet<mdVoltageLevel>("mdVoltageLevels"); 
    builder.EntitySet<mdBPanel>("mdBPanels"); 
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdBaysController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdBays
        [EnableQuery]
        public IQueryable<mdBay> GetmdBays()
        {
            return db.mdBays;
        }

        // GET: odata/mdBays(5)
        [EnableQuery]
        public SingleResult<mdBay> GetmdBay([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdBays.Where(mdBay => mdBay.BayID == key));
        }

        // PUT: odata/mdBays(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdBay> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdBay mdBay = await db.mdBays.FindAsync(key);
            if (mdBay == null)
            {
                return NotFound();
            }

            patch.Put(mdBay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdBayExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdBay);
        }

        // POST: odata/mdBays
        public async Task<IHttpActionResult> Post(mdBay mdBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdBays.Add(mdBay);
            await db.SaveChangesAsync();

            return Created(mdBay);
        }

        // PATCH: odata/mdBays(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdBay> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdBay mdBay = await db.mdBays.FindAsync(key);
            if (mdBay == null)
            {
                return NotFound();
            }

            patch.Patch(mdBay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdBayExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdBay);
        }

        // DELETE: odata/mdBays(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdBay mdBay = await db.mdBays.FindAsync(key);
            if (mdBay == null)
            {
                return NotFound();
            }

            db.mdBays.Remove(mdBay);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdBays(5)/mdVoltageLevel
        [EnableQuery]
        public SingleResult<mdVoltageLevel> GetmdVoltageLevel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdBays.Where(m => m.BayID == key).Select(m => m.mdVoltageLevel));
        }

        // GET: odata/mdBays(5)/mdBPanels
        [EnableQuery]
        public IQueryable<mdBPanel> GetmdBPanels([FromODataUri] int key)
        {
            return db.mdBays.Where(m => m.BayID == key).SelectMany(m => m.mdBPanels);
        }

        // GET: odata/mdBays(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdBays.Where(m => m.BayID == key).Select(m => m.msSubstationStructure));
        }

        // GET: odata/mdBays(5)/mdPrimaryEquipments
        [EnableQuery]
        public IQueryable<mdPrimaryEquipment> GetmdPrimaryEquipments([FromODataUri] int key)
        {
            return db.mdBays.Where(m => m.BayID == key).SelectMany(m => m.mdPrimaryEquipments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdBayExists(int key)
        {
            return db.mdBays.Count(e => e.BayID == key) > 0;
        }
    }
}

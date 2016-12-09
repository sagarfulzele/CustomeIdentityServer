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
    builder.EntitySet<mdVoltageLevel>("mdVoltageLevels");
    builder.EntitySet<mdBay>("mdBays"); 
    builder.EntitySet<mdRemoteEndDetail>("mdRemoteEndDetails"); 
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    builder.EntitySet<mdVPanel>("mdVPanels"); 
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdVoltageLevelsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdVoltageLevels
        [EnableQuery]
        public IQueryable<mdVoltageLevel> GetmdVoltageLevels()
        {
            return db.mdVoltageLevels;
        }

        // GET: odata/mdVoltageLevels(5)
        [EnableQuery]
        public SingleResult<mdVoltageLevel> GetmdVoltageLevel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageLevels.Where(mdVoltageLevel => mdVoltageLevel.VoltageLevelID == key));
        }

        // PUT: odata/mdVoltageLevels(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdVoltageLevel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdVoltageLevel mdVoltageLevel = await db.mdVoltageLevels.FindAsync(key);
            if (mdVoltageLevel == null)
            {
                return NotFound();
            }

            patch.Put(mdVoltageLevel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdVoltageLevelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdVoltageLevel);
        }

        // POST: odata/mdVoltageLevels
        public async Task<IHttpActionResult> Post(mdVoltageLevel mdVoltageLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdVoltageLevels.Add(mdVoltageLevel);
            await db.SaveChangesAsync();

            return Created(mdVoltageLevel);
        }

        // PATCH: odata/mdVoltageLevels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdVoltageLevel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdVoltageLevel mdVoltageLevel = await db.mdVoltageLevels.FindAsync(key);
            if (mdVoltageLevel == null)
            {
                return NotFound();
            }

            patch.Patch(mdVoltageLevel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdVoltageLevelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdVoltageLevel);
        }

        // DELETE: odata/mdVoltageLevels(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdVoltageLevel mdVoltageLevel = await db.mdVoltageLevels.FindAsync(key);
            if (mdVoltageLevel == null)
            {
                return NotFound();
            }

            db.mdVoltageLevels.Remove(mdVoltageLevel);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdVoltageLevels(5)/mdBays
        [EnableQuery]
        public IQueryable<mdBay> GetmdBays([FromODataUri] int key)
        {
            return db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).SelectMany(m => m.mdBays);
        }

        // GET: odata/mdVoltageLevels(5)/mdRemoteEndDetails
        [EnableQuery]
        public IQueryable<mdRemoteEndDetail> GetmdRemoteEndDetails([FromODataUri] int key)
        {
            return db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).SelectMany(m => m.mdRemoteEndDetails);
        }

        // GET: odata/mdVoltageLevels(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).Select(m => m.mdSubstation));
        }

        // GET: odata/mdVoltageLevels(5)/mdVPanels
        [EnableQuery]
        public IQueryable<mdVPanel> GetmdVPanels([FromODataUri] int key)
        {
            return db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).SelectMany(m => m.mdVPanels);
        }

        // GET: odata/mdVoltageLevels(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).Select(m => m.msSubstationStructure));
        }

        // GET: odata/mdVoltageLevels(5)/msSubstationStructure1
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure1([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).Select(m => m.msSubstationStructure1));
        }

        // GET: odata/mdVoltageLevels(5)/msSubstationStructure2
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure2([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).Select(m => m.msSubstationStructure2));
        }

        // GET: odata/mdVoltageLevels(5)/msSubstationStructure3
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure3([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVoltageLevels.Where(m => m.VoltageLevelID == key).Select(m => m.msSubstationStructure3));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdVoltageLevelExists(int key)
        {
            return db.mdVoltageLevels.Count(e => e.VoltageLevelID == key) > 0;
        }
    }
}

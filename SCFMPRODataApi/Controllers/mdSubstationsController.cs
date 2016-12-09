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
    builder.EntitySet<mdSubstation>("mdSubstations");
    builder.EntitySet<mdArrangementAcLoopRow>("mdArrangementAcLoopRows"); 
    builder.EntitySet<mdArrangementAnnunciationRow>("mdArrangementAnnunciationRows"); 
    builder.EntitySet<mdArrangementPanelRow>("mdArrangementPanelRows"); 
    builder.EntitySet<mdArrangementsIndicationRow>("mdArrangementsIndicationRows"); 
    builder.EntitySet<mdArrangementSynchCircuitRow>("mdArrangementSynchCircuitRows"); 
    builder.EntitySet<mdProject>("mdProjects"); 
    builder.EntitySet<mdSchematicProjectPath>("mdSchematicProjectPaths"); 
    builder.EntitySet<mdSPanel>("mdSPanels"); 
    builder.EntitySet<mdVoltageLevel>("mdVoltageLevels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class mdSubstationsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdSubstations
        [EnableQuery]
        public IQueryable<mdSubstation> GetmdSubstations()
        {
            return db.mdSubstations;
        }

        // GET: odata/mdSubstations(5)
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSubstations.Where(mdSubstation => mdSubstation.SubstationID == key));
        }

        // PUT: odata/mdSubstations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdSubstation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSubstation mdSubstation = await db.mdSubstations.FindAsync(key);
            if (mdSubstation == null)
            {
                return NotFound();
            }

            patch.Put(mdSubstation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSubstationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSubstation);
        }

        // POST: odata/mdSubstations
        public async Task<IHttpActionResult> Post(mdSubstation mdSubstation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdSubstations.Add(mdSubstation);
            await db.SaveChangesAsync();

            return Created(mdSubstation);
        }

        // PATCH: odata/mdSubstations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdSubstation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSubstation mdSubstation = await db.mdSubstations.FindAsync(key);
            if (mdSubstation == null)
            {
                return NotFound();
            }

            patch.Patch(mdSubstation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSubstationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSubstation);
        }

        // DELETE: odata/mdSubstations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdSubstation mdSubstation = await db.mdSubstations.FindAsync(key);
            if (mdSubstation == null)
            {
                return NotFound();
            }

            db.mdSubstations.Remove(mdSubstation);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdSubstations(5)/mdArrangementAcLoopRows
        [EnableQuery]
        public IQueryable<mdArrangementAcLoopRow> GetmdArrangementAcLoopRows([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdArrangementAcLoopRows);
        }

        // GET: odata/mdSubstations(5)/mdArrangementAnnunciationRows
        [EnableQuery]
        public IQueryable<mdArrangementAnnunciationRow> GetmdArrangementAnnunciationRows([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdArrangementAnnunciationRows);
        }

        // GET: odata/mdSubstations(5)/mdArrangementPanelRows
        [EnableQuery]
        public IQueryable<mdArrangementPanelRow> GetmdArrangementPanelRows([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdArrangementPanelRows);
        }

        // GET: odata/mdSubstations(5)/mdArrangementsIndicationRows
        [EnableQuery]
        public IQueryable<mdArrangementsIndicationRow> GetmdArrangementsIndicationRows([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdArrangementsIndicationRows);
        }

        // GET: odata/mdSubstations(5)/mdArrangementSynchCircuitRows
        [EnableQuery]
        public IQueryable<mdArrangementSynchCircuitRow> GetmdArrangementSynchCircuitRows([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdArrangementSynchCircuitRows);
        }

        // GET: odata/mdSubstations(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSubstations.Where(m => m.SubstationID == key).Select(m => m.mdProject));
        }

        // GET: odata/mdSubstations(5)/mdSchematicProjectPaths
        [EnableQuery]
        public IQueryable<mdSchematicProjectPath> GetmdSchematicProjectPaths([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdSchematicProjectPaths);
        }

        // GET: odata/mdSubstations(5)/mdSPanels
        [EnableQuery]
        public IQueryable<mdSPanel> GetmdSPanels([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdSPanels);
        }

        // GET: odata/mdSubstations(5)/mdVoltageLevels
        [EnableQuery]
        public IQueryable<mdVoltageLevel> GetmdVoltageLevels([FromODataUri] int key)
        {
            return db.mdSubstations.Where(m => m.SubstationID == key).SelectMany(m => m.mdVoltageLevels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdSubstationExists(int key)
        {
            return db.mdSubstations.Count(e => e.SubstationID == key) > 0;
        }
    }
}

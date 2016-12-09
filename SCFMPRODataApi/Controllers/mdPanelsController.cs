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
    builder.EntitySet<mdPanel>("mdPanels");
    builder.EntitySet<mdArrangementAcLoopRow>("mdArrangementAcLoopRows"); 
    builder.EntitySet<mdArrangementAnnunciationRow>("mdArrangementAnnunciationRows"); 
    builder.EntitySet<mdArrangementPanelRow>("mdArrangementPanelRows"); 
    builder.EntitySet<mdArrangementsIndicationRow>("mdArrangementsIndicationRows"); 
    builder.EntitySet<mdArrangementSynchCircuitRow>("mdArrangementSynchCircuitRows"); 
    builder.EntitySet<mdBPanel>("mdBPanels"); 
    builder.EntitySet<mdPanelFunction>("mdPanelFunctions"); 
    builder.EntitySet<mdPanelSize>("mdPanelSizes"); 
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    builder.EntitySet<mdSPanel>("mdSPanels"); 
    builder.EntitySet<mdVPanel>("mdVPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPanelsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels()
        {
            return db.mdPanels;
        }

        // GET: odata/mdPanels(5)
        [EnableQuery]
        public SingleResult<mdPanel> GetmdPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(mdPanel => mdPanel.PanelID == key));
        }

        // PUT: odata/mdPanels(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanel mdPanel = await db.mdPanels.FindAsync(key);
            if (mdPanel == null)
            {
                return NotFound();
            }

            patch.Put(mdPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanel);
        }

        // POST: odata/mdPanels
        public async Task<IHttpActionResult> Post(mdPanel mdPanel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPanels.Add(mdPanel);
            await db.SaveChangesAsync();

            return Created(mdPanel);
        }

        // PATCH: odata/mdPanels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanel mdPanel = await db.mdPanels.FindAsync(key);
            if (mdPanel == null)
            {
                return NotFound();
            }

            patch.Patch(mdPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanel);
        }

        // DELETE: odata/mdPanels(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPanel mdPanel = await db.mdPanels.FindAsync(key);
            if (mdPanel == null)
            {
                return NotFound();
            }

            db.mdPanels.Remove(mdPanel);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPanels(5)/mdArrangementAcLoopRow
        [EnableQuery]
        public SingleResult<mdArrangementAcLoopRow> GetmdArrangementAcLoopRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdArrangementAcLoopRow));
        }

        // GET: odata/mdPanels(5)/mdArrangementAnnunciationRow
        [EnableQuery]
        public SingleResult<mdArrangementAnnunciationRow> GetmdArrangementAnnunciationRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdArrangementAnnunciationRow));
        }

        // GET: odata/mdPanels(5)/mdArrangementPanelRow
        [EnableQuery]
        public SingleResult<mdArrangementPanelRow> GetmdArrangementPanelRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdArrangementPanelRow));
        }

        // GET: odata/mdPanels(5)/mdArrangementsIndicationRow
        [EnableQuery]
        public SingleResult<mdArrangementsIndicationRow> GetmdArrangementsIndicationRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdArrangementsIndicationRow));
        }

        // GET: odata/mdPanels(5)/mdArrangementSynchCircuitRow
        [EnableQuery]
        public SingleResult<mdArrangementSynchCircuitRow> GetmdArrangementSynchCircuitRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdArrangementSynchCircuitRow));
        }

        // GET: odata/mdPanels(5)/mdBPanel
        [EnableQuery]
        public SingleResult<mdBPanel> GetmdBPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdBPanel));
        }

        // GET: odata/mdPanels(5)/mdPanelFunctions
        [EnableQuery]
        public IQueryable<mdPanelFunction> GetmdPanelFunctions([FromODataUri] int key)
        {
            return db.mdPanels.Where(m => m.PanelID == key).SelectMany(m => m.mdPanelFunctions);
        }

        // GET: odata/mdPanels(5)/mdPanels1
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels1([FromODataUri] int key)
        {
            return db.mdPanels.Where(m => m.PanelID == key).SelectMany(m => m.mdPanels1);
        }

        // GET: odata/mdPanels(5)/mdPanel1
        [EnableQuery]
        public SingleResult<mdPanel> GetmdPanel1([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdPanel1));
        }

        // GET: odata/mdPanels(5)/mdPanelSize
        [EnableQuery]
        public SingleResult<mdPanelSize> GetmdPanelSize([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdPanelSize));
        }

        // GET: odata/mdPanels(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.msSubstationStructure));
        }

        // GET: odata/mdPanels(5)/mdSPanel
        [EnableQuery]
        public SingleResult<mdSPanel> GetmdSPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdSPanel));
        }

        // GET: odata/mdPanels(5)/mdVPanel
        [EnableQuery]
        public SingleResult<mdVPanel> GetmdVPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanels.Where(m => m.PanelID == key).Select(m => m.mdVPanel));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPanelExists(int key)
        {
            return db.mdPanels.Count(e => e.PanelID == key) > 0;
        }
    }
}

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
    builder.EntitySet<mdVPanel>("mdVPanels");
    builder.EntitySet<mdPanel>("mdPanels"); 
    builder.EntitySet<mdVoltageLevel>("mdVoltageLevels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdVPanelsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdVPanels
        [EnableQuery]
        public IQueryable<mdVPanel> GetmdVPanels()
        {
            return db.mdVPanels;
        }

        // GET: odata/mdVPanels(5)
        [EnableQuery]
        public SingleResult<mdVPanel> GetmdVPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVPanels.Where(mdVPanel => mdVPanel.PanelID == key));
        }

        // PUT: odata/mdVPanels(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdVPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdVPanel mdVPanel = await db.mdVPanels.FindAsync(key);
            if (mdVPanel == null)
            {
                return NotFound();
            }

            patch.Put(mdVPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdVPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdVPanel);
        }

        // POST: odata/mdVPanels
        public async Task<IHttpActionResult> Post(mdVPanel mdVPanel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdVPanels.Add(mdVPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdVPanelExists(mdVPanel.PanelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdVPanel);
        }

        // PATCH: odata/mdVPanels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdVPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdVPanel mdVPanel = await db.mdVPanels.FindAsync(key);
            if (mdVPanel == null)
            {
                return NotFound();
            }

            patch.Patch(mdVPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdVPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdVPanel);
        }

        // DELETE: odata/mdVPanels(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdVPanel mdVPanel = await db.mdVPanels.FindAsync(key);
            if (mdVPanel == null)
            {
                return NotFound();
            }

            db.mdVPanels.Remove(mdVPanel);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdVPanels(5)/mdPanel
        [EnableQuery]
        public SingleResult<mdPanel> GetmdPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVPanels.Where(m => m.PanelID == key).Select(m => m.mdPanel));
        }

        // GET: odata/mdVPanels(5)/mdVoltageLevel
        [EnableQuery]
        public SingleResult<mdVoltageLevel> GetmdVoltageLevel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdVPanels.Where(m => m.PanelID == key).Select(m => m.mdVoltageLevel));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdVPanelExists(int key)
        {
            return db.mdVPanels.Count(e => e.PanelID == key) > 0;
        }
    }
}

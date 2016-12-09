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
    builder.EntitySet<mdSPanel>("mdSPanels");
    builder.EntitySet<mdPanel>("mdPanels"); 
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdSPanelsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdSPanels
        [EnableQuery]
        public IQueryable<mdSPanel> GetmdSPanels()
        {
            return db.mdSPanels;
        }

        // GET: odata/mdSPanels(5)
        [EnableQuery]
        public SingleResult<mdSPanel> GetmdSPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSPanels.Where(mdSPanel => mdSPanel.PanelID == key));
        }

        // PUT: odata/mdSPanels(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdSPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSPanel mdSPanel = await db.mdSPanels.FindAsync(key);
            if (mdSPanel == null)
            {
                return NotFound();
            }

            patch.Put(mdSPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSPanel);
        }

        // POST: odata/mdSPanels
        public async Task<IHttpActionResult> Post(mdSPanel mdSPanel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdSPanels.Add(mdSPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdSPanelExists(mdSPanel.PanelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdSPanel);
        }

        // PATCH: odata/mdSPanels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdSPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSPanel mdSPanel = await db.mdSPanels.FindAsync(key);
            if (mdSPanel == null)
            {
                return NotFound();
            }

            patch.Patch(mdSPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSPanel);
        }

        // DELETE: odata/mdSPanels(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdSPanel mdSPanel = await db.mdSPanels.FindAsync(key);
            if (mdSPanel == null)
            {
                return NotFound();
            }

            db.mdSPanels.Remove(mdSPanel);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdSPanels(5)/mdPanel
        [EnableQuery]
        public SingleResult<mdPanel> GetmdPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSPanels.Where(m => m.PanelID == key).Select(m => m.mdPanel));
        }

        // GET: odata/mdSPanels(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSPanels.Where(m => m.PanelID == key).Select(m => m.mdSubstation));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdSPanelExists(int key)
        {
            return db.mdSPanels.Count(e => e.PanelID == key) > 0;
        }
    }
}

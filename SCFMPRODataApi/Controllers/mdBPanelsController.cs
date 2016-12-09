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
    builder.EntitySet<mdBPanel>("mdBPanels");
    builder.EntitySet<mdBay>("mdBays"); 
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdBPanelsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdBPanels
        [EnableQuery]
        public IQueryable<mdBPanel> GetmdBPanels()
        {
            return db.mdBPanels;
        }

        // GET: odata/mdBPanels(5)
        [EnableQuery]
        public SingleResult<mdBPanel> GetmdBPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdBPanels.Where(mdBPanel => mdBPanel.PanelID == key));
        }

        // PUT: odata/mdBPanels(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdBPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdBPanel mdBPanel = await db.mdBPanels.FindAsync(key);
            if (mdBPanel == null)
            {
                return NotFound();
            }

            patch.Put(mdBPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdBPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdBPanel);
        }

        // POST: odata/mdBPanels
        public async Task<IHttpActionResult> Post(mdBPanel mdBPanel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdBPanels.Add(mdBPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdBPanelExists(mdBPanel.PanelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdBPanel);
        }

        // PATCH: odata/mdBPanels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdBPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdBPanel mdBPanel = await db.mdBPanels.FindAsync(key);
            if (mdBPanel == null)
            {
                return NotFound();
            }

            patch.Patch(mdBPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdBPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdBPanel);
        }

        // DELETE: odata/mdBPanels(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdBPanel mdBPanel = await db.mdBPanels.FindAsync(key);
            if (mdBPanel == null)
            {
                return NotFound();
            }

            db.mdBPanels.Remove(mdBPanel);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdBPanels(5)/mdBay
        [EnableQuery]
        public SingleResult<mdBay> GetmdBay([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdBPanels.Where(m => m.PanelID == key).Select(m => m.mdBay));
        }

        // GET: odata/mdBPanels(5)/mdPanel
        [EnableQuery]
        public SingleResult<mdPanel> GetmdPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdBPanels.Where(m => m.PanelID == key).Select(m => m.mdPanel));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdBPanelExists(int key)
        {
            return db.mdBPanels.Count(e => e.PanelID == key) > 0;
        }
    }
}

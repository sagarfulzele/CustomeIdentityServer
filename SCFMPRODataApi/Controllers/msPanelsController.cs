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
    builder.EntitySet<msPanel>("msPanels");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msPanelsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msPanels
        [EnableQuery]
        public IQueryable<msPanel> GetmsPanels()
        {
            return db.msPanels;
        }

        // GET: odata/msPanels(5)
        [EnableQuery]
        public SingleResult<msPanel> GetmsPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPanels.Where(msPanel => msPanel.PanelID == key));
        }

        // PUT: odata/msPanels(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPanel msPanel = await db.msPanels.FindAsync(key);
            if (msPanel == null)
            {
                return NotFound();
            }

            patch.Put(msPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPanel);
        }

        // POST: odata/msPanels
        public async Task<IHttpActionResult> Post(msPanel msPanel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msPanels.Add(msPanel);
            await db.SaveChangesAsync();

            return Created(msPanel);
        }

        // PATCH: odata/msPanels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msPanel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPanel msPanel = await db.msPanels.FindAsync(key);
            if (msPanel == null)
            {
                return NotFound();
            }

            patch.Patch(msPanel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPanelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPanel);
        }

        // DELETE: odata/msPanels(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msPanel msPanel = await db.msPanels.FindAsync(key);
            if (msPanel == null)
            {
                return NotFound();
            }

            db.msPanels.Remove(msPanel);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msPanelExists(int key)
        {
            return db.msPanels.Count(e => e.PanelID == key) > 0;
        }
    }
}

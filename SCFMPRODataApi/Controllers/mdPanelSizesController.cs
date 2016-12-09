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
    builder.EntitySet<mdPanelSize>("mdPanelSizes");
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPanelSizesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPanelSizes
        [EnableQuery]
        public IQueryable<mdPanelSize> GetmdPanelSizes()
        {
            return db.mdPanelSizes;
        }

        // GET: odata/mdPanelSizes(5)
        [EnableQuery]
        public SingleResult<mdPanelSize> GetmdPanelSize([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelSizes.Where(mdPanelSize => mdPanelSize.PanelSizeID == key));
        }

        // PUT: odata/mdPanelSizes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPanelSize> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelSize mdPanelSize = await db.mdPanelSizes.FindAsync(key);
            if (mdPanelSize == null)
            {
                return NotFound();
            }

            patch.Put(mdPanelSize);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelSizeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelSize);
        }

        // POST: odata/mdPanelSizes
        public async Task<IHttpActionResult> Post(mdPanelSize mdPanelSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPanelSizes.Add(mdPanelSize);
            await db.SaveChangesAsync();

            return Created(mdPanelSize);
        }

        // PATCH: odata/mdPanelSizes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPanelSize> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelSize mdPanelSize = await db.mdPanelSizes.FindAsync(key);
            if (mdPanelSize == null)
            {
                return NotFound();
            }

            patch.Patch(mdPanelSize);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelSizeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelSize);
        }

        // DELETE: odata/mdPanelSizes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPanelSize mdPanelSize = await db.mdPanelSizes.FindAsync(key);
            if (mdPanelSize == null)
            {
                return NotFound();
            }

            db.mdPanelSizes.Remove(mdPanelSize);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPanelSizes(5)/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels([FromODataUri] int key)
        {
            return db.mdPanelSizes.Where(m => m.PanelSizeID == key).SelectMany(m => m.mdPanels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPanelSizeExists(int key)
        {
            return db.mdPanelSizes.Count(e => e.PanelSizeID == key) > 0;
        }
    }
}

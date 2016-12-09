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
    builder.EntitySet<mdPanelSizesSub>("mdPanelSizesSubs");
    builder.EntitySet<mdPanelsSub>("mdPanelsSubs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPanelSizesSubsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPanelSizesSubs
        [EnableQuery]
        public IQueryable<mdPanelSizesSub> GetmdPanelSizesSubs()
        {
            return db.mdPanelSizesSubs;
        }

        // GET: odata/mdPanelSizesSubs(5)
        [EnableQuery]
        public SingleResult<mdPanelSizesSub> GetmdPanelSizesSub([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelSizesSubs.Where(mdPanelSizesSub => mdPanelSizesSub.PanelSizeID == key));
        }

        // PUT: odata/mdPanelSizesSubs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPanelSizesSub> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelSizesSub mdPanelSizesSub = await db.mdPanelSizesSubs.FindAsync(key);
            if (mdPanelSizesSub == null)
            {
                return NotFound();
            }

            patch.Put(mdPanelSizesSub);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelSizesSubExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelSizesSub);
        }

        // POST: odata/mdPanelSizesSubs
        public async Task<IHttpActionResult> Post(mdPanelSizesSub mdPanelSizesSub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPanelSizesSubs.Add(mdPanelSizesSub);
            await db.SaveChangesAsync();

            return Created(mdPanelSizesSub);
        }

        // PATCH: odata/mdPanelSizesSubs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPanelSizesSub> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelSizesSub mdPanelSizesSub = await db.mdPanelSizesSubs.FindAsync(key);
            if (mdPanelSizesSub == null)
            {
                return NotFound();
            }

            patch.Patch(mdPanelSizesSub);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelSizesSubExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelSizesSub);
        }

        // DELETE: odata/mdPanelSizesSubs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPanelSizesSub mdPanelSizesSub = await db.mdPanelSizesSubs.FindAsync(key);
            if (mdPanelSizesSub == null)
            {
                return NotFound();
            }

            db.mdPanelSizesSubs.Remove(mdPanelSizesSub);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPanelSizesSubs(5)/mdPanelsSub
        [EnableQuery]
        public SingleResult<mdPanelsSub> GetmdPanelsSub([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelSizesSubs.Where(m => m.PanelSizeID == key).Select(m => m.mdPanelsSub));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPanelSizesSubExists(int key)
        {
            return db.mdPanelSizesSubs.Count(e => e.PanelSizeID == key) > 0;
        }
    }
}

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
    builder.EntitySet<mdPanelsSub>("mdPanelsSubs");
    builder.EntitySet<mdPanelFunctionsSub>("mdPanelFunctionsSubs"); 
    builder.EntitySet<mdPanelSizesSub>("mdPanelSizesSubs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPanelsSubsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPanelsSubs
        [EnableQuery]
        public IQueryable<mdPanelsSub> GetmdPanelsSubs()
        {
            return db.mdPanelsSubs;
        }

        // GET: odata/mdPanelsSubs(5)
        [EnableQuery]
        public SingleResult<mdPanelsSub> GetmdPanelsSub([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelsSubs.Where(mdPanelsSub => mdPanelsSub.PanelID == key));
        }

        // PUT: odata/mdPanelsSubs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPanelsSub> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelsSub mdPanelsSub = await db.mdPanelsSubs.FindAsync(key);
            if (mdPanelsSub == null)
            {
                return NotFound();
            }

            patch.Put(mdPanelsSub);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelsSubExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelsSub);
        }

        // POST: odata/mdPanelsSubs
        public async Task<IHttpActionResult> Post(mdPanelsSub mdPanelsSub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPanelsSubs.Add(mdPanelsSub);
            await db.SaveChangesAsync();

            return Created(mdPanelsSub);
        }

        // PATCH: odata/mdPanelsSubs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPanelsSub> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelsSub mdPanelsSub = await db.mdPanelsSubs.FindAsync(key);
            if (mdPanelsSub == null)
            {
                return NotFound();
            }

            patch.Patch(mdPanelsSub);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelsSubExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelsSub);
        }

        // DELETE: odata/mdPanelsSubs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPanelsSub mdPanelsSub = await db.mdPanelsSubs.FindAsync(key);
            if (mdPanelsSub == null)
            {
                return NotFound();
            }

            db.mdPanelsSubs.Remove(mdPanelsSub);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPanelsSubs(5)/mdPanelFunctionsSubs
        [EnableQuery]
        public IQueryable<mdPanelFunctionsSub> GetmdPanelFunctionsSubs([FromODataUri] int key)
        {
            return db.mdPanelsSubs.Where(m => m.PanelID == key).SelectMany(m => m.mdPanelFunctionsSubs);
        }

        // GET: odata/mdPanelsSubs(5)/mdPanelSizesSubs
        [EnableQuery]
        public IQueryable<mdPanelSizesSub> GetmdPanelSizesSubs([FromODataUri] int key)
        {
            return db.mdPanelsSubs.Where(m => m.PanelID == key).SelectMany(m => m.mdPanelSizesSubs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPanelsSubExists(int key)
        {
            return db.mdPanelsSubs.Count(e => e.PanelID == key) > 0;
        }
    }
}

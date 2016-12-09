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
    builder.EntitySet<mdPanelFunctionsSub>("mdPanelFunctionsSubs");
    builder.EntitySet<mdPanelsSub>("mdPanelsSubs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPanelFunctionsSubsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPanelFunctionsSubs
        [EnableQuery]
        public IQueryable<mdPanelFunctionsSub> GetmdPanelFunctionsSubs()
        {
            return db.mdPanelFunctionsSubs;
        }

        // GET: odata/mdPanelFunctionsSubs(5)
        [EnableQuery]
        public SingleResult<mdPanelFunctionsSub> GetmdPanelFunctionsSub([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelFunctionsSubs.Where(mdPanelFunctionsSub => mdPanelFunctionsSub.PanelFunctionID == key));
        }

        // PUT: odata/mdPanelFunctionsSubs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPanelFunctionsSub> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelFunctionsSub mdPanelFunctionsSub = await db.mdPanelFunctionsSubs.FindAsync(key);
            if (mdPanelFunctionsSub == null)
            {
                return NotFound();
            }

            patch.Put(mdPanelFunctionsSub);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelFunctionsSubExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelFunctionsSub);
        }

        // POST: odata/mdPanelFunctionsSubs
        public async Task<IHttpActionResult> Post(mdPanelFunctionsSub mdPanelFunctionsSub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPanelFunctionsSubs.Add(mdPanelFunctionsSub);
            await db.SaveChangesAsync();

            return Created(mdPanelFunctionsSub);
        }

        // PATCH: odata/mdPanelFunctionsSubs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPanelFunctionsSub> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelFunctionsSub mdPanelFunctionsSub = await db.mdPanelFunctionsSubs.FindAsync(key);
            if (mdPanelFunctionsSub == null)
            {
                return NotFound();
            }

            patch.Patch(mdPanelFunctionsSub);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelFunctionsSubExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelFunctionsSub);
        }

        // DELETE: odata/mdPanelFunctionsSubs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPanelFunctionsSub mdPanelFunctionsSub = await db.mdPanelFunctionsSubs.FindAsync(key);
            if (mdPanelFunctionsSub == null)
            {
                return NotFound();
            }

            db.mdPanelFunctionsSubs.Remove(mdPanelFunctionsSub);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPanelFunctionsSubs(5)/mdPanelsSub
        [EnableQuery]
        public SingleResult<mdPanelsSub> GetmdPanelsSub([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelFunctionsSubs.Where(m => m.PanelFunctionID == key).Select(m => m.mdPanelsSub));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPanelFunctionsSubExists(int key)
        {
            return db.mdPanelFunctionsSubs.Count(e => e.PanelFunctionID == key) > 0;
        }
    }
}

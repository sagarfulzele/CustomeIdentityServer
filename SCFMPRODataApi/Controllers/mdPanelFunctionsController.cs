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
    builder.EntitySet<mdPanelFunction>("mdPanelFunctions");
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPanelFunctionsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPanelFunctions
        [EnableQuery]
        public IQueryable<mdPanelFunction> GetmdPanelFunctions()
        {
            return db.mdPanelFunctions;
        }

        // GET: odata/mdPanelFunctions(5)
        [EnableQuery]
        public SingleResult<mdPanelFunction> GetmdPanelFunction([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelFunctions.Where(mdPanelFunction => mdPanelFunction.PanelFunctionID == key));
        }

        // PUT: odata/mdPanelFunctions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPanelFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelFunction mdPanelFunction = await db.mdPanelFunctions.FindAsync(key);
            if (mdPanelFunction == null)
            {
                return NotFound();
            }

            patch.Put(mdPanelFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelFunction);
        }

        // POST: odata/mdPanelFunctions
        public async Task<IHttpActionResult> Post(mdPanelFunction mdPanelFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPanelFunctions.Add(mdPanelFunction);
            await db.SaveChangesAsync();

            return Created(mdPanelFunction);
        }

        // PATCH: odata/mdPanelFunctions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPanelFunction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelFunction mdPanelFunction = await db.mdPanelFunctions.FindAsync(key);
            if (mdPanelFunction == null)
            {
                return NotFound();
            }

            patch.Patch(mdPanelFunction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelFunctionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelFunction);
        }

        // DELETE: odata/mdPanelFunctions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPanelFunction mdPanelFunction = await db.mdPanelFunctions.FindAsync(key);
            if (mdPanelFunction == null)
            {
                return NotFound();
            }

            db.mdPanelFunctions.Remove(mdPanelFunction);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPanelFunctions(5)/mdPanel
        [EnableQuery]
        public SingleResult<mdPanel> GetmdPanel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelFunctions.Where(m => m.PanelFunctionID == key).Select(m => m.mdPanel));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPanelFunctionExists(int key)
        {
            return db.mdPanelFunctions.Count(e => e.PanelFunctionID == key) > 0;
        }
    }
}

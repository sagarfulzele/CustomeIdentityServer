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
    builder.EntitySet<mdAllTerminalBlocksFromScheme>("mdAllTerminalBlocksFromSchemes");
    builder.EntitySet<mdSchematicProjectPath>("mdSchematicProjectPaths"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdAllTerminalBlocksFromSchemesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdAllTerminalBlocksFromSchemes
        [EnableQuery]
        public IQueryable<mdAllTerminalBlocksFromScheme> GetmdAllTerminalBlocksFromSchemes()
        {
            return db.mdAllTerminalBlocksFromSchemes;
        }

        // GET: odata/mdAllTerminalBlocksFromSchemes(5)
        [EnableQuery]
        public SingleResult<mdAllTerminalBlocksFromScheme> GetmdAllTerminalBlocksFromScheme([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAllTerminalBlocksFromSchemes.Where(mdAllTerminalBlocksFromScheme => mdAllTerminalBlocksFromScheme.ID == key));
        }

        // PUT: odata/mdAllTerminalBlocksFromSchemes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdAllTerminalBlocksFromScheme> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAllTerminalBlocksFromScheme mdAllTerminalBlocksFromScheme = await db.mdAllTerminalBlocksFromSchemes.FindAsync(key);
            if (mdAllTerminalBlocksFromScheme == null)
            {
                return NotFound();
            }

            patch.Put(mdAllTerminalBlocksFromScheme);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAllTerminalBlocksFromSchemeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAllTerminalBlocksFromScheme);
        }

        // POST: odata/mdAllTerminalBlocksFromSchemes
        public async Task<IHttpActionResult> Post(mdAllTerminalBlocksFromScheme mdAllTerminalBlocksFromScheme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdAllTerminalBlocksFromSchemes.Add(mdAllTerminalBlocksFromScheme);
            await db.SaveChangesAsync();

            return Created(mdAllTerminalBlocksFromScheme);
        }

        // PATCH: odata/mdAllTerminalBlocksFromSchemes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdAllTerminalBlocksFromScheme> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAllTerminalBlocksFromScheme mdAllTerminalBlocksFromScheme = await db.mdAllTerminalBlocksFromSchemes.FindAsync(key);
            if (mdAllTerminalBlocksFromScheme == null)
            {
                return NotFound();
            }

            patch.Patch(mdAllTerminalBlocksFromScheme);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAllTerminalBlocksFromSchemeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAllTerminalBlocksFromScheme);
        }

        // DELETE: odata/mdAllTerminalBlocksFromSchemes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdAllTerminalBlocksFromScheme mdAllTerminalBlocksFromScheme = await db.mdAllTerminalBlocksFromSchemes.FindAsync(key);
            if (mdAllTerminalBlocksFromScheme == null)
            {
                return NotFound();
            }

            db.mdAllTerminalBlocksFromSchemes.Remove(mdAllTerminalBlocksFromScheme);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdAllTerminalBlocksFromSchemes(5)/mdSchematicProjectPath
        [EnableQuery]
        public SingleResult<mdSchematicProjectPath> GetmdSchematicProjectPath([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAllTerminalBlocksFromSchemes.Where(m => m.ID == key).Select(m => m.mdSchematicProjectPath));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdAllTerminalBlocksFromSchemeExists(int key)
        {
            return db.mdAllTerminalBlocksFromSchemes.Count(e => e.ID == key) > 0;
        }
    }
}

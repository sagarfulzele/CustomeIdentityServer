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
    builder.EntitySet<mdSchematicProjectPath>("mdSchematicProjectPaths");
    builder.EntitySet<mdAllComponentsFromScheme>("mdAllComponentsFromSchemes"); 
    builder.EntitySet<mdAllTerminalBlocksFromScheme>("mdAllTerminalBlocksFromSchemes"); 
    builder.EntitySet<mdFileTime>("mdFileTimes"); 
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdSchematicProjectPathsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdSchematicProjectPaths
        [EnableQuery]
        public IQueryable<mdSchematicProjectPath> GetmdSchematicProjectPaths()
        {
            return db.mdSchematicProjectPaths;
        }

        // GET: odata/mdSchematicProjectPaths(5)
        [EnableQuery]
        public SingleResult<mdSchematicProjectPath> GetmdSchematicProjectPath([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSchematicProjectPaths.Where(mdSchematicProjectPath => mdSchematicProjectPath.PathID == key));
        }

        // PUT: odata/mdSchematicProjectPaths(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdSchematicProjectPath> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSchematicProjectPath mdSchematicProjectPath = await db.mdSchematicProjectPaths.FindAsync(key);
            if (mdSchematicProjectPath == null)
            {
                return NotFound();
            }

            patch.Put(mdSchematicProjectPath);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSchematicProjectPathExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSchematicProjectPath);
        }

        // POST: odata/mdSchematicProjectPaths
        public async Task<IHttpActionResult> Post(mdSchematicProjectPath mdSchematicProjectPath)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdSchematicProjectPaths.Add(mdSchematicProjectPath);
            await db.SaveChangesAsync();

            return Created(mdSchematicProjectPath);
        }

        // PATCH: odata/mdSchematicProjectPaths(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdSchematicProjectPath> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSchematicProjectPath mdSchematicProjectPath = await db.mdSchematicProjectPaths.FindAsync(key);
            if (mdSchematicProjectPath == null)
            {
                return NotFound();
            }

            patch.Patch(mdSchematicProjectPath);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSchematicProjectPathExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSchematicProjectPath);
        }

        // DELETE: odata/mdSchematicProjectPaths(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdSchematicProjectPath mdSchematicProjectPath = await db.mdSchematicProjectPaths.FindAsync(key);
            if (mdSchematicProjectPath == null)
            {
                return NotFound();
            }

            db.mdSchematicProjectPaths.Remove(mdSchematicProjectPath);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdSchematicProjectPaths(5)/mdAllComponentsFromSchemes
        [EnableQuery]
        public IQueryable<mdAllComponentsFromScheme> GetmdAllComponentsFromSchemes([FromODataUri] int key)
        {
            return db.mdSchematicProjectPaths.Where(m => m.PathID == key).SelectMany(m => m.mdAllComponentsFromSchemes);
        }

        // GET: odata/mdSchematicProjectPaths(5)/mdAllTerminalBlocksFromSchemes
        [EnableQuery]
        public IQueryable<mdAllTerminalBlocksFromScheme> GetmdAllTerminalBlocksFromSchemes([FromODataUri] int key)
        {
            return db.mdSchematicProjectPaths.Where(m => m.PathID == key).SelectMany(m => m.mdAllTerminalBlocksFromSchemes);
        }

        // GET: odata/mdSchematicProjectPaths(5)/mdFileTimes
        [EnableQuery]
        public IQueryable<mdFileTime> GetmdFileTimes([FromODataUri] int key)
        {
            return db.mdSchematicProjectPaths.Where(m => m.PathID == key).SelectMany(m => m.mdFileTimes);
        }

        // GET: odata/mdSchematicProjectPaths(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSchematicProjectPaths.Where(m => m.PathID == key).Select(m => m.mdSubstation));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdSchematicProjectPathExists(int key)
        {
            return db.mdSchematicProjectPaths.Count(e => e.PathID == key) > 0;
        }
    }
}

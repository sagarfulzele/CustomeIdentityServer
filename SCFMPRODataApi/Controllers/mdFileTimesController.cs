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
    builder.EntitySet<mdFileTime>("mdFileTimes");
    builder.EntitySet<mdSchematicProjectPath>("mdSchematicProjectPaths"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdFileTimesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdFileTimes
        [EnableQuery]
        public IQueryable<mdFileTime> GetmdFileTimes()
        {
            return db.mdFileTimes;
        }

        // GET: odata/mdFileTimes(5)
        [EnableQuery]
        public SingleResult<mdFileTime> GetmdFileTime([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdFileTimes.Where(mdFileTime => mdFileTime.FileTimeID == key));
        }

        // PUT: odata/mdFileTimes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdFileTime> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdFileTime mdFileTime = await db.mdFileTimes.FindAsync(key);
            if (mdFileTime == null)
            {
                return NotFound();
            }

            patch.Put(mdFileTime);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdFileTimeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdFileTime);
        }

        // POST: odata/mdFileTimes
        public async Task<IHttpActionResult> Post(mdFileTime mdFileTime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdFileTimes.Add(mdFileTime);
            await db.SaveChangesAsync();

            return Created(mdFileTime);
        }

        // PATCH: odata/mdFileTimes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdFileTime> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdFileTime mdFileTime = await db.mdFileTimes.FindAsync(key);
            if (mdFileTime == null)
            {
                return NotFound();
            }

            patch.Patch(mdFileTime);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdFileTimeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdFileTime);
        }

        // DELETE: odata/mdFileTimes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdFileTime mdFileTime = await db.mdFileTimes.FindAsync(key);
            if (mdFileTime == null)
            {
                return NotFound();
            }

            db.mdFileTimes.Remove(mdFileTime);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdFileTimes(5)/mdSchematicProjectPath
        [EnableQuery]
        public SingleResult<mdSchematicProjectPath> GetmdSchematicProjectPath([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdFileTimes.Where(m => m.FileTimeID == key).Select(m => m.mdSchematicProjectPath));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdFileTimeExists(int key)
        {
            return db.mdFileTimes.Count(e => e.FileTimeID == key) > 0;
        }
    }
}

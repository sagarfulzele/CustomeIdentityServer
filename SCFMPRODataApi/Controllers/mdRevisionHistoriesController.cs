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
    builder.EntitySet<mdRevisionHistory>("mdRevisionHistories");
    builder.EntitySet<mdProject>("mdProjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdRevisionHistoriesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdRevisionHistories
        [EnableQuery]
        public IQueryable<mdRevisionHistory> GetmdRevisionHistories()
        {
            return db.mdRevisionHistories;
        }

        // GET: odata/mdRevisionHistories(5)
        [EnableQuery]
        public SingleResult<mdRevisionHistory> GetmdRevisionHistory([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdRevisionHistories.Where(mdRevisionHistory => mdRevisionHistory.RevisionHistoryID == key));
        }

        // PUT: odata/mdRevisionHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdRevisionHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdRevisionHistory mdRevisionHistory = await db.mdRevisionHistories.FindAsync(key);
            if (mdRevisionHistory == null)
            {
                return NotFound();
            }

            patch.Put(mdRevisionHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdRevisionHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdRevisionHistory);
        }

        // POST: odata/mdRevisionHistories
        public async Task<IHttpActionResult> Post(mdRevisionHistory mdRevisionHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdRevisionHistories.Add(mdRevisionHistory);
            await db.SaveChangesAsync();

            return Created(mdRevisionHistory);
        }

        // PATCH: odata/mdRevisionHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdRevisionHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdRevisionHistory mdRevisionHistory = await db.mdRevisionHistories.FindAsync(key);
            if (mdRevisionHistory == null)
            {
                return NotFound();
            }

            patch.Patch(mdRevisionHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdRevisionHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdRevisionHistory);
        }

        // DELETE: odata/mdRevisionHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdRevisionHistory mdRevisionHistory = await db.mdRevisionHistories.FindAsync(key);
            if (mdRevisionHistory == null)
            {
                return NotFound();
            }

            db.mdRevisionHistories.Remove(mdRevisionHistory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdRevisionHistories(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdRevisionHistories.Where(m => m.RevisionHistoryID == key).Select(m => m.mdProject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdRevisionHistoryExists(int key)
        {
            return db.mdRevisionHistories.Count(e => e.RevisionHistoryID == key) > 0;
        }
    }
}

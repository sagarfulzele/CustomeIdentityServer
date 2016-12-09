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
    builder.EntitySet<mdMasterProjectSchedule>("mdMasterProjectSchedules");
    builder.EntitySet<mdProject>("mdProjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdMasterProjectSchedulesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdMasterProjectSchedules
        [EnableQuery]
        public IQueryable<mdMasterProjectSchedule> GetmdMasterProjectSchedules()
        {
            return db.mdMasterProjectSchedules;
        }

        // GET: odata/mdMasterProjectSchedules(5)
        [EnableQuery]
        public SingleResult<mdMasterProjectSchedule> GetmdMasterProjectSchedule([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdMasterProjectSchedules.Where(mdMasterProjectSchedule => mdMasterProjectSchedule.ID == key));
        }

        // PUT: odata/mdMasterProjectSchedules(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdMasterProjectSchedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdMasterProjectSchedule mdMasterProjectSchedule = await db.mdMasterProjectSchedules.FindAsync(key);
            if (mdMasterProjectSchedule == null)
            {
                return NotFound();
            }

            patch.Put(mdMasterProjectSchedule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdMasterProjectScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdMasterProjectSchedule);
        }

        // POST: odata/mdMasterProjectSchedules
        public async Task<IHttpActionResult> Post(mdMasterProjectSchedule mdMasterProjectSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdMasterProjectSchedules.Add(mdMasterProjectSchedule);
            await db.SaveChangesAsync();

            return Created(mdMasterProjectSchedule);
        }

        // PATCH: odata/mdMasterProjectSchedules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdMasterProjectSchedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdMasterProjectSchedule mdMasterProjectSchedule = await db.mdMasterProjectSchedules.FindAsync(key);
            if (mdMasterProjectSchedule == null)
            {
                return NotFound();
            }

            patch.Patch(mdMasterProjectSchedule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdMasterProjectScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdMasterProjectSchedule);
        }

        // DELETE: odata/mdMasterProjectSchedules(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdMasterProjectSchedule mdMasterProjectSchedule = await db.mdMasterProjectSchedules.FindAsync(key);
            if (mdMasterProjectSchedule == null)
            {
                return NotFound();
            }

            db.mdMasterProjectSchedules.Remove(mdMasterProjectSchedule);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdMasterProjectSchedules(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdMasterProjectSchedules.Where(m => m.ID == key).Select(m => m.mdProject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdMasterProjectScheduleExists(int key)
        {
            return db.mdMasterProjectSchedules.Count(e => e.ID == key) > 0;
        }
    }
}

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
    builder.EntitySet<mdSchematicProject>("mdSchematicProjects");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdSchematicProjectsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdSchematicProjects
        [EnableQuery]
        public IQueryable<mdSchematicProject> GetmdSchematicProjects()
        {
            return db.mdSchematicProjects;
        }

        // GET: odata/mdSchematicProjects(5)
        [EnableQuery]
        public SingleResult<mdSchematicProject> GetmdSchematicProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSchematicProjects.Where(mdSchematicProject => mdSchematicProject.ID == key));
        }

        // PUT: odata/mdSchematicProjects(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdSchematicProject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSchematicProject mdSchematicProject = await db.mdSchematicProjects.FindAsync(key);
            if (mdSchematicProject == null)
            {
                return NotFound();
            }

            patch.Put(mdSchematicProject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSchematicProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSchematicProject);
        }

        // POST: odata/mdSchematicProjects
        public async Task<IHttpActionResult> Post(mdSchematicProject mdSchematicProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdSchematicProjects.Add(mdSchematicProject);
            await db.SaveChangesAsync();

            return Created(mdSchematicProject);
        }

        // PATCH: odata/mdSchematicProjects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdSchematicProject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSchematicProject mdSchematicProject = await db.mdSchematicProjects.FindAsync(key);
            if (mdSchematicProject == null)
            {
                return NotFound();
            }

            patch.Patch(mdSchematicProject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSchematicProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSchematicProject);
        }

        // DELETE: odata/mdSchematicProjects(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdSchematicProject mdSchematicProject = await db.mdSchematicProjects.FindAsync(key);
            if (mdSchematicProject == null)
            {
                return NotFound();
            }

            db.mdSchematicProjects.Remove(mdSchematicProject);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdSchematicProjectExists(int key)
        {
            return db.mdSchematicProjects.Count(e => e.ID == key) > 0;
        }
    }
}

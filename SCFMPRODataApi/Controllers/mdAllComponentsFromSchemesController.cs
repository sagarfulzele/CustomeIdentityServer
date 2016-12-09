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
    builder.EntitySet<mdAllComponentsFromScheme>("mdAllComponentsFromSchemes");
    builder.EntitySet<mdSchematicProjectPath>("mdSchematicProjectPaths"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    [Authorize]
    public class mdAllComponentsFromSchemesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdAllComponentsFromSchemes
        [EnableQuery]
        public IQueryable<mdAllComponentsFromScheme> GetmdAllComponentsFromSchemes()
        {
            return db.mdAllComponentsFromSchemes;
        }

        // GET: odata/mdAllComponentsFromSchemes(5)
        [EnableQuery]
        public SingleResult<mdAllComponentsFromScheme> GetmdAllComponentsFromScheme([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAllComponentsFromSchemes.Where(mdAllComponentsFromScheme => mdAllComponentsFromScheme.ID == key));
        }

        // PUT: odata/mdAllComponentsFromSchemes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdAllComponentsFromScheme> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAllComponentsFromScheme mdAllComponentsFromScheme = await db.mdAllComponentsFromSchemes.FindAsync(key);
            if (mdAllComponentsFromScheme == null)
            {
                return NotFound();
            }

            patch.Put(mdAllComponentsFromScheme);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAllComponentsFromSchemeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAllComponentsFromScheme);
        }

        // POST: odata/mdAllComponentsFromSchemes
        public async Task<IHttpActionResult> Post(mdAllComponentsFromScheme mdAllComponentsFromScheme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdAllComponentsFromSchemes.Add(mdAllComponentsFromScheme);
            await db.SaveChangesAsync();

            return Created(mdAllComponentsFromScheme);
        }

        // PATCH: odata/mdAllComponentsFromSchemes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdAllComponentsFromScheme> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAllComponentsFromScheme mdAllComponentsFromScheme = await db.mdAllComponentsFromSchemes.FindAsync(key);
            if (mdAllComponentsFromScheme == null)
            {
                return NotFound();
            }

            patch.Patch(mdAllComponentsFromScheme);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAllComponentsFromSchemeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAllComponentsFromScheme);
        }

        // DELETE: odata/mdAllComponentsFromSchemes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdAllComponentsFromScheme mdAllComponentsFromScheme = await db.mdAllComponentsFromSchemes.FindAsync(key);
            if (mdAllComponentsFromScheme == null)
            {
                return NotFound();
            }

            db.mdAllComponentsFromSchemes.Remove(mdAllComponentsFromScheme);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdAllComponentsFromSchemes(5)/mdSchematicProjectPath
        [EnableQuery]
        public SingleResult<mdSchematicProjectPath> GetmdSchematicProjectPath([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAllComponentsFromSchemes.Where(m => m.ID == key).Select(m => m.mdSchematicProjectPath));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdAllComponentsFromSchemeExists(int key)
        {
            return db.mdAllComponentsFromSchemes.Count(e => e.ID == key) > 0;
        }
    }
}

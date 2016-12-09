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
    builder.EntitySet<mdCore>("mdCores");
    builder.EntitySet<mdCurrentTransformer>("mdCurrentTransformers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdCoresController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdCores
        [EnableQuery]
        public IQueryable<mdCore> GetmdCores()
        {
            return db.mdCores;
        }

        // GET: odata/mdCores(5)
        [EnableQuery]
        public SingleResult<mdCore> GetmdCore([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCores.Where(mdCore => mdCore.CoreID == key));
        }

        // PUT: odata/mdCores(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdCore> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCore mdCore = await db.mdCores.FindAsync(key);
            if (mdCore == null)
            {
                return NotFound();
            }

            patch.Put(mdCore);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCoreExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCore);
        }

        // POST: odata/mdCores
        public async Task<IHttpActionResult> Post(mdCore mdCore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdCores.Add(mdCore);
            await db.SaveChangesAsync();

            return Created(mdCore);
        }

        // PATCH: odata/mdCores(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdCore> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCore mdCore = await db.mdCores.FindAsync(key);
            if (mdCore == null)
            {
                return NotFound();
            }

            patch.Patch(mdCore);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCoreExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCore);
        }

        // DELETE: odata/mdCores(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdCore mdCore = await db.mdCores.FindAsync(key);
            if (mdCore == null)
            {
                return NotFound();
            }

            db.mdCores.Remove(mdCore);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdCores(5)/mdCurrentTransformer
        [EnableQuery]
        public SingleResult<mdCurrentTransformer> GetmdCurrentTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCores.Where(m => m.CoreID == key).Select(m => m.mdCurrentTransformer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdCoreExists(int key)
        {
            return db.mdCores.Count(e => e.CoreID == key) > 0;
        }
    }
}

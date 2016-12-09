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
    builder.EntitySet<mdTransducerChannelSpec>("mdTransducerChannelSpecs");
    builder.EntitySet<mdTransducer>("mdTransducers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdTransducerChannelSpecsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdTransducerChannelSpecs
        [EnableQuery]
        public IQueryable<mdTransducerChannelSpec> GetmdTransducerChannelSpecs()
        {
            return db.mdTransducerChannelSpecs;
        }

        // GET: odata/mdTransducerChannelSpecs(5)
        [EnableQuery]
        public SingleResult<mdTransducerChannelSpec> GetmdTransducerChannelSpec([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTransducerChannelSpecs.Where(mdTransducerChannelSpec => mdTransducerChannelSpec.TransducerChannelSpecID == key));
        }

        // PUT: odata/mdTransducerChannelSpecs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdTransducerChannelSpec> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTransducerChannelSpec mdTransducerChannelSpec = await db.mdTransducerChannelSpecs.FindAsync(key);
            if (mdTransducerChannelSpec == null)
            {
                return NotFound();
            }

            patch.Put(mdTransducerChannelSpec);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTransducerChannelSpecExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTransducerChannelSpec);
        }

        // POST: odata/mdTransducerChannelSpecs
        public async Task<IHttpActionResult> Post(mdTransducerChannelSpec mdTransducerChannelSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdTransducerChannelSpecs.Add(mdTransducerChannelSpec);
            await db.SaveChangesAsync();

            return Created(mdTransducerChannelSpec);
        }

        // PATCH: odata/mdTransducerChannelSpecs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdTransducerChannelSpec> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTransducerChannelSpec mdTransducerChannelSpec = await db.mdTransducerChannelSpecs.FindAsync(key);
            if (mdTransducerChannelSpec == null)
            {
                return NotFound();
            }

            patch.Patch(mdTransducerChannelSpec);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTransducerChannelSpecExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTransducerChannelSpec);
        }

        // DELETE: odata/mdTransducerChannelSpecs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdTransducerChannelSpec mdTransducerChannelSpec = await db.mdTransducerChannelSpecs.FindAsync(key);
            if (mdTransducerChannelSpec == null)
            {
                return NotFound();
            }

            db.mdTransducerChannelSpecs.Remove(mdTransducerChannelSpec);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdTransducerChannelSpecs(5)/mdTransducer
        [EnableQuery]
        public SingleResult<mdTransducer> GetmdTransducer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTransducerChannelSpecs.Where(m => m.TransducerChannelSpecID == key).Select(m => m.mdTransducer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdTransducerChannelSpecExists(int key)
        {
            return db.mdTransducerChannelSpecs.Count(e => e.TransducerChannelSpecID == key) > 0;
        }
    }
}

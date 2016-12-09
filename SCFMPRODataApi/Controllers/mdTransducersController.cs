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
    builder.EntitySet<mdTransducer>("mdTransducers");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    builder.EntitySet<mdTransducerChannelSpec>("mdTransducerChannelSpecs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdTransducersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdTransducers
        [EnableQuery]
        public IQueryable<mdTransducer> GetmdTransducers()
        {
            return db.mdTransducers;
        }

        // GET: odata/mdTransducers(5)
        [EnableQuery]
        public SingleResult<mdTransducer> GetmdTransducer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTransducers.Where(mdTransducer => mdTransducer.ID == key));
        }

        // PUT: odata/mdTransducers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdTransducer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTransducer mdTransducer = await db.mdTransducers.FindAsync(key);
            if (mdTransducer == null)
            {
                return NotFound();
            }

            patch.Put(mdTransducer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTransducerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTransducer);
        }

        // POST: odata/mdTransducers
        public async Task<IHttpActionResult> Post(mdTransducer mdTransducer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdTransducers.Add(mdTransducer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdTransducerExists(mdTransducer.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdTransducer);
        }

        // PATCH: odata/mdTransducers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdTransducer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTransducer mdTransducer = await db.mdTransducers.FindAsync(key);
            if (mdTransducer == null)
            {
                return NotFound();
            }

            patch.Patch(mdTransducer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTransducerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTransducer);
        }

        // DELETE: odata/mdTransducers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdTransducer mdTransducer = await db.mdTransducers.FindAsync(key);
            if (mdTransducer == null)
            {
                return NotFound();
            }

            db.mdTransducers.Remove(mdTransducer);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdTransducers(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTransducers.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        // GET: odata/mdTransducers(5)/mdTransducerChannelSpecs
        [EnableQuery]
        public IQueryable<mdTransducerChannelSpec> GetmdTransducerChannelSpecs([FromODataUri] int key)
        {
            return db.mdTransducers.Where(m => m.ID == key).SelectMany(m => m.mdTransducerChannelSpecs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdTransducerExists(int key)
        {
            return db.mdTransducers.Count(e => e.ID == key) > 0;
        }
    }
}

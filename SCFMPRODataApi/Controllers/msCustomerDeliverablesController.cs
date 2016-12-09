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
    builder.EntitySet<msCustomerDeliverable>("msCustomerDeliverables");
    builder.EntitySet<mdCustomerDeliverableDate>("mdCustomerDeliverableDates"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msCustomerDeliverablesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msCustomerDeliverables
        [EnableQuery]
        public IQueryable<msCustomerDeliverable> GetmsCustomerDeliverables()
        {
            return db.msCustomerDeliverables;
        }

        // GET: odata/msCustomerDeliverables(5)
        [EnableQuery]
        public SingleResult<msCustomerDeliverable> GetmsCustomerDeliverable([FromODataUri] int key)
        {
            return SingleResult.Create(db.msCustomerDeliverables.Where(msCustomerDeliverable => msCustomerDeliverable.ID == key));
        }

        // PUT: odata/msCustomerDeliverables(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msCustomerDeliverable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCustomerDeliverable msCustomerDeliverable = await db.msCustomerDeliverables.FindAsync(key);
            if (msCustomerDeliverable == null)
            {
                return NotFound();
            }

            patch.Put(msCustomerDeliverable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCustomerDeliverableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCustomerDeliverable);
        }

        // POST: odata/msCustomerDeliverables
        public async Task<IHttpActionResult> Post(msCustomerDeliverable msCustomerDeliverable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msCustomerDeliverables.Add(msCustomerDeliverable);
            await db.SaveChangesAsync();

            return Created(msCustomerDeliverable);
        }

        // PATCH: odata/msCustomerDeliverables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msCustomerDeliverable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCustomerDeliverable msCustomerDeliverable = await db.msCustomerDeliverables.FindAsync(key);
            if (msCustomerDeliverable == null)
            {
                return NotFound();
            }

            patch.Patch(msCustomerDeliverable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCustomerDeliverableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCustomerDeliverable);
        }

        // DELETE: odata/msCustomerDeliverables(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msCustomerDeliverable msCustomerDeliverable = await db.msCustomerDeliverables.FindAsync(key);
            if (msCustomerDeliverable == null)
            {
                return NotFound();
            }

            db.msCustomerDeliverables.Remove(msCustomerDeliverable);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msCustomerDeliverables(5)/mdCustomerDeliverableDates
        [EnableQuery]
        public IQueryable<mdCustomerDeliverableDate> GetmdCustomerDeliverableDates([FromODataUri] int key)
        {
            return db.msCustomerDeliverables.Where(m => m.ID == key).SelectMany(m => m.mdCustomerDeliverableDates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msCustomerDeliverableExists(int key)
        {
            return db.msCustomerDeliverables.Count(e => e.ID == key) > 0;
        }
    }
}

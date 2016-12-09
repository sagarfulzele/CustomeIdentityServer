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
    builder.EntitySet<mdCustomerDeliverableDate>("mdCustomerDeliverableDates");
    builder.EntitySet<mdCustomerDeliverableVoltage>("mdCustomerDeliverableVoltages"); 
    builder.EntitySet<msCustomerDeliverable>("msCustomerDeliverables"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdCustomerDeliverableDatesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdCustomerDeliverableDates
        [EnableQuery]
        public IQueryable<mdCustomerDeliverableDate> GetmdCustomerDeliverableDates()
        {
            return db.mdCustomerDeliverableDates;
        }

        // GET: odata/mdCustomerDeliverableDates(5)
        [EnableQuery]
        public SingleResult<mdCustomerDeliverableDate> GetmdCustomerDeliverableDate([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCustomerDeliverableDates.Where(mdCustomerDeliverableDate => mdCustomerDeliverableDate.ID == key));
        }

        // PUT: odata/mdCustomerDeliverableDates(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdCustomerDeliverableDate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCustomerDeliverableDate mdCustomerDeliverableDate = await db.mdCustomerDeliverableDates.FindAsync(key);
            if (mdCustomerDeliverableDate == null)
            {
                return NotFound();
            }

            patch.Put(mdCustomerDeliverableDate);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCustomerDeliverableDateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCustomerDeliverableDate);
        }

        // POST: odata/mdCustomerDeliverableDates
        public async Task<IHttpActionResult> Post(mdCustomerDeliverableDate mdCustomerDeliverableDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdCustomerDeliverableDates.Add(mdCustomerDeliverableDate);
            await db.SaveChangesAsync();

            return Created(mdCustomerDeliverableDate);
        }

        // PATCH: odata/mdCustomerDeliverableDates(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdCustomerDeliverableDate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCustomerDeliverableDate mdCustomerDeliverableDate = await db.mdCustomerDeliverableDates.FindAsync(key);
            if (mdCustomerDeliverableDate == null)
            {
                return NotFound();
            }

            patch.Patch(mdCustomerDeliverableDate);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCustomerDeliverableDateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCustomerDeliverableDate);
        }

        // DELETE: odata/mdCustomerDeliverableDates(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdCustomerDeliverableDate mdCustomerDeliverableDate = await db.mdCustomerDeliverableDates.FindAsync(key);
            if (mdCustomerDeliverableDate == null)
            {
                return NotFound();
            }

            db.mdCustomerDeliverableDates.Remove(mdCustomerDeliverableDate);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdCustomerDeliverableDates(5)/mdCustomerDeliverableVoltage
        [EnableQuery]
        public SingleResult<mdCustomerDeliverableVoltage> GetmdCustomerDeliverableVoltage([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCustomerDeliverableDates.Where(m => m.ID == key).Select(m => m.mdCustomerDeliverableVoltage));
        }

        // GET: odata/mdCustomerDeliverableDates(5)/msCustomerDeliverable
        [EnableQuery]
        public SingleResult<msCustomerDeliverable> GetmsCustomerDeliverable([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCustomerDeliverableDates.Where(m => m.ID == key).Select(m => m.msCustomerDeliverable));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdCustomerDeliverableDateExists(int key)
        {
            return db.mdCustomerDeliverableDates.Count(e => e.ID == key) > 0;
        }
    }
}

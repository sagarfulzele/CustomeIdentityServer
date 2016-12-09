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
    builder.EntitySet<mdCustomerDeliverableVoltage>("mdCustomerDeliverableVoltages");
    builder.EntitySet<mdCustomerDeliverableDate>("mdCustomerDeliverableDates"); 
    builder.EntitySet<mdProject>("mdProjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdCustomerDeliverableVoltagesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdCustomerDeliverableVoltages
        [EnableQuery]
        public IQueryable<mdCustomerDeliverableVoltage> GetmdCustomerDeliverableVoltages()
        {
            return db.mdCustomerDeliverableVoltages;
        }

        // GET: odata/mdCustomerDeliverableVoltages(5)
        [EnableQuery]
        public SingleResult<mdCustomerDeliverableVoltage> GetmdCustomerDeliverableVoltage([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCustomerDeliverableVoltages.Where(mdCustomerDeliverableVoltage => mdCustomerDeliverableVoltage.ID == key));
        }

        // PUT: odata/mdCustomerDeliverableVoltages(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdCustomerDeliverableVoltage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCustomerDeliverableVoltage mdCustomerDeliverableVoltage = await db.mdCustomerDeliverableVoltages.FindAsync(key);
            if (mdCustomerDeliverableVoltage == null)
            {
                return NotFound();
            }

            patch.Put(mdCustomerDeliverableVoltage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCustomerDeliverableVoltageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCustomerDeliverableVoltage);
        }

        // POST: odata/mdCustomerDeliverableVoltages
        public async Task<IHttpActionResult> Post(mdCustomerDeliverableVoltage mdCustomerDeliverableVoltage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdCustomerDeliverableVoltages.Add(mdCustomerDeliverableVoltage);
            await db.SaveChangesAsync();

            return Created(mdCustomerDeliverableVoltage);
        }

        // PATCH: odata/mdCustomerDeliverableVoltages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdCustomerDeliverableVoltage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdCustomerDeliverableVoltage mdCustomerDeliverableVoltage = await db.mdCustomerDeliverableVoltages.FindAsync(key);
            if (mdCustomerDeliverableVoltage == null)
            {
                return NotFound();
            }

            patch.Patch(mdCustomerDeliverableVoltage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdCustomerDeliverableVoltageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdCustomerDeliverableVoltage);
        }

        // DELETE: odata/mdCustomerDeliverableVoltages(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdCustomerDeliverableVoltage mdCustomerDeliverableVoltage = await db.mdCustomerDeliverableVoltages.FindAsync(key);
            if (mdCustomerDeliverableVoltage == null)
            {
                return NotFound();
            }

            db.mdCustomerDeliverableVoltages.Remove(mdCustomerDeliverableVoltage);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdCustomerDeliverableVoltages(5)/mdCustomerDeliverableDates
        [EnableQuery]
        public IQueryable<mdCustomerDeliverableDate> GetmdCustomerDeliverableDates([FromODataUri] int key)
        {
            return db.mdCustomerDeliverableVoltages.Where(m => m.ID == key).SelectMany(m => m.mdCustomerDeliverableDates);
        }

        // GET: odata/mdCustomerDeliverableVoltages(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdCustomerDeliverableVoltages.Where(m => m.ID == key).Select(m => m.mdProject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdCustomerDeliverableVoltageExists(int key)
        {
            return db.mdCustomerDeliverableVoltages.Count(e => e.ID == key) > 0;
        }
    }
}

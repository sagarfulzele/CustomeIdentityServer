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
    builder.EntitySet<msScheduleHItem>("msScheduleHItems");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msScheduleHItemsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msScheduleHItems
        [EnableQuery]
        public IQueryable<msScheduleHItem> GetmsScheduleHItems()
        {
            return db.msScheduleHItems;
        }

        // GET: odata/msScheduleHItems(5)
        [EnableQuery]
        public SingleResult<msScheduleHItem> GetmsScheduleHItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.msScheduleHItems.Where(msScheduleHItem => msScheduleHItem.ID == key));
        }

        // PUT: odata/msScheduleHItems(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msScheduleHItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msScheduleHItem msScheduleHItem = await db.msScheduleHItems.FindAsync(key);
            if (msScheduleHItem == null)
            {
                return NotFound();
            }

            patch.Put(msScheduleHItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msScheduleHItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msScheduleHItem);
        }

        // POST: odata/msScheduleHItems
        public async Task<IHttpActionResult> Post(msScheduleHItem msScheduleHItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msScheduleHItems.Add(msScheduleHItem);
            await db.SaveChangesAsync();

            return Created(msScheduleHItem);
        }

        // PATCH: odata/msScheduleHItems(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msScheduleHItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msScheduleHItem msScheduleHItem = await db.msScheduleHItems.FindAsync(key);
            if (msScheduleHItem == null)
            {
                return NotFound();
            }

            patch.Patch(msScheduleHItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msScheduleHItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msScheduleHItem);
        }

        // DELETE: odata/msScheduleHItems(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msScheduleHItem msScheduleHItem = await db.msScheduleHItems.FindAsync(key);
            if (msScheduleHItem == null)
            {
                return NotFound();
            }

            db.msScheduleHItems.Remove(msScheduleHItem);
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

        private bool msScheduleHItemExists(int key)
        {
            return db.msScheduleHItems.Count(e => e.ID == key) > 0;
        }
    }
}

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
    builder.EntitySet<mdConsumableItem>("mdConsumableItems");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdConsumableItemsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdConsumableItems
        [EnableQuery]
        public IQueryable<mdConsumableItem> GetmdConsumableItems()
        {
            return db.mdConsumableItems;
        }

        // GET: odata/mdConsumableItems(5)
        [EnableQuery]
        public SingleResult<mdConsumableItem> GetmdConsumableItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdConsumableItems.Where(mdConsumableItem => mdConsumableItem.ID == key));
        }

        // PUT: odata/mdConsumableItems(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdConsumableItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdConsumableItem mdConsumableItem = await db.mdConsumableItems.FindAsync(key);
            if (mdConsumableItem == null)
            {
                return NotFound();
            }

            patch.Put(mdConsumableItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdConsumableItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdConsumableItem);
        }

        // POST: odata/mdConsumableItems
        public async Task<IHttpActionResult> Post(mdConsumableItem mdConsumableItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdConsumableItems.Add(mdConsumableItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdConsumableItemExists(mdConsumableItem.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdConsumableItem);
        }

        // PATCH: odata/mdConsumableItems(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdConsumableItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdConsumableItem mdConsumableItem = await db.mdConsumableItems.FindAsync(key);
            if (mdConsumableItem == null)
            {
                return NotFound();
            }

            patch.Patch(mdConsumableItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdConsumableItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdConsumableItem);
        }

        // DELETE: odata/mdConsumableItems(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdConsumableItem mdConsumableItem = await db.mdConsumableItems.FindAsync(key);
            if (mdConsumableItem == null)
            {
                return NotFound();
            }

            db.mdConsumableItems.Remove(mdConsumableItem);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdConsumableItems(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdConsumableItems.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdConsumableItemExists(int key)
        {
            return db.mdConsumableItems.Count(e => e.ID == key) > 0;
        }
    }
}

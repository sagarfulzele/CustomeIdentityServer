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
    builder.EntitySet<mdTestBlock>("mdTestBlocks");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdTestBlocksController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdTestBlocks
        [EnableQuery]
        public IQueryable<mdTestBlock> GetmdTestBlocks()
        {
            return db.mdTestBlocks;
        }

        // GET: odata/mdTestBlocks(5)
        [EnableQuery]
        public SingleResult<mdTestBlock> GetmdTestBlock([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTestBlocks.Where(mdTestBlock => mdTestBlock.ID == key));
        }

        // PUT: odata/mdTestBlocks(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdTestBlock> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTestBlock mdTestBlock = await db.mdTestBlocks.FindAsync(key);
            if (mdTestBlock == null)
            {
                return NotFound();
            }

            patch.Put(mdTestBlock);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTestBlockExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTestBlock);
        }

        // POST: odata/mdTestBlocks
        public async Task<IHttpActionResult> Post(mdTestBlock mdTestBlock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdTestBlocks.Add(mdTestBlock);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdTestBlockExists(mdTestBlock.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdTestBlock);
        }

        // PATCH: odata/mdTestBlocks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdTestBlock> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTestBlock mdTestBlock = await db.mdTestBlocks.FindAsync(key);
            if (mdTestBlock == null)
            {
                return NotFound();
            }

            patch.Patch(mdTestBlock);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTestBlockExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTestBlock);
        }

        // DELETE: odata/mdTestBlocks(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdTestBlock mdTestBlock = await db.mdTestBlocks.FindAsync(key);
            if (mdTestBlock == null)
            {
                return NotFound();
            }

            db.mdTestBlocks.Remove(mdTestBlock);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdTestBlocks(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTestBlocks.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdTestBlockExists(int key)
        {
            return db.mdTestBlocks.Count(e => e.ID == key) > 0;
        }
    }
}

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
    builder.EntitySet<mdTerminalBlock>("mdTerminalBlocks");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdTerminalBlocksController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdTerminalBlocks
        [EnableQuery]
        public IQueryable<mdTerminalBlock> GetmdTerminalBlocks()
        {
            return db.mdTerminalBlocks;
        }

        // GET: odata/mdTerminalBlocks(5)
        [EnableQuery]
        public SingleResult<mdTerminalBlock> GetmdTerminalBlock([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTerminalBlocks.Where(mdTerminalBlock => mdTerminalBlock.ID == key));
        }

        // PUT: odata/mdTerminalBlocks(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdTerminalBlock> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTerminalBlock mdTerminalBlock = await db.mdTerminalBlocks.FindAsync(key);
            if (mdTerminalBlock == null)
            {
                return NotFound();
            }

            patch.Put(mdTerminalBlock);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTerminalBlockExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTerminalBlock);
        }

        // POST: odata/mdTerminalBlocks
        public async Task<IHttpActionResult> Post(mdTerminalBlock mdTerminalBlock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdTerminalBlocks.Add(mdTerminalBlock);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdTerminalBlockExists(mdTerminalBlock.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdTerminalBlock);
        }

        // PATCH: odata/mdTerminalBlocks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdTerminalBlock> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdTerminalBlock mdTerminalBlock = await db.mdTerminalBlocks.FindAsync(key);
            if (mdTerminalBlock == null)
            {
                return NotFound();
            }

            patch.Patch(mdTerminalBlock);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdTerminalBlockExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdTerminalBlock);
        }

        // DELETE: odata/mdTerminalBlocks(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdTerminalBlock mdTerminalBlock = await db.mdTerminalBlocks.FindAsync(key);
            if (mdTerminalBlock == null)
            {
                return NotFound();
            }

            db.mdTerminalBlocks.Remove(mdTerminalBlock);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdTerminalBlocks(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdTerminalBlocks.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdTerminalBlockExists(int key)
        {
            return db.mdTerminalBlocks.Count(e => e.ID == key) > 0;
        }
    }
}

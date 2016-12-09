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
    builder.EntitySet<mdPanelComponent>("mdPanelComponents");
    builder.EntitySet<mdAnnunciator>("mdAnnunciators"); 
    builder.EntitySet<mdAuxiliaryComponent>("mdAuxiliaryComponents"); 
    builder.EntitySet<mdAuxiliaryRelay>("mdAuxiliaryRelays"); 
    builder.EntitySet<mdConsumableItem>("mdConsumableItems"); 
    builder.EntitySet<mdContractSecondaryDeliverable>("mdContractSecondaryDeliverables"); 
    builder.EntitySet<mdMainRelay>("mdMainRelays"); 
    builder.EntitySet<mdMeter>("mdMeters"); 
    builder.EntitySet<mdTerminalBlock>("mdTerminalBlocks"); 
    builder.EntitySet<mdTestBlock>("mdTestBlocks"); 
    builder.EntitySet<mdTransducer>("mdTransducers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPanelComponentsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPanelComponents
        [EnableQuery]
        public IQueryable<mdPanelComponent> GetmdPanelComponents()
        {
            return db.mdPanelComponents;
        }

        // GET: odata/mdPanelComponents(5)
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(mdPanelComponent => mdPanelComponent.ID == key));
        }

        // PUT: odata/mdPanelComponents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPanelComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelComponent mdPanelComponent = await db.mdPanelComponents.FindAsync(key);
            if (mdPanelComponent == null)
            {
                return NotFound();
            }

            patch.Put(mdPanelComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelComponent);
        }

        // POST: odata/mdPanelComponents
        public async Task<IHttpActionResult> Post(mdPanelComponent mdPanelComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPanelComponents.Add(mdPanelComponent);
            await db.SaveChangesAsync();

            return Created(mdPanelComponent);
        }

        // PATCH: odata/mdPanelComponents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPanelComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPanelComponent mdPanelComponent = await db.mdPanelComponents.FindAsync(key);
            if (mdPanelComponent == null)
            {
                return NotFound();
            }

            patch.Patch(mdPanelComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPanelComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPanelComponent);
        }

        // DELETE: odata/mdPanelComponents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPanelComponent mdPanelComponent = await db.mdPanelComponents.FindAsync(key);
            if (mdPanelComponent == null)
            {
                return NotFound();
            }

            db.mdPanelComponents.Remove(mdPanelComponent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPanelComponents(5)/mdAnnunciator
        [EnableQuery]
        public SingleResult<mdAnnunciator> GetmdAnnunciator([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdAnnunciator));
        }

        // GET: odata/mdPanelComponents(5)/mdAuxiliaryComponent
        [EnableQuery]
        public SingleResult<mdAuxiliaryComponent> GetmdAuxiliaryComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdAuxiliaryComponent));
        }

        // GET: odata/mdPanelComponents(5)/mdAuxiliaryRelay
        [EnableQuery]
        public SingleResult<mdAuxiliaryRelay> GetmdAuxiliaryRelay([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdAuxiliaryRelay));
        }

        // GET: odata/mdPanelComponents(5)/mdConsumableItem
        [EnableQuery]
        public SingleResult<mdConsumableItem> GetmdConsumableItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdConsumableItem));
        }

        // GET: odata/mdPanelComponents(5)/mdContractSecondaryDeliverable
        [EnableQuery]
        public SingleResult<mdContractSecondaryDeliverable> GetmdContractSecondaryDeliverable([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdContractSecondaryDeliverable));
        }

        // GET: odata/mdPanelComponents(5)/mdMainRelay
        [EnableQuery]
        public SingleResult<mdMainRelay> GetmdMainRelay([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdMainRelay));
        }

        // GET: odata/mdPanelComponents(5)/mdMeter
        [EnableQuery]
        public SingleResult<mdMeter> GetmdMeter([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdMeter));
        }

        // GET: odata/mdPanelComponents(5)/mdTerminalBlock
        [EnableQuery]
        public SingleResult<mdTerminalBlock> GetmdTerminalBlock([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdTerminalBlock));
        }

        // GET: odata/mdPanelComponents(5)/mdTestBlock
        [EnableQuery]
        public SingleResult<mdTestBlock> GetmdTestBlock([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdTestBlock));
        }

        // GET: odata/mdPanelComponents(5)/mdTransducer
        [EnableQuery]
        public SingleResult<mdTransducer> GetmdTransducer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPanelComponents.Where(m => m.ID == key).Select(m => m.mdTransducer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPanelComponentExists(int key)
        {
            return db.mdPanelComponents.Count(e => e.ID == key) > 0;
        }
    }
}

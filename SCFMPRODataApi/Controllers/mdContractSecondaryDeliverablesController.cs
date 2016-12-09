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
    builder.EntitySet<mdContractSecondaryDeliverable>("mdContractSecondaryDeliverables");
    builder.EntitySet<mdProject>("mdProjects"); 
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdContractSecondaryDeliverablesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdContractSecondaryDeliverables
        [EnableQuery]
        public IQueryable<mdContractSecondaryDeliverable> GetmdContractSecondaryDeliverables()
        {
            return db.mdContractSecondaryDeliverables;
        }

        // GET: odata/mdContractSecondaryDeliverables(5)
        [EnableQuery]
        public SingleResult<mdContractSecondaryDeliverable> GetmdContractSecondaryDeliverable([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdContractSecondaryDeliverables.Where(mdContractSecondaryDeliverable => mdContractSecondaryDeliverable.ID == key));
        }

        // PUT: odata/mdContractSecondaryDeliverables(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdContractSecondaryDeliverable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdContractSecondaryDeliverable mdContractSecondaryDeliverable = await db.mdContractSecondaryDeliverables.FindAsync(key);
            if (mdContractSecondaryDeliverable == null)
            {
                return NotFound();
            }

            patch.Put(mdContractSecondaryDeliverable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdContractSecondaryDeliverableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdContractSecondaryDeliverable);
        }

        // POST: odata/mdContractSecondaryDeliverables
        public async Task<IHttpActionResult> Post(mdContractSecondaryDeliverable mdContractSecondaryDeliverable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdContractSecondaryDeliverables.Add(mdContractSecondaryDeliverable);
            await db.SaveChangesAsync();

            return Created(mdContractSecondaryDeliverable);
        }

        // PATCH: odata/mdContractSecondaryDeliverables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdContractSecondaryDeliverable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdContractSecondaryDeliverable mdContractSecondaryDeliverable = await db.mdContractSecondaryDeliverables.FindAsync(key);
            if (mdContractSecondaryDeliverable == null)
            {
                return NotFound();
            }

            patch.Patch(mdContractSecondaryDeliverable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdContractSecondaryDeliverableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdContractSecondaryDeliverable);
        }

        // DELETE: odata/mdContractSecondaryDeliverables(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdContractSecondaryDeliverable mdContractSecondaryDeliverable = await db.mdContractSecondaryDeliverables.FindAsync(key);
            if (mdContractSecondaryDeliverable == null)
            {
                return NotFound();
            }

            db.mdContractSecondaryDeliverables.Remove(mdContractSecondaryDeliverable);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdContractSecondaryDeliverables(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdContractSecondaryDeliverables.Where(m => m.ID == key).Select(m => m.mdProject));
        }

        // GET: odata/mdContractSecondaryDeliverables(5)/mdPanelComponents
        [EnableQuery]
        public IQueryable<mdPanelComponent> GetmdPanelComponents([FromODataUri] int key)
        {
            return db.mdContractSecondaryDeliverables.Where(m => m.ID == key).SelectMany(m => m.mdPanelComponents);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdContractSecondaryDeliverableExists(int key)
        {
            return db.mdContractSecondaryDeliverables.Count(e => e.ID == key) > 0;
        }

        // 


        [HttpPost]
        public async Task<IHttpActionResult> SavemdContractSecondaryDeliverables([FromODataUri] int key, mdContractSecondaryDeliverable parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            parameters.ProjectID = key;
            if (mdContractSecondaryDeliverableExists(parameters.ID))
            {
                Delta<mdContractSecondaryDeliverable> obj = new Delta<mdContractSecondaryDeliverable>();
                obj.Put(parameters);
                return await Put(parameters.ID, obj);
            }
            else
            {
                return await Post(parameters);
            } 
        } 
    }
}

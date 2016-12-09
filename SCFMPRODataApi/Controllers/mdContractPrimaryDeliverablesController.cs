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
    builder.EntitySet<mdContractPrimaryDeliverable>("mdContractPrimaryDeliverables");
    builder.EntitySet<mdProject>("mdProjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdContractPrimaryDeliverablesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdContractPrimaryDeliverables
        [EnableQuery]
        public IQueryable<mdContractPrimaryDeliverable> GetmdContractPrimaryDeliverables()
        {
            return db.mdContractPrimaryDeliverables;
        }

        // GET: odata/mdContractPrimaryDeliverables(5)
        [EnableQuery]
        public SingleResult<mdContractPrimaryDeliverable> GetmdContractPrimaryDeliverable([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdContractPrimaryDeliverables.Where(mdContractPrimaryDeliverable => mdContractPrimaryDeliverable.ID == key));
        }

        // PUT: odata/mdContractPrimaryDeliverables(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdContractPrimaryDeliverable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdContractPrimaryDeliverable mdContractPrimaryDeliverable = await db.mdContractPrimaryDeliverables.FindAsync(key);
            if (mdContractPrimaryDeliverable == null)
            {
                return NotFound();
            }

            patch.Put(mdContractPrimaryDeliverable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdContractPrimaryDeliverableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdContractPrimaryDeliverable);
        }

        // POST: odata/mdContractPrimaryDeliverables
        public async Task<IHttpActionResult> Post(mdContractPrimaryDeliverable mdContractPrimaryDeliverable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdContractPrimaryDeliverables.Add(mdContractPrimaryDeliverable);
            await db.SaveChangesAsync();

            return Created(mdContractPrimaryDeliverable);
        }

        // PATCH: odata/mdContractPrimaryDeliverables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdContractPrimaryDeliverable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdContractPrimaryDeliverable mdContractPrimaryDeliverable = await db.mdContractPrimaryDeliverables.FindAsync(key);
            if (mdContractPrimaryDeliverable == null)
            {
                return NotFound();
            }

            patch.Patch(mdContractPrimaryDeliverable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdContractPrimaryDeliverableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdContractPrimaryDeliverable);
        }

        // DELETE: odata/mdContractPrimaryDeliverables(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdContractPrimaryDeliverable mdContractPrimaryDeliverable = await db.mdContractPrimaryDeliverables.FindAsync(key);
            if (mdContractPrimaryDeliverable == null)
            {
                return NotFound();
            }

            db.mdContractPrimaryDeliverables.Remove(mdContractPrimaryDeliverable);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdContractPrimaryDeliverables(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdContractPrimaryDeliverables.Where(m => m.ID == key).Select(m => m.mdProject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdContractPrimaryDeliverableExists(int key)
        {
            return db.mdContractPrimaryDeliverables.Count(e => e.ID == key) > 0;
        }


        [HttpPost]
        public async Task<IHttpActionResult> SavemdContractPrimaryDeliverables([FromODataUri] int key, mdContractPrimaryDeliverable parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            parameters.ProjectID = key;
            if (mdContractPrimaryDeliverableExists(parameters.ID))
            {
                Delta<mdContractPrimaryDeliverable> obj = new Delta<mdContractPrimaryDeliverable>();
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

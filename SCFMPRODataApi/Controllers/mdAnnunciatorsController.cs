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
    builder.EntitySet<mdAnnunciator>("mdAnnunciators");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdAnnunciatorsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdAnnunciators
        [EnableQuery]
        public IQueryable<mdAnnunciator> GetmdAnnunciators()
        {
            return db.mdAnnunciators;
        }

        // GET: odata/mdAnnunciators(5)
        [EnableQuery]
        public SingleResult<mdAnnunciator> GetmdAnnunciator([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAnnunciators.Where(mdAnnunciator => mdAnnunciator.ID == key));
        }

        // PUT: odata/mdAnnunciators(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdAnnunciator> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAnnunciator mdAnnunciator = await db.mdAnnunciators.FindAsync(key);
            if (mdAnnunciator == null)
            {
                return NotFound();
            }

            patch.Put(mdAnnunciator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAnnunciatorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAnnunciator);
        }

        // POST: odata/mdAnnunciators
        public async Task<IHttpActionResult> Post(mdAnnunciator mdAnnunciator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdAnnunciators.Add(mdAnnunciator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdAnnunciatorExists(mdAnnunciator.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdAnnunciator);
        }

        // PATCH: odata/mdAnnunciators(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdAnnunciator> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAnnunciator mdAnnunciator = await db.mdAnnunciators.FindAsync(key);
            if (mdAnnunciator == null)
            {
                return NotFound();
            }

            patch.Patch(mdAnnunciator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAnnunciatorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAnnunciator);
        }

        // DELETE: odata/mdAnnunciators(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdAnnunciator mdAnnunciator = await db.mdAnnunciators.FindAsync(key);
            if (mdAnnunciator == null)
            {
                return NotFound();
            }

            db.mdAnnunciators.Remove(mdAnnunciator);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdAnnunciators(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAnnunciators.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdAnnunciatorExists(int key)
        {
            return db.mdAnnunciators.Count(e => e.ID == key) > 0;
        }
    }
}

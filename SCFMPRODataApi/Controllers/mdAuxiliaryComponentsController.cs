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
    builder.EntitySet<mdAuxiliaryComponent>("mdAuxiliaryComponents");
    builder.EntitySet<mdPanelComponent>("mdPanelComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdAuxiliaryComponentsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdAuxiliaryComponents
        [EnableQuery]
        public IQueryable<mdAuxiliaryComponent> GetmdAuxiliaryComponents()
        {
            return db.mdAuxiliaryComponents;
        }

        // GET: odata/mdAuxiliaryComponents(5)
        [EnableQuery]
        public SingleResult<mdAuxiliaryComponent> GetmdAuxiliaryComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAuxiliaryComponents.Where(mdAuxiliaryComponent => mdAuxiliaryComponent.ID == key));
        }

        // PUT: odata/mdAuxiliaryComponents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdAuxiliaryComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAuxiliaryComponent mdAuxiliaryComponent = await db.mdAuxiliaryComponents.FindAsync(key);
            if (mdAuxiliaryComponent == null)
            {
                return NotFound();
            }

            patch.Put(mdAuxiliaryComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAuxiliaryComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAuxiliaryComponent);
        }

        // POST: odata/mdAuxiliaryComponents
        public async Task<IHttpActionResult> Post(mdAuxiliaryComponent mdAuxiliaryComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdAuxiliaryComponents.Add(mdAuxiliaryComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mdAuxiliaryComponentExists(mdAuxiliaryComponent.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mdAuxiliaryComponent);
        }

        // PATCH: odata/mdAuxiliaryComponents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdAuxiliaryComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdAuxiliaryComponent mdAuxiliaryComponent = await db.mdAuxiliaryComponents.FindAsync(key);
            if (mdAuxiliaryComponent == null)
            {
                return NotFound();
            }

            patch.Patch(mdAuxiliaryComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdAuxiliaryComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdAuxiliaryComponent);
        }

        // DELETE: odata/mdAuxiliaryComponents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdAuxiliaryComponent mdAuxiliaryComponent = await db.mdAuxiliaryComponents.FindAsync(key);
            if (mdAuxiliaryComponent == null)
            {
                return NotFound();
            }

            db.mdAuxiliaryComponents.Remove(mdAuxiliaryComponent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdAuxiliaryComponents(5)/mdPanelComponent
        [EnableQuery]
        public SingleResult<mdPanelComponent> GetmdPanelComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdAuxiliaryComponents.Where(m => m.ID == key).Select(m => m.mdPanelComponent));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdAuxiliaryComponentExists(int key)
        {
            return db.mdAuxiliaryComponents.Count(e => e.ID == key) > 0;
        }
    }
}

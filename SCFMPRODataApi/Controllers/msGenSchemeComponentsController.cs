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
    builder.EntitySet<msGenSchemeComponent>("msGenSchemeComponents");
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class msGenSchemeComponentsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msGenSchemeComponents
        [EnableQuery]
        public IQueryable<msGenSchemeComponent> GetmsGenSchemeComponents()
        {
            return db.msGenSchemeComponents;
        }

        // GET: odata/msGenSchemeComponents(5)
        [EnableQuery]
        public SingleResult<msGenSchemeComponent> GetmsGenSchemeComponent([FromODataUri] int key)
        {
            return SingleResult.Create(db.msGenSchemeComponents.Where(msGenSchemeComponent => msGenSchemeComponent.ID == key));
        }

        // PUT: odata/msGenSchemeComponents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msGenSchemeComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msGenSchemeComponent msGenSchemeComponent = await db.msGenSchemeComponents.FindAsync(key);
            if (msGenSchemeComponent == null)
            {
                return NotFound();
            }

            patch.Put(msGenSchemeComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msGenSchemeComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msGenSchemeComponent);
        }

        // POST: odata/msGenSchemeComponents
        public async Task<IHttpActionResult> Post(msGenSchemeComponent msGenSchemeComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msGenSchemeComponents.Add(msGenSchemeComponent);
            await db.SaveChangesAsync();

            return Created(msGenSchemeComponent);
        }

        // PATCH: odata/msGenSchemeComponents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msGenSchemeComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msGenSchemeComponent msGenSchemeComponent = await db.msGenSchemeComponents.FindAsync(key);
            if (msGenSchemeComponent == null)
            {
                return NotFound();
            }

            patch.Patch(msGenSchemeComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msGenSchemeComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msGenSchemeComponent);
        }

        // DELETE: odata/msGenSchemeComponents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msGenSchemeComponent msGenSchemeComponent = await db.msGenSchemeComponents.FindAsync(key);
            if (msGenSchemeComponent == null)
            {
                return NotFound();
            }

            db.msGenSchemeComponents.Remove(msGenSchemeComponent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msGenSchemeComponents(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.msGenSchemeComponents.Where(m => m.ID == key).Select(m => m.msSubstationStructure));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msGenSchemeComponentExists(int key)
        {
            return db.msGenSchemeComponents.Count(e => e.ID == key) > 0;
        }
    }
}

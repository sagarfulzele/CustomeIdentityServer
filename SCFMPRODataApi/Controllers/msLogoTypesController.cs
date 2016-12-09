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
    builder.EntitySet<msLogoType>("msLogoTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msLogoTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msLogoTypes
        [EnableQuery]
        public IQueryable<msLogoType> GetmsLogoTypes()
        {
            return db.msLogoTypes;
        }

        // GET: odata/msLogoTypes(5)
        [EnableQuery]
        public SingleResult<msLogoType> GetmsLogoType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msLogoTypes.Where(msLogoType => msLogoType.ID == key));
        }

        // PUT: odata/msLogoTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msLogoType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msLogoType msLogoType = await db.msLogoTypes.FindAsync(key);
            if (msLogoType == null)
            {
                return NotFound();
            }

            patch.Put(msLogoType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msLogoTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msLogoType);
        }

        // POST: odata/msLogoTypes
        public async Task<IHttpActionResult> Post(msLogoType msLogoType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msLogoTypes.Add(msLogoType);
            await db.SaveChangesAsync();

            return Created(msLogoType);
        }

        // PATCH: odata/msLogoTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msLogoType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msLogoType msLogoType = await db.msLogoTypes.FindAsync(key);
            if (msLogoType == null)
            {
                return NotFound();
            }

            patch.Patch(msLogoType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msLogoTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msLogoType);
        }

        // DELETE: odata/msLogoTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msLogoType msLogoType = await db.msLogoTypes.FindAsync(key);
            if (msLogoType == null)
            {
                return NotFound();
            }

            db.msLogoTypes.Remove(msLogoType);
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

        private bool msLogoTypeExists(int key)
        {
            return db.msLogoTypes.Count(e => e.ID == key) > 0;
        }
    }
}

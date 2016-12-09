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
    builder.EntitySet<msBayType>("msBayTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msBayTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msBayTypes
        [EnableQuery]
        public IQueryable<msBayType> GetmsBayTypes()
        {
            return db.msBayTypes;
        }

        // GET: odata/msBayTypes(5)
        [EnableQuery]
        public SingleResult<msBayType> GetmsBayType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msBayTypes.Where(msBayType => msBayType.ID == key));
        }

        // PUT: odata/msBayTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msBayType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msBayType msBayType = await db.msBayTypes.FindAsync(key);
            if (msBayType == null)
            {
                return NotFound();
            }

            patch.Put(msBayType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msBayTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msBayType);
        }

        // POST: odata/msBayTypes
        public async Task<IHttpActionResult> Post(msBayType msBayType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msBayTypes.Add(msBayType);
            await db.SaveChangesAsync();

            return Created(msBayType);
        }

        // PATCH: odata/msBayTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msBayType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msBayType msBayType = await db.msBayTypes.FindAsync(key);
            if (msBayType == null)
            {
                return NotFound();
            }

            patch.Patch(msBayType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msBayTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msBayType);
        }

        // DELETE: odata/msBayTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msBayType msBayType = await db.msBayTypes.FindAsync(key);
            if (msBayType == null)
            {
                return NotFound();
            }

            db.msBayTypes.Remove(msBayType);
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

        private bool msBayTypeExists(int key)
        {
            return db.msBayTypes.Count(e => e.ID == key) > 0;
        }
    }
}

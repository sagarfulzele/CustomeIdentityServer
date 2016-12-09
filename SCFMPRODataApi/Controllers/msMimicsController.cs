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
    builder.EntitySet<msMimic>("msMimics");
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msMimicsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msMimics
        [EnableQuery]
        public IQueryable<msMimic> GetmsMimics()
        {
            return db.msMimics;
        }

        // GET: odata/msMimics(5)
        [EnableQuery]
        public SingleResult<msMimic> GetmsMimic([FromODataUri] int key)
        {
            return SingleResult.Create(db.msMimics.Where(msMimic => msMimic.ID == key));
        }

        // PUT: odata/msMimics(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msMimic> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msMimic msMimic = await db.msMimics.FindAsync(key);
            if (msMimic == null)
            {
                return NotFound();
            }

            patch.Put(msMimic);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msMimicExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msMimic);
        }

        // POST: odata/msMimics
        public async Task<IHttpActionResult> Post(msMimic msMimic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msMimics.Add(msMimic);
            await db.SaveChangesAsync();

            return Created(msMimic);
        }

        // PATCH: odata/msMimics(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msMimic> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msMimic msMimic = await db.msMimics.FindAsync(key);
            if (msMimic == null)
            {
                return NotFound();
            }

            patch.Patch(msMimic);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msMimicExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msMimic);
        }

        // DELETE: odata/msMimics(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msMimic msMimic = await db.msMimics.FindAsync(key);
            if (msMimic == null)
            {
                return NotFound();
            }

            db.msMimics.Remove(msMimic);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msMimics(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.msMimics.Where(m => m.ID == key).Select(m => m.msSubstationStructure));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msMimicExists(int key)
        {
            return db.msMimics.Count(e => e.ID == key) > 0;
        }
    }
}

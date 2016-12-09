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
    builder.EntitySet<msDrawingType>("msDrawingTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msDrawingTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msDrawingTypes
        [EnableQuery]
        public IQueryable<msDrawingType> GetmsDrawingTypes()
        {
            return db.msDrawingTypes;
        }

        // GET: odata/msDrawingTypes(5)
        [EnableQuery]
        public SingleResult<msDrawingType> GetmsDrawingType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msDrawingTypes.Where(msDrawingType => msDrawingType.ID == key));
        }

        // PUT: odata/msDrawingTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msDrawingType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msDrawingType msDrawingType = await db.msDrawingTypes.FindAsync(key);
            if (msDrawingType == null)
            {
                return NotFound();
            }

            patch.Put(msDrawingType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msDrawingTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msDrawingType);
        }

        // POST: odata/msDrawingTypes
        public async Task<IHttpActionResult> Post(msDrawingType msDrawingType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msDrawingTypes.Add(msDrawingType);
            await db.SaveChangesAsync();

            return Created(msDrawingType);
        }

        // PATCH: odata/msDrawingTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msDrawingType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msDrawingType msDrawingType = await db.msDrawingTypes.FindAsync(key);
            if (msDrawingType == null)
            {
                return NotFound();
            }

            patch.Patch(msDrawingType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msDrawingTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msDrawingType);
        }

        // DELETE: odata/msDrawingTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msDrawingType msDrawingType = await db.msDrawingTypes.FindAsync(key);
            if (msDrawingType == null)
            {
                return NotFound();
            }

            db.msDrawingTypes.Remove(msDrawingType);
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

        private bool msDrawingTypeExists(int key)
        {
            return db.msDrawingTypes.Count(e => e.ID == key) > 0;
        }
    }
}

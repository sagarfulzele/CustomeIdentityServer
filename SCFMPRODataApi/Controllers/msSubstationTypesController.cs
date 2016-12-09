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
    builder.EntitySet<msSubstationType>("msSubstationTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msSubstationTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msSubstationTypes
        [EnableQuery]
        public IQueryable<msSubstationType> GetmsSubstationTypes()
        {
            return db.msSubstationTypes;
        }

        // GET: odata/msSubstationTypes(5)
        [EnableQuery]
        public SingleResult<msSubstationType> GetmsSubstationType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msSubstationTypes.Where(msSubstationType => msSubstationType.ID == key));
        }

        // PUT: odata/msSubstationTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msSubstationType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msSubstationType msSubstationType = await db.msSubstationTypes.FindAsync(key);
            if (msSubstationType == null)
            {
                return NotFound();
            }

            patch.Put(msSubstationType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msSubstationTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msSubstationType);
        }

        // POST: odata/msSubstationTypes
        public async Task<IHttpActionResult> Post(msSubstationType msSubstationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msSubstationTypes.Add(msSubstationType);
            await db.SaveChangesAsync();

            return Created(msSubstationType);
        }

        // PATCH: odata/msSubstationTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msSubstationType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msSubstationType msSubstationType = await db.msSubstationTypes.FindAsync(key);
            if (msSubstationType == null)
            {
                return NotFound();
            }

            patch.Patch(msSubstationType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msSubstationTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msSubstationType);
        }

        // DELETE: odata/msSubstationTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msSubstationType msSubstationType = await db.msSubstationTypes.FindAsync(key);
            if (msSubstationType == null)
            {
                return NotFound();
            }

            db.msSubstationTypes.Remove(msSubstationType);
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

        private bool msSubstationTypeExists(int key)
        {
            return db.msSubstationTypes.Count(e => e.ID == key) > 0;
        }
    }
}

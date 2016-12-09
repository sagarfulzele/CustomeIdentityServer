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
    builder.EntitySet<msInsulationType>("msInsulationTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msInsulationTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msInsulationTypes
        [EnableQuery]
        public IQueryable<msInsulationType> GetmsInsulationTypes()
        {
            return db.msInsulationTypes;
        }

        // GET: odata/msInsulationTypes(5)
        [EnableQuery]
        public SingleResult<msInsulationType> GetmsInsulationType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msInsulationTypes.Where(msInsulationType => msInsulationType.ID == key));
        }

        // PUT: odata/msInsulationTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msInsulationType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msInsulationType msInsulationType = await db.msInsulationTypes.FindAsync(key);
            if (msInsulationType == null)
            {
                return NotFound();
            }

            patch.Put(msInsulationType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msInsulationTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msInsulationType);
        }

        // POST: odata/msInsulationTypes
        public async Task<IHttpActionResult> Post(msInsulationType msInsulationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msInsulationTypes.Add(msInsulationType);
            await db.SaveChangesAsync();

            return Created(msInsulationType);
        }

        // PATCH: odata/msInsulationTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msInsulationType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msInsulationType msInsulationType = await db.msInsulationTypes.FindAsync(key);
            if (msInsulationType == null)
            {
                return NotFound();
            }

            patch.Patch(msInsulationType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msInsulationTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msInsulationType);
        }

        // DELETE: odata/msInsulationTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msInsulationType msInsulationType = await db.msInsulationTypes.FindAsync(key);
            if (msInsulationType == null)
            {
                return NotFound();
            }

            db.msInsulationTypes.Remove(msInsulationType);
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

        private bool msInsulationTypeExists(int key)
        {
            return db.msInsulationTypes.Count(e => e.ID == key) > 0;
        }
    }
}

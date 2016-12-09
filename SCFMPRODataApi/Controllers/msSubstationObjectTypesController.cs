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
    builder.EntitySet<msSubstationObjectType>("msSubstationObjectTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msSubstationObjectTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msSubstationObjectTypes
        [EnableQuery]
        public IQueryable<msSubstationObjectType> GetmsSubstationObjectTypes()
        {
            return db.msSubstationObjectTypes;
        }

        // GET: odata/msSubstationObjectTypes(5)
        [EnableQuery]
        public SingleResult<msSubstationObjectType> GetmsSubstationObjectType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msSubstationObjectTypes.Where(msSubstationObjectType => msSubstationObjectType.ID == key));
        }

        // PUT: odata/msSubstationObjectTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msSubstationObjectType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msSubstationObjectType msSubstationObjectType = await db.msSubstationObjectTypes.FindAsync(key);
            if (msSubstationObjectType == null)
            {
                return NotFound();
            }

            patch.Put(msSubstationObjectType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msSubstationObjectTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msSubstationObjectType);
        }

        // POST: odata/msSubstationObjectTypes
        public async Task<IHttpActionResult> Post(msSubstationObjectType msSubstationObjectType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msSubstationObjectTypes.Add(msSubstationObjectType);
            await db.SaveChangesAsync();

            return Created(msSubstationObjectType);
        }

        // PATCH: odata/msSubstationObjectTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msSubstationObjectType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msSubstationObjectType msSubstationObjectType = await db.msSubstationObjectTypes.FindAsync(key);
            if (msSubstationObjectType == null)
            {
                return NotFound();
            }

            patch.Patch(msSubstationObjectType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msSubstationObjectTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msSubstationObjectType);
        }

        // DELETE: odata/msSubstationObjectTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msSubstationObjectType msSubstationObjectType = await db.msSubstationObjectTypes.FindAsync(key);
            if (msSubstationObjectType == null)
            {
                return NotFound();
            }

            db.msSubstationObjectTypes.Remove(msSubstationObjectType);
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

        private bool msSubstationObjectTypeExists(int key)
        {
            return db.msSubstationObjectTypes.Count(e => e.ID == key) > 0;
        }
    }
}

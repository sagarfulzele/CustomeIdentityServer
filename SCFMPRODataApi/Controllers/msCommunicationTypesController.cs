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
    builder.EntitySet<msCommunicationType>("msCommunicationTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msCommunicationTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msCommunicationTypes
        [EnableQuery]
        public IQueryable<msCommunicationType> GetmsCommunicationTypes()
        {
            return db.msCommunicationTypes;
        }

        // GET: odata/msCommunicationTypes(5)
        [EnableQuery]
        public SingleResult<msCommunicationType> GetmsCommunicationType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msCommunicationTypes.Where(msCommunicationType => msCommunicationType.ID == key));
        }

        // PUT: odata/msCommunicationTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msCommunicationType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCommunicationType msCommunicationType = await db.msCommunicationTypes.FindAsync(key);
            if (msCommunicationType == null)
            {
                return NotFound();
            }

            patch.Put(msCommunicationType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCommunicationTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCommunicationType);
        }

        // POST: odata/msCommunicationTypes
        public async Task<IHttpActionResult> Post(msCommunicationType msCommunicationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msCommunicationTypes.Add(msCommunicationType);
            await db.SaveChangesAsync();

            return Created(msCommunicationType);
        }

        // PATCH: odata/msCommunicationTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msCommunicationType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCommunicationType msCommunicationType = await db.msCommunicationTypes.FindAsync(key);
            if (msCommunicationType == null)
            {
                return NotFound();
            }

            patch.Patch(msCommunicationType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCommunicationTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCommunicationType);
        }

        // DELETE: odata/msCommunicationTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msCommunicationType msCommunicationType = await db.msCommunicationTypes.FindAsync(key);
            if (msCommunicationType == null)
            {
                return NotFound();
            }

            db.msCommunicationTypes.Remove(msCommunicationType);
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

        private bool msCommunicationTypeExists(int key)
        {
            return db.msCommunicationTypes.Count(e => e.ID == key) > 0;
        }
    }
}

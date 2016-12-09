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
    builder.EntitySet<msPanelArgType>("msPanelArgTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msPanelArgTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msPanelArgTypes
        [EnableQuery]
        public IQueryable<msPanelArgType> GetmsPanelArgTypes()
        {
            return db.msPanelArgTypes;
        }

        // GET: odata/msPanelArgTypes(5)
        [EnableQuery]
        public SingleResult<msPanelArgType> GetmsPanelArgType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPanelArgTypes.Where(msPanelArgType => msPanelArgType.ID == key));
        }

        // PUT: odata/msPanelArgTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msPanelArgType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPanelArgType msPanelArgType = await db.msPanelArgTypes.FindAsync(key);
            if (msPanelArgType == null)
            {
                return NotFound();
            }

            patch.Put(msPanelArgType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPanelArgTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPanelArgType);
        }

        // POST: odata/msPanelArgTypes
        public async Task<IHttpActionResult> Post(msPanelArgType msPanelArgType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msPanelArgTypes.Add(msPanelArgType);
            await db.SaveChangesAsync();

            return Created(msPanelArgType);
        }

        // PATCH: odata/msPanelArgTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msPanelArgType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPanelArgType msPanelArgType = await db.msPanelArgTypes.FindAsync(key);
            if (msPanelArgType == null)
            {
                return NotFound();
            }

            patch.Patch(msPanelArgType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPanelArgTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPanelArgType);
        }

        // DELETE: odata/msPanelArgTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msPanelArgType msPanelArgType = await db.msPanelArgTypes.FindAsync(key);
            if (msPanelArgType == null)
            {
                return NotFound();
            }

            db.msPanelArgTypes.Remove(msPanelArgType);
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

        private bool msPanelArgTypeExists(int key)
        {
            return db.msPanelArgTypes.Count(e => e.ID == key) > 0;
        }
    }
}

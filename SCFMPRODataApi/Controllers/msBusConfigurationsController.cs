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
    builder.EntitySet<msBusConfiguration>("msBusConfigurations");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msBusConfigurationsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msBusConfigurations
        [EnableQuery]
        public IQueryable<msBusConfiguration> GetmsBusConfigurations()
        {
            return db.msBusConfigurations;
        }

        // GET: odata/msBusConfigurations(5)
        [EnableQuery]
        public SingleResult<msBusConfiguration> GetmsBusConfiguration([FromODataUri] int key)
        {
            return SingleResult.Create(db.msBusConfigurations.Where(msBusConfiguration => msBusConfiguration.ID == key));
        }

        // PUT: odata/msBusConfigurations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msBusConfiguration> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msBusConfiguration msBusConfiguration = await db.msBusConfigurations.FindAsync(key);
            if (msBusConfiguration == null)
            {
                return NotFound();
            }

            patch.Put(msBusConfiguration);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msBusConfigurationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msBusConfiguration);
        }

        // POST: odata/msBusConfigurations
        public async Task<IHttpActionResult> Post(msBusConfiguration msBusConfiguration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msBusConfigurations.Add(msBusConfiguration);
            await db.SaveChangesAsync();

            return Created(msBusConfiguration);
        }

        // PATCH: odata/msBusConfigurations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msBusConfiguration> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msBusConfiguration msBusConfiguration = await db.msBusConfigurations.FindAsync(key);
            if (msBusConfiguration == null)
            {
                return NotFound();
            }

            patch.Patch(msBusConfiguration);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msBusConfigurationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msBusConfiguration);
        }

        // DELETE: odata/msBusConfigurations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msBusConfiguration msBusConfiguration = await db.msBusConfigurations.FindAsync(key);
            if (msBusConfiguration == null)
            {
                return NotFound();
            }

            db.msBusConfigurations.Remove(msBusConfiguration);
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

        private bool msBusConfigurationExists(int key)
        {
            return db.msBusConfigurations.Count(e => e.ID == key) > 0;
        }
    }
}

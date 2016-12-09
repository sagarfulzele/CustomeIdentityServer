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
    builder.EntitySet<msMPRSetting>("msMPRSettings");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msMPRSettingsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msMPRSettings
        [EnableQuery]
        public IQueryable<msMPRSetting> GetmsMPRSettings()
        {
            return db.msMPRSettings;
        }

        // GET: odata/msMPRSettings(5)
        [EnableQuery]
        public SingleResult<msMPRSetting> GetmsMPRSetting([FromODataUri] int key)
        {
            return SingleResult.Create(db.msMPRSettings.Where(msMPRSetting => msMPRSetting.ID == key));
        }

        // PUT: odata/msMPRSettings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msMPRSetting> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msMPRSetting msMPRSetting = await db.msMPRSettings.FindAsync(key);
            if (msMPRSetting == null)
            {
                return NotFound();
            }

            patch.Put(msMPRSetting);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msMPRSettingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msMPRSetting);
        }

        // POST: odata/msMPRSettings
        public async Task<IHttpActionResult> Post(msMPRSetting msMPRSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msMPRSettings.Add(msMPRSetting);
            await db.SaveChangesAsync();

            return Created(msMPRSetting);
        }

        // PATCH: odata/msMPRSettings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msMPRSetting> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msMPRSetting msMPRSetting = await db.msMPRSettings.FindAsync(key);
            if (msMPRSetting == null)
            {
                return NotFound();
            }

            patch.Patch(msMPRSetting);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msMPRSettingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msMPRSetting);
        }

        // DELETE: odata/msMPRSettings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msMPRSetting msMPRSetting = await db.msMPRSettings.FindAsync(key);
            if (msMPRSetting == null)
            {
                return NotFound();
            }

            db.msMPRSettings.Remove(msMPRSetting);
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

        private bool msMPRSettingExists(int key)
        {
            return db.msMPRSettings.Count(e => e.ID == key) > 0;
        }
    }
}

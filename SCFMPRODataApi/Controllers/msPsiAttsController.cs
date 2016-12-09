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
    builder.EntitySet<msPsiAtt>("msPsiAtts");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msPsiAttsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msPsiAtts
        [EnableQuery]
        public IQueryable<msPsiAtt> GetmsPsiAtts()
        {
            return db.msPsiAtts;
        }

        // GET: odata/msPsiAtts(5)
        [EnableQuery]
        public SingleResult<msPsiAtt> GetmsPsiAtt([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPsiAtts.Where(msPsiAtt => msPsiAtt.ID == key));
        }

        // PUT: odata/msPsiAtts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msPsiAtt> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPsiAtt msPsiAtt = await db.msPsiAtts.FindAsync(key);
            if (msPsiAtt == null)
            {
                return NotFound();
            }

            patch.Put(msPsiAtt);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPsiAttExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPsiAtt);
        }

        // POST: odata/msPsiAtts
        public async Task<IHttpActionResult> Post(msPsiAtt msPsiAtt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msPsiAtts.Add(msPsiAtt);
            await db.SaveChangesAsync();

            return Created(msPsiAtt);
        }

        // PATCH: odata/msPsiAtts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msPsiAtt> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPsiAtt msPsiAtt = await db.msPsiAtts.FindAsync(key);
            if (msPsiAtt == null)
            {
                return NotFound();
            }

            patch.Patch(msPsiAtt);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPsiAttExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPsiAtt);
        }

        // DELETE: odata/msPsiAtts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msPsiAtt msPsiAtt = await db.msPsiAtts.FindAsync(key);
            if (msPsiAtt == null)
            {
                return NotFound();
            }

            db.msPsiAtts.Remove(msPsiAtt);
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

        private bool msPsiAttExists(int key)
        {
            return db.msPsiAtts.Count(e => e.ID == key) > 0;
        }
    }
}

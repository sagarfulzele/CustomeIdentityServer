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
    builder.EntitySet<msPsiAttWireNo>("msPsiAttWireNoes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msPsiAttWireNoesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msPsiAttWireNoes
        [EnableQuery]
        public IQueryable<msPsiAttWireNo> GetmsPsiAttWireNoes()
        {
            return db.msPsiAttWireNoes;
        }

        // GET: odata/msPsiAttWireNoes(5)
        [EnableQuery]
        public SingleResult<msPsiAttWireNo> GetmsPsiAttWireNo([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPsiAttWireNoes.Where(msPsiAttWireNo => msPsiAttWireNo.ID == key));
        }

        // PUT: odata/msPsiAttWireNoes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msPsiAttWireNo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPsiAttWireNo msPsiAttWireNo = await db.msPsiAttWireNoes.FindAsync(key);
            if (msPsiAttWireNo == null)
            {
                return NotFound();
            }

            patch.Put(msPsiAttWireNo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPsiAttWireNoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPsiAttWireNo);
        }

        // POST: odata/msPsiAttWireNoes
        public async Task<IHttpActionResult> Post(msPsiAttWireNo msPsiAttWireNo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msPsiAttWireNoes.Add(msPsiAttWireNo);
            await db.SaveChangesAsync();

            return Created(msPsiAttWireNo);
        }

        // PATCH: odata/msPsiAttWireNoes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msPsiAttWireNo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPsiAttWireNo msPsiAttWireNo = await db.msPsiAttWireNoes.FindAsync(key);
            if (msPsiAttWireNo == null)
            {
                return NotFound();
            }

            patch.Patch(msPsiAttWireNo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPsiAttWireNoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPsiAttWireNo);
        }

        // DELETE: odata/msPsiAttWireNoes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msPsiAttWireNo msPsiAttWireNo = await db.msPsiAttWireNoes.FindAsync(key);
            if (msPsiAttWireNo == null)
            {
                return NotFound();
            }

            db.msPsiAttWireNoes.Remove(msPsiAttWireNo);
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

        private bool msPsiAttWireNoExists(int key)
        {
            return db.msPsiAttWireNoes.Count(e => e.ID == key) > 0;
        }
    }
}

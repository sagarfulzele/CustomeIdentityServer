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
    builder.EntitySet<mdPRData>("mdPRDatas");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPRDatasController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPRDatas
        [EnableQuery]
        public IQueryable<mdPRData> GetmdPRDatas()
        {
            return db.mdPRDatas;
        }

        // GET: odata/mdPRDatas(5)
        [EnableQuery]
        public SingleResult<mdPRData> GetmdPRData([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPRDatas.Where(mdPRData => mdPRData.PrDataId == key));
        }

        // PUT: odata/mdPRDatas(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPRData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPRData mdPRData = await db.mdPRDatas.FindAsync(key);
            if (mdPRData == null)
            {
                return NotFound();
            }

            patch.Put(mdPRData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPRDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPRData);
        }

        // POST: odata/mdPRDatas
        public async Task<IHttpActionResult> Post(mdPRData mdPRData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPRDatas.Add(mdPRData);
            await db.SaveChangesAsync();

            return Created(mdPRData);
        }

        // PATCH: odata/mdPRDatas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPRData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPRData mdPRData = await db.mdPRDatas.FindAsync(key);
            if (mdPRData == null)
            {
                return NotFound();
            }

            patch.Patch(mdPRData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPRDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPRData);
        }

        // DELETE: odata/mdPRDatas(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPRData mdPRData = await db.mdPRDatas.FindAsync(key);
            if (mdPRData == null)
            {
                return NotFound();
            }

            db.mdPRDatas.Remove(mdPRData);
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

        private bool mdPRDataExists(int key)
        {
            return db.mdPRDatas.Count(e => e.PrDataId == key) > 0;
        }
    }
}

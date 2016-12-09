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
    builder.EntitySet<mdRemoteEndDetail>("mdRemoteEndDetails");
    builder.EntitySet<mdVoltageLevel>("mdVoltageLevels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdRemoteEndDetailsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdRemoteEndDetails
        [EnableQuery]
        public IQueryable<mdRemoteEndDetail> GetmdRemoteEndDetails()
        {
            return db.mdRemoteEndDetails;
        }

        // GET: odata/mdRemoteEndDetails(5)
        [EnableQuery]
        public SingleResult<mdRemoteEndDetail> GetmdRemoteEndDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdRemoteEndDetails.Where(mdRemoteEndDetail => mdRemoteEndDetail.RemoteEndDetailID == key));
        }

        // PUT: odata/mdRemoteEndDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdRemoteEndDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdRemoteEndDetail mdRemoteEndDetail = await db.mdRemoteEndDetails.FindAsync(key);
            if (mdRemoteEndDetail == null)
            {
                return NotFound();
            }

            patch.Put(mdRemoteEndDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdRemoteEndDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdRemoteEndDetail);
        }

        // POST: odata/mdRemoteEndDetails
        public async Task<IHttpActionResult> Post(mdRemoteEndDetail mdRemoteEndDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdRemoteEndDetails.Add(mdRemoteEndDetail);
            await db.SaveChangesAsync();

            return Created(mdRemoteEndDetail);
        }

        // PATCH: odata/mdRemoteEndDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdRemoteEndDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdRemoteEndDetail mdRemoteEndDetail = await db.mdRemoteEndDetails.FindAsync(key);
            if (mdRemoteEndDetail == null)
            {
                return NotFound();
            }

            patch.Patch(mdRemoteEndDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdRemoteEndDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdRemoteEndDetail);
        }

        // DELETE: odata/mdRemoteEndDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdRemoteEndDetail mdRemoteEndDetail = await db.mdRemoteEndDetails.FindAsync(key);
            if (mdRemoteEndDetail == null)
            {
                return NotFound();
            }

            db.mdRemoteEndDetails.Remove(mdRemoteEndDetail);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdRemoteEndDetails(5)/mdVoltageLevel
        [EnableQuery]
        public SingleResult<mdVoltageLevel> GetmdVoltageLevel([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdRemoteEndDetails.Where(m => m.RemoteEndDetailID == key).Select(m => m.mdVoltageLevel));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdRemoteEndDetailExists(int key)
        {
            return db.mdRemoteEndDetails.Count(e => e.RemoteEndDetailID == key) > 0;
        }
    }
}

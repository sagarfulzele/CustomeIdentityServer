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
    builder.EntitySet<msContactDetail>("msContactDetails");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msContactDetailsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msContactDetails
        [EnableQuery]
        public IQueryable<msContactDetail> GetmsContactDetails()
        {
            return db.msContactDetails;
        }

        // GET: odata/msContactDetails(5)
        [EnableQuery]
        public SingleResult<msContactDetail> GetmsContactDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.msContactDetails.Where(msContactDetail => msContactDetail.ID == key));
        }

        // PUT: odata/msContactDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msContactDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msContactDetail msContactDetail = await db.msContactDetails.FindAsync(key);
            if (msContactDetail == null)
            {
                return NotFound();
            }

            patch.Put(msContactDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msContactDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msContactDetail);
        }

        // POST: odata/msContactDetails
        public async Task<IHttpActionResult> Post(msContactDetail msContactDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msContactDetails.Add(msContactDetail);
            await db.SaveChangesAsync();

            return Created(msContactDetail);
        }

        // PATCH: odata/msContactDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msContactDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msContactDetail msContactDetail = await db.msContactDetails.FindAsync(key);
            if (msContactDetail == null)
            {
                return NotFound();
            }

            patch.Patch(msContactDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msContactDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msContactDetail);
        }

        // DELETE: odata/msContactDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msContactDetail msContactDetail = await db.msContactDetails.FindAsync(key);
            if (msContactDetail == null)
            {
                return NotFound();
            }

            db.msContactDetails.Remove(msContactDetail);
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

        private bool msContactDetailExists(int key)
        {
            return db.msContactDetails.Count(e => e.ID == key) > 0;
        }
    }
}

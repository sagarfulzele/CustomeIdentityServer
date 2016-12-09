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
    builder.EntitySet<mdContactDetail>("mdContactDetails");
    builder.EntitySet<mdLogoDetail>("mdLogoDetails"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdContactDetailsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdContactDetails
        [EnableQuery]
        public IQueryable<mdContactDetail> GetmdContactDetails()
        {
            return db.mdContactDetails;
        }

        // GET: odata/mdContactDetails(5)
        [EnableQuery]
        public SingleResult<mdContactDetail> GetmdContactDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdContactDetails.Where(mdContactDetail => mdContactDetail.ContactDetailsID == key));
        }

        // PUT: odata/mdContactDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdContactDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdContactDetail mdContactDetail = await db.mdContactDetails.FindAsync(key);
            if (mdContactDetail == null)
            {
                return NotFound();
            }

            patch.Put(mdContactDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdContactDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdContactDetail);
        }

        // POST: odata/mdContactDetails
        public async Task<IHttpActionResult> Post(mdContactDetail mdContactDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdContactDetails.Add(mdContactDetail);
            await db.SaveChangesAsync();

            return Created(mdContactDetail);
        }

        // PATCH: odata/mdContactDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdContactDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdContactDetail mdContactDetail = await db.mdContactDetails.FindAsync(key);
            if (mdContactDetail == null)
            {
                return NotFound();
            }

            patch.Patch(mdContactDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdContactDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdContactDetail);
        }

        // DELETE: odata/mdContactDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdContactDetail mdContactDetail = await db.mdContactDetails.FindAsync(key);
            if (mdContactDetail == null)
            {
                return NotFound();
            }

            db.mdContactDetails.Remove(mdContactDetail);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdContactDetails(5)/mdLogoDetail
        [EnableQuery]
        public SingleResult<mdLogoDetail> GetmdLogoDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdContactDetails.Where(m => m.ContactDetailsID == key).Select(m => m.mdLogoDetail));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdContactDetailExists(int key)
        {
            return db.mdContactDetails.Count(e => e.ContactDetailsID == key) > 0;
        }
    }
}

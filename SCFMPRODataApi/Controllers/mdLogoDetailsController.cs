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
    builder.EntitySet<mdLogoDetail>("mdLogoDetails");
    builder.EntitySet<mdContactDetail>("mdContactDetails"); 
    builder.EntitySet<mdDoubleLogo>("mdDoubleLogoes"); 
    builder.EntitySet<mdSingleLogo>("mdSingleLogoes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdLogoDetailsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdLogoDetails
        [EnableQuery]
        public IQueryable<mdLogoDetail> GetmdLogoDetails()
        {
            return db.mdLogoDetails;
        }

        // GET: odata/mdLogoDetails(5)
        [EnableQuery]
        public SingleResult<mdLogoDetail> GetmdLogoDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdLogoDetails.Where(mdLogoDetail => mdLogoDetail.LogoDetailID == key));
        }

        // PUT: odata/mdLogoDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdLogoDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdLogoDetail mdLogoDetail = await db.mdLogoDetails.FindAsync(key);
            if (mdLogoDetail == null)
            {
                return NotFound();
            }

            patch.Put(mdLogoDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdLogoDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdLogoDetail);
        }

        // POST: odata/mdLogoDetails
        public async Task<IHttpActionResult> Post(mdLogoDetail mdLogoDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdLogoDetails.Add(mdLogoDetail);
            await db.SaveChangesAsync();

            return Created(mdLogoDetail);
        }

        // PATCH: odata/mdLogoDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdLogoDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdLogoDetail mdLogoDetail = await db.mdLogoDetails.FindAsync(key);
            if (mdLogoDetail == null)
            {
                return NotFound();
            }

            patch.Patch(mdLogoDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdLogoDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdLogoDetail);
        }

        // DELETE: odata/mdLogoDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdLogoDetail mdLogoDetail = await db.mdLogoDetails.FindAsync(key);
            if (mdLogoDetail == null)
            {
                return NotFound();
            }

            db.mdLogoDetails.Remove(mdLogoDetail);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdLogoDetails(5)/mdContactDetails
        [EnableQuery]
        public IQueryable<mdContactDetail> GetmdContactDetails([FromODataUri] int key)
        {
            return db.mdLogoDetails.Where(m => m.LogoDetailID == key).SelectMany(m => m.mdContactDetails);
        }

        // GET: odata/mdLogoDetails(5)/mdDoubleLogoes
        [EnableQuery]
        public IQueryable<mdDoubleLogo> GetmdDoubleLogoes([FromODataUri] int key)
        {
            return db.mdLogoDetails.Where(m => m.LogoDetailID == key).SelectMany(m => m.mdDoubleLogoes);
        }

        // GET: odata/mdLogoDetails(5)/mdDoubleLogoes1
        [EnableQuery]
        public IQueryable<mdDoubleLogo> GetmdDoubleLogoes1([FromODataUri] int key)
        {
            return db.mdLogoDetails.Where(m => m.LogoDetailID == key).SelectMany(m => m.mdDoubleLogoes1);
        }

        // GET: odata/mdLogoDetails(5)/mdSingleLogoes
        [EnableQuery]
        public IQueryable<mdSingleLogo> GetmdSingleLogoes([FromODataUri] int key)
        {
            return db.mdLogoDetails.Where(m => m.LogoDetailID == key).SelectMany(m => m.mdSingleLogoes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdLogoDetailExists(int key)
        {
            return db.mdLogoDetails.Count(e => e.LogoDetailID == key) > 0;
        }
    }
}

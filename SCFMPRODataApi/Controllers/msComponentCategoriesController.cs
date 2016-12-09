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
    builder.EntitySet<msComponentCategory>("msComponentCategories");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msComponentCategoriesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msComponentCategories
        [EnableQuery]
        public IQueryable<msComponentCategory> GetmsComponentCategories()
        {
            return db.msComponentCategories;
        }

        // GET: odata/msComponentCategories(5)
        [EnableQuery]
        public SingleResult<msComponentCategory> GetmsComponentCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.msComponentCategories.Where(msComponentCategory => msComponentCategory.ID == key));
        }

        // PUT: odata/msComponentCategories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msComponentCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msComponentCategory msComponentCategory = await db.msComponentCategories.FindAsync(key);
            if (msComponentCategory == null)
            {
                return NotFound();
            }

            patch.Put(msComponentCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msComponentCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msComponentCategory);
        }

        // POST: odata/msComponentCategories
        public async Task<IHttpActionResult> Post(msComponentCategory msComponentCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msComponentCategories.Add(msComponentCategory);
            await db.SaveChangesAsync();

            return Created(msComponentCategory);
        }

        // PATCH: odata/msComponentCategories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msComponentCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msComponentCategory msComponentCategory = await db.msComponentCategories.FindAsync(key);
            if (msComponentCategory == null)
            {
                return NotFound();
            }

            patch.Patch(msComponentCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msComponentCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msComponentCategory);
        }

        // DELETE: odata/msComponentCategories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msComponentCategory msComponentCategory = await db.msComponentCategories.FindAsync(key);
            if (msComponentCategory == null)
            {
                return NotFound();
            }

            db.msComponentCategories.Remove(msComponentCategory);
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

        private bool msComponentCategoryExists(int key)
        {
            return db.msComponentCategories.Count(e => e.ID == key) > 0;
        }
    }
}

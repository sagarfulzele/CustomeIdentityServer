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
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using PsiMprODataApi.Models;

namespace PsiMprODataApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using PsiMprODataApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<AspNetRoleClaim>("AspNetRoleClaims");
    builder.EntitySet<AspNetRole>("AspNetRoles"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AspNetRoleClaimsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/AspNetRoleClaims
        [EnableQuery]
        public IQueryable<AspNetRoleClaim> GetAspNetRoleClaims()
        {
            return db.AspNetRoleClaims;
        }

        // GET: odata/AspNetRoleClaims(5)
        [EnableQuery]
        public SingleResult<AspNetRoleClaim> GetAspNetRoleClaim([FromODataUri] int key)
        {
            return SingleResult.Create(db.AspNetRoleClaims.Where(aspNetRoleClaim => aspNetRoleClaim.Id == key));
        }

        // PUT: odata/AspNetRoleClaims(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<AspNetRoleClaim> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetRoleClaim aspNetRoleClaim = await db.AspNetRoleClaims.FindAsync(key);
            if (aspNetRoleClaim == null)
            {
                return NotFound();
            }

            patch.Put(aspNetRoleClaim);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetRoleClaimExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetRoleClaim);
        }

        // POST: odata/AspNetRoleClaims
        public async Task<IHttpActionResult> Post(AspNetRoleClaim aspNetRoleClaim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AspNetRoleClaims.Add(aspNetRoleClaim);
            await db.SaveChangesAsync();

            return Created(aspNetRoleClaim);
        }

        // PATCH: odata/AspNetRoleClaims(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<AspNetRoleClaim> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetRoleClaim aspNetRoleClaim = await db.AspNetRoleClaims.FindAsync(key);
            if (aspNetRoleClaim == null)
            {
                return NotFound();
            }

            patch.Patch(aspNetRoleClaim);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetRoleClaimExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetRoleClaim);
        }

        // DELETE: odata/AspNetRoleClaims(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            AspNetRoleClaim aspNetRoleClaim = await db.AspNetRoleClaims.FindAsync(key);
            if (aspNetRoleClaim == null)
            {
                return NotFound();
            }

            db.AspNetRoleClaims.Remove(aspNetRoleClaim);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AspNetRoleClaims(5)/AspNetRole
        [EnableQuery]
        public SingleResult<AspNetRole> GetAspNetRole([FromODataUri] int key)
        {
            return SingleResult.Create(db.AspNetRoleClaims.Where(m => m.Id == key).Select(m => m.AspNetRole));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetRoleClaimExists(int key)
        {
            return db.AspNetRoleClaims.Count(e => e.Id == key) > 0;
        }
    }
}

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
    builder.EntitySet<msCompany>("msCompanies");
    builder.EntitySet<mdProject>("mdProjects"); 
    builder.EntitySet<mdProjectStakeHolder>("mdProjectStakeHolders"); 
    builder.EntitySet<msStakeHolderContact>("msStakeHolderContacts"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msCompaniesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msCompanies
        [EnableQuery]
        public IQueryable<msCompany> GetmsCompanies()
        {
            return db.msCompanies;
        }

        // GET: odata/msCompanies(5)
        [EnableQuery]
        public SingleResult<msCompany> GetmsCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.msCompanies.Where(msCompany => msCompany.ID == key));
        }

        // PUT: odata/msCompanies(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msCompany> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCompany msCompany = await db.msCompanies.FindAsync(key);
            if (msCompany == null)
            {
                return NotFound();
            }

            patch.Put(msCompany);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCompanyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCompany);
        }

        // POST: odata/msCompanies
        public async Task<IHttpActionResult> Post(msCompany msCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msCompanies.Add(msCompany);
            await db.SaveChangesAsync();

            return Created(msCompany);
        }

        // PATCH: odata/msCompanies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msCompany> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msCompany msCompany = await db.msCompanies.FindAsync(key);
            if (msCompany == null)
            {
                return NotFound();
            }

            patch.Patch(msCompany);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msCompanyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msCompany);
        }

        // DELETE: odata/msCompanies(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msCompany msCompany = await db.msCompanies.FindAsync(key);
            if (msCompany == null)
            {
                return NotFound();
            }

            db.msCompanies.Remove(msCompany);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msCompanies(5)/mdProjects
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects([FromODataUri] int key)
        {
            return db.msCompanies.Where(m => m.ID == key).SelectMany(m => m.mdProjects);
        }

        // GET: odata/msCompanies(5)/mdProjectStakeHolders
        [EnableQuery]
        public IQueryable<mdProjectStakeHolder> GetmdProjectStakeHolders([FromODataUri] int key)
        {
            return db.msCompanies.Where(m => m.ID == key).SelectMany(m => m.mdProjectStakeHolders);
        }

        // GET: odata/msCompanies(5)/msStakeHolderContacts
        [EnableQuery]
        public IQueryable<msStakeHolderContact> GetmsStakeHolderContacts([FromODataUri] int key)
        {
            return db.msCompanies.Where(m => m.ID == key).SelectMany(m => m.msStakeHolderContacts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msCompanyExists(int key)
        {
            return db.msCompanies.Count(e => e.ID == key) > 0;
        }
    }
}

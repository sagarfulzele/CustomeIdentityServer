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
    builder.EntitySet<mdProjectStakeHolder>("mdProjectStakeHolders");
    builder.EntitySet<mdProject>("mdProjects"); 
    builder.EntitySet<msCompany>("msCompanies"); 
    builder.EntitySet<msStakeHolderContact>("msStakeHolderContacts"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdProjectStakeHoldersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdProjectStakeHolders
        [EnableQuery]
        public IQueryable<mdProjectStakeHolder> GetmdProjectStakeHolders()
        {
            return db.mdProjectStakeHolders;
        }

        // GET: odata/mdProjectStakeHolders(5)
        [EnableQuery]
        public SingleResult<mdProjectStakeHolder> GetmdProjectStakeHolder([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjectStakeHolders.Where(mdProjectStakeHolder => mdProjectStakeHolder.Id == key));
        }

        // PUT: odata/mdProjectStakeHolders(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdProjectStakeHolder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdProjectStakeHolder mdProjectStakeHolder = await db.mdProjectStakeHolders.FindAsync(key);
            if (mdProjectStakeHolder == null)
            {
                return NotFound();
            }

            patch.Put(mdProjectStakeHolder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdProjectStakeHolderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdProjectStakeHolder);
        }

        // POST: odata/mdProjectStakeHolders
        public async Task<IHttpActionResult> Post(mdProjectStakeHolder mdProjectStakeHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdProjectStakeHolders.Add(mdProjectStakeHolder);
            await db.SaveChangesAsync();

            return Created(mdProjectStakeHolder);
        }

        // PATCH: odata/mdProjectStakeHolders(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdProjectStakeHolder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdProjectStakeHolder mdProjectStakeHolder = await db.mdProjectStakeHolders.FindAsync(key);
            if (mdProjectStakeHolder == null)
            {
                return NotFound();
            }

            patch.Patch(mdProjectStakeHolder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdProjectStakeHolderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdProjectStakeHolder);
        }

        // DELETE: odata/mdProjectStakeHolders(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdProjectStakeHolder mdProjectStakeHolder = await db.mdProjectStakeHolders.FindAsync(key);
            if (mdProjectStakeHolder == null)
            {
                return NotFound();
            }

            db.mdProjectStakeHolders.Remove(mdProjectStakeHolder);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdProjectStakeHolders(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjectStakeHolders.Where(m => m.Id == key).Select(m => m.mdProject));
        }

        // GET: odata/mdProjectStakeHolders(5)/msCompany
        [EnableQuery]
        public SingleResult<msCompany> GetmsCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjectStakeHolders.Where(m => m.Id == key).Select(m => m.msCompany));
        }

        // GET: odata/mdProjectStakeHolders(5)/msStakeHolderContacts
        [EnableQuery]
        public IQueryable<msStakeHolderContact> GetmsStakeHolderContacts([FromODataUri] int key)
        {
            return db.mdProjectStakeHolders.Where(m => m.Id == key).SelectMany(m => m.msStakeHolderContacts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdProjectStakeHolderExists(int key)
        {
            return db.mdProjectStakeHolders.Count(e => e.Id == key) > 0;
        }




        [HttpPost]
        public async Task<IHttpActionResult> SavemdProjectStakeHolders([FromODataUri] int key, mdProjectStakeHolder parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            parameters.ProjectID = key;
            if (mdProjectStakeHolderExists(parameters.Id))
            {
                Delta<mdProjectStakeHolder> obj = new Delta<mdProjectStakeHolder>();
                obj.Put(parameters);
                return await Put(parameters.Id, obj);
            }
            else
            {
                return await Post(parameters);
            }

        }
    }
}

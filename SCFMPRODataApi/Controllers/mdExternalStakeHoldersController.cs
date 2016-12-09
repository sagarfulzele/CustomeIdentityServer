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
    builder.EntitySet<mdExternalStakeHolder>("mdExternalStakeHolders");
    builder.EntitySet<mdProject>("mdProjects"); 
    builder.EntitySet<msStakeHolderContact>("msStakeHolderContacts"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdExternalStakeHoldersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdExternalStakeHolders
        [EnableQuery]
        public IQueryable<mdExternalStakeHolder> GetmdExternalStakeHolders()
        {
            return db.mdExternalStakeHolders;
        }

        // GET: odata/mdExternalStakeHolders(5)
        [EnableQuery]
        public SingleResult<mdExternalStakeHolder> GetmdExternalStakeHolder([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdExternalStakeHolders.Where(mdExternalStakeHolder => mdExternalStakeHolder.ID == key));
        }

        // PUT: odata/mdExternalStakeHolders(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdExternalStakeHolder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdExternalStakeHolder mdExternalStakeHolder = await db.mdExternalStakeHolders.FindAsync(key);
            if (mdExternalStakeHolder == null)
            {
                return NotFound();
            }

            patch.Put(mdExternalStakeHolder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdExternalStakeHolderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdExternalStakeHolder);
        }

        // POST: odata/mdExternalStakeHolders
        public async Task<IHttpActionResult> Post(mdExternalStakeHolder mdExternalStakeHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdExternalStakeHolders.Add(mdExternalStakeHolder);
            await db.SaveChangesAsync();

            return Created(mdExternalStakeHolder);
        }

        // PATCH: odata/mdExternalStakeHolders(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdExternalStakeHolder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdExternalStakeHolder mdExternalStakeHolder = await db.mdExternalStakeHolders.FindAsync(key);
            if (mdExternalStakeHolder == null)
            {
                return NotFound();
            }

            patch.Patch(mdExternalStakeHolder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdExternalStakeHolderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdExternalStakeHolder);
        }

        // DELETE: odata/mdExternalStakeHolders(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdExternalStakeHolder mdExternalStakeHolder = await db.mdExternalStakeHolders.FindAsync(key);
            if (mdExternalStakeHolder == null)
            {
                return NotFound();
            }

            db.mdExternalStakeHolders.Remove(mdExternalStakeHolder);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdExternalStakeHolders(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdExternalStakeHolders.Where(m => m.ID == key).Select(m => m.mdProject));
        }

        // GET: odata/mdExternalStakeHolders(5)/msStakeHolderContact
        [EnableQuery]
        public SingleResult<msStakeHolderContact> GetmsStakeHolderContact([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdExternalStakeHolders.Where(m => m.ID == key).Select(m => m.msStakeHolderContact));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdExternalStakeHolderExists(int key)
        {
            return db.mdExternalStakeHolders.Count(e => e.ID == key) > 0;
        }


         

        [HttpPost]
        public async Task<IHttpActionResult> SavemdExternalStakeHolders([FromODataUri] int key, mdExternalStakeHolder parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            parameters.ProjectID = key;
            if (mdExternalStakeHolderExists(parameters.ID))
            {
                Delta<mdExternalStakeHolder> obj = new Delta<mdExternalStakeHolder>();
                obj.Put(parameters);
                return await Put(parameters.ID, obj);
            }
            else
            {
                return await Post(parameters);
            }

        }

    }
}

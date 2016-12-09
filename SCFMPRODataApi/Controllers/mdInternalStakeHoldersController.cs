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
using System.Web.Http.Routing;
using System.Web.Http.OData.Extensions;
using Microsoft.Data.OData.Query;
using Microsoft.Data.OData;
namespace PsiMprODataApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using PsiMprODataApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<mdInternalStakeHolder>("mdInternalStakeHolders");
    builder.EntitySet<AspNetUser>("AspNetUsers"); 
    builder.EntitySet<mdProject>("mdProjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdInternalStakeHoldersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdInternalStakeHolders
        [EnableQuery]
        public IQueryable<mdInternalStakeHolder> GetmdInternalStakeHolders()
        {
            return db.mdInternalStakeHolders;
        }

        // GET: odata/mdInternalStakeHolders(5)
        [EnableQuery]
        public SingleResult<mdInternalStakeHolder> GetmdInternalStakeHolder([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdInternalStakeHolders.Where(mdInternalStakeHolder => mdInternalStakeHolder.ID == key));
        }

        // PUT: odata/mdInternalStakeHolders(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdInternalStakeHolder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdInternalStakeHolder mdInternalStakeHolder = await db.mdInternalStakeHolders.FindAsync(key);
            if (mdInternalStakeHolder == null)
            {
                return NotFound();
            }

            patch.Put(mdInternalStakeHolder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdInternalStakeHolderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdInternalStakeHolder);
        }

        // POST: odata/mdInternalStakeHolders
        public async Task<IHttpActionResult> Post(mdInternalStakeHolder mdInternalStakeHolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdInternalStakeHolders.Add(mdInternalStakeHolder);
            await db.SaveChangesAsync();

            return Created(mdInternalStakeHolder);
        }

        // PATCH: odata/mdInternalStakeHolders(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdInternalStakeHolder> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdInternalStakeHolder mdInternalStakeHolder = await db.mdInternalStakeHolders.FindAsync(key);
            if (mdInternalStakeHolder == null)
            {
                return NotFound();
            }

            patch.Patch(mdInternalStakeHolder);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdInternalStakeHolderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdInternalStakeHolder);
        }

        // DELETE: odata/mdInternalStakeHolders(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdInternalStakeHolder mdInternalStakeHolder = await db.mdInternalStakeHolders.FindAsync(key);
            if (mdInternalStakeHolder == null)
            {
                return NotFound();
            }

            db.mdInternalStakeHolders.Remove(mdInternalStakeHolder);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdInternalStakeHolders(5)/AspNetUser
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdInternalStakeHolders.Where(m => m.ID == key).Select(m => m.AspNetUser));
        }

        // GET: odata/mdInternalStakeHolders(5)/mdProject
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdInternalStakeHolders.Where(m => m.ID == key).Select(m => m.mdProject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdInternalStakeHolderExists(int key)
        {
            return db.mdInternalStakeHolders.Count(e => e.ID == key) > 0;
        }



        [HttpPost]
        public async Task<IHttpActionResult> SavemdInternalStakeHolders([FromODataUri] int key, mdInternalStakeHolder parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            parameters.ProjectID = key;
            if (mdInternalStakeHolderExists(parameters.ID))
            {
                Delta<mdInternalStakeHolder> obj = new Delta<mdInternalStakeHolder>();
                obj.Put(parameters);
               return await Put(parameters.ID, obj);
            }
            else
            {
                return await Post(parameters);
            } 
           
        }



        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> CreateLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdInternalStakeHolder stakeholder = await db.mdInternalStakeHolders.FindAsync(key);
            if (stakeholder == null)
            {
                return NotFound();
            }
            string id = MprOdataHelprer.GetKeyFromLinkUri<string>(link, Request);
            switch (navigationProperty)
            {
                case "AspNetUser":

                    AspNetUser user = await db.AspNetUsers.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    stakeholder.AspNetUser = user;
                    await db.SaveChangesAsync();
                    return StatusCode(HttpStatusCode.NoContent);
                
                default:
                    return NotFound();
            }

            return NotFound();
        }


        

        
    }
}

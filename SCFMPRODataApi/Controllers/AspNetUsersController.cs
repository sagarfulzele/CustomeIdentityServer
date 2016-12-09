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
    builder.EntitySet<AspNetUser>("AspNetUsers");
    builder.EntitySet<AspNetUserClaim>("AspNetUserClaims"); 
    builder.EntitySet<AspNetUserLogin>("AspNetUserLogins"); 
    builder.EntitySet<mdInternalStakeHolder>("mdInternalStakeHolders"); 
    builder.EntitySet<mdProject>("mdProjects"); 
    builder.EntitySet<AspNetRole>("AspNetRoles"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AspNetUsersController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/AspNetUsers
        [EnableQuery]
        public IQueryable<AspNetUser> GetAspNetUsers()
        {
            return db.AspNetUsers;
        }

        // GET: odata/AspNetUsers(5)
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] string key)
        {
            return SingleResult.Create(db.AspNetUsers.Where(aspNetUser => aspNetUser.Id == key));
        }

        // PUT: odata/AspNetUsers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<AspNetUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(key);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            patch.Put(aspNetUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetUser);
        }

        // POST: odata/AspNetUsers
        public async Task<IHttpActionResult> Post(AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AspNetUsers.Add(aspNetUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserExists(aspNetUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(aspNetUser);
        }

        // PATCH: odata/AspNetUsers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<AspNetUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(key);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            patch.Patch(aspNetUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetUser);
        }

        // DELETE: odata/AspNetUsers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            AspNetUser aspNetUser = await db.AspNetUsers.FindAsync(key);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            db.AspNetUsers.Remove(aspNetUser);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AspNetUsers(5)/AspNetUserClaims
        [EnableQuery]
        public IQueryable<AspNetUserClaim> GetAspNetUserClaims([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.AspNetUserClaims);
        }

        // GET: odata/AspNetUsers(5)/AspNetUserLogins
        [EnableQuery]
        public IQueryable<AspNetUserLogin> GetAspNetUserLogins([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.AspNetUserLogins);
        }

        // GET: odata/AspNetUsers(5)/mdInternalStakeHolders
        [EnableQuery]
        public IQueryable<mdInternalStakeHolder> GetmdInternalStakeHolders([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdInternalStakeHolders);
        }

        // GET: odata/AspNetUsers(5)/mdProjects
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects);
        }

        // GET: odata/AspNetUsers(5)/mdProjects1
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects1([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects1);
        }

        // GET: odata/AspNetUsers(5)/mdProjects2
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects2([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects2);
        }

        // GET: odata/AspNetUsers(5)/mdProjects3
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects3([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects3);
        }

        // GET: odata/AspNetUsers(5)/mdProjects4
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects4([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects4);
        }

        // GET: odata/AspNetUsers(5)/mdProjects5
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects5([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects5);
        }

        // GET: odata/AspNetUsers(5)/mdProjects6
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects6([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects6);
        }

        // GET: odata/AspNetUsers(5)/mdProjects7
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects7([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects7);
        }

        // GET: odata/AspNetUsers(5)/mdProjects8
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects8([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects8);
        }

        // GET: odata/AspNetUsers(5)/mdProjects9
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects9([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects9);
        }

        // GET: odata/AspNetUsers(5)/mdProjects10
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects10([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects10);
        }

        // GET: odata/AspNetUsers(5)/mdProjects11
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects11([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.mdProjects11);
        }

        // GET: odata/AspNetUsers(5)/AspNetRoles
        [EnableQuery]
        public IQueryable<AspNetRole> GetAspNetRoles([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.AspNetRoles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetUserExists(string key)
        {
            return db.AspNetUsers.Count(e => e.Id == key) > 0;
        }
    }
}

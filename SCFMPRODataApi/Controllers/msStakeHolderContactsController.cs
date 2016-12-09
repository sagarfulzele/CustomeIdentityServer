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
    builder.EntitySet<msStakeHolderContact>("msStakeHolderContacts");
    builder.EntitySet<mdExternalStakeHolder>("mdExternalStakeHolders"); 
    builder.EntitySet<msCompany>("msCompanies"); 
    builder.EntitySet<mdProjectStakeHolder>("mdProjectStakeHolders"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msStakeHolderContactsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msStakeHolderContacts
        [EnableQuery]
        public IQueryable<msStakeHolderContact> GetmsStakeHolderContacts()
        {
            return db.msStakeHolderContacts;
        }

        // GET: odata/msStakeHolderContacts(5)
        [EnableQuery]
        public SingleResult<msStakeHolderContact> GetmsStakeHolderContact([FromODataUri] int key)
        {
            return SingleResult.Create(db.msStakeHolderContacts.Where(msStakeHolderContact => msStakeHolderContact.Id == key));
        }

        // PUT: odata/msStakeHolderContacts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msStakeHolderContact> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msStakeHolderContact msStakeHolderContact = await db.msStakeHolderContacts.FindAsync(key);
            if (msStakeHolderContact == null)
            {
                return NotFound();
            }

            patch.Put(msStakeHolderContact);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msStakeHolderContactExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msStakeHolderContact);
        }

        // POST: odata/msStakeHolderContacts
        public async Task<IHttpActionResult> Post(msStakeHolderContact msStakeHolderContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msStakeHolderContacts.Add(msStakeHolderContact);
            await db.SaveChangesAsync();

            return Created(msStakeHolderContact);
        }

        // PATCH: odata/msStakeHolderContacts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msStakeHolderContact> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msStakeHolderContact msStakeHolderContact = await db.msStakeHolderContacts.FindAsync(key);
            if (msStakeHolderContact == null)
            {
                return NotFound();
            }

            patch.Patch(msStakeHolderContact);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msStakeHolderContactExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msStakeHolderContact);
        }

        // DELETE: odata/msStakeHolderContacts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msStakeHolderContact msStakeHolderContact = await db.msStakeHolderContacts.FindAsync(key);
            if (msStakeHolderContact == null)
            {
                return NotFound();
            }

            db.msStakeHolderContacts.Remove(msStakeHolderContact);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msStakeHolderContacts(5)/mdExternalStakeHolders
        [EnableQuery]
        public IQueryable<mdExternalStakeHolder> GetmdExternalStakeHolders([FromODataUri] int key)
        {
            return db.msStakeHolderContacts.Where(m => m.Id == key).SelectMany(m => m.mdExternalStakeHolders);
        }

        // GET: odata/msStakeHolderContacts(5)/msCompany
        [EnableQuery]
        public SingleResult<msCompany> GetmsCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.msStakeHolderContacts.Where(m => m.Id == key).Select(m => m.msCompany));
        }

        // GET: odata/msStakeHolderContacts(5)/mdProjectStakeHolders
        [EnableQuery]
        public IQueryable<mdProjectStakeHolder> GetmdProjectStakeHolders([FromODataUri] int key)
        {
            return db.msStakeHolderContacts.Where(m => m.Id == key).SelectMany(m => m.mdProjectStakeHolders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msStakeHolderContactExists(int key)
        {
            return db.msStakeHolderContacts.Count(e => e.Id == key) > 0;
        }
    }
}

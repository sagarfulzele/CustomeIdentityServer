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
    builder.EntitySet<msVtWindingFunciton>("msVtWindingFuncitons");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msVtWindingFuncitonsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msVtWindingFuncitons
        [EnableQuery]
        public IQueryable<msVtWindingFunciton> GetmsVtWindingFuncitons()
        {
            return db.msVtWindingFuncitons;
        }

        // GET: odata/msVtWindingFuncitons(5)
        [EnableQuery]
        public SingleResult<msVtWindingFunciton> GetmsVtWindingFunciton([FromODataUri] int key)
        {
            return SingleResult.Create(db.msVtWindingFuncitons.Where(msVtWindingFunciton => msVtWindingFunciton.ID == key));
        }

        // PUT: odata/msVtWindingFuncitons(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msVtWindingFunciton> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msVtWindingFunciton msVtWindingFunciton = await db.msVtWindingFuncitons.FindAsync(key);
            if (msVtWindingFunciton == null)
            {
                return NotFound();
            }

            patch.Put(msVtWindingFunciton);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msVtWindingFuncitonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msVtWindingFunciton);
        }

        // POST: odata/msVtWindingFuncitons
        public async Task<IHttpActionResult> Post(msVtWindingFunciton msVtWindingFunciton)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msVtWindingFuncitons.Add(msVtWindingFunciton);
            await db.SaveChangesAsync();

            return Created(msVtWindingFunciton);
        }

        // PATCH: odata/msVtWindingFuncitons(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msVtWindingFunciton> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msVtWindingFunciton msVtWindingFunciton = await db.msVtWindingFuncitons.FindAsync(key);
            if (msVtWindingFunciton == null)
            {
                return NotFound();
            }

            patch.Patch(msVtWindingFunciton);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msVtWindingFuncitonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msVtWindingFunciton);
        }

        // DELETE: odata/msVtWindingFuncitons(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msVtWindingFunciton msVtWindingFunciton = await db.msVtWindingFuncitons.FindAsync(key);
            if (msVtWindingFunciton == null)
            {
                return NotFound();
            }

            db.msVtWindingFuncitons.Remove(msVtWindingFunciton);
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

        private bool msVtWindingFuncitonExists(int key)
        {
            return db.msVtWindingFuncitons.Count(e => e.ID == key) > 0;
        }
    }
}

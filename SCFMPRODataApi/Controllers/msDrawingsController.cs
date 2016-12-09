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
    builder.EntitySet<msDrawing>("msDrawings");
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msDrawingsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msDrawings
        [EnableQuery]
        public IQueryable<msDrawing> GetmsDrawings()
        {
            return db.msDrawings;
        }

        // GET: odata/msDrawings(5)
        [EnableQuery]
        public SingleResult<msDrawing> GetmsDrawing([FromODataUri] int key)
        {
            return SingleResult.Create(db.msDrawings.Where(msDrawing => msDrawing.ID == key));
        }

        // PUT: odata/msDrawings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msDrawing> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msDrawing msDrawing = await db.msDrawings.FindAsync(key);
            if (msDrawing == null)
            {
                return NotFound();
            }

            patch.Put(msDrawing);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msDrawingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msDrawing);
        }

        // POST: odata/msDrawings
        public async Task<IHttpActionResult> Post(msDrawing msDrawing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msDrawings.Add(msDrawing);
            await db.SaveChangesAsync();

            return Created(msDrawing);
        }

        // PATCH: odata/msDrawings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msDrawing> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msDrawing msDrawing = await db.msDrawings.FindAsync(key);
            if (msDrawing == null)
            {
                return NotFound();
            }

            patch.Patch(msDrawing);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msDrawingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msDrawing);
        }

        // DELETE: odata/msDrawings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msDrawing msDrawing = await db.msDrawings.FindAsync(key);
            if (msDrawing == null)
            {
                return NotFound();
            }

            db.msDrawings.Remove(msDrawing);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msDrawings(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.msDrawings.Where(m => m.ID == key).Select(m => m.msSubstationStructure));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msDrawingExists(int key)
        {
            return db.msDrawings.Count(e => e.ID == key) > 0;
        }
    }
}

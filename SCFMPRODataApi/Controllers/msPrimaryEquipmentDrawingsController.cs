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
    builder.EntitySet<msPrimaryEquipmentDrawing>("msPrimaryEquipmentDrawings");
    builder.EntitySet<msPrimaryEquipment>("msPrimaryEquipments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msPrimaryEquipmentDrawingsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msPrimaryEquipmentDrawings
        [EnableQuery]
        public IQueryable<msPrimaryEquipmentDrawing> GetmsPrimaryEquipmentDrawings()
        {
            return db.msPrimaryEquipmentDrawings;
        }

        // GET: odata/msPrimaryEquipmentDrawings(5)
        [EnableQuery]
        public SingleResult<msPrimaryEquipmentDrawing> GetmsPrimaryEquipmentDrawing([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPrimaryEquipmentDrawings.Where(msPrimaryEquipmentDrawing => msPrimaryEquipmentDrawing.ID == key));
        }

        // PUT: odata/msPrimaryEquipmentDrawings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msPrimaryEquipmentDrawing> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPrimaryEquipmentDrawing msPrimaryEquipmentDrawing = await db.msPrimaryEquipmentDrawings.FindAsync(key);
            if (msPrimaryEquipmentDrawing == null)
            {
                return NotFound();
            }

            patch.Put(msPrimaryEquipmentDrawing);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPrimaryEquipmentDrawingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPrimaryEquipmentDrawing);
        }

        // POST: odata/msPrimaryEquipmentDrawings
        public async Task<IHttpActionResult> Post(msPrimaryEquipmentDrawing msPrimaryEquipmentDrawing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msPrimaryEquipmentDrawings.Add(msPrimaryEquipmentDrawing);
            await db.SaveChangesAsync();

            return Created(msPrimaryEquipmentDrawing);
        }

        // PATCH: odata/msPrimaryEquipmentDrawings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msPrimaryEquipmentDrawing> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPrimaryEquipmentDrawing msPrimaryEquipmentDrawing = await db.msPrimaryEquipmentDrawings.FindAsync(key);
            if (msPrimaryEquipmentDrawing == null)
            {
                return NotFound();
            }

            patch.Patch(msPrimaryEquipmentDrawing);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPrimaryEquipmentDrawingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPrimaryEquipmentDrawing);
        }

        // DELETE: odata/msPrimaryEquipmentDrawings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msPrimaryEquipmentDrawing msPrimaryEquipmentDrawing = await db.msPrimaryEquipmentDrawings.FindAsync(key);
            if (msPrimaryEquipmentDrawing == null)
            {
                return NotFound();
            }

            db.msPrimaryEquipmentDrawings.Remove(msPrimaryEquipmentDrawing);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msPrimaryEquipmentDrawings(5)/msPrimaryEquipment
        [EnableQuery]
        public SingleResult<msPrimaryEquipment> GetmsPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPrimaryEquipmentDrawings.Where(m => m.ID == key).Select(m => m.msPrimaryEquipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msPrimaryEquipmentDrawingExists(int key)
        {
            return db.msPrimaryEquipmentDrawings.Count(e => e.ID == key) > 0;
        }
    }
}

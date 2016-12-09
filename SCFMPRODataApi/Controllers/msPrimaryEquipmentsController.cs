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
    builder.EntitySet<msPrimaryEquipment>("msPrimaryEquipments");
    builder.EntitySet<msPrimaryEquipmentDrawing>("msPrimaryEquipmentDrawings"); 
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msPrimaryEquipmentsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msPrimaryEquipments
        [EnableQuery]
        public IQueryable<msPrimaryEquipment> GetmsPrimaryEquipments()
        {
            return db.msPrimaryEquipments;
        }

        // GET: odata/msPrimaryEquipments(5)
        [EnableQuery]
        public SingleResult<msPrimaryEquipment> GetmsPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPrimaryEquipments.Where(msPrimaryEquipment => msPrimaryEquipment.ID == key));
        }

        // PUT: odata/msPrimaryEquipments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msPrimaryEquipment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPrimaryEquipment msPrimaryEquipment = await db.msPrimaryEquipments.FindAsync(key);
            if (msPrimaryEquipment == null)
            {
                return NotFound();
            }

            patch.Put(msPrimaryEquipment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPrimaryEquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPrimaryEquipment);
        }

        // POST: odata/msPrimaryEquipments
        public async Task<IHttpActionResult> Post(msPrimaryEquipment msPrimaryEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msPrimaryEquipments.Add(msPrimaryEquipment);
            await db.SaveChangesAsync();

            return Created(msPrimaryEquipment);
        }

        // PATCH: odata/msPrimaryEquipments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msPrimaryEquipment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPrimaryEquipment msPrimaryEquipment = await db.msPrimaryEquipments.FindAsync(key);
            if (msPrimaryEquipment == null)
            {
                return NotFound();
            }

            patch.Patch(msPrimaryEquipment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPrimaryEquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPrimaryEquipment);
        }

        // DELETE: odata/msPrimaryEquipments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msPrimaryEquipment msPrimaryEquipment = await db.msPrimaryEquipments.FindAsync(key);
            if (msPrimaryEquipment == null)
            {
                return NotFound();
            }

            db.msPrimaryEquipments.Remove(msPrimaryEquipment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msPrimaryEquipments(5)/msPrimaryEquipmentDrawings
        [EnableQuery]
        public IQueryable<msPrimaryEquipmentDrawing> GetmsPrimaryEquipmentDrawings([FromODataUri] int key)
        {
            return db.msPrimaryEquipments.Where(m => m.ID == key).SelectMany(m => m.msPrimaryEquipmentDrawings);
        }

        // GET: odata/msPrimaryEquipments(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPrimaryEquipments.Where(m => m.ID == key).Select(m => m.msSubstationStructure));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msPrimaryEquipmentExists(int key)
        {
            return db.msPrimaryEquipments.Count(e => e.ID == key) > 0;
        }
    }
}

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
    builder.EntitySet<msTNBApprovedEquipment>("msTNBApprovedEquipments");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msTNBApprovedEquipmentsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msTNBApprovedEquipments
        [EnableQuery]
        public IQueryable<msTNBApprovedEquipment> GetmsTNBApprovedEquipments()
        {
            return db.msTNBApprovedEquipments;
        }

        // GET: odata/msTNBApprovedEquipments(5)
        [EnableQuery]
        public SingleResult<msTNBApprovedEquipment> GetmsTNBApprovedEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.msTNBApprovedEquipments.Where(msTNBApprovedEquipment => msTNBApprovedEquipment.ID == key));
        }

        // PUT: odata/msTNBApprovedEquipments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msTNBApprovedEquipment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msTNBApprovedEquipment msTNBApprovedEquipment = await db.msTNBApprovedEquipments.FindAsync(key);
            if (msTNBApprovedEquipment == null)
            {
                return NotFound();
            }

            patch.Put(msTNBApprovedEquipment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msTNBApprovedEquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msTNBApprovedEquipment);
        }

        // POST: odata/msTNBApprovedEquipments
        public async Task<IHttpActionResult> Post(msTNBApprovedEquipment msTNBApprovedEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msTNBApprovedEquipments.Add(msTNBApprovedEquipment);
            await db.SaveChangesAsync();

            return Created(msTNBApprovedEquipment);
        }

        // PATCH: odata/msTNBApprovedEquipments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msTNBApprovedEquipment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msTNBApprovedEquipment msTNBApprovedEquipment = await db.msTNBApprovedEquipments.FindAsync(key);
            if (msTNBApprovedEquipment == null)
            {
                return NotFound();
            }

            patch.Patch(msTNBApprovedEquipment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msTNBApprovedEquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msTNBApprovedEquipment);
        }

        // DELETE: odata/msTNBApprovedEquipments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msTNBApprovedEquipment msTNBApprovedEquipment = await db.msTNBApprovedEquipments.FindAsync(key);
            if (msTNBApprovedEquipment == null)
            {
                return NotFound();
            }

            db.msTNBApprovedEquipments.Remove(msTNBApprovedEquipment);
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

        private bool msTNBApprovedEquipmentExists(int key)
        {
            return db.msTNBApprovedEquipments.Count(e => e.ID == key) > 0;
        }
    }
}

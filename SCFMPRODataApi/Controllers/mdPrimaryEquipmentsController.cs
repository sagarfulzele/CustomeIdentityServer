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
    builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments");
    builder.EntitySet<mdBay>("mdBays"); 
    builder.EntitySet<mdCircuitBreaker>("mdCircuitBreakers"); 
    builder.EntitySet<mdCurrentTransformer>("mdCurrentTransformers"); 
    builder.EntitySet<mdEarthSwitch>("mdEarthSwitches"); 
    builder.EntitySet<mdIsolator>("mdIsolators"); 
    builder.EntitySet<mdPowerTransformer>("mdPowerTransformers"); 
    builder.EntitySet<mdVoltageTransformer>("mdVoltageTransformers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdPrimaryEquipmentsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdPrimaryEquipments
        [EnableQuery]
        public IQueryable<mdPrimaryEquipment> GetmdPrimaryEquipments()
        {
            return db.mdPrimaryEquipments;
        }

        // GET: odata/mdPrimaryEquipments(5)
        [EnableQuery]
        public SingleResult<mdPrimaryEquipment> GetmdPrimaryEquipment([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(mdPrimaryEquipment => mdPrimaryEquipment.PrimaryEquipmentId == key));
        }

        // PUT: odata/mdPrimaryEquipments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdPrimaryEquipment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPrimaryEquipment mdPrimaryEquipment = await db.mdPrimaryEquipments.FindAsync(key);
            if (mdPrimaryEquipment == null)
            {
                return NotFound();
            }

            patch.Put(mdPrimaryEquipment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPrimaryEquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPrimaryEquipment);
        }

        // POST: odata/mdPrimaryEquipments
        public async Task<IHttpActionResult> Post(mdPrimaryEquipment mdPrimaryEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdPrimaryEquipments.Add(mdPrimaryEquipment);
            await db.SaveChangesAsync();

            return Created(mdPrimaryEquipment);
        }

        // PATCH: odata/mdPrimaryEquipments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdPrimaryEquipment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdPrimaryEquipment mdPrimaryEquipment = await db.mdPrimaryEquipments.FindAsync(key);
            if (mdPrimaryEquipment == null)
            {
                return NotFound();
            }

            patch.Patch(mdPrimaryEquipment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdPrimaryEquipmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdPrimaryEquipment);
        }

        // DELETE: odata/mdPrimaryEquipments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdPrimaryEquipment mdPrimaryEquipment = await db.mdPrimaryEquipments.FindAsync(key);
            if (mdPrimaryEquipment == null)
            {
                return NotFound();
            }

            db.mdPrimaryEquipments.Remove(mdPrimaryEquipment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdPrimaryEquipments(5)/mdBay
        [EnableQuery]
        public SingleResult<mdBay> GetmdBay([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdBay));
        }

        // GET: odata/mdPrimaryEquipments(5)/mdCircuitBreaker
        [EnableQuery]
        public SingleResult<mdCircuitBreaker> GetmdCircuitBreaker([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdCircuitBreaker));
        }

        // GET: odata/mdPrimaryEquipments(5)/mdCurrentTransformer
        [EnableQuery]
        public SingleResult<mdCurrentTransformer> GetmdCurrentTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdCurrentTransformer));
        }

        // GET: odata/mdPrimaryEquipments(5)/mdEarthSwitch
        [EnableQuery]
        public SingleResult<mdEarthSwitch> GetmdEarthSwitch([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdEarthSwitch));
        }

        // GET: odata/mdPrimaryEquipments(5)/mdIsolator
        [EnableQuery]
        public SingleResult<mdIsolator> GetmdIsolator([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdIsolator));
        }

        // GET: odata/mdPrimaryEquipments(5)/mdPowerTransformer
        [EnableQuery]
        public SingleResult<mdPowerTransformer> GetmdPowerTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdPowerTransformer));
        }

        // GET: odata/mdPrimaryEquipments(5)/mdVoltageTransformer
        [EnableQuery]
        public SingleResult<mdVoltageTransformer> GetmdVoltageTransformer([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdPrimaryEquipments.Where(m => m.PrimaryEquipmentId == key).Select(m => m.mdVoltageTransformer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdPrimaryEquipmentExists(int key)
        {
            return db.mdPrimaryEquipments.Count(e => e.PrimaryEquipmentId == key) > 0;
        }
    }
}

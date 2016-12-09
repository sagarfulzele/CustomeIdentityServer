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
    builder.EntitySet<msSubstationStructure>("msSubstationStructures");
    builder.EntitySet<mdBay>("mdBays"); 
    builder.EntitySet<mdPanel>("mdPanels"); 
    builder.EntitySet<mdProject>("mdProjects"); 
    builder.EntitySet<mdVoltageLevel>("mdVoltageLevels"); 
    builder.EntitySet<msBayRelayFunction>("msBayRelayFunctions"); 
    builder.EntitySet<msDrawing>("msDrawings"); 
    builder.EntitySet<msGenSchemeComponent>("msGenSchemeComponents"); 
    builder.EntitySet<msMimic>("msMimics"); 
    builder.EntitySet<msPrimaryEquipment>("msPrimaryEquipments"); 
    builder.EntitySet<msRequiredQuantity>("msRequiredQuantities"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class msSubstationStructuresController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msSubstationStructures
        [EnableQuery]
        public IQueryable<msSubstationStructure> GetmsSubstationStructures()
        {
            return db.msSubstationStructures;
        }

        // GET: odata/msSubstationStructures(5)
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.msSubstationStructures.Where(msSubstationStructure => msSubstationStructure.ID == key));
        }

        // PUT: odata/msSubstationStructures(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msSubstationStructure> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msSubstationStructure msSubstationStructure = await db.msSubstationStructures.FindAsync(key);
            if (msSubstationStructure == null)
            {
                return NotFound();
            }

            patch.Put(msSubstationStructure);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msSubstationStructureExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msSubstationStructure);
        }

        // POST: odata/msSubstationStructures
        public async Task<IHttpActionResult> Post(msSubstationStructure msSubstationStructure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msSubstationStructures.Add(msSubstationStructure);
            await db.SaveChangesAsync();

            return Created(msSubstationStructure);
        }

        // PATCH: odata/msSubstationStructures(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msSubstationStructure> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msSubstationStructure msSubstationStructure = await db.msSubstationStructures.FindAsync(key);
            if (msSubstationStructure == null)
            {
                return NotFound();
            }

            patch.Patch(msSubstationStructure);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msSubstationStructureExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msSubstationStructure);
        }

        // DELETE: odata/msSubstationStructures(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msSubstationStructure msSubstationStructure = await db.msSubstationStructures.FindAsync(key);
            if (msSubstationStructure == null)
            {
                return NotFound();
            }

            db.msSubstationStructures.Remove(msSubstationStructure);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/msSubstationStructures(5)/mdBays
        [EnableQuery]
        public IQueryable<mdBay> GetmdBays([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.mdBays);
        }

        // GET: odata/msSubstationStructures(5)/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.mdPanels);
        }

        // GET: odata/msSubstationStructures(5)/mdProjects
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.mdProjects);
        }

        // GET: odata/msSubstationStructures(5)/mdVoltageLevels
        [EnableQuery]
        public IQueryable<mdVoltageLevel> GetmdVoltageLevels([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.mdVoltageLevels);
        }

        // GET: odata/msSubstationStructures(5)/mdVoltageLevels1
        [EnableQuery]
        public IQueryable<mdVoltageLevel> GetmdVoltageLevels1([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.mdVoltageLevels1);
        }

        // GET: odata/msSubstationStructures(5)/mdVoltageLevels2
        [EnableQuery]
        public IQueryable<mdVoltageLevel> GetmdVoltageLevels2([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.mdVoltageLevels2);
        }

        // GET: odata/msSubstationStructures(5)/mdVoltageLevels3
        [EnableQuery]
        public IQueryable<mdVoltageLevel> GetmdVoltageLevels3([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.mdVoltageLevels3);
        }

        // GET: odata/msSubstationStructures(5)/msBayRelayFunctions
        [EnableQuery]
        public IQueryable<msBayRelayFunction> GetmsBayRelayFunctions([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.msBayRelayFunctions);
        }

        // GET: odata/msSubstationStructures(5)/msDrawings
        [EnableQuery]
        public IQueryable<msDrawing> GetmsDrawings([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.msDrawings);
        }

        // GET: odata/msSubstationStructures(5)/msGenSchemeComponents
        [EnableQuery]
        public IQueryable<msGenSchemeComponent> GetmsGenSchemeComponents([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.msGenSchemeComponents);
        }

        // GET: odata/msSubstationStructures(5)/msMimics
        [EnableQuery]
        public IQueryable<msMimic> GetmsMimics([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.msMimics);
        }

        // GET: odata/msSubstationStructures(5)/msPrimaryEquipments
        [EnableQuery]
        public IQueryable<msPrimaryEquipment> GetmsPrimaryEquipments([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.msPrimaryEquipments);
        }

        // GET: odata/msSubstationStructures(5)/msRequiredQuantities
        [EnableQuery]
        public IQueryable<msRequiredQuantity> GetmsRequiredQuantities([FromODataUri] int key)
        {
            return db.msSubstationStructures.Where(m => m.ID == key).SelectMany(m => m.msRequiredQuantities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msSubstationStructureExists(int key)
        {
            return db.msSubstationStructures.Count(e => e.ID == key) > 0;
        }
    }
}

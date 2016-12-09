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
    builder.EntitySet<mdArrangementsIndicationRow>("mdArrangementsIndicationRows");
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdArrangementsIndicationRowsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdArrangementsIndicationRows
        [EnableQuery]
        public IQueryable<mdArrangementsIndicationRow> GetmdArrangementsIndicationRows()
        {
            return db.mdArrangementsIndicationRows;
        }

        // GET: odata/mdArrangementsIndicationRows(5)
        [EnableQuery]
        public SingleResult<mdArrangementsIndicationRow> GetmdArrangementsIndicationRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementsIndicationRows.Where(mdArrangementsIndicationRow => mdArrangementsIndicationRow.RowID == key));
        }

        // PUT: odata/mdArrangementsIndicationRows(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdArrangementsIndicationRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementsIndicationRow mdArrangementsIndicationRow = await db.mdArrangementsIndicationRows.FindAsync(key);
            if (mdArrangementsIndicationRow == null)
            {
                return NotFound();
            }

            patch.Put(mdArrangementsIndicationRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementsIndicationRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementsIndicationRow);
        }

        // POST: odata/mdArrangementsIndicationRows
        public async Task<IHttpActionResult> Post(mdArrangementsIndicationRow mdArrangementsIndicationRow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdArrangementsIndicationRows.Add(mdArrangementsIndicationRow);
            await db.SaveChangesAsync();

            return Created(mdArrangementsIndicationRow);
        }

        // PATCH: odata/mdArrangementsIndicationRows(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdArrangementsIndicationRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementsIndicationRow mdArrangementsIndicationRow = await db.mdArrangementsIndicationRows.FindAsync(key);
            if (mdArrangementsIndicationRow == null)
            {
                return NotFound();
            }

            patch.Patch(mdArrangementsIndicationRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementsIndicationRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementsIndicationRow);
        }

        // DELETE: odata/mdArrangementsIndicationRows(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdArrangementsIndicationRow mdArrangementsIndicationRow = await db.mdArrangementsIndicationRows.FindAsync(key);
            if (mdArrangementsIndicationRow == null)
            {
                return NotFound();
            }

            db.mdArrangementsIndicationRows.Remove(mdArrangementsIndicationRow);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdArrangementsIndicationRows(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementsIndicationRows.Where(m => m.RowID == key).Select(m => m.mdSubstation));
        }

        // GET: odata/mdArrangementsIndicationRows(5)/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels([FromODataUri] int key)
        {
            return db.mdArrangementsIndicationRows.Where(m => m.RowID == key).SelectMany(m => m.mdPanels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdArrangementsIndicationRowExists(int key)
        {
            return db.mdArrangementsIndicationRows.Count(e => e.RowID == key) > 0;
        }
    }
}

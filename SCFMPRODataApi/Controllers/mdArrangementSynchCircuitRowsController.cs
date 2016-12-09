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
    builder.EntitySet<mdArrangementSynchCircuitRow>("mdArrangementSynchCircuitRows");
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdArrangementSynchCircuitRowsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdArrangementSynchCircuitRows
        [EnableQuery]
        public IQueryable<mdArrangementSynchCircuitRow> GetmdArrangementSynchCircuitRows()
        {
            return db.mdArrangementSynchCircuitRows;
        }

        // GET: odata/mdArrangementSynchCircuitRows(5)
        [EnableQuery]
        public SingleResult<mdArrangementSynchCircuitRow> GetmdArrangementSynchCircuitRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementSynchCircuitRows.Where(mdArrangementSynchCircuitRow => mdArrangementSynchCircuitRow.RowID == key));
        }

        // PUT: odata/mdArrangementSynchCircuitRows(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdArrangementSynchCircuitRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementSynchCircuitRow mdArrangementSynchCircuitRow = await db.mdArrangementSynchCircuitRows.FindAsync(key);
            if (mdArrangementSynchCircuitRow == null)
            {
                return NotFound();
            }

            patch.Put(mdArrangementSynchCircuitRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementSynchCircuitRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementSynchCircuitRow);
        }

        // POST: odata/mdArrangementSynchCircuitRows
        public async Task<IHttpActionResult> Post(mdArrangementSynchCircuitRow mdArrangementSynchCircuitRow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdArrangementSynchCircuitRows.Add(mdArrangementSynchCircuitRow);
            await db.SaveChangesAsync();

            return Created(mdArrangementSynchCircuitRow);
        }

        // PATCH: odata/mdArrangementSynchCircuitRows(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdArrangementSynchCircuitRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementSynchCircuitRow mdArrangementSynchCircuitRow = await db.mdArrangementSynchCircuitRows.FindAsync(key);
            if (mdArrangementSynchCircuitRow == null)
            {
                return NotFound();
            }

            patch.Patch(mdArrangementSynchCircuitRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementSynchCircuitRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementSynchCircuitRow);
        }

        // DELETE: odata/mdArrangementSynchCircuitRows(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdArrangementSynchCircuitRow mdArrangementSynchCircuitRow = await db.mdArrangementSynchCircuitRows.FindAsync(key);
            if (mdArrangementSynchCircuitRow == null)
            {
                return NotFound();
            }

            db.mdArrangementSynchCircuitRows.Remove(mdArrangementSynchCircuitRow);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdArrangementSynchCircuitRows(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementSynchCircuitRows.Where(m => m.RowID == key).Select(m => m.mdSubstation));
        }

        // GET: odata/mdArrangementSynchCircuitRows(5)/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels([FromODataUri] int key)
        {
            return db.mdArrangementSynchCircuitRows.Where(m => m.RowID == key).SelectMany(m => m.mdPanels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdArrangementSynchCircuitRowExists(int key)
        {
            return db.mdArrangementSynchCircuitRows.Count(e => e.RowID == key) > 0;
        }
    }
}

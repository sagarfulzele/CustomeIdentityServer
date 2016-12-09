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
    builder.EntitySet<mdArrangementAnnunciationRow>("mdArrangementAnnunciationRows");
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdArrangementAnnunciationRowsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdArrangementAnnunciationRows
        [EnableQuery]
        public IQueryable<mdArrangementAnnunciationRow> GetmdArrangementAnnunciationRows()
        {
            return db.mdArrangementAnnunciationRows;
        }

        // GET: odata/mdArrangementAnnunciationRows(5)
        [EnableQuery]
        public SingleResult<mdArrangementAnnunciationRow> GetmdArrangementAnnunciationRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementAnnunciationRows.Where(mdArrangementAnnunciationRow => mdArrangementAnnunciationRow.RowID == key));
        }

        // PUT: odata/mdArrangementAnnunciationRows(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdArrangementAnnunciationRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementAnnunciationRow mdArrangementAnnunciationRow = await db.mdArrangementAnnunciationRows.FindAsync(key);
            if (mdArrangementAnnunciationRow == null)
            {
                return NotFound();
            }

            patch.Put(mdArrangementAnnunciationRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementAnnunciationRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementAnnunciationRow);
        }

        // POST: odata/mdArrangementAnnunciationRows
        public async Task<IHttpActionResult> Post(mdArrangementAnnunciationRow mdArrangementAnnunciationRow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdArrangementAnnunciationRows.Add(mdArrangementAnnunciationRow);
            await db.SaveChangesAsync();

            return Created(mdArrangementAnnunciationRow);
        }

        // PATCH: odata/mdArrangementAnnunciationRows(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdArrangementAnnunciationRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementAnnunciationRow mdArrangementAnnunciationRow = await db.mdArrangementAnnunciationRows.FindAsync(key);
            if (mdArrangementAnnunciationRow == null)
            {
                return NotFound();
            }

            patch.Patch(mdArrangementAnnunciationRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementAnnunciationRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementAnnunciationRow);
        }

        // DELETE: odata/mdArrangementAnnunciationRows(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdArrangementAnnunciationRow mdArrangementAnnunciationRow = await db.mdArrangementAnnunciationRows.FindAsync(key);
            if (mdArrangementAnnunciationRow == null)
            {
                return NotFound();
            }

            db.mdArrangementAnnunciationRows.Remove(mdArrangementAnnunciationRow);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdArrangementAnnunciationRows(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementAnnunciationRows.Where(m => m.RowID == key).Select(m => m.mdSubstation));
        }

        // GET: odata/mdArrangementAnnunciationRows(5)/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels([FromODataUri] int key)
        {
            return db.mdArrangementAnnunciationRows.Where(m => m.RowID == key).SelectMany(m => m.mdPanels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdArrangementAnnunciationRowExists(int key)
        {
            return db.mdArrangementAnnunciationRows.Count(e => e.RowID == key) > 0;
        }
    }
}

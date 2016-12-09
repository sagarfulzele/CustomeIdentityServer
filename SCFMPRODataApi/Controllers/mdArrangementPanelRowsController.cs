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
    builder.EntitySet<mdArrangementPanelRow>("mdArrangementPanelRows");
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdArrangementPanelRowsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdArrangementPanelRows
        [EnableQuery]
        public IQueryable<mdArrangementPanelRow> GetmdArrangementPanelRows()
        {
            return db.mdArrangementPanelRows;
        }

        // GET: odata/mdArrangementPanelRows(5)
        [EnableQuery]
        public SingleResult<mdArrangementPanelRow> GetmdArrangementPanelRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementPanelRows.Where(mdArrangementPanelRow => mdArrangementPanelRow.RowID == key));
        }

        // PUT: odata/mdArrangementPanelRows(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdArrangementPanelRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementPanelRow mdArrangementPanelRow = await db.mdArrangementPanelRows.FindAsync(key);
            if (mdArrangementPanelRow == null)
            {
                return NotFound();
            }

            patch.Put(mdArrangementPanelRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementPanelRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementPanelRow);
        }

        // POST: odata/mdArrangementPanelRows
        public async Task<IHttpActionResult> Post(mdArrangementPanelRow mdArrangementPanelRow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdArrangementPanelRows.Add(mdArrangementPanelRow);
            await db.SaveChangesAsync();

            return Created(mdArrangementPanelRow);
        }

        // PATCH: odata/mdArrangementPanelRows(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdArrangementPanelRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementPanelRow mdArrangementPanelRow = await db.mdArrangementPanelRows.FindAsync(key);
            if (mdArrangementPanelRow == null)
            {
                return NotFound();
            }

            patch.Patch(mdArrangementPanelRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementPanelRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementPanelRow);
        }

        // DELETE: odata/mdArrangementPanelRows(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdArrangementPanelRow mdArrangementPanelRow = await db.mdArrangementPanelRows.FindAsync(key);
            if (mdArrangementPanelRow == null)
            {
                return NotFound();
            }

            db.mdArrangementPanelRows.Remove(mdArrangementPanelRow);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdArrangementPanelRows(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementPanelRows.Where(m => m.RowID == key).Select(m => m.mdSubstation));
        }

        // GET: odata/mdArrangementPanelRows(5)/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels([FromODataUri] int key)
        {
            return db.mdArrangementPanelRows.Where(m => m.RowID == key).SelectMany(m => m.mdPanels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdArrangementPanelRowExists(int key)
        {
            return db.mdArrangementPanelRows.Count(e => e.RowID == key) > 0;
        }
    }
}

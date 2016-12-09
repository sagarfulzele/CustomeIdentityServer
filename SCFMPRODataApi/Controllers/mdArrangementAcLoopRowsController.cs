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
    builder.EntitySet<mdArrangementAcLoopRow>("mdArrangementAcLoopRows");
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    builder.EntitySet<mdPanel>("mdPanels"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdArrangementAcLoopRowsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdArrangementAcLoopRows
        [EnableQuery]
        public IQueryable<mdArrangementAcLoopRow> GetmdArrangementAcLoopRows()
        {
            return db.mdArrangementAcLoopRows;
        }

        // GET: odata/mdArrangementAcLoopRows(5)
        [EnableQuery]
        public SingleResult<mdArrangementAcLoopRow> GetmdArrangementAcLoopRow([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementAcLoopRows.Where(mdArrangementAcLoopRow => mdArrangementAcLoopRow.RowID == key));
        }

        // PUT: odata/mdArrangementAcLoopRows(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdArrangementAcLoopRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementAcLoopRow mdArrangementAcLoopRow = await db.mdArrangementAcLoopRows.FindAsync(key);
            if (mdArrangementAcLoopRow == null)
            {
                return NotFound();
            }

            patch.Put(mdArrangementAcLoopRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementAcLoopRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementAcLoopRow);
        }

        // POST: odata/mdArrangementAcLoopRows
        public async Task<IHttpActionResult> Post(mdArrangementAcLoopRow mdArrangementAcLoopRow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdArrangementAcLoopRows.Add(mdArrangementAcLoopRow);
            await db.SaveChangesAsync();

            return Created(mdArrangementAcLoopRow);
        }

        // PATCH: odata/mdArrangementAcLoopRows(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdArrangementAcLoopRow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdArrangementAcLoopRow mdArrangementAcLoopRow = await db.mdArrangementAcLoopRows.FindAsync(key);
            if (mdArrangementAcLoopRow == null)
            {
                return NotFound();
            }

            patch.Patch(mdArrangementAcLoopRow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdArrangementAcLoopRowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdArrangementAcLoopRow);
        }

        // DELETE: odata/mdArrangementAcLoopRows(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdArrangementAcLoopRow mdArrangementAcLoopRow = await db.mdArrangementAcLoopRows.FindAsync(key);
            if (mdArrangementAcLoopRow == null)
            {
                return NotFound();
            }

            db.mdArrangementAcLoopRows.Remove(mdArrangementAcLoopRow);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdArrangementAcLoopRows(5)/mdSubstation
        [EnableQuery]
        public SingleResult<mdSubstation> GetmdSubstation([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdArrangementAcLoopRows.Where(m => m.RowID == key).Select(m => m.mdSubstation));
        }

        // GET: odata/mdArrangementAcLoopRows(5)/mdPanels
        [EnableQuery]
        public IQueryable<mdPanel> GetmdPanels([FromODataUri] int key)
        {
            return db.mdArrangementAcLoopRows.Where(m => m.RowID == key).SelectMany(m => m.mdPanels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdArrangementAcLoopRowExists(int key)
        {
            return db.mdArrangementAcLoopRows.Count(e => e.RowID == key) > 0;
        }
    }
}

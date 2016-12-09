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
    builder.EntitySet<mdSingleLogo>("mdSingleLogoes");
    builder.EntitySet<mdLogoDetail>("mdLogoDetails"); 
    builder.EntitySet<mdProjectLogo>("mdProjectLogoes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdSingleLogoesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdSingleLogoes
        [EnableQuery]
        public IQueryable<mdSingleLogo> GetmdSingleLogoes()
        {
            return db.mdSingleLogoes;
        }

        // GET: odata/mdSingleLogoes(5)
        [EnableQuery]
        public SingleResult<mdSingleLogo> GetmdSingleLogo([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSingleLogoes.Where(mdSingleLogo => mdSingleLogo.SingleLogoID == key));
        }

        // PUT: odata/mdSingleLogoes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdSingleLogo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSingleLogo mdSingleLogo = await db.mdSingleLogoes.FindAsync(key);
            if (mdSingleLogo == null)
            {
                return NotFound();
            }

            patch.Put(mdSingleLogo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSingleLogoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSingleLogo);
        }

        // POST: odata/mdSingleLogoes
        public async Task<IHttpActionResult> Post(mdSingleLogo mdSingleLogo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdSingleLogoes.Add(mdSingleLogo);
            await db.SaveChangesAsync();

            return Created(mdSingleLogo);
        }

        // PATCH: odata/mdSingleLogoes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdSingleLogo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdSingleLogo mdSingleLogo = await db.mdSingleLogoes.FindAsync(key);
            if (mdSingleLogo == null)
            {
                return NotFound();
            }

            patch.Patch(mdSingleLogo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdSingleLogoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdSingleLogo);
        }

        // DELETE: odata/mdSingleLogoes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdSingleLogo mdSingleLogo = await db.mdSingleLogoes.FindAsync(key);
            if (mdSingleLogo == null)
            {
                return NotFound();
            }

            db.mdSingleLogoes.Remove(mdSingleLogo);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdSingleLogoes(5)/mdLogoDetail
        [EnableQuery]
        public SingleResult<mdLogoDetail> GetmdLogoDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSingleLogoes.Where(m => m.SingleLogoID == key).Select(m => m.mdLogoDetail));
        }

        // GET: odata/mdSingleLogoes(5)/mdProjectLogo
        [EnableQuery]
        public SingleResult<mdProjectLogo> GetmdProjectLogo([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdSingleLogoes.Where(m => m.SingleLogoID == key).Select(m => m.mdProjectLogo));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdSingleLogoExists(int key)
        {
            return db.mdSingleLogoes.Count(e => e.SingleLogoID == key) > 0;
        }
    }
}

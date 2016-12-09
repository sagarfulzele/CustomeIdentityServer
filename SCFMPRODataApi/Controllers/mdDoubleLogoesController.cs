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
    builder.EntitySet<mdDoubleLogo>("mdDoubleLogoes");
    builder.EntitySet<mdLogoDetail>("mdLogoDetails"); 
    builder.EntitySet<mdProjectLogo>("mdProjectLogoes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdDoubleLogoesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdDoubleLogoes
        [EnableQuery]
        public IQueryable<mdDoubleLogo> GetmdDoubleLogoes()
        {
            return db.mdDoubleLogoes;
        }

        // GET: odata/mdDoubleLogoes(5)
        [EnableQuery]
        public SingleResult<mdDoubleLogo> GetmdDoubleLogo([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdDoubleLogoes.Where(mdDoubleLogo => mdDoubleLogo.DoubleLogoID == key));
        }

        // PUT: odata/mdDoubleLogoes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdDoubleLogo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdDoubleLogo mdDoubleLogo = await db.mdDoubleLogoes.FindAsync(key);
            if (mdDoubleLogo == null)
            {
                return NotFound();
            }

            patch.Put(mdDoubleLogo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdDoubleLogoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdDoubleLogo);
        }

        // POST: odata/mdDoubleLogoes
        public async Task<IHttpActionResult> Post(mdDoubleLogo mdDoubleLogo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdDoubleLogoes.Add(mdDoubleLogo);
            await db.SaveChangesAsync();

            return Created(mdDoubleLogo);
        }

        // PATCH: odata/mdDoubleLogoes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdDoubleLogo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdDoubleLogo mdDoubleLogo = await db.mdDoubleLogoes.FindAsync(key);
            if (mdDoubleLogo == null)
            {
                return NotFound();
            }

            patch.Patch(mdDoubleLogo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdDoubleLogoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdDoubleLogo);
        }

        // DELETE: odata/mdDoubleLogoes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdDoubleLogo mdDoubleLogo = await db.mdDoubleLogoes.FindAsync(key);
            if (mdDoubleLogo == null)
            {
                return NotFound();
            }

            db.mdDoubleLogoes.Remove(mdDoubleLogo);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdDoubleLogoes(5)/mdLogoDetail
        [EnableQuery]
        public SingleResult<mdLogoDetail> GetmdLogoDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdDoubleLogoes.Where(m => m.DoubleLogoID == key).Select(m => m.mdLogoDetail));
        }

        // GET: odata/mdDoubleLogoes(5)/mdLogoDetail1
        [EnableQuery]
        public SingleResult<mdLogoDetail> GetmdLogoDetail1([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdDoubleLogoes.Where(m => m.DoubleLogoID == key).Select(m => m.mdLogoDetail1));
        }

        // GET: odata/mdDoubleLogoes(5)/mdProjectLogo
        [EnableQuery]
        public SingleResult<mdProjectLogo> GetmdProjectLogo([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdDoubleLogoes.Where(m => m.DoubleLogoID == key).Select(m => m.mdProjectLogo));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdDoubleLogoExists(int key)
        {
            return db.mdDoubleLogoes.Count(e => e.DoubleLogoID == key) > 0;
        }
    }
}

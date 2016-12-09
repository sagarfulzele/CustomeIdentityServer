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
    builder.EntitySet<mdProjectLogo>("mdProjectLogoes");
    builder.EntitySet<mdDoubleLogo>("mdDoubleLogoes"); 
    builder.EntitySet<mdSingleLogo>("mdSingleLogoes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class mdProjectLogoesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdProjectLogoes
        [EnableQuery]
        public IQueryable<mdProjectLogo> GetmdProjectLogoes()
        {
            return db.mdProjectLogoes;
        }

        // GET: odata/mdProjectLogoes(5)
        [EnableQuery]
        public SingleResult<mdProjectLogo> GetmdProjectLogo([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjectLogoes.Where(mdProjectLogo => mdProjectLogo.LogoID == key));
        }

        // PUT: odata/mdProjectLogoes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdProjectLogo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdProjectLogo mdProjectLogo = await db.mdProjectLogoes.FindAsync(key);
            if (mdProjectLogo == null)
            {
                return NotFound();
            }

            patch.Put(mdProjectLogo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdProjectLogoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdProjectLogo);
        }

        // POST: odata/mdProjectLogoes
        public async Task<IHttpActionResult> Post(mdProjectLogo mdProjectLogo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdProjectLogoes.Add(mdProjectLogo);
            await db.SaveChangesAsync();

            return Created(mdProjectLogo);
        }

        // PATCH: odata/mdProjectLogoes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdProjectLogo> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdProjectLogo mdProjectLogo = await db.mdProjectLogoes.FindAsync(key);
            if (mdProjectLogo == null)
            {
                return NotFound();
            }

            patch.Patch(mdProjectLogo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdProjectLogoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdProjectLogo);
        }

        // DELETE: odata/mdProjectLogoes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdProjectLogo mdProjectLogo = await db.mdProjectLogoes.FindAsync(key);
            if (mdProjectLogo == null)
            {
                return NotFound();
            }

            db.mdProjectLogoes.Remove(mdProjectLogo);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdProjectLogoes(5)/mdDoubleLogoes
        [EnableQuery]
        public IQueryable<mdDoubleLogo> GetmdDoubleLogoes([FromODataUri] int key)
        {
            return db.mdProjectLogoes.Where(m => m.LogoID == key).SelectMany(m => m.mdDoubleLogoes);
        }

        // GET: odata/mdProjectLogoes(5)/mdSingleLogoes
        [EnableQuery]
        public IQueryable<mdSingleLogo> GetmdSingleLogoes([FromODataUri] int key)
        {
            return db.mdProjectLogoes.Where(m => m.LogoID == key).SelectMany(m => m.mdSingleLogoes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdProjectLogoExists(int key)
        {
            return db.mdProjectLogoes.Count(e => e.LogoID == key) > 0;
        }
    }
}

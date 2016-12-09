﻿using System;
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
    builder.EntitySet<msPanelType>("msPanelTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class msPanelTypesController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/msPanelTypes
        [EnableQuery]
        public IQueryable<msPanelType> GetmsPanelTypes()
        {
            return db.msPanelTypes;
        }

        // GET: odata/msPanelTypes(5)
        [EnableQuery]
        public SingleResult<msPanelType> GetmsPanelType([FromODataUri] int key)
        {
            return SingleResult.Create(db.msPanelTypes.Where(msPanelType => msPanelType.ID == key));
        }

        // PUT: odata/msPanelTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<msPanelType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPanelType msPanelType = await db.msPanelTypes.FindAsync(key);
            if (msPanelType == null)
            {
                return NotFound();
            }

            patch.Put(msPanelType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPanelTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPanelType);
        }

        // POST: odata/msPanelTypes
        public async Task<IHttpActionResult> Post(msPanelType msPanelType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.msPanelTypes.Add(msPanelType);
            await db.SaveChangesAsync();

            return Created(msPanelType);
        }

        // PATCH: odata/msPanelTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<msPanelType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            msPanelType msPanelType = await db.msPanelTypes.FindAsync(key);
            if (msPanelType == null)
            {
                return NotFound();
            }

            patch.Patch(msPanelType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msPanelTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(msPanelType);
        }

        // DELETE: odata/msPanelTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            msPanelType msPanelType = await db.msPanelTypes.FindAsync(key);
            if (msPanelType == null)
            {
                return NotFound();
            }

            db.msPanelTypes.Remove(msPanelType);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msPanelTypeExists(int key)
        {
            return db.msPanelTypes.Count(e => e.ID == key) > 0;
        }
    }
}
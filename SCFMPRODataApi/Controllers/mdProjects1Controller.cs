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
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using PsiMprODataApi.Models;

namespace PsiMprODataApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using PsiMprODataApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<mdProject>("mdProjects1");
    builder.EntitySet<AspNetUser>("AspNetUsers"); 
    builder.EntitySet<mdContractPrimaryDeliverable>("mdContractPrimaryDeliverables"); 
    builder.EntitySet<mdContractSecondaryDeliverable>("mdContractSecondaryDeliverables"); 
    builder.EntitySet<mdCustomerDeliverableVoltage>("mdCustomerDeliverableVoltages"); 
    builder.EntitySet<mdExternalStakeHolder>("mdExternalStakeHolders"); 
    builder.EntitySet<mdInternalStakeHolder>("mdInternalStakeHolders"); 
    builder.EntitySet<mdMasterProjectSchedule>("mdMasterProjectSchedules"); 
    builder.EntitySet<msCompany>("msCompanies"); 
    builder.EntitySet<msSubstationStructure>("msSubstationStructures"); 
    builder.EntitySet<mdProjectStakeHolder>("mdProjectStakeHolders"); 
    builder.EntitySet<mdRevisionHistory>("mdRevisionHistories"); 
    builder.EntitySet<mdSubstation>("mdSubstations"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class mdProjects1Controller : ODataController
    {
        private MprDataEntities db = new MprDataEntities();

        // GET: odata/mdProjects1
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects1()
        {
            return db.mdProjects;
        }

        // GET: odata/mdProjects1(5)
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(mdProject => mdProject.ProjectID == key));
        }

        // PUT: odata/mdProjects1(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<mdProject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdProject mdProject = await db.mdProjects.FindAsync(key);
            if (mdProject == null)
            {
                return NotFound();
            }

            patch.Put(mdProject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdProject);
        }

        // POST: odata/mdProjects1
        public async Task<IHttpActionResult> Post(mdProject mdProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mdProjects.Add(mdProject);
            await db.SaveChangesAsync();

            return Created(mdProject);
        }

        // PATCH: odata/mdProjects1(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<mdProject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdProject mdProject = await db.mdProjects.FindAsync(key);
            if (mdProject == null)
            {
                return NotFound();
            }

            patch.Patch(mdProject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mdProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mdProject);
        }

        // DELETE: odata/mdProjects1(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            mdProject mdProject = await db.mdProjects.FindAsync(key);
            if (mdProject == null)
            {
                return NotFound();
            }

            db.mdProjects.Remove(mdProject);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/mdProjects1(5)/AspNetUser
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser));
        }

        // GET: odata/mdProjects1(5)/AspNetUser1
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser1([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser1));
        }

        // GET: odata/mdProjects1(5)/AspNetUser2
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser2([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser2));
        }

        // GET: odata/mdProjects1(5)/AspNetUser3
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser3([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser3));
        }

        // GET: odata/mdProjects1(5)/AspNetUser4
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser4([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser4));
        }

        // GET: odata/mdProjects1(5)/AspNetUser5
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser5([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser5));
        }

        // GET: odata/mdProjects1(5)/AspNetUser6
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser6([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser6));
        }

        // GET: odata/mdProjects1(5)/AspNetUser7
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser7([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser7));
        }

        // GET: odata/mdProjects1(5)/AspNetUser8
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser8([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser8));
        }

        // GET: odata/mdProjects1(5)/AspNetUser9
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser9([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser9));
        }

        // GET: odata/mdProjects1(5)/AspNetUser10
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser10([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser10));
        }

        // GET: odata/mdProjects1(5)/AspNetUser11
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser11([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser11));
        }

        // GET: odata/mdProjects1(5)/mdContractPrimaryDeliverables
        [EnableQuery]
        public IQueryable<mdContractPrimaryDeliverable> GetmdContractPrimaryDeliverables([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdContractPrimaryDeliverables);
        }

        // GET: odata/mdProjects1(5)/mdContractSecondaryDeliverables
        [EnableQuery]
        public IQueryable<mdContractSecondaryDeliverable> GetmdContractSecondaryDeliverables([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdContractSecondaryDeliverables);
        }

        // GET: odata/mdProjects1(5)/mdCustomerDeliverableVoltages
        [EnableQuery]
        public IQueryable<mdCustomerDeliverableVoltage> GetmdCustomerDeliverableVoltages([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdCustomerDeliverableVoltages);
        }

        // GET: odata/mdProjects1(5)/mdExternalStakeHolders
        [EnableQuery]
        public IQueryable<mdExternalStakeHolder> GetmdExternalStakeHolders([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdExternalStakeHolders);
        }

        // GET: odata/mdProjects1(5)/mdInternalStakeHolders
        [EnableQuery]
        public IQueryable<mdInternalStakeHolder> GetmdInternalStakeHolders([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdInternalStakeHolders);
        }

        // GET: odata/mdProjects1(5)/mdMasterProjectSchedules
        [EnableQuery]
        public IQueryable<mdMasterProjectSchedule> GetmdMasterProjectSchedules([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdMasterProjectSchedules);
        }

        // GET: odata/mdProjects1(5)/msCompany
        [EnableQuery]
        public SingleResult<msCompany> GetmsCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.msCompany));
        }

        // GET: odata/mdProjects1(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.msSubstationStructure));
        }

        // GET: odata/mdProjects1(5)/mdProjectStakeHolders
        [EnableQuery]
        public IQueryable<mdProjectStakeHolder> GetmdProjectStakeHolders([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdProjectStakeHolders);
        }

        // GET: odata/mdProjects1(5)/mdRevisionHistories
        [EnableQuery]
        public IQueryable<mdRevisionHistory> GetmdRevisionHistories([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdRevisionHistories);
        }

        // GET: odata/mdProjects1(5)/mdSubstations
        [EnableQuery]
        public IQueryable<mdSubstation> GetmdSubstations([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdSubstations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mdProjectExists(int key)
        {
            return db.mdProjects.Count(e => e.ProjectID == key) > 0;
        }
    }
}

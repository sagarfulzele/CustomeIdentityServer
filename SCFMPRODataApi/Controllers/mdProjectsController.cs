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
using System.Web.Http.Routing;
using System.Web.Http.OData.Extensions;
using Microsoft.Data.OData.Query;
using Microsoft.Data.OData;


namespace PsiMprODataApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using PsiMprODataApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<mdProject>("mdProjects");
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
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class mdProjectsController : ODataController
    {
        private MprDataEntities db = new MprDataEntities();



        [HttpPost]
        public async Task<IHttpActionResult> SavemdInternalStakeHolders([FromODataUri] int key, ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

           
          

            return Ok();
        }


        // GET: odata/mdProjects
        [EnableQuery]
        public IQueryable<mdProject> GetmdProjects()
        {
            return db.mdProjects;
        }

        // GET: odata/mdProjects(5)
        [EnableQuery]
        public SingleResult<mdProject> GetmdProject([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(mdProject => mdProject.ProjectID == key));
        }

        // PUT: odata/mdProjects(5)
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

        // POST: odata/mdProjects
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

        // PATCH: odata/mdProjects(5)
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

        // DELETE: odata/mdProjects(5)
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

        // GET: odata/mdProjects(5)/AspNetUser
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser));
        }

        // GET: odata/mdProjects(5)/AspNetUser1
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser1([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser1));
        }

        // GET: odata/mdProjects(5)/AspNetUser2
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser2([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser2));
        }

        // GET: odata/mdProjects(5)/AspNetUser3
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser3([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser3));
        }

        // GET: odata/mdProjects(5)/AspNetUser4
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser4([FromODataUri] int key)
        { 
            if (SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser4)) != null)
                return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser4));
            else
           return null; 

        }

        // GET: odata/mdProjects(5)/AspNetUser5
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser5([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser5));
        }

        // GET: odata/mdProjects(5)/AspNetUser6
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser6([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser6));
        }

        // GET: odata/mdProjects(5)/AspNetUser7
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser7([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser7));
        }

        // GET: odata/mdProjects(5)/AspNetUser8
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser8([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser8));
        }

        // GET: odata/mdProjects(5)/AspNetUser9
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser9([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser9));
        }

        // GET: odata/mdProjects(5)/AspNetUser10
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser10([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser10));
        }

        // GET: odata/mdProjects(5)/AspNetUser11
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser11([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.AspNetUser11));
        }

        // GET: odata/mdProjects(5)/mdContractPrimaryDeliverables
        [EnableQuery]
        public IQueryable<mdContractPrimaryDeliverable> GetmdContractPrimaryDeliverables([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdContractPrimaryDeliverables);
        }

        // GET: odata/mdProjects(5)/mdContractSecondaryDeliverables
        [EnableQuery]
        public IQueryable<mdContractSecondaryDeliverable> GetmdContractSecondaryDeliverables([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdContractSecondaryDeliverables);
        }

        // GET: odata/mdProjects(5)/mdCustomerDeliverableVoltages
        [EnableQuery]
        public IQueryable<mdCustomerDeliverableVoltage> GetmdCustomerDeliverableVoltages([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdCustomerDeliverableVoltages);
        }

        // GET: odata/mdProjects(5)/mdExternalStakeHolders
        [EnableQuery]
        public IQueryable<mdExternalStakeHolder> GetmdExternalStakeHolders([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdExternalStakeHolders);
        }

        // GET: odata/mdProjects(5)/mdInternalStakeHolders
        [EnableQuery] 
        public IQueryable<mdInternalStakeHolder> GetmdInternalStakeHolders([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdInternalStakeHolders);
        }

         
        // GET: odata/mdProjects(5)/mdMasterProjectSchedules
        [EnableQuery]
        public IQueryable<mdMasterProjectSchedule> GetmdMasterProjectSchedules([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdMasterProjectSchedules);
        }

        // GET: odata/mdProjects(5)/msCompany
        [EnableQuery]
        public SingleResult<msCompany> GetmsCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.msCompany));
        }

        // GET: odata/mdProjects(5)/msSubstationStructure
        [EnableQuery]
        public SingleResult<msSubstationStructure> GetmsSubstationStructure([FromODataUri] int key)
        {
            return SingleResult.Create(db.mdProjects.Where(m => m.ProjectID == key).Select(m => m.msSubstationStructure));
        }

        // GET: odata/mdProjects(5)/mdProjectStakeHolders
        [EnableQuery]
        public IQueryable<mdProjectStakeHolder> GetmdProjectStakeHolders([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdProjectStakeHolders);
        }

        // GET: odata/mdProjects(5)/mdRevisionHistories
        [EnableQuery]
        public IQueryable<mdRevisionHistory> GetmdRevisionHistories([FromODataUri] int key)
        {
            return db.mdProjects.Where(m => m.ProjectID == key).SelectMany(m => m.mdRevisionHistories);
        }

        // GET: odata/mdProjects(5)/mdSubstations
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

        [HttpPost]
        public async Task<IHttpActionResult> SaveInternalStakeHolder([FromODataUri] int key, ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //int rating = (int)parameters["Rating"];

            //Product product = await db.Products.FindAsync(key);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //product.Ratings.Add(new ProductRating() { Rating = rating });
            //db.SaveChanges();

            //double average = product.Ratings.Average(x => x.Rating);

            return Ok();
        }


        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> CreateRef([FromODataUri] int key,
       string navigationProperty, [FromBody] Uri link)
        {
            //var product = await db.Products.SingleOrDefaultAsync(p => p.Id == key);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            //switch (navigationProperty)
            //{
            //    case "Supplier":
            //        // Note: The code for GetKeyFromUri is shown later in this topic.
            //        var relatedKey = Helpers.GetKeyFromUri<int>(Request, link);
            //        var supplier = await db.Suppliers.SingleOrDefaultAsync(f => f.Id == relatedKey);
            //        if (supplier == null)
            //        {
            //            return NotFound();
            //        }

            //        product.Supplier = supplier;
            //        break;

            //    default:
            //        return StatusCode(HttpStatusCode.NotImplemented);
            //}
            //await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //public IHttpActionResult GetName(string key)
        //{

        //    ODataPath path = Request.ODataProperties().Path;

        //    if (path.PathTemplate != "~/entityset/key/property")
        //    {
        //        return BadRequest("Not the correct property access request!");
        //    }

        //   PropertyAccessPathSegment property = path.Segments.Last() as PropertyAccessPathSegment;
        //    IEdmEntityType entityType = property.Property.DeclaringType as IEdmEntityType;

        //    // Create an untyped entity object with the entity type.
        //    EdmEntityObject entity = new EdmEntityObject(entityType);

        //    DataSourceProvider.Get((string)Request.Properties[Constants.ODataDataSource], key, entity);

        //    object value = DataSourceProvider.GetProperty((string)Request.Properties[Constants.ODataDataSource], "Name", entity);

        //    if (value == null)
        //    {
        //        return NotFound();
        //    }

        //    string strValue = value as string;
        //    return Ok(strValue);
        //}


        /**/
        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> CreateLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mdProject project = await db.mdProjects.FindAsync(key);
            if (project == null)
            {
                return NotFound();
            }
            string id = GetKeyFromLinkUri<string>(link);
            switch (navigationProperty)
            {
                case "AspNetUser":
                    
                    AspNetUser user = await db.AspNetUsers.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    project.AspNetUser = user;
                    await db.SaveChangesAsync();
                    return StatusCode(HttpStatusCode.NoContent); 
                //case "mdInternalStakeHolder":
                //    mdInternalStakeHolder IStatkeHolder = await db.mdInternalStakeHolders.FindAsync(id);
                //    if (IStatkeHolder == null)
                //    {
                //        return NotFound();
                //    }
                //    project.mdInternalStakeHolders.Add(IStatkeHolder);
                //    await db.SaveChangesAsync();
                //    return StatusCode(HttpStatusCode.NoContent);
                default:
                    return NotFound();
            }

            return NotFound();
        }


        public async Task<IHttpActionResult> DeleteLink([FromODataUri] int key, string navigationProperty)
        {
            mdProject project = await db.mdProjects.FindAsync(key);
            if (project == null)
            {
                return NotFound();
            }

            switch (navigationProperty)
            {
                //case "mdInternalStakeHolder":
                //    project.mdInternalStakeHolders = null;
                //    await db.SaveChangesAsync();
                //    return StatusCode(HttpStatusCode.NoContent);

                default:
                    return NotFound();

            }
        }

        public TKey GetKeyFromLinkUri<TKey>(Uri link)
        {
            TKey key = default(TKey);

            // Get the route that was used for this request.
            IHttpRoute route = Request.GetRouteData().Route;

            // Create an equivalent self-hosted route. 
            IHttpRoute newRoute = new HttpRoute(route.RouteTemplate,
                new HttpRouteValueDictionary(route.Defaults),
                new HttpRouteValueDictionary(route.Constraints),
                new HttpRouteValueDictionary(route.DataTokens), route.Handler);

            // Create a fake GET request for the link URI.
            var tmpRequest = new HttpRequestMessage(HttpMethod.Get, link);

            // Send this request through the routing process.
            var routeData = newRoute.GetRouteData(
                Request.GetConfiguration().VirtualPathRoot, tmpRequest);

            // If the GET request matches the route, use the path segments to find the key.
            if (routeData != null)
            {
                ODataPath path = tmpRequest.ODataProperties().Path;// .GetODataPath();
                var segment = path.Segments.OfType<KeyValuePathSegment>().FirstOrDefault();
                if (segment != null)
                {
                    // Convert the segment into the key type.
                    key = (TKey)ODataUriUtils.ConvertFromUriLiteral(
                        segment.Value, ODataVersion.V3);
                }
            }
            return key;
        }
    }
}


using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;

namespace PsiMprODataApi.App_Start
{
    public class MatchAllRoutingConvention : IODataRoutingConvention
    {
        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            IEdmEntitySet entitySet = odataPath.EntitySet;
            if (controllerContext.Request.Method == HttpMethod.Get)
            {
                if (odataPath.PathTemplate == "~/entityset/key/navigation")
                {
                    controllerContext.RouteData.Values["key"] = (odataPath.Segments[1] as KeyValuePathSegment).Value;

                    return "Get" + entitySet.Name;

                }
                else if (odataPath.PathTemplate == "~/entityset/key/navigation/key")
                {
                    controllerContext.RouteData.Values["key1"] = (odataPath.Segments[1] as KeyValuePathSegment).Value;
                    controllerContext.RouteData.Values["key2"] = (odataPath.Segments[3] as KeyValuePathSegment).Value;
                    return "Get" + entitySet.ElementType.Name;
                }
            }
            else if (controllerContext.Request.Method == HttpMethod.Post)
            {
                if (odataPath.PathTemplate == "~/entityset/key/navigation")
                {
                    controllerContext.RouteData.Values["key"] = (odataPath.Segments[1] as KeyValuePathSegment).Value;
                     
                    if ("mdProjects("+ controllerContext.RouteData.Values["key"] + ")/"+ entitySet.Name + "" == odataPath.ToString())
                    {
                        return "Save"+ entitySet.Name;
                    }  
                }
            }
                return null;
        }

        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (odataPath.PathTemplate == "~/entityset/key/navigation" ||
                odataPath.PathTemplate == "~/entityset/key/navigation/key")
            {
                IEdmNavigationProperty navigationProperty = (odataPath.Segments[2] as NavigationPathSegment).NavigationProperty;
                IEdmEntitySet entitySet = odataPath.EntitySet; // the target entity set, which would be 'SubItems';

                return entitySet.Name;
            }

            return null;
        }
    }
}
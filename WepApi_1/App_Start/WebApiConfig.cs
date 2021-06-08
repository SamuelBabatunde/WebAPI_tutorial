using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using WepApi_1.Constraints;
using WepApi_1.Handlers;

namespace WepApi_1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //Delegating Handlers
            //config.MessageHandlers.Add(new FullPipelineTimerHandler());
            //config.MessageHandlers.Add(new ApiKeyHeaderHandler());
            config.MessageHandlers.Add(new RemoveBadHeadersHandler());


            // Web API routes
            //config.MapHttpAttributeRoutes();

            //Route Constraints
            var constraintResolverConfig = new DefaultInlineConstraintResolver();

            constraintResolverConfig.ConstraintMap.Add("validAccount", typeof(AccountNumberConstraint));
            constraintResolverConfig.ConstraintMap.Add("enum", typeof(EnumConstraint));
            constraintResolverConfig.ConstraintMap.Add("base64", typeof(Base64Constraint));
            config.MapHttpAttributeRoutes(constraintResolverConfig);

            //  config.Routes.MapHttpRoute(
            //    name: "ProdApi",
            //    routeTemplate: "api/prod/{id}",
            //    defaults: new { controller = "products",  id = RouteParameter.Optional }
            //);

            //  config.Routes.MapHttpRoute(
            //      name: "DefaultApi",
            //      routeTemplate: "api/{controller}/{id}",
            //      defaults: new { id = RouteParameter.Optional }
            //      constraints: new { id = new RegexBasedConstraintTemplate() },
            //      handler: new DelegatingHandlerTemplate()
            //  );

            #region TO MANUALLY CREATE A ROUTE

            // define route
            //IHttpRoute defaultRoute = config.Routes.CreateRoute("api/{controller}/{id}",
            //                                    new { id = RouteParameter.Optional }, null);
            //// Add route
            //config.Routes.Add("DefaultApi", defaultRoute);
            #endregion

        }
    }
}

using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace MvcRouting
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.RouteExistingFiles = true;

            //routes.MapMvcAttributeRoutes();
            
            // default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute("InternalBlog", "InternalBlog/{controller}/{action}",
            //    new { controller = "Posts", action = "Display" });

            // naming is optional, must be unique though

            //routes.MapRoute("MyUniqueRouteName", "{controller}/{action}");

            // controller and action are fixed
            //routes.MapRoute("BlogArchive", "Blog/Archive",
            //    new { controller = "Posts", action = "ShowArchivePosts" });

            //routes.MapRoute("Blog", "Blog/{action}",
            //    new { controller = "Posts" });


            // must start with External, must before default, otherwise does not work
            //routes.MapRoute("", "External{controller}/{action}");

            // route with default paramter
            //routes.MapRoute("DefaultParameter", "{controller}/{action}", new
            //{
            //    controller = "Home",
            //    action = "Index"
            //});

            //static url segments
            //Not all of the segments in a URL pattern need to be variables.You can also create patterns that have static segments.
            //Suppose that I want to match a URL like this to support URLs that are prefixed with Public


            // must have Public\
            //routes.MapRoute("StaticRoute", "Public/{controller}/{action}",
            //    new { controller = "Home", action = "Index" });

            // custom segment variable
            //Caution Some names are reserved and not available for custom segment variable names.these are controller,
            //    action, and area.the meaning of the first two is obvious, and i will explain areas in the next chapter.

            //routes.MapRoute("DisplayUser", "{controller}/{action}/{id}",
            //    new
            //    {
            //        controller = "User",
            //        action = "Details",
            //        id = "UserId"
            //    });

            //routes.MapRoute("OptionalVariable", "{controller}/{action}/{id}",
            //    new
            //    {
            //        controller = "User",
            //        action = "Display",
            //        id = UrlParameter.Optional
            //    });
            
            //routes.MapRoute("IntId", "{controller}/{action}/{id}",
            //    new
            //    {
            //        id = new IntRouteConstraint()
            //    });

            //routes.MapRoute("RangeId", "{controller}/{action}/{id}",
            //    new
            //    {
            //        id = new RangeRouteConstraint(100, 1000)
            //    });

            //routes.MapRoute("HttpConstraint", "{controller}/{action}/{id}",
            //    new
            //    {
            //       httpMethod = new HttpMethodConstraint("GET")
            //    });
        }
    }
}
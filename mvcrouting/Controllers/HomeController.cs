using System.Web.Mvc;

namespace MvcRouting.Controllers
{
    //[RoutePrefix("Users")]
    public class HomeController : Controller
    {
        //[Route("MyRoute")]
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";

            return View("ActionName");
        }

        public ActionResult CustomVariable(string id = "DefaultId>")//, object catchall)
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = id;
            // Alternative
            //ViewBag.CustomVariable = id ?? "<no value>";
            // alternative way to get variable
            var test = RouteData.Values["catchall"];
            //ViewBag.CustomVariable = RouteData.Values["id"];

            return View();
        }

        //[Route("Users/Add/{user}/{id}")]
        public string CreateUser(string user, string id)
        {
            return $"User: {user}, ID: {id}";
        }

        // Route constraint
        //[Route("Users/Add/{user}/{id:int}")]
        public string CreateUser(string user, int id)
        {
            return $"Create Method - User: {user}, ID: {id}";
        }
    }
}
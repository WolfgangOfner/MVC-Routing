using System.Web.Mvc;

namespace MvcRouting.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Details(string id)
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace Vidly1.Controllers
{
    public class RentalController : Controller
    {
        // GET: Rental
        public ActionResult New()
        {
            return View();
        }
    }
}
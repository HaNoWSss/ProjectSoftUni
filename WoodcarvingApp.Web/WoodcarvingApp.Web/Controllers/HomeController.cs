using Microsoft.AspNetCore.Mvc;

namespace WoodcarvingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (!statusCode.HasValue)
            {
                return this.View();
            }

            if (statusCode == 404)
            {
                return this.View("Error404");
            }
            else if (statusCode == 401 || statusCode == 403)
            {
                return this.View("Error403");
            }

            return this.View("Error500");
        }
    }
}

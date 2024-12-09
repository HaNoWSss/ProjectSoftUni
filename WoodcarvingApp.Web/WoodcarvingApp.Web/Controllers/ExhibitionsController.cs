using Microsoft.AspNetCore.Mvc;

namespace WoodcarvingApp.Web.Controllers
{
    public class ExhibitionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

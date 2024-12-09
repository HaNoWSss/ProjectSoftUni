using Microsoft.AspNetCore.Mvc;

namespace WoodcarvingApp.Web.Controllers
{
    public class ExhibitionsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

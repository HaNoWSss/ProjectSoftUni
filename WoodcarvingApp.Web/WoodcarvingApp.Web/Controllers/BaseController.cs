using Microsoft.AspNetCore.Mvc;

namespace WoodcarvingApp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsGuidIdValid(string? id, ref Guid parsedGuid)
        {
            if (String.IsNullOrWhiteSpace(id)) //Non-existing parameter in the URL
            {
                return false;
            }
            //Invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (isGuidValid == false)
            {
                return false;
            }

            return true;
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound")]
        public new IActionResult NotFound()
        {
            return View();
        }
    }
}

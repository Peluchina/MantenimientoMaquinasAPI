using Microsoft.AspNetCore.Mvc;

namespace MantenimientoMaquinasAPI.Presentation.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

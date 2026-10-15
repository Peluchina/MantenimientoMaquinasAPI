using Microsoft.AspNetCore.Mvc;

namespace MantenimientoMaquinasAPI.Presentation.Controllers
{
    public class MachineTemperatureHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

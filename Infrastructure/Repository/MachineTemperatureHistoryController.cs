using Microsoft.AspNetCore.Mvc;

namespace MantenimientoMaquinasAPI.Infrastructure.Repository
{
    public class MachineTemperatureHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

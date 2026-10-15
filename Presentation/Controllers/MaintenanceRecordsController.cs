using Microsoft.AspNetCore.Mvc;

namespace MantenimientoMaquinasAPI.Presentation.Controllers
{
    public class MaintenanceRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

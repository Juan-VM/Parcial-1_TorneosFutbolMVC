using Microsoft.AspNetCore.Mvc;
using TorneosFutbolMVC.Models;
using TorneosFutbolMVC.Services;

namespace TorneosFutbolMVC.Controllers
{
    public class EquipoController : Controller
    {
        private readonly EquipoService _equipoService;

        public EquipoController()
        {
            _equipoService = new EquipoService();
        }

        public IActionResult Create(long torneoId)
        {
            Equipo equipo = new()
            {
                TorneoId = torneoId
            };

            return View(equipo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Equipo equipo)
        {
            if (!ModelState.IsValid)
                return View(equipo);

            await _equipoService.Crear(equipo);

            TempData["Success"] = "Equipo agregado correctamente.";

            return RedirectToAction(
                "Detail",
                "Torneo",
                new { id = equipo.TorneoId });
        }

        public async Task<IActionResult> Edit(long id)
        {
            var equipo = await _equipoService.ObtenerPorId(id);

            if (equipo == null)
                return NotFound();

            return View(equipo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Equipo equipo)
        {
            if (!ModelState.IsValid)
                return View(equipo);

            await _equipoService.Editar(equipo);

            TempData["Success"] = "Equipo actualizado.";

            return RedirectToAction(
                "Detail",
                "Torneo",
                new { id = equipo.TorneoId });
        }

        public async Task<IActionResult> Delete(long id)
        {
            var equipo = await _equipoService.ObtenerPorId(id);

            if (equipo == null)
                return RedirectToAction("Index", "Torneo");

            var error = await _equipoService.Eliminar(id);

            if (error != null)
                TempData["Error"] = error;
            else
                TempData["Success"] = "Equipo eliminado.";

            return RedirectToAction(
                "Detail",
                "Torneo",
                new { id = equipo.TorneoId });
        }
    }
}
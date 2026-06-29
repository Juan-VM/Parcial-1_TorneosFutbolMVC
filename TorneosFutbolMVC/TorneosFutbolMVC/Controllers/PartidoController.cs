using Microsoft.AspNetCore.Mvc;
using TorneosFutbolMVC.Models;
using TorneosFutbolMVC.Services;

namespace TorneosFutbolMVC.Controllers
{
    public class PartidoController : Controller
    {
        private readonly PartidoService _partidoService;

        public PartidoController()
        {
            _partidoService = new PartidoService();
        }

        public async Task<IActionResult> Create(long torneoId)
        {
            EquipoService equipoService = new();

            ViewBag.Equipos =
                await equipoService.ObtenerPorTorneo(torneoId);

            PartidoCrearViewModel vm = new()
            {
                TorneoId = torneoId,
                FechaPartido = DateTime.Now
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartidoCrearViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            Partido partido = new()
            {
                TorneoId = vm.TorneoId,
                EquipoLocalId = vm.EquipoLocalId,
                EquipoVisitanteId = vm.EquipoVisitanteId,
                FechaPartido = vm.FechaPartido,
                Jugado = false
            };

            await _partidoService.Crear(partido);

            TempData["Success"] = "Partido creado correctamente.";

            return RedirectToAction(
                "Detail",
                "Torneo",
                new { id = vm.TorneoId });
        }

        public async Task<IActionResult> Resultado(long id)
        {
            var partido = await _partidoService.ObtenerPorId(id);

            if (partido == null)
                return NotFound();

            PartidoResultadoViewModel vm = new()
            {
                PartidoId = partido.Id,
                GolesLocal = partido.GolesLocal,
                GolesVisitante = partido.GolesVisitante
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Resultado(
            PartidoResultadoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var partido =
                await _partidoService.ObtenerPorId(vm.PartidoId);

            if (partido == null)
                return NotFound();

            var error =
                await _partidoService.RegistrarResultado(
                    vm.PartidoId,
                    vm.GolesLocal,
                    vm.GolesVisitante);

            if (error != null)
            {
                TempData["Error"] = error;

                return View(vm);
            }

            TempData["Success"] =
                "Resultado registrado correctamente.";

            return RedirectToAction(
                "Detail",
                "Torneo",
                new { id = partido.TorneoId });
        }

        public async Task<IActionResult> Delete(long id)
        {
            var partido =
                await _partidoService.ObtenerPorId(id);

            if (partido == null)
                return RedirectToAction("Index", "Torneo");

            var error =
                await _partidoService.Eliminar(id);

            if (error != null)
                TempData["Error"] = error;
            else
                TempData["Success"] = "Partido eliminado.";

            return RedirectToAction(
                "Detail",
                "Torneo",
                new { id = partido.TorneoId });
        }

        public async Task<IActionResult> GenerarAleatorios(long torneoId)
        {
            await _partidoService
                .GenerarPartidosAleatorios(torneoId);

            TempData["Success"] =
                "Partidos generados correctamente.";

            return RedirectToAction(
                "Detail",
                "Torneo",
                new { id = torneoId });
        }
    }
}
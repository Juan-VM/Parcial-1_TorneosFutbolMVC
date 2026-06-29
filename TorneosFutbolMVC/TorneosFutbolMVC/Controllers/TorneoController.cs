using Microsoft.AspNetCore.Mvc;
using TorneosFutbolMVC.Models;
using TorneosFutbolMVC.Services;

namespace TorneosFutbolMVC.Controllers
{
    public class TorneoController : Controller
    {
        private readonly TorneoService _torneoService;

        public TorneoController()
        {
            _torneoService = new TorneoService();
        }

        public async Task<IActionResult> Index()
        {
            var torneos = await _torneoService.ObtenerActivos();

            return View(torneos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Torneo torneo)
        {
            if (!ModelState.IsValid)
                return View(torneo);

            await _torneoService.Crear(torneo);

            TempData["Success"] = "Torneo creado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var torneo = await _torneoService.ObtenerPorId(id);

            if (torneo == null)
                return NotFound();

            return View(torneo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Torneo torneo)
        {
            if (!ModelState.IsValid)
                return View(torneo);

            await _torneoService.Editar(torneo);

            TempData["Success"] = "Torneo actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Desactivar(long id)
        {
            var error = await _torneoService.Desactivar(id);

            if (error != null)
                TempData["Error"] = error;
            else
                TempData["Success"] = "Torneo desactivado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(long id)
        {
            TorneoService torneoService = new();
            EquipoService equipoService = new();
            PartidoService partidoService = new();

            var torneo =
                await torneoService.ObtenerPorId(id);

            var equipos =
                await equipoService.ObtenerPorTorneo(id);

            var partidos =
                await partidoService.ObtenerPorTorneo(id);

            var partidosDetalle =
                partidos.Select(p => new PartidoDetalleViewModel
                {
                    Id = p.Id,

                    EquipoLocal =
                        equipos.FirstOrDefault(
                            e => e.Id == p.EquipoLocalId
                        )?.Nombre ?? "Desconocido",

                    EquipoVisitante =
                        equipos.FirstOrDefault(
                            e => e.Id == p.EquipoVisitanteId
                        )?.Nombre ?? "Desconocido",

                    FechaPartido = p.FechaPartido,

                    Jugado = p.Jugado,

                    GolesLocal = p.GolesLocal,

                    GolesVisitante = p.GolesVisitante

                }).ToList();

            var vm = new TorneoDetalleViewModel
            {
                Torneo = torneo,
                Equipos = equipos,
                Partidos = partidosDetalle
            };

            return View(vm);
        }
    }
}
using TorneosFutbolMVC.Models;

namespace TorneosFutbolMVC.Services
{
    public class PartidoService
    {
        public async Task<List<Partido>> ObtenerPorTorneo(long torneoId)
        {
            var client = await SupabClient.GetClient();

            var response = await client
                .From<Partido>()
                .Where(x => x.TorneoId == torneoId)
                .Get();

            return response.Models;
        }

        public async Task<Partido?> ObtenerPorId(long id)
        {
            var client = await SupabClient.GetClient();

            var response = await client
                .From<Partido>()
                .Where(x => x.Id == id)
                .Get();

            return response.Models.FirstOrDefault();
        }

        public async Task Crear(Partido partido)
        {
            var client = await SupabClient.GetClient();

            await client
                .From<Partido>()
                .Insert(partido);
        }

        public async Task<string?> RegistrarResultado(
            long partidoId,
            int golesLocal,
            int golesVisitante)
        {
            var partido = await ObtenerPorId(partidoId);

            if (partido == null)
                return "Partido no encontrado.";

            if (partido.Jugado)
            {
                return "El partido ya fue disputado.";
            }

            partido.GolesLocal = golesLocal;
            partido.GolesVisitante = golesVisitante;
            partido.Jugado = true;

            var client = await SupabClient.GetClient();

            await client
                .From<Partido>()
                .Update(partido);

            return null;
        }

        public async Task<string?> Eliminar(long partidoId)
        {
            var partido = await ObtenerPorId(partidoId);

            if (partido == null)
                return "Partido no encontrado.";

            if (partido.Jugado)
            {
                return "No se puede eliminar un partido ya disputado.";
            }

            var client = await SupabClient.GetClient();

            await client
                .From<Partido>()
                .Delete(partido);

            return null;
        }

        public async Task<List<Partido>> ObtenerPartidosDeEquipo(long equipoId)
        {
            var client = await SupabClient.GetClient();

            var response = await client
                .From<Partido>()
                .Get();

            return response.Models
                .Where(x =>
                    x.EquipoLocalId == equipoId ||
                    x.EquipoVisitanteId == equipoId)
                .ToList();
        }

        public async Task GenerarPartidosAleatorios(long torneoId)
        {
            EquipoService equipoService = new();

            var equipos =
                await equipoService.ObtenerPorTorneo(torneoId);

            Random rnd = new();

            equipos = equipos
                .OrderBy(x => rnd.Next())
                .ToList();

            for (int i = 0; i < equipos.Count - 1; i += 2)
            {
                Partido partido = new()
                {
                    TorneoId = torneoId,
                    EquipoLocalId = equipos[i].Id,
                    EquipoVisitanteId = equipos[i + 1].Id,
                    FechaPartido = DateTime.Now.AddDays(i),
                    GolesLocal = 0,
                    GolesVisitante = 0,
                    Jugado = false
                };

                await Crear(partido);
            }
        }
    }
}
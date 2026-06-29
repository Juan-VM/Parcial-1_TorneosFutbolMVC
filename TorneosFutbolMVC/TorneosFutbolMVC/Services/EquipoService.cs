using TorneosFutbolMVC.Models;

namespace TorneosFutbolMVC.Services
{
    public class EquipoService
    {
        public async Task<List<Equipo>> ObtenerPorTorneo(long torneoId)
        {
            var client = await SupabClient.GetClient();

            var response = await client
                .From<Equipo>()
                .Where(x => x.TorneoId == torneoId)
                .Get();

            return response.Models;
        }

        public async Task<Equipo?> ObtenerPorId(long id)
        {
            var client = await SupabClient.GetClient();

            var response = await client
                .From<Equipo>()
                .Where(x => x.Id == id)
                .Get();

            return response.Models.FirstOrDefault();
        }

        public async Task Crear(Equipo equipo)
        {
            var client = await SupabClient.GetClient();

            await client
                .From<Equipo>()
                .Insert(equipo);
        }

        public async Task Editar(Equipo equipo)
        {
            var client = await SupabClient.GetClient();

            await client
                .From<Equipo>()
                .Update(equipo);
        }

        public async Task<string?> Eliminar(long equipoId)
        {
            PartidoService partidoService = new();

            var partidos =
                await partidoService.ObtenerPartidosDeEquipo(equipoId);

            if (partidos.Any())
            {
                return "No se puede eliminar un equipo que ya participó en partidos.";
            }

            var equipo = await ObtenerPorId(equipoId);

            if (equipo == null)
                return "Equipo no encontrado.";

            var client = await SupabClient.GetClient();

            await client
                .From<Equipo>()
                .Delete(equipo);

            return null;
        }
    }
}
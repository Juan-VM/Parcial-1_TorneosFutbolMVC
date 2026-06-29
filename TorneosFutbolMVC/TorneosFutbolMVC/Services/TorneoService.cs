using TorneosFutbolMVC.Models;

namespace TorneosFutbolMVC.Services
{
    public class TorneoService
    {
        public async Task<List<Torneo>> ObtenerActivos()
        {
            var client = await SupabClient.GetClient();

            var response = await client
                .From<Torneo>()
                .Where(x => x.Activo == true)
                .Get();

            return response.Models;
        }

        public async Task<Torneo?> ObtenerPorId(long id)
        {
            var client = await SupabClient.GetClient();

            var response = await client
                .From<Torneo>()
                .Where(x => x.Id == id)
                .Get();

            return response.Models.FirstOrDefault();
        }

        public async Task Crear(Torneo torneo)
        {
            var client = await SupabClient.GetClient();

            torneo.Activo = true;
            torneo.CreadoEn = DateTime.Now;

            await client
                .From<Torneo>()
                .Insert(torneo);
        }

        public async Task Editar(Torneo torneo)
        {
            var client = await SupabClient.GetClient();

            await client
                .From<Torneo>()
                .Update(torneo);
        }

        public async Task<string?> Desactivar(long torneoId)
        {
            EquipoService equipoService = new();

            var equipos = await equipoService.ObtenerPorTorneo(torneoId);

            if (equipos.Any())
            {
                return "No se puede desactivar un torneo que tiene equipos inscritos.";
            }

            var torneo = await ObtenerPorId(torneoId);

            if (torneo == null)
                return "Torneo no encontrado.";

            torneo.Activo = false;

            var client = await SupabClient.GetClient();

            await client
                .From<Torneo>()
                .Update(torneo);

            return null;
        }
    }
}
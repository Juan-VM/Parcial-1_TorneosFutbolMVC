using Supabase;

namespace TorneosFutbolMVC.Services
{
    public static class SupabClient
    {
        private static string url =
            "https://dflzyllacwcrruquqyyi.supabase.co";

        private static string key =
            "sb_publishable_uXAmkg2HQHKJGlyXteyQlQ_7FjaBfWO";

        private static Client _client;

        public static async Task<Client> GetClient()
        {
            if (_client == null)
            {
                _client = new Client(url, key);

                await _client.InitializeAsync();
            }

            return _client;
        }
    }
}

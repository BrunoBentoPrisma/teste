using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class PermissaoService : IPermissaoService
    {
        private HttpClient _httpClient;
        public PermissaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> ValidarPermissaoUsuario(string permissaoId, string token)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);

            var response = await _httpClient.GetAsync($"Permissao?id={permissaoId}");

            if (response.IsSuccessStatusCode) return true;

            return false;
        }
    }
}
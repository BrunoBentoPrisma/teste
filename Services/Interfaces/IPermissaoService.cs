namespace Ms_Compras.Services.Interfaces
{
    public interface IPermissaoService
    {
        Task<bool> ValidarPermissaoUsuario(string permissaoId, string token);
    }
}
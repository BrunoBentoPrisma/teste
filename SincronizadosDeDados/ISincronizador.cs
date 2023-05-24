namespace Ms_Compras.SincronizadosDeDados
{
    public interface ISincronizador
    {
        Task SincronizarGrupo();
        Task SincronizarLaboratorio();
        Task SincronizarMovimento();
        Task SincronizarItensFormuzaVenda();
        Task SincronizarFornecedor();
        Task SincronizarProduto();
        Task SincronizarClientes();
        Task SincronizarNfeEntrada();
        Task SincronizarItensNfeEntrada();
        Task SincronizarOrdemDeProducao();
        Task SincronizarItensOrdemDeProducao();
        Task SincronizarEmbalagem();
        Task SincronizarItemEmbalagem();
        Task SincronizarVenda();
        Task SincronizarItemVenda();
        Task SincronizarLote();
        Task SincronizarFaltas();
        Task SincronizarVendedor();
    }
}

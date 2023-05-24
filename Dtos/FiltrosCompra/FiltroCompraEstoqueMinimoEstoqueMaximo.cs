namespace Ms_Compras.Dtos.FiltrosCompra
{
    public class FiltroCompraEstoqueMinimoEstoqueMaximo
    {
        public Guid EmpresaId { get; set; }
        public Guid? LaboratorioId { get; set; }
        public List<Guid>? GruposIds { get; set; }
        public List<Guid>? ProdutosIds { get; set; }

        public FiltroCompraEstoqueMinimoEstoqueMaximo(Guid empresaId,
            Guid? laboratorioId,
            List<Guid>? gruposIds,
            List<Guid>? produtosIds)
        {
            EmpresaId = empresaId;
            LaboratorioId = laboratorioId;
            GruposIds = gruposIds;
            ProdutosIds = produtosIds;
        }
    }
}

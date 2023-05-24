namespace Ms_Compras.Dtos.FiltrosCompra
{
    public class FiltroCompraConsumo
    {
        public Guid EmpresaId { get; set; }
        public List<Guid>? ProdutoId { get; set; }
        public List<Guid>? GrupoId { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public FiltroCompraConsumo(Guid empresaId,
            List<Guid>? produtoId,
            List<Guid>? grupoId,
            DateTime dataInicial,
            DateTime dataFinal
            )
        {
            EmpresaId = empresaId;
            ProdutoId = produtoId;
            GrupoId = grupoId;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
        }
    }
}
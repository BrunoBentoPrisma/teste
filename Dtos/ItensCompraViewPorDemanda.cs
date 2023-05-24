namespace Ms_Compras.Dtos
{
    public class ItensCompraViewPorDemanda
    {
        public Guid GrupoId { get; set; }
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Guid? LaboratorioId { get; set; }
        public string? NomeLaboratorio { get; set; }
        public string SiglaUnidade { get; set; } = string.Empty;
        public string? CurvaAbc { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal ValorCustoMedio { get; set; }
        public string? CodigoCas { get; set; }
        public string? CodigoDcb { get; set; }
        public string? CodigoBarra { get; set; }
        public decimal? QuantidadeVendida { get; set; }
    }
}

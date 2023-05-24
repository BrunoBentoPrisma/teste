namespace Ms_Compras.Dtos.NotaFiscalDeEntrada
{
    public class UltimasComprasDto
    {
        public DateTime DataDeEmissao { get; set; }
        public string NomeFornecedor { get; set; } = string.Empty;
        public DateTime DataDeCadastro { get; set; }
        public string SerieNota { get; set; } = string.Empty;
        public string NumeroNota { get; set; } = string.Empty;
        public decimal QuantidadeUnidadeEstoque { get; set; }
        public string SiglaUnidade { get; set; } = string.Empty;
        public decimal Custo { get; set; }
        public decimal ValorIpi { get; set; }
        public decimal ValorFreteItem { get; set; }
        public decimal ValorDiversos { get; set; }
        public decimal Total { get; set; }
    }
}
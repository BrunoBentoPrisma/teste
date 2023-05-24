namespace Ms_Compras.Dtos.Compra
{
    public class ItensCompraFiltroDto
    {
        public int Id { get; internal set; }
        public string? NomeLaboratorio { get; internal set; }
        public int GrupoId { get; internal set; }
        public int LaboratorioId { get; internal set; }
        public double Estoque { get; internal set; }
        public double EstoqueMaximo { get; internal set; }
        public double EstoqueMinimo { get; internal set; }
        public int ProdutoId { get; internal set; }
        public string? Curva { get; internal set; }
        public string? SiglaUnidade { get; internal set; }
        public string? CodigoCas { get; internal set; }
        public string? CodigoDcb { get; internal set; }
        public string? CodigoProduto { get; internal set; }
        public string? CodigoGrupo { get; internal set; }
        public double QuantidadeVendida { get; internal set; }
        public double ValorUnitario { get; internal set; }
        public bool Comprar { get; internal set; }
        public int QuantidadeCompra { get; internal set; }
        public string? CodigoBarra { get; internal set; }
        public string? Descricao { get; internal set; }
    }
}
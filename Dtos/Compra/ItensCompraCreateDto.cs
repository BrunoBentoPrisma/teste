namespace Ms_Compras.Dtos.Compra
{
    public class ItensCompraCreateDto
    {
        public Guid? LaboratorioId { get; set; }
        public Guid ProdutoId { get; set; }
        public bool Comprar { get; set; }
        public bool Encomenda { get; set; }
        public double Estoque { get; set; }
        public double QuantidadeCompra { get; set; }
        public double QuantidadeVendida { get; set; }
        public double QuantidadeTotal { get; set; }
        public double ValorTotal { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorVendido { get; set; }
        public string? SelecionadoGerar { get; set; }
        public double ConsumoDiario { get; set; }
        public double QuantidadeCompraMaxima { get; set; }
    }
}

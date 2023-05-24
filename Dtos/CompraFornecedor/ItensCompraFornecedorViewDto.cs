using Ms_Compras.Database.Entidades;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class ItensCompraFornecedorViewDto
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public bool Comprar { get; set; }
        public DateTime DataValidade { get; set; }
        public Guid CompraFornecedorId { get; set; }
        public double QuantidadeCompra { get; set; }
        public double ValorTotal { get; set; }
        public double ValorUnitario { get; set; }
        public double QuatidadeCompraUnidadeEstoque { get; set; }
        public double ValorUnitarioUnidadeEstoque { get; set; }
        public int StatusItemPedido { get; set; }
        public string SelecionadoGerar { get; set; } = string.Empty;
    }
}

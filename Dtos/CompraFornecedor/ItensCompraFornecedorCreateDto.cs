using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.CompraFornecedor
{
    public class ItensCompraFornecedorCreateDto
    {
        [Required]
        public Guid ProdutoId { get; set; }
        public bool Comprar { get; set; }
        public DateTime DataValidade { get; set; }
        public double QuantidadeCompra { get; set; }
        public double ValorTotal { get; set; }
        public double ValorUnitario { get; set; }
        public double QuatidadeCompraUnidadeEstoque { get; set; }
        public double ValorUnitarioUnidadeEstoque { get; set; }
        public int StatusItemPedido { get; set; }
        public string SelecionadoGerar { get; set; } = string.Empty;
    }
}

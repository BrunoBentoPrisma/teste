using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.ProdutoDto;

namespace Ms_Compras.Dtos.Compra
{
    public class ItensCompraViewDto
    {
        public Guid Id { get; set; }
        public Guid? LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; }
        public Guid ProdutoId { get; set; }
        public ProdutoViewDto Produto { get; set; } = new ProdutoViewDto();
        public bool Comprar { get; set; }
        public bool Encomenda { get; set; }
        public decimal Estoque { get; set; }
        public decimal EstoqueTotal { get; set; }
        public Guid CompraId { get; set; }
        public decimal QuantidadeCompra { get; set; }
        public decimal QuantidadeVendida { get; set; }
        public decimal QuantidadeTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorVendido { get; set; }
        public string? SelecionadoGerar { get; set; }
        public decimal ConsumoDiario { get; set; }
        public decimal QuantidadeCompraMaxima { get; set; }
    }
}

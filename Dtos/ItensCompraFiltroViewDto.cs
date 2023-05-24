using Ms_Compras.Database.Entidades;
using System.Text.Json.Serialization;

namespace Ms_Compras.Dtos
{
    public class ItensCompraFiltroViewDto
    {
        public Guid GrupoId { get; set; }
        public Guid ProdutoId { get; set; }
        public string DescricaoProduto { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DescricaoLaboratorio { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CurvaAbc { get; set; }
        public decimal EstoqueMinimo { get; set; }
        public decimal EstoqueMaximo { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QuantidadeVendida { get; set; }
        public decimal ValorVendido { get; set; }
        public decimal Estoque { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalParaDias { get; set; }
        public bool Comprar { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? NomeFornecedor { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CodigoCas { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CodigoDcb { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CodigoBarras { get; set; }
        public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
        public Guid? LaboratorioId { get; set; }
        public int? Encomenda { get; set; }
        public decimal? ConsumoDiario { get; set;}
    }
}
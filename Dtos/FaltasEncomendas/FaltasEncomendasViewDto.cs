using Ms_Compras.Dtos.ClienteDto;
using Ms_Compras.Dtos.GrupoDto;
using Ms_Compras.Dtos.ProdutoDto;
using Ms_Compras.Dtos.VendedorDto;
using System.Text.Json.Serialization;

namespace Ms_Compras.Dtos.FaltasEncomendas
{
    public class FaltasEncomendasViewDto
    {
        public Guid Id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? ClienteId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ClienteViewDto? Cliente { get; set; }
        public Guid GrupoId { get; set; }
        public GrupoViewDto Grupo { get; set; } = null!;
        public Guid ProdutoId { get; set; }
        public ProdutoViewDto Produto { get; set; } = null!;
        public Guid VendedorId { get; set; }
        public VendedorViewDto Vendedor { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? CompraId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Observacao { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? PrevisaoDeEntrega { get; set; }
        public decimal Quantidade { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Status { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Telefone { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Ddd { get; set; }
        public int Tipo { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using Ms_Compras.Enum;

namespace Ms_Compras.Dtos.Compra
{
    public class CompraCreateDto
    {
        public StatusCompra StatusCompra { get; set; }
        public double TotalCompra { get; set; }
        public int? TempoDeReposicaoMaxima { get; set; }
        public DateTime VendaDe { get; set; }
        public DateTime VendaAte { get; set; }
        public CurvaAbc CurvaAbc { get; set; }
        public Guid? LaboratorioId { get; set; }
        public TipoCompra TipoCompra { get; set; }
        [Range(1, 2, ErrorMessage = "Tipo de demanda inválido")]
        public int? TipoDemanda { get; set; }
        public bool? ConsideraEncomendaFaltas { get; set; }
        public int? TempoDeReposicao { get; set; }
        public int? QuantidadeDias { get; set; }
        public int? TipoValor { get; set; }
        public DateTime? APartirDe { get; set; }
        public bool? SaldoQuantidadeComprometida { get; set; }
        public bool? ConsiderarApenasEmpresaSelecionada { get; set; }
        public List<ItensCompraCreateDto> ItensCompras { get; set; } = new List<ItensCompraCreateDto>();
        public List<Guid> FornecedoresIds { get; set; } = new List<Guid>();
        public List<Guid>? GruposIds { get; set; }
        public List<Guid>? ProdutosIds { get; set; }
    }
}

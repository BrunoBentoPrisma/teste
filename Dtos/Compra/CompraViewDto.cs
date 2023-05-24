using Ms_Compras.Database.Entidades;
using Ms_Compras.Enum;

namespace Ms_Compras.Dtos.Compra
{
    public class CompraViewDto
    {
        public Guid Id { get; set; }
        public StatusCompra StatusCompra { get; set; }
        public double TotalCompra { get; set; }
        public int? TempoDeReposicaoMaxima { get; set; }
        public DateTime VendaDe { get; set; }
        public DateTime VendaAte { get; set; }
        public CurvaAbc CurvaAbc { get; set; }
        public Guid? LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; }
        public TipoCompra TipoCompra { get; set; }
        public int? TipoDemanda { get; set; }
        public bool? ConsideraEncomendaFaltas { get; set; }
        public int? TempoDeReposicao { get; set; }
        public int? QuantidadeDias { get; set; }
        public int? TipoValor { get; set; }
        public DateTime? APartirDe { get; set; }
        public bool? SaldoQuantidadeComprometida { get; set; }
        public bool? ConsiderarApenasEmpresaSelecionada { get; set; }
        public List<ItensCompraViewDto> ItensCompras { get; set; } = new List<ItensCompraViewDto>();
    }
}

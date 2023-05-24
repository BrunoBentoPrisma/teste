using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ms_Compras.Enum;

namespace Ms_Compras.Database.Entidades
{
    public class Compra : BaseEntity
    {

        [Column("StatusCompra")]
        public StatusCompra StatusCompra { get; set; }

        [Column("TotalCompra", TypeName = "decimal(24,4)")]
        public decimal TotalCompra { get; set; }

        [Column("TempoDeReposicaoMaxima")]
        public int? TempoDeReposicaoMaxima { get; set; }

        [Column("VendaDe")]
        public DateTime VendaDe { get; set; }

        [Column("VendaAte")]
        public DateTime VendaAte { get; set; }

        [Column("CurvaAbc")]
        public CurvaAbc CurvaAbc { get; set; }

        [Column("LaboratorioId")]
        public Guid? LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; }

        [Column("TipoCompra")]
        public TipoCompra TipoCompra { get; set; }

        [Column("TipoDemanda")]
        [Range(1, 2, ErrorMessage = "Tipo de demanda inválido")]
        public int? TipoDemanda { get; set; }

        [Column("ConsideraEncomendaFaltas")]
        public bool? ConsideraEncomendaFaltas { get; set; }

        [Column("TempoDeReposicao")]
        public int? TempoDeReposicao { get; set; }

        [Column("QuantidadeDias")]
        public int? QuantidadeDias { get; set; }

        [Column("TipoValor")]
        public int? TipoValor { get; set; }

        [Column("APartirDe")]
        public DateTime? APartirDe { get; set; }

        [Column("SaldoQuantidadeComprometida")]
        public bool? SaldoQuantidadeComprometida { get; set; }

        [Column("ConsiderarApenasEmpresaSelecionada")]
        public bool? ConsiderarApenasEmpresaSelecionada { get; set; }

        public List<ItensCompra> ItensCompras { get; set; } = new List<ItensCompra>();

    }
}

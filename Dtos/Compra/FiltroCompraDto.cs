using Ms_Compras.Enum;

namespace Ms_Compras.Dtos.Compra
{
    public class FiltroCompraDto
    {
        public int? TipoDemanda { get; set; }
        public DateTime? VendaDe { get; set; }
        public DateTime? VendaAte { get; set; }
        public CurvaAbc? CurvaAbc { get; set; }
        public bool? ConsideraEncomendaFaltas { get; set; }
        public int? TempoDeRep { get; set; }
        public int? QuantidadeDias { get; set; }
        public int? TipoValor { get; set; }
        public DateTime? APartirDe { get; set; }
        public bool? SaldoQuantidadeComprometida { get; set; }
        public Guid? LaboratorioId { get; set; }
        public Guid? EmpresaId { get; set; }
        public bool? ConsiderarApenasEmpresaSelecionada { get; set; }
        public List<Guid> FornecedoresIds { get; set; } = new();
        public List<Guid> GruposIds { get; set; } = new();
        public List<Guid> ProdutosIds { get; set; } = new();
    }
}
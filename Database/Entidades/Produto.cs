using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Database.Entidades
{
    public class Produto : BaseEntity
    {
            
        [Column("GrupoId")]
        [Required]
        public Guid GrupoId { get; set; }
        public Grupo Grupo { get; set; } = null!;
        [Column("Descricao")]
        [Required]
        [MaxLength(100)]
        public string Descricao { get; set; } = string.Empty;
        [Column("UnidadeManipulacao")]
        [Required]
        public string UnidadeManipulacao { get; set; } = string.Empty;
        [Column("UnidadeEstoque")]
        [Required] 
        public string UnidadeEstoque { get; set; } = string.Empty;
        [Column("ValorCusto", TypeName = "decimal(24,4)")]
        public decimal ValorCusto { get; set; }
        [Column("ValorCustoMedio", TypeName = "decimal(24,4)")]
        public decimal? ValorCustoMedio { get; set; }
        [Column("ValorVenda", TypeName = "decimal(24,4)")]
        public decimal? ValorVenda { get; set; }
        [Column("EstoqueMinimo", TypeName = "decimal(24,4)")]
        public decimal? EstoqueMinimo { get; set; }
        [Column("EstoqueMaximo", TypeName = "decimal(24,4)")]
        public decimal? EstoqueMaximo { get; set; }
        [Column("FornecedorId")]
        public Guid? FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        [Column("DataUltimaCompra")]
        public DateTime? DataUltimaCompra { get; set; }
        [Column("CurvaAbc")]
        public int? CurvaAbc { get; set; }
        [Column("AliquotaIcms", TypeName = "decimal(24,4)")]
        [Required]
        public decimal? AliquotaIcms { get; set; }
        [Column("Calculo")]
        [Required]
        [Range(0, 3, ErrorMessage = "Campo calculo inválido")]
        public TipoCalculo Calculo { get; set; }
        [Column("SituacaoTributaria")]
        [Required]
        [Range(0, 5, ErrorMessage = "Campo calculo inválido")]
        public SituacaoTributaria SituacaoTributaria { get; set; }
        [Column("CodigoBarra")]
        public string? CodigoBarra { get; set; }
        [Column("CodigoDcb")]
        public string? CodigoDcb { get; set; }
        [Column("CodigoCas")]
        public string? CodigoCas { get; set; }
        [Column("Inativo")]
        public bool Inativo { get; set; } = false;
        [Column("InativoCompras")]
        public bool InativoCompras { get; set; } = false;
        [Column("LaboratorioId")]
        public Guid? LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; }

    }

    public enum TipoCalculo
    {
        Percentual,
        Capsula,
        Qsp,
        SemCalculo
    }
    public enum SituacaoTributaria
    {
        NaoInformado,
        TributadaIntegralmente,
        Isenta,
        SubstituicaoTributaria,
        NaoIncidencia,
        TributadoIss
    }
}


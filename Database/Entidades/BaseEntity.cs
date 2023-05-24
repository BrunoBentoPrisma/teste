using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Database.Entidades
{
    public abstract class BaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("EmpresaId")]
        [Required]
        public Guid EmpresaId { get; set; }

        [Column("IntegracaoId")]
        [StringLength(50)]
        public string? IntegracaoId { get; set; }

        [Column("DataDeCadastro")]
        [Required]
        public DateTime DataDeCadastro { get; set; } = DateTime.Now;

        [Column("DataDeExclusao")]
        public DateTime? DataDeExclusao { get; set; }

        [Column("DataDeAlteracao")]
        public DateTime? DataDeAlteracao { get; set; }

        [Column("NomeCriador")]
        [MaxLength(50)]
        [Required]
        public string NomeCriador { get; set; } = string.Empty;

        [Column("NomeEditor")]
        [MaxLength(50)]
        public string? NomeEditor { get; set; }

        [Column("Excluido")]
        public bool Excluido { get; set; } = false;
    }
}

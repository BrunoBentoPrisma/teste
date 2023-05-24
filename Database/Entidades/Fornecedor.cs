using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Database.Entidades
{
    public class Fornecedor : BaseEntity
    {

        [Column("NomeFornecedor")]
        [Required]
        [MaxLength(100)]
        public string NomeFornecedor { get; set; } = string.Empty;
    }
}

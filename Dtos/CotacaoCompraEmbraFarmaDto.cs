using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos
{
    public class CotacaoCompraEmbraFarmaDto
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Destinatario { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        public string? Copia { get; set; }
        [Required]
        public string Mensagem { get; set; } = string.Empty;
        public List<Guid> Ids { get; set; } = null!;
    }
}
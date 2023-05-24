using System.ComponentModel.DataAnnotations.Schema;

namespace Ms_Compras.Database.Entidades
{
    public class ArquivosCotacaoCompra : BaseEntity
    {
        [Column("CoteFacil")]
        public byte[]? CoteFacil { get; set; }
        [Column("EmbraFarma")]
        public byte[]? EmbraFarma { get; set; }
        [Column("CompraId")]
        public Guid CompraId { get; set; }
        [NotMapped]
        public string? CoteFacilString { get; set; }
        [NotMapped]
        public string? EmbraFarmaString { get; set; }
    }
}
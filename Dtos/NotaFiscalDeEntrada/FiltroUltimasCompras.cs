using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.NotaFiscalDeEntrada
{
    public class FiltroUltimasCompras
    {
        [Required]
        public Guid GrupoId { get; set; }
        [Required]
        public Guid ProdutoId { get; set; }
        [Required]
        public DateTime DataInicial { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
    }
}
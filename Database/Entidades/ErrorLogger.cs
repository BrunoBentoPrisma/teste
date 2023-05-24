using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Database.Entidades
{
    public class ErrorLogger
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("EmpresaId")]
        [Required]
        public Guid? EmpresaId { get; set; }

        [Column("ErrorMessage")]
        [Required]
        public string ErrorMessage { get; set; }

        [Column("CustomErrorMessage")]
        [Required]
        public string CustomErrorMessage { get; set; }

        [Column("CustomErrorCode")]
        [Required]
        public string CustomErrorCode { get; set; }

        [Column("CreatedAt")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ErrorLogger(Guid? EmpresaId, string ErrorMessage, string CustomErrorCode, string CustomErrorMessage)
        {
            this.EmpresaId = EmpresaId;
            this.ErrorMessage = ErrorMessage;
            this.CustomErrorCode = CustomErrorCode;
            this.CustomErrorMessage = CustomErrorMessage;
        }
    }
}

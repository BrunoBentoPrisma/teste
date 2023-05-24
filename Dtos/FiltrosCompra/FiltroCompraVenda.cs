using Ms_Compras.Enum;
using System.ComponentModel.DataAnnotations;

namespace Ms_Compras.Dtos.FiltrosCompra
{
    public class FiltroCompraVenda
    {
        [Required]
        public DateTime DataInicial { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
        [Required]
        public Guid EmpresaId { get; set; }
        public DateTime? APartirDe { get; set; }
        public Guid? LaboratorioId { get; set; }
        public List<Guid>? GruposIds { get; set; }
        public List<Guid>? ProdutosIds { get; set; }
        public CurvaAbc? CurvaAbc { get; set; }

        public FiltroCompraVenda(DateTime? dataInicial,
            DateTime? dataFinal,
            Guid? empresaId,
            DateTime? aPartirDe,
            Guid? laboratorioId,
            List<Guid>? gruposIds,
            List<Guid>? produtosIds,
            CurvaAbc? curvaAbc)
        {

            if (dataInicial is null) throw new ArgumentException("É obrigatório informar o período !");
            if (dataFinal is null) throw new ArgumentException("É obrigatório informar o período !");
            if (empresaId is null) throw new ArgumentException("EmpresaId null");

            CurvaAbc = curvaAbc;
            DataInicial = dataInicial.Value;
            DataFinal = dataFinal.Value;
            EmpresaId = empresaId.Value;
            APartirDe = aPartirDe;
            LaboratorioId = laboratorioId;
            GruposIds = gruposIds;
            ProdutosIds = produtosIds;
        }
    }
}

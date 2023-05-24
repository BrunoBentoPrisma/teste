using Ms_Compras.Enum;

namespace Ms_Compras.Dtos.FiltrosCompra
{
    public class FiltroCompraFaltasEncomendas
    {
        public CurvaAbc? CurvaAbc { get; set; }
        public int? TipoValor { get; set; }
        public DateTime? APartirDe { get; set; }
        public Guid? LaboratorioId { get; set; }
        public Guid? EmpresaId { get; set; }

        public FiltroCompraFaltasEncomendas(
            CurvaAbc? curvaAbc,
            int? tipoValor,
            DateTime? apartirDe,
            Guid? laboratorioId,
            Guid? empresaId)
        {
            CurvaAbc = curvaAbc;
            TipoValor = tipoValor;
            APartirDe = apartirDe;
            LaboratorioId = laboratorioId;
            EmpresaId = empresaId;
        }
    }
}
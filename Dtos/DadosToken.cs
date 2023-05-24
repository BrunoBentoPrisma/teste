namespace Ms_Compras.Dtos
{
    public class DadosToken
    {
        public Guid EmpresaId { get; set; }
        public string Nome { get; set; } = string.Empty;

        public DadosToken(Guid empresaId, string nome)
        {
            this.EmpresaId = empresaId;
            this.Nome = nome;
        }
    }
}
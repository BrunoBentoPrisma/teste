namespace Ms_Compras.Dtos
{
    public class MessageEmailDto
    {
        public Guid EmpresaId { get; set; }
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public string? Copia { get; set; }
        public string NomeArquivo { get; set; }
        public string TipoArquivo { get; set; }
        public MemoryStream Arquivo { get; set; }

        public MessageEmailDto(
            Guid empresaId,
            string destinatario,
            string assunto, 
            string conteudo, 
            string? copia,
            string nomeArquivo,
            string tipoArquivo,
            MemoryStream arquivo)
        {
            this.EmpresaId = empresaId;
            this.Destinatario = destinatario;
            this.Assunto = assunto;
            this.Conteudo = conteudo;
            this.Copia = copia;
            this.NomeArquivo = nomeArquivo;
            this.TipoArquivo = tipoArquivo;
            this.Arquivo = arquivo;
        }
    }
}
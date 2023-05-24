namespace Ms_Compras.Dtos
{
    public class EmpresaDto
    {
        public string Cnpj { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Senha { get; set; } = string.Empty;

    }
}
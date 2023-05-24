namespace Ms_Compras.Dtos.ProdutoDto
{
    public class ProdutoViewDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string UnidadeEstoque { get; set; } = string.Empty;
    }
}

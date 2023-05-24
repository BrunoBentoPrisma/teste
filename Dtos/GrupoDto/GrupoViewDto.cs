namespace Ms_Compras.Dtos.GrupoDto
{
    public class GrupoViewDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Tipo { get; set; }
    }
}

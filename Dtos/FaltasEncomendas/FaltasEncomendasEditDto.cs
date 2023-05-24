namespace Ms_Compras.Dtos.FaltasEncomendas
{
    public class FaltasEncomendasEditDto
    {
        public Guid Id { get; set; }
        public List<FaltasEncomendasCreateDto> FaltasEncomendas { get; set; } = null!;
    }
}
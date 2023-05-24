namespace Ms_Compras.Dtos
{
    public class PaginacaoDto<T>
    {
        public int PageCount { get; set; }
        public List<T> Values { get; set; } = new();
    }
}
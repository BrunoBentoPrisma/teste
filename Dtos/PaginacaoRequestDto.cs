namespace Ms_Compras.Dtos
{
    public class PaginacaoRequestDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool Asc { get; set; }
        public Guid EmpresaId { get; set; }

        public PaginacaoRequestDto(int page,
            int pageSize, string search, string sortBy,
            bool asc, Guid empresaId
            )
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Search = search;
            this.SortBy = sortBy;
            this.Asc = asc;
            this.EmpresaId = empresaId;
        }

    }
}
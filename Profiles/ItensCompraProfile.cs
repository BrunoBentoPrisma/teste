using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.Compra;

namespace Ms_Compras.Profiles
{
    public class ItensCompraProfile : Profile
    {
        public ItensCompraProfile()
        {
            CreateMap<ItensCompra, ItensCompraCreateDto>().ReverseMap();
            CreateMap<ItensCompra, ItensCompraFornecedor>().ReverseMap();
            CreateMap<ItensCompra, ItensCompraViewDto>().ReverseMap();
            CreateMap<ItensCompraFiltroDto, ItensCompraViewDto>().ReverseMap();
        }
    }
}

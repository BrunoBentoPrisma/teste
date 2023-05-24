using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.CompraFornecedor;

namespace Ms_Compras.Profiles
{
    public class ItensCompraFornecedorProfile : Profile
    {
        public ItensCompraFornecedorProfile()
        {
            CreateMap<ItensCompraFornecedor, ItensCompraFornecedorViewDto>().ReverseMap();
            CreateMap<ItensCompraFornecedor, ItensCompraFornecedorCreateDto>().ReverseMap();
        }
    }
}

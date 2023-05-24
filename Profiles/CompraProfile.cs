using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.Compra;

namespace Ms_Compras.Profiles
{
    public class CompraProfile : Profile
    {
        public CompraProfile()
        {
            CreateMap<Compra,CompraCreateDto>().ReverseMap();
            CreateMap<Compra, CompraFornecedor>().ReverseMap();
            CreateMap<Compra, CompraViewDto>().ReverseMap();
        }
    }
}

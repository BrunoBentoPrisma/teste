using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.CompraFornecedor;

namespace Ms_Compras.Profiles
{
    public class CompraFornecedorProfile : Profile
    {
        public CompraFornecedorProfile()
        {
            CreateMap<CompraFornecedor, CompraFornecedorViewDto>().ReverseMap();
            CreateMap<CompraFornecedor, CompraFornecedorCreateDto>().ReverseMap();  
        }
    }
}

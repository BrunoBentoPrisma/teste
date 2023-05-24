using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.ProdutoDto;

namespace Ms_Compras.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoViewDto>().ReverseMap();
        }
    }
}

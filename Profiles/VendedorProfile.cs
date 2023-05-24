using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.VendedorDto;

namespace Ms_Compras.Profiles
{
    public class VendedorProfile : Profile
    {
        public VendedorProfile()
        {
            CreateMap<Vendedor, VendedorViewDto>().ReverseMap();
        }
    }
}

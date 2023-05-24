using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.ClienteDto;

namespace Ms_Compras.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteViewDto>().ReverseMap();
        }
    }
}

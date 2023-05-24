using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.GrupoDto;

namespace Ms_Compras.Profiles
{
    public class GrupoProfile : Profile
    {
        public GrupoProfile()
        {
            CreateMap<Grupo, GrupoViewDto>().ReverseMap();
        }
    }
}

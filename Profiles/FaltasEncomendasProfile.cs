using AutoMapper;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos.FaltasEncomendas;

namespace Ms_Compras.Profiles
{
    public class FaltasEncomendasProfile : Profile
    {
        public FaltasEncomendasProfile()
        {
            CreateMap<FaltasEncomendas, FaltasEncomendasCreateDto>().ReverseMap();
            CreateMap<FaltasEncomendas, FaltasEncomendasViewDto>().ReverseMap();
        }
    }
}
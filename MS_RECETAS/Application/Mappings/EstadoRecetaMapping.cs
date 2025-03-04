using AutoMapper;
using MS_RECETAS.Application.Dto;
using MS_RECETAS.Domain.Models;



namespace MS_RECETAS.Application.Mappings
{
    public class EstadoRecetaMapping : Profile

    {
        public EstadoRecetaMapping()

        {

            CreateMap<EstadosReceta, EstadoRecetaDto>().ReverseMap();

            //Ignora coleccion recetas
            CreateMap<EstadoRecetaDto, EstadosReceta>().ForMember(dest => dest.Recetas, opt => opt.Ignore());
        }
    }
}
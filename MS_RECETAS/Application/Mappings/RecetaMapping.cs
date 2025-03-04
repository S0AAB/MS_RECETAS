using AutoMapper;
using MS_RECETAS.Application.Dto;
using MS_RECETAS.Domain.Models;


namespace MS_RECETAS.Application.Mappings
{
	public class RecetaMapping:Profile
    {
        public RecetaMapping()
        {
            CreateMap<RecetaDto, Recetas>().ReverseMap().ForMember(dest => dest.EstadoId, opt => opt.Ignore());
        }
    }

}
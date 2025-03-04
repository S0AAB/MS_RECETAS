using System.Collections.Generic;
using System.Threading.Tasks;
using MS_RECETAS.Application.Dto;
using MS_RECETAS.Domain.Models;
namespace MS_RECETAS.Application.ServicesInterfaces
{
	public interface IRecetaService
	{
		IEnumerable<RecetaDto> GetAll();
		RecetaDto GetById(int id);
        EstadoRecetaDto GetEstadosRecetaById(int id);
        Task<bool> Create(RecetaDto receta);
        Task<bool> Update(int id,RecetaDto receta);
        Task<bool> Delete(int id);
    


    }
}   
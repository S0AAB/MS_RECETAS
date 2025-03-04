using System.Collections.Generic;
using MS_RECETAS.Domain.Models;

namespace MS_RECETAS.Domain.Interfaces
{
    public interface IRecetasRepository
    {
        IEnumerable<Recetas> GetAll();
        Recetas GetById(int id);
        EstadosReceta GetEstadosRecetaById(int id);
        void Add(Recetas receta);
        void Update(Recetas receta);
        void Delete(Recetas receta);
        bool Exists(int id);
        bool CodigoExists(string codigo);


    }
}

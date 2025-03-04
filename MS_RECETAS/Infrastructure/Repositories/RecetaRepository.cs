using MS_RECETAS.Domain.Interfaces;
using MS_RECETAS.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MS_RECETAS.Infrastructure.Repositories
{
    public class RecetaRepository : IRecetasRepository

    {
       private readonly RecetasDBEntities _context;

       public RecetaRepository(RecetasDBEntities context) {
            _context = context;
        }

        //Traer recetas
        public IEnumerable<Recetas> GetAll()
        {
            return _context.Recetas.ToList();
        }

        //Traer receta por ID
        public Recetas GetById(int id)
        {
            return _context.Recetas.Find(id);
        }

        //Trae estado receta por ID
        public EstadosReceta GetEstadosRecetaById(int id)
        {
            return _context.EstadosReceta.Find(id);
        }

        //Crear receta
        public void Add(Recetas receta)
        {
            _context.Recetas.Add(receta);
            _context.SaveChanges();
        }

        //Actualizar receta
        public void Update(Recetas receta)
        {
            _context.Entry(receta).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //Borrar receta
        public void Delete(Recetas receta)
        {
            
            _context.Recetas.Remove(receta);
            _context.SaveChanges();
           
        }


        //Comprobar existencia de una receta por ID
        public bool Exists(int id)
        {
            return _context.Recetas.Any(e => e.RecetaId == id);
        }
        //Comprobar que un codigo exista
        public bool CodigoExists(string codigo)
        {
            return _context.Recetas.Any(e => e.CodigoUnico == codigo);
        }
        
    }
}

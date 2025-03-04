using MS_RECETAS.Application.Dto;
using MS_RECETAS.Application.ServicesInterfaces;
using MS_RECETAS.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace MS_RECETAS.Controllers
{

    public class RecetaController : ApiController
    {
        private readonly IRecetaService _recetaService;

        // Inyección de dependencias
        public RecetaController(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        // GET: api/Recetas
        [HttpGet]
        public IHttpActionResult GetRecetas()
        {
            return Ok(_recetaService.GetAll());
        }

        // GET: api/Recetas/{id}
        [HttpGet]
        public IHttpActionResult GetReceta(int id)
        {
            try
            {
                return Ok(_recetaService.GetById(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Recetas/Estado/{id}
        [HttpGet]
        [Route("Estado")]
        public IHttpActionResult GetEstadoReceta(int id)
        {
            try
            {
                return Ok(_recetaService.GetEstadosRecetaById(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Recetas
        [HttpPost]
        public async Task<IHttpActionResult> PostReceta([FromBody] RecetaDto nuevaReceta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _recetaService.Create(nuevaReceta))
            {
                return CreatedAtRoute("DefaultApi", new { id = nuevaReceta.RecetaId }, nuevaReceta);
            }
            else { return BadRequest("La receta no se creo"); }

        }

        // PUT: api/Recetas/{id}
        [HttpPut]

        public async Task<IHttpActionResult> PutReceta(int id, RecetaDto recetaActualizada)
        {
            if (!ModelState.IsValid)
                return BadRequest("Faltan campos");

            if (await _recetaService.Update(id, recetaActualizada))
            {
                return Ok("Receta actualizada exitosamente.");
            }
            else
            {
                return NotFound();
            }

        }

        // DELETE: api/Recetas/{id}
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteReceta(int id)
        {
            if (await _recetaService.Delete(id))
            {
                return Ok("Receta eliminada exitosamente.");
            }
            else
            {
                return NotFound();
            }
        }
    }
}

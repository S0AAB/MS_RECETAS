using MS_RECETAS.Application.ServicesInterfaces;
using MS_RECETAS.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using MS_RECETAS.Application.Dto;
using AutoMapper;
using MS_RECETAS.Domain.Models;
namespace MS_RECETAS.Application.Implementation
    {
	    public class RecetaService: IRecetaService
	    {
		    private IRecetasRepository _recetasRepository;
            private PersonaServiceAPI _personaServiceAPI;
            private readonly IMapper _mapper;
            //Inyeccion de dependencias
            public RecetaService(IRecetasRepository recetasRepository, IMapper mapper, PersonaServiceAPI personaServiceAPI)
            {
                _recetasRepository = recetasRepository;
                _personaServiceAPI = personaServiceAPI;
                _mapper = mapper;
            }


            public IEnumerable<RecetaDto> GetAll()
            {
                return _mapper.Map<IEnumerable<RecetaDto>>(_recetasRepository.GetAll());
            }

       
            public RecetaDto GetById(int id)
            {
                return _mapper.Map<RecetaDto>(_recetasRepository.GetById(id));
            }

            public EstadoRecetaDto GetEstadosRecetaById(int id)
            {
                return _mapper.Map<EstadoRecetaDto>(_recetasRepository.GetEstadosRecetaById(id));
            }


            public async Task<bool> Create(RecetaDto receta)
            {   
                //Comprueba que no exista la receta
                if (_recetasRepository.CodigoExists(receta.CodigoUnico)){
                    Debug.WriteLine($"Error: Codigo existente");
                    return false;
                }
                if (!await ValidarPaciente(receta.PacienteId)) {
                    Debug.WriteLine($"Error: IdPaciente no valido");
                    return false;
                }

            
                _recetasRepository.Add(_mapper.Map<Recetas>(receta));
                return true;
            }

            public async Task<bool> Update(int id, RecetaDto receta)
            {
                var recetaExistente = _recetasRepository.GetById(id);
                if (recetaExistente == null)
                {
                    return false; 
                }
            
                recetaExistente.PacienteId = receta.PacienteId;
                recetaExistente.FechaCreacion = receta.FechaCreacion;
                recetaExistente.FechaVencimiento = receta.FechaVencimiento;
                recetaExistente.Descripcion = receta.Descripcion;
                recetaExistente.EstadoId = receta.EstadoId;

                if(!await ValidarPaciente(recetaExistente.PacienteId))
                {
                    return false;
                }

                _recetasRepository.Update(recetaExistente);
                return true;
            }

            public async Task<bool> Delete(int id)
            {
                if (!_recetasRepository.Exists(id))
                {
                    return false;
                }
                var receta= _recetasRepository.GetById(id);
                _recetasRepository.Delete(receta);
                return true;
            }


            //Funcion para validar roles y estado activo antes de crear la receta
            private async Task<bool> ValidarPaciente(int pacienteId)
            {
                PersonaDto paciente = await _personaServiceAPI.ObtenerPersona(pacienteId);
                Debug.WriteLine($"Paciente: {JsonConvert.SerializeObject(paciente)}");
           ;

                // Validar que existan, que sean del tipo correcto (pacient y medico) y que estén activos
                if (paciente == null || paciente.TipoPersonaId != 2  || paciente.Activo != true )
                {
                    return false;
                }

                return true;
            }



        }
    }
using System;


namespace MS_RECETAS.Application.Dto
{
	public class RecetaDto
	{
        public int RecetaId { get; set; }
        public string CodigoUnico { get; set; }
        public int PacienteId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int EstadoId { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using ControlEscolarCore.Controller;


namespace ControlEstudiantesAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesControllerAPI : ControllerBase
    {
        private readonly EstudiantesController _estudiantesController;

        /// <summary>
        /// Obtiene todos los estudiantes con filtros opcionales
        /// </summary>
        /// <param name="soloActivos">Filtrar solo estudiantes activos</param>
        /// <param name="tipoFecha">1=Fecha nacimiento, 2=Fecha alta, 3=Fecha baja</param>
        /// <param name="fechaInicio">Fecha inicial del rango</param>
        /// <param name="fechaFin">Fecha final del rango</param>
        /// <returns>Lista de estudiantes</returns>
        [HttpGet]
        public IActionResult GetEstudiantes(
            [FromQuery] bool soloActivos = true,
            [FromQuery] int tipoFecha = 0,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null)
        {
            try
            {
                var estudiantes = _estudiantesController.ObtenerEstudiantes(
                    soloActivos,
                    tipoFecha,
                    fechaInicio,
                    fechaFin);

                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor" + ex.Message);
            }
        }

    }
}

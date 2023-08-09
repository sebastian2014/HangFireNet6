using HangFireNet6.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IRepositorioPersonas repositorioPersonas;

        public PersonaController(IRepositorioPersonas repositorioPersonas)
        {
            this.repositorioPersonas = repositorioPersonas;
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Crear(string nombrePersona)
        {
            Console.WriteLine($"agregando a la persona {nombrePersona}");
            var persona = new Persona { Nombre = nombrePersona };
            await Task.Delay(2000);
            await repositorioPersonas.Crear(nombrePersona);
            Console.WriteLine($"agregada la persona {nombrePersona}");
            return Ok();
        }

    }
}

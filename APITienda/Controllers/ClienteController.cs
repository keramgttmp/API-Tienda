using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITienda.Models;
using APITienda.Models.Dtos;
using APITienda.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITienda.Controllers
{
    //Hace referencia a api/controller donde controller es el nombre del controlador, 
    //se puede dejar así o cambiarlo por el nombre que en este caso sería CategoriaController.
    [Route("api/[controller]")]
    [ApiController]
    public class Clientecontroller : Controller //hereda del base pero lo dejamos solo Controller
    {
        //instanciamos el repository
        private readonly IClienteRepository _clienteRepository;

        //instanciamos el mapper para no acceder el modelo directamente
        private readonly IMapper _clienteMapper;

        public Clientecontroller( IClienteRepository clienteRepository, IMapper clienteMapper)
        {
            _clienteRepository = clienteRepository;
            _clienteMapper = clienteMapper;
        }

        [HttpGet]
        [Produces(typeof(ClienteDto))]
        public IActionResult GetCliente()
        {
            var listaCliente = _clienteRepository.GetCliente();

            //usamos el DTO
            var listaClienteDto = new List<ClienteDto>();

            foreach ( var lista in listaCliente) 
            {
                //Cargamos la lista DTO con los datos según el mapper realizado en ModelsMapper
                //para el modelo Cliente
                listaClienteDto.Add(_clienteMapper.Map<ClienteDto>(lista));
            }
            return Ok(listaClienteDto);
        }

        [HttpGet("{clienteId:int}", Name = "GetCliente")]
        //Se anota de esta forma porque si se deja solo HTTPGet salta el siguiente error:
        /*Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
        APITienda.Controllers.CategoriaController.GetCliente (APITienda)
        APITienda.Controllers.CategoriaController.GetCliente (APITienda)*/
        public IActionResult GetCliente(int clienteId) 
        {
            var registroCliente = _clienteRepository.GetCliente(clienteId);
            //si no encuentra regresa error
            if (registroCliente == null) 
            {
                return NotFound("Cliente no encontrado");
            }


            //si la encuentra entonces usa el DTO para regresar la estructura del modelo Cliente
            var registroClienteDto = _clienteMapper.Map<ClienteDto>(registroCliente);

            return Ok(registroClienteDto);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] ClienteDto clienteDto)
        // se indica FromBody para que la estructura que se requiere 
        //en el cuerpo de la petición
        //esté vinculada con los datos en el DTO
        //tanto en cantidad como en el nombre de los campos
        {
            //validamos si la info enviada es nula
            if ( clienteDto == null) 
            {
                return BadRequest(ModelState);
            }

            if (_clienteRepository.ExisteCliente (clienteDto.Nombre+clienteDto.PrimerApellido+clienteDto.SegundoApellido)) 
            {
                //Carga el mensaje de error y lo devuelve
                ModelState.AddModelError("", "El cliente con ese nombre ya existe");
                return StatusCode(404, ModelState);
            }

            //uso el Mapper pero en forma inversa
            var cliente = _clienteMapper.Map<Cliente>(clienteDto);

            if (! _clienteRepository.CrearCliente (cliente) )
            {
                //carga mensaje de error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando al cliente {cliente.Nombre + ' ' + cliente.PrimerApellido + ' ' + cliente.SegundoApellido}");
                return StatusCode(404, ModelState);
            }

            //devolvemos el ok pero indicando cuál es el último registro creado
            return CreatedAtRoute("GetCliente", new { clienteId = cliente.Id }, cliente);
        }

        [HttpPatch("{clienteId:int}", Name = "ActualizarCliente")]
        public IActionResult ActualizarCliente(int clienteId, [FromBody] ClienteDto clienteDto)
        {
            //Validamos la información enviada
            //si es nulo regresa error o el id no corresponde al id del DTO
            if (clienteDto == null || clienteId != clienteDto.Id)
            {
                return BadRequest(ModelState);
            }

            clienteDto.FechaActualizacion = DateTime.Now;

            var cliente = _clienteMapper.Map<Cliente>(clienteDto);
            //Uso el Mapper pero en este caso de forma inversa
            if (!_clienteRepository.ActualizarCliente(cliente))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {cliente.Nombre + ' ' + cliente.PrimerApellido + ' ' + cliente.SegundoApellido}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{clienteId:int}", Name = "BorrarCliente")]
        public IActionResult BorrarCliente(int clienteId)
        {
            //Validamos la información enviada
            //si existe la categoria
            if (!_clienteRepository.ExisteCliente(clienteId))
            {
                return NotFound();
            }

            var cliente = _clienteRepository.GetCliente(clienteId);

            if (!_clienteRepository.BorrarCliente(cliente))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal borrando el registro el registro {cliente.Nombre + ' ' + cliente.PrimerApellido + ' ' + cliente.SegundoApellido}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }


}

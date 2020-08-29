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
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : Controller
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IClienteRepository _clienteRepository;

        //instanciamos el mapper para no acceder el modelo directamente
        private readonly IMapper _ordenMapper;

        public OrdenController(IOrdenRepository ordenRepository, IClienteRepository clienteRepository, IMapper ordenMapper)
        {
            _ordenRepository = ordenRepository;
            _clienteRepository = clienteRepository;
            _ordenMapper = ordenMapper;
        }

        [HttpGet]
        [Produces(typeof(OrdenDto))]

        public IActionResult GetOrden()
        {
            var listaOrden = _ordenRepository.GetOrden();
            //aplicamos el DTO

            var listaOrdenDto = new List<OrdenDto>();

            foreach ( var lista in listaOrden)
            { //se aplica la estructura usando el Mapper
                listaOrdenDto.Add(_ordenMapper.Map<OrdenDto>(lista));
            }
            return Ok(listaOrdenDto);
        }

        [HttpGet("{ordenId:int}", Name = "GetOrden")]
        //Se anota de esta forma porque si se deja solo HTTPGet salta el siguiente error:
        /*Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
        APITienda.Controllers.OrdenController.GetOrden (APITienda)
        APITienda.Controllers.OrdenController.GetOrden (APITienda)*/
        public IActionResult GetOrden(int ordenId)
        {
            //buscamos la orden por id
            var itemOrden = _ordenRepository.GetOrden(ordenId);

            //si no encuentra regresa error
            if (itemOrden == null)
            {
                return NotFound("Orden no encontrada");
            }

            //si la encuentra entonces busca en el DTO para regresar la estructura que corresponde
            var itemOrdenDto = _ordenMapper.Map<OrdenDto>(itemOrden);

            return Ok(itemOrdenDto);

        }

        [HttpPost]
        public IActionResult CrearOrden([FromBody] OrdenDto ordenDto)
        // se indica FromBody para que la estructura que se requiere en el cuerpo de la petición
        //esté vinculada con los datos en el DTO
        //tanto en cantidad como en el nombre de los campos
        {
            //Validamos la información enviada
            //si es nulo regresa error
            if (ordenDto == null)
            { return BadRequest(ModelState); }

            //Verifico si existe el cliente
            if (!_clienteRepository.ExisteCliente(ordenDto.ClienteId))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"El cliente con el id {ordenDto.ClienteId} no existe");
                return StatusCode(404, ModelState);

            }

            //Valida si ya existe una orden con esa misma descripción
            if (_ordenRepository.ExisteOrden(ordenDto.Id))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", "La orden con esa id ya existe");
                return StatusCode(404, ModelState);
            }

            //Uso el Mapper pero en este caso de forma inversa
            var orden = _ordenMapper.Map<Orden>(ordenDto);

            if (!_ordenRepository.CrearOrden(orden))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {orden.Id}");
                return StatusCode(404, ModelState);
            }

            //devolvemos el ok, pero indicando cuál es el último registro creado de la siguiente forma usando el GetOrden
            return CreatedAtRoute("GetOrden", new { ordenId = orden.Id }, orden);
        }

        [HttpPatch("{ordenId:int}", Name = "ActualizarOrden")]
        public IActionResult ActualizarOrden(int ordenId, [FromBody] OrdenDto ordenDto)
        {
            //Validamos la información enviada
            //si es nulo regresa error o el id no corresponde al id del DTO
            if (ordenDto == null || ordenId != ordenDto.Id)
            {
                return BadRequest(ModelState);
            }

            ordenDto.FechaActualizacion = DateTime.Now;

            var orden = _ordenMapper.Map<Orden>(ordenDto);
            //Uso el Mapper pero en este caso de forma inversa

            if (!_ordenRepository.ActualizarOrden(orden))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {orden.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

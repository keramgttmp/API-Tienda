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
    //se puede dejar así o cambiarlo por el nombre que en este caso sería OpinionProductoController.
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionProductoController : Controller //hereda del base pero lo dejamos solo Controller
    {
        private readonly IOpinionProductoRepository _opinionproductoRepository;

        //instanciamos el mapper para no acceder el modelo directamente
        private readonly IMapper _opinionproductoMapper;

        public OpinionProductoController(IOpinionProductoRepository opinionproductoRepository, IMapper opinionproductoMapper)
        {
            _opinionproductoRepository = opinionproductoRepository;
            _opinionproductoMapper = opinionproductoMapper;
        }

        /// <summary>
        /// Obtiene todas las opiniones de los productos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(OpinionProductoDto))]
        public IActionResult GetOpinionProducto()
        {
            var listaOpinionProducto = _opinionproductoRepository.GetOpinionProducto();
            //usamos el DTO
            var listaOpinionProductoDto = new List<OpinionProductoDto>();

            foreach (var lista in listaOpinionProducto)
            {
                //Cargamos la lista DTO con los datos según el mapper realizado en ModelsMapper 
                //para la entidad OpinionProducto
                listaOpinionProductoDto.Add(_opinionproductoMapper.Map<OpinionProductoDto>(lista));
            }
            return Ok(listaOpinionProductoDto);
        }

        /// <summary>
        /// Obtiene una opinión con base en un id dado
        /// </summary>
        /// <param name="opinionproductoId"></param>
        /// <returns></returns>
        [HttpGet("{opinionproductoId:int}", Name = "GetOpinionProducto")]
        //Se anota de esta forma porque si se deja solo HTTPGet salta el siguiente error:
        /*Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
        APITienda.Controllers.OpinionProductoController.GetOpinionProducto (APITienda)
        APITienda.Controllers.OpinionProductoController.GetOpinionProducto (APITienda)*/
        public IActionResult GetOpinionProducto(int opinionproductoId)
        {
            //buscamos la opinionproducto por id
            var itemOpinionProducto = _opinionproductoRepository.GetOpinionProducto(opinionproductoId);

            //si no encuentra regresa error
            if (itemOpinionProducto == null)
            {
                return NotFound("OpinionProducto no encontrada");
            }

            //si la encuentra entonces busca en el DTO para regresar la estructura que corresponde
            var itemOpinionProductoDto = _opinionproductoMapper.Map<OpinionProductoDto>(itemOpinionProducto);

            return Ok(itemOpinionProductoDto);

        }


        /// <summary>
        /// Obtiene todos las opiniones de un producto
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorProducto")]
        //Se anota de esta forma porque si se deja solo HTTPGet salta el siguiente error:
        /*Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
        APITienda.Controllers.OpinionProductoController.GetOpinionProducto (APITienda)
        APITienda.Controllers.OpinionProductoController.GetOpinionProducto (APITienda)*/
        public IActionResult GetOpinionPorProducto(int productoId)
        {
            var listaOpinionProducto = _opinionproductoRepository.GetOpinionPorProducto(productoId);

            var listaOpinionProductoDto = new List<OpinionProductoDto>();

            foreach (var lista in listaOpinionProducto)
            {
                //Cargamos la lista DTO con los datos según el mapper realizado en ModelsMapper 
                //para la entidad Categoria
                listaOpinionProductoDto.Add(_opinionproductoMapper.Map<OpinionProductoDto>(lista));
            }
            return Ok(listaOpinionProductoDto);

        }

        /// <summary>
        /// Crea una opinión de un producto
        /// </summary>
        /// <param name="opinionproductoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Crear([FromBody] OpinionProductoDto opinionproductoDto)
        // se indica FromBody para que la estructura que se requiere en el cuerpo de la petición
        //esté vinculada con los datos en el DTO
        //tanto en cantidad como en el nombre de los campos
        {
            //Validamos la información enviada
            //si es nulo regresa error
            if (opinionproductoDto == null)
            { return BadRequest(ModelState); }

            //Valida si ya existe una opinión del producto con esa misma descripción
            if (_opinionproductoRepository.ExisteOpinionProducto(opinionproductoDto.Id))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", "La opinión del producto con ese identificador ya existe");
                return StatusCode(404, ModelState);
            }

            //Uso el Mapper pero en este caso de forma inversa
            var opinionproducto = _opinionproductoMapper.Map<OpinionProducto>(opinionproductoDto);

            if (!_opinionproductoRepository.CrearOpinionProducto(opinionproducto))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {opinionproducto.Id}");
                return StatusCode(404, ModelState);
            }

            //devolvemos el ok, pero indicando cuál es el último registro creado de la siguiente forma usando el GetOpinionProducto
            return CreatedAtRoute("GetOpinionProducto", new { opinionproductoId = opinionproducto.Id }, opinionproducto);
        }

        /// <summary>
        /// Actualizar una opinión dada según un id
        /// </summary>
        /// <param name="opinionproductoId"></param>
        /// <param name="opinionproductoDto"></param>
        /// <returns></returns>
        [HttpPatch("{opinionproductoId:int}", Name = "ActualizarOpinionProducto")]
        public IActionResult ActualizarOpinionProducto (int opinionproductoId, [FromBody] OpinionProductoDto opinionproductoDto)
        {
            //Validamos la información enviada
            //si es nulo regresa error o el id no corresponde al id del DTO
            if (opinionproductoDto == null || opinionproductoId != opinionproductoDto.Id)
            { 
                return BadRequest(ModelState); 
            }

            opinionproductoDto.FechaActualizacion = DateTime.Now;

            var opinionproducto = _opinionproductoMapper.Map<OpinionProducto>(opinionproductoDto);
            //Uso el Mapper pero en este caso de forma inversa

            if (!_opinionproductoRepository.ActualizarOpinionProducto(opinionproducto))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {opinionproducto.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Borra una opinión dada según un id
        /// </summary>
        /// <param name="opinionproductoId"></param>
        /// <returns></returns>
        [HttpDelete("{opinionproductoId:int}", Name = "BorrarOpinionProducto")]
        public IActionResult BorrarOpinionProducto(int opinionproductoId)
        {
            //Validamos la información enviada
            //si existe la opinionproducto
            if (!_opinionproductoRepository.ExisteOpinionProducto(opinionproductoId))
            {
                return NotFound();
            }

            var opinionproducto = _opinionproductoRepository.GetOpinionProducto(opinionproductoId);
            
            if (!_opinionproductoRepository.BorrarOpinionProducto(opinionproducto))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {opinionproducto.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

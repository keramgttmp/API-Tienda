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
    public class DetalleOrdenController : Controller
    {
        private readonly IDetalleOrdenRepository _detalleordenRepository;
        private readonly IProductoRepository _productoRepository;

        //instanciamos el mapper para no acceder el modelo directamente
        private readonly IMapper _detalleordenMapper;

        public DetalleOrdenController(IDetalleOrdenRepository detalleordenRepository, IProductoRepository productoRepository, IMapper detalleordenMapper)
        {
            _detalleordenRepository = detalleordenRepository;
            _productoRepository = productoRepository;
            _detalleordenMapper = detalleordenMapper;
        }

        /// <summary>
        /// Obtiene todos los detalles de órdenes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(DetalleOrdenDto))]

        public IActionResult GetDetalle()
        {
            var listaDetalle = _detalleordenRepository.GetDetalle();
            //aplicamos el DTO

            var listaDetalleDto = new List<DetalleOrdenDto>();

            foreach ( var lista in listaDetalle)
            { //se aplica la estructura usando el Mapper
                listaDetalleDto.Add(_detalleordenMapper.Map<DetalleOrdenDto>(lista));
            }
            return Ok(listaDetalleDto);
        }

        /// <summary>
        /// Obtiene los detalles de una orden en particular
        /// </summary>
        /// <param name="detalleordenId"></param>
        /// <returns></returns>
        [HttpGet("{detalleordenId:int}", Name = "GetDetalle")]
        //Se anota de esta forma porque si se deja solo HTTPGet salta el siguiente error:
        /*Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
        APITienda.Controllers.OrdenController.GetOrden (APITienda)
        APITienda.Controllers.OrdenController.GetOrden (APITienda)*/
        public IActionResult GetOrden(int detalleordenId)
        {
            //buscamos la orden por id
            var itemDetalleOrden = _detalleordenRepository.GetDetalle(detalleordenId);

            //si no encuentra regresa error
            if (itemDetalleOrden == null)
            {
                return NotFound("Detalle de orden no encontrada");
            }

            //si la encuentra entonces busca en el DTO para regresar la estructura que corresponde
            var itemDetalleOrdenDto = _detalleordenMapper.Map<DetalleOrdenDto>(itemDetalleOrden);

            return Ok(itemDetalleOrdenDto);

        }

        /// <summary>
        /// Crea el detalle de una orden
        /// </summary>
        /// <param name="detalleordenDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CrearDetalle([FromBody] DetalleOrdenDto detalleordenDto)
        // se indica FromBody para que la estructura que se requiere en el cuerpo de la petición
        //esté vinculada con los datos en el DTO
        //tanto en cantidad como en el nombre de los campos
        {
            //Validamos la información enviada
            //si es nulo regresa error
            if (detalleordenDto == null)
            { return BadRequest(ModelState); }

            //Valida si ya existe una orden con esa misma descripción
            if (_detalleordenRepository.ExisteDetalle(detalleordenDto.Id))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", "El detalle de la orden con esa id ya existe");
                return StatusCode(404, ModelState);
            }

            //Validamos si el producto existe
            if (!_productoRepository.ExisteProducto(detalleordenDto.ProductoId))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"El producto con el id {detalleordenDto.ProductoId} no existe");
                return StatusCode(404, ModelState);
            }

            //Uso el Mapper pero en este caso de forma inversa
            var detalleorden = _detalleordenMapper.Map<DetalleOrden>(detalleordenDto);

            if (!_detalleordenRepository.CrearDetalle(detalleorden))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {detalleorden.Id}");
                return StatusCode(404, ModelState);
            }

            //Actualizamos la cantida de los productos disponibles
            //buscamos el producto y lo enviamos a actualizar
            var itemProducto = _productoRepository.GetProducto(detalleorden.ProductoId);
            itemProducto.Cantidad = itemProducto.Cantidad - detalleorden.CantidadDetalle;
            if (!_productoRepository.Actualizar(itemProducto))
            {
                //carga el mensaje de error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal al modificar el registro {itemProducto.Descripcion}");
                return StatusCode(500, ModelState);
            }

            //devolvemos el ok, pero indicando cuál es el último registro creado de la siguiente forma usando el GetOrden
            return CreatedAtRoute("GetDetalle", new { detalleordenId = detalleorden.Id }, detalleorden);
        }

        /// <summary>
        /// Actualiza el detalle de una orden
        /// </summary>
        /// <param name="detalleordenId"></param>
        /// <param name="detalleordenDto"></param>
        /// <returns></returns>
        [HttpPatch("{detalleordenId:int}", Name = "ActualizarDetalle")]
        public IActionResult ActualizarDetalle(int detalleordenId, [FromBody] DetalleOrdenDto detalleordenDto)
        {
            //Validamos la información enviada
            //si es nulo regresa error o el id no corresponde al id del DTO
            if (detalleordenDto == null || detalleordenId != detalleordenDto.Id)
            {
                return BadRequest(ModelState);
            }

            detalleordenDto.FechaActualizacion = DateTime.Now;

            var detalleorden = _detalleordenMapper.Map<DetalleOrden>(detalleordenDto);
            //Uso el Mapper pero en este caso de forma inversa

            if (!_detalleordenRepository.ActualizarDetalle(detalleorden))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {detalleorden.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

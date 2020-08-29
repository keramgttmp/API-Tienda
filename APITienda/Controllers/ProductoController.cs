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
    public class ProductoController : Controller //hereda del base pero lo dejamos solo Controller
    {
        private readonly IProductoRepository _productoRepository;

        //instanciamos el mapper para no acceder el modelo directamente
        private readonly IMapper _productoMapper;
        public ProductoController(IProductoRepository productoRepository, IMapper productoMapper)
        {
            _productoRepository = productoRepository;
            _productoMapper = productoMapper;

        }

        [HttpGet]
        [Produces(typeof(ProductoDto))]
        public IActionResult GetProducto()
        {
            var listaProducto = _productoRepository.GetProducto();
            //usamos el dto

            var listaProductoDto = new List<ProductoDto>();

            foreach (var lista in listaProducto)
            {
                //cargamos la lista dto con los datos del modelo según el Mapper para Producto
                listaProductoDto.Add(_productoMapper.Map<ProductoDto>(lista));
            }

            return Ok(listaProductoDto);
        }

        [HttpGet("{id:int}", Name = "GetProducto")]
        //Se anota de esta forma porque si se deja solo HTTPGet salta el siguiente error:
        /*Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
        APITienda.Controllers.CategoriaController.GetCategoria (APITienda)
        APITienda.Controllers.CategoriaController.GetCategoria (APITienda)*/
        public IActionResult GetProducto(int id)
        {
            //buscamos el producto por le id
            var itemProducto = _productoRepository.GetProducto(id);

            //si no se encuentre se regresa error
            if (itemProducto == null)
            {
                return NotFound("Producto no encontrado");
            }

            //Si la encuentra en usa el mapper para aplica el DTO

            var itemProductoDto = _productoMapper.Map<ProductoDto>(itemProducto);

            return Ok(itemProductoDto);
        }

        [HttpGet("BuscarPorCategoria")]
        public IActionResult ObtenerProductoPorCategoria(int categoriaId)
        {
            var listaProducto = _productoRepository.GetProductoPorCategoria(categoriaId);

            var listaProductoDto = new List<ProductoDto>();

            foreach (var lista in listaProducto)
            {
                //Cargamos la lista DTO con los datos según el mapper realizado en ModelsMapper 
                //para la entidad Categoria
                listaProductoDto.Add(_productoMapper.Map<ProductoDto>(lista));
            }
            return Ok(listaProductoDto);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] ProductoDto productoDto)
        // se indica FromBody para que la estructura que se requiere en el cuerpo de la petición
        //esté vinculada con los datos en el DTO
        //tanto en cantidad como en el nombre de los campos
        {
            //NOTA MENTAL: Resolver con DTO particular la inserción pues da error si se envía 
            //el valor del pk pues es un identiy
            //Validamos la información enviada
            //si es nulo regresa error
            if (productoDto == null)
            { return BadRequest(ModelState); }

            //Valida si ya existe un producto con esa misma descripción
            if (_productoRepository.ExisteProducto(productoDto.Descripcion))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", "El producto con esa descripción ya existe");
                return StatusCode(404, ModelState);
            }

            //Uso el Mapper pero en este caso de forma inversa
            var producto = _productoMapper.Map<Producto>(productoDto);

            if (!_productoRepository.Crear(producto))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {producto.Descripcion}");
                return StatusCode(404, ModelState);
            }

            //devolvemos el ok, pero indicando cuál es el último registro creado de la siguiente forma usando el GetProducto
            return CreatedAtRoute("GetProducto", new { Id = producto.Id }, producto);
        }

        [HttpPatch("{id:int}", Name = "ActualizarProducto")]
        public IActionResult ActualizarProducto(int id, [FromBody] ProductoDto productoDto)
        { 
            //NOTA MENTAL AVERIGUAR POR QUÉ DEBO ENVIARLO EN EL PARÁMETRO Y EN EL JSON
            //Validamos la información enviada
            //si es nulo o el id no corresponde devuelve error
            if (productoDto == null || id != productoDto.Id)
            {
                return BadRequest(ModelState);
            }

            productoDto.FechaActualizacion = DateTime.Now;

            //aplica el mapper en forma inversa para aplicar la estructura
            var producto = _productoMapper.Map<Producto>(productoDto);

            if (!_productoRepository.Actualizar(producto) )
            {
                //carga el mensaje de error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal al modificar el registro {producto.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "BorrarProducto")]
        public IActionResult BorrarProducto(int id)
        {
            //Validamos la información enviada
            //si existe el producto
            if (!_productoRepository.ExisteProducto(id))
            {
                return NotFound();
            }

            var producto = _productoRepository.GetProducto(id);

            if (!_productoRepository.Borrar(producto))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {producto.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    } //final de la clase
}//final del namespace

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
    public class CategoriaController : Controller //hereda del base pero lo dejamos solo Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        //instanciamos el mapper para no acceder el modelo directamente
        private readonly IMapper _categoriaMapper;

        public CategoriaController(ICategoriaRepository categoriaRepository, IMapper categoriaMapper)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaMapper = categoriaMapper;
        }

        [HttpGet]
        [Produces(typeof(CategoriaDto))]
        public IActionResult GetCategoria()
        {
            var listaCategoria = _categoriaRepository.GetCategoria();
            //usamos el DTO
            var listaCategoriaDto = new List<CategoriaDto>();

            foreach (var lista in listaCategoria)
            {
                //Cargamos la lista DTO con los datos según el mapper realizado en ModelsMapper 
                //para la entidad Categoria
                listaCategoriaDto.Add(_categoriaMapper.Map<CategoriaDto>(lista));
            }
            return Ok(listaCategoriaDto);
        }


        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        //Se anota de esta forma porque si se deja solo HTTPGet salta el siguiente error:
        /*Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
        APITienda.Controllers.CategoriaController.GetCategoria (APITienda)
        APITienda.Controllers.CategoriaController.GetCategoria (APITienda)*/
        public IActionResult GetCategoria(int categoriaId)
        {
            //buscamos la categoria por id
            var itemCategoria = _categoriaRepository.GetCategoria(categoriaId);

            //si no encuentra regresa error
            if (itemCategoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            //si la encuentra entonces busca en el DTO para regresar la estructura que corresponde
            var itemCategoriaDto = _categoriaMapper.Map<CategoriaDto>(itemCategoria);

            return Ok(itemCategoriaDto);

        }

        [HttpPost]
        public IActionResult Crear([FromBody] CategoriaDto categoriaDto)
        // se indica FromBody para que la estructura que se requiere en el cuerpo de la petición
        //esté vinculada con los datos en el DTO
        //tanto en cantidad como en el nombre de los campos
        {
            //Validamos la información enviada
            //si es nulo regresa error
            if (categoriaDto == null)
            { return BadRequest(ModelState); }

            //Valida si ya existe una categoría con esa misma descripción
            if (_categoriaRepository.ExisteCategoria(categoriaDto.Descripcion))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", "La categoría con esa descripción ya existe");
                return StatusCode(404, ModelState);
            }

            //Uso el Mapper pero en este caso de forma inversa
            var categoria = _categoriaMapper.Map<Categoria>(categoriaDto);

            if (!_categoriaRepository.Crear(categoria))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {categoria.Descripcion}");
                return StatusCode(404, ModelState);
            }

            //devolvemos el ok, pero indicando cuál es el último registro creado de la siguiente forma usando el GetCategoria
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
        }

        [HttpPatch("{categoriaId:int}", Name = "Actualizar")]
        public IActionResult Actualizar (int categoriaId, [FromBody] CategoriaDto categoriaDto)
        {
            //Validamos la información enviada
            //si es nulo regresa error o el id no corresponde al id del DTO
            if (categoriaDto == null || categoriaId != categoriaDto.Id)
            { 
                return BadRequest(ModelState); 
            }

            categoriaDto.FechaActualizacion = DateTime.Now;

            var categoria = _categoriaMapper.Map<Categoria>(categoriaDto);
            //Uso el Mapper pero en este caso de forma inversa

            if (!_categoriaRepository.Actualizar(categoria))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {categoria.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoriaId:int}", Name = "Borrar")]
        public IActionResult Borrar(int categoriaId)
        {
            //Validamos la información enviada
            //si existe la categoria
            if (!_categoriaRepository.ExisteCategoria(categoriaId))
            {
                return NotFound();
            }

            var categoria = _categoriaRepository.GetCategoria(categoriaId);
            
            if (!_categoriaRepository.Borrar(categoria))
            {
                //Carga el mensaje error y lo devuelve
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {categoria.Descripcion}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

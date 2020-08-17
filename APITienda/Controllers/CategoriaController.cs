using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CategoriaController( ICategoriaRepository categoriaRepository, IMapper categoriaMapper)
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
    }
}

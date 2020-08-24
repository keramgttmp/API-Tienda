using APITienda.Models;
using APITienda.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.ModelsMappers
{
    //con la referencia a AutoMapper puedo usar la herencia desde Profile
    public class ModelsMapper : Profile
    {
        public ModelsMapper()
        {
            //Se debe hacer un CreateMap para cada entidad
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<OpinionProducto, OpinionProductoDto>().ReverseMap();
            CreateMap<Orden, OrdenDto>().ReverseMap();
            CreateMap<DetalleOrden, DetalleOrdenDto>().ReverseMap();
        }
    }
}

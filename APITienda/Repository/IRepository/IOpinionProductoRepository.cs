using APITienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository.IRepository
{
    public interface IOpinionProductoRepository 
    {
        ICollection<OpinionProducto> GetOpinionProducto();

        ICollection<OpinionProducto> GetOpinionPorProducto(int productoId);

        OpinionProducto GetOpinionProducto(int id);

        bool ExisteOpinionProducto(int id);

        bool CrearOpinionProducto(OpinionProducto opinionproducto);

        bool ActualizarOpinionProducto(OpinionProducto opinionproducto);

        bool BorrarOpinionProducto(OpinionProducto opinionproducto);

        bool Guardar();
    }
}

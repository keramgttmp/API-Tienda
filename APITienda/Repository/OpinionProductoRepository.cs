using APITienda.Data;
using APITienda.Models;
using APITienda.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository
{
    public class OpinionProductoRepository : IOpinionProductoRepository
    {
        //el dbcontext
        private readonly ApplicationDbContext _DbContext;

        public OpinionProductoRepository( ApplicationDbContext dbContext)
        {
            //paso el valor del contexto por parámetro y así lo puedo usar en la clase.
            _DbContext = dbContext;
        }

        public bool ActualizarOpinionProducto(OpinionProducto opinionproducto)
        {
            _DbContext.OpinionProducto.Update(opinionproducto);
            return Guardar();
        }

        public bool BorrarOpinionProducto(OpinionProducto opinionproducto)
        {
            _DbContext.OpinionProducto.Remove(opinionproducto);
            return Guardar();
        }

        public bool CrearOpinionProducto(OpinionProducto opinionproducto)
        {
            _DbContext.OpinionProducto.Add(opinionproducto);
            return Guardar();
        }

        public bool ExisteOpinionProducto(int id)
        {
            bool valor = _DbContext.OpinionProducto.Any(c => c.Id == id);
            return valor;
        }

        public ICollection<OpinionProducto> GetOpinionProducto()
        {
            return _DbContext.OpinionProducto.OrderBy(c => c.Id).ToList();
        }

        public OpinionProducto GetOpinionProducto(int id)
        {
            return _DbContext.OpinionProducto.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<OpinionProducto> GetOpinionPorProducto(int productoId)
        {
            var lista = _DbContext.OpinionProducto.Where(c => c.ProductoId == productoId).ToList();
            return (lista);

        }

        public bool Guardar()
        {
            return _DbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}

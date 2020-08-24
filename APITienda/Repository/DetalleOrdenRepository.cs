using APITienda.Data;
using APITienda.Models;
using APITienda.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository
{
    public class DetalleOrdenRepository : IDetalleOrdenRepository
    {
        // Para instanciar el contexto de la base de datos
        private readonly ApplicationDbContext _DbContext;

        public DetalleOrdenRepository(ApplicationDbContext dbContext)
        {
            //paso el valor del contexto por parámetro y así lo puedo usar en la clase.
            _DbContext = dbContext;
        }
        public bool ActualizarDetalle(DetalleOrden detalle)
        {
            _DbContext.DetalleOrden.Update(detalle);
            return Guardar();
        }

        /*public bool BorrarOrden(Orden orden)
        {
            _DbContext.Orden.Remove(orden);
            return Guardar();
        }*/

        public bool CrearDetalle(DetalleOrden detalle)
        {
            _DbContext.DetalleOrden.Add(detalle);
            return Guardar();
        }

        public bool ExisteDetalle(int id)
        {
            bool resultado = _DbContext.DetalleOrden.Any(c => c.Id == id);
            return resultado;
        }

        public ICollection<DetalleOrden> GetDetalle()
        {
            return _DbContext.DetalleOrden.OrderBy(c => c.Id).ToList();
        }

        public DetalleOrden GetDetalle(int id)
        {
            return _DbContext.DetalleOrden.FirstOrDefault(c => c.Id == id);
        }

        public bool Guardar()
        {
            return _DbContext.SaveChanges() >= 0 ? true : false;
        }
    };
}

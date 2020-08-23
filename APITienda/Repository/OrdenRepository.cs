using APITienda.Data;
using APITienda.Models;
using APITienda.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository
{
    public class OrdenRepository : IOrdenRepository
    {
        // Para instanciar el contexto de la base de datos
        private readonly ApplicationDbContext _DbContext;

        public OrdenRepository(ApplicationDbContext dbContext)
        {
            //paso el valor del contexto por parámetro y así lo puedo usar en la clase.
            _DbContext = dbContext;
        }
        public bool ActualizarOrden(Orden orden)
        {
            _DbContext.Orden.Update(orden);
            return Guardar();
        }

        /*public bool BorrarOrden(Orden orden)
        {
            _DbContext.Orden.Remove(orden);
            return Guardar();
        }*/

        public bool CrearOrden(Orden orden)
        {
            _DbContext.Orden.Add(orden);
            return Guardar();
        }

        public bool ExisteOrden(int id)
        {
            bool resultado = _DbContext.Orden.Any(c => c.Id == id);
            return resultado;
        }

        public bool ExisteOrdenPendiente(string estado)
        {
            bool resultado = _DbContext.Orden.Any(c => c.Estado == 'P');
            return resultado;
        }

        public ICollection<Orden> GetOrden()
        {
            return _DbContext.Orden.OrderBy(c => c.Id).ToList();
        }

        public Orden GetOrden(int id)
        {
            return _DbContext.Orden.FirstOrDefault(c => c.Id == id);
        }

        public bool Guardar()
        {
            return _DbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}

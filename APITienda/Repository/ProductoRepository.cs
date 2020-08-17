using APITienda.Data;
using APITienda.Models;
using APITienda.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        // Para instanciar el contexto de la base de datos
        private readonly ApplicationDbContext _DbContext;

        public ProductoRepository(ApplicationDbContext dbContext)
        {
            //paso el valor del contexto por parámetro y así lo puedo usar en la clase.
            _DbContext = dbContext;
        }

        public bool Actualizar(Producto producto)
        {
            _DbContext.Producto.Update(producto);
            return Guardar();
        }

        public bool Borrar(Producto producto)
        {
            _DbContext.Producto.Remove(producto);
            return Guardar();
        }

        public bool Crear(Producto producto)
        {
            _DbContext.Producto.Add(producto);
            return Guardar();

        }

        public bool ExisteProducto(string descripcion)
        {
            bool valor = _DbContext.Producto.Any(p => p.Descripcion.ToLower().Trim() == descripcion.ToLower().Trim());
            return valor;

        }

        public bool ExisteProducto(int id)
        {
            bool valor = _DbContext.Producto.Any(p => p.Id == id);
            return valor;
        }

        public ICollection<Producto> GetProducto()
        {
            return _DbContext.Producto.OrderBy(p => p.Id).ToList();
        }

        public Producto GetProducto(int id)
        {
            return _DbContext.Producto.FirstOrDefault(p => p.Id == id);
        }

        public ICollection<Producto> GetProductoPorCategoria(int categoriaId)
        {
            //var lista = _DbContext.Producto.Include("Categoria").Where (ca => ca.Id== categoriaId).ToList();
            var lista = _DbContext.Producto.Where(p => p.CategoriaId == categoriaId).ToList();
            return (lista);
        }

        public bool Guardar()
        {
            return _DbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}

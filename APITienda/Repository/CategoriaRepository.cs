using APITienda.Data;
using APITienda.Models;
using APITienda.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        // Para instanciar el contexto de la base de datos
        private readonly ApplicationDbContext _DbContext;

        public CategoriaRepository( ApplicationDbContext dbContext)
        {
            //paso el valor del contexto por parámetro y así lo puedo usar en la clase.
            _DbContext = dbContext;
        }
        public bool Actualizar(Categoria categoria)
        {
            _DbContext.Categoria.Update(categoria);
            return Guardar();
        }

        public bool Borrar(Categoria categoria)
        {
            _DbContext.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool Crear(Categoria categoria)
        {
            _DbContext.Categoria.Add(categoria);
            return Guardar();

        }

        public bool ExisteCategoria(string descripcion)
        {
            bool valor = _DbContext.Categoria.Any(c => c.Descripcion.ToLower().Trim() == descripcion.ToLower().Trim());
            return valor;
        }

        public bool ExisteCategoria(int id)
        {
            bool valor = _DbContext.Categoria.Any(c => c.Id == id);
            return valor;
        }

        public ICollection<Categoria> GetCategoria()
        {
            return _DbContext.Categoria.OrderBy(c => c.Id).ToList();
        }

        public Categoria GetCategoria(int id)
        {
            return _DbContext.Categoria.FirstOrDefault(c => c.Id == id);
        }

        public bool Guardar()
        {
            return _DbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}

using APITienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository.IRepository
{
    //Contiene los métodos que utilizaremos para acceder a la entidad (tabla)
    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategoria();

        Categoria GetCategoria(int id);

        ICollection<Categoria> GetCategoriaPorDescripcion(string descripcion);

        bool ExisteCategoria(string descripcion);

        bool ExisteCategoria(int id);

        bool Crear(Categoria categoria);

        bool Actualizar(Categoria categoria);

        bool Borrar(Categoria categoria);

        bool Guardar();

    }
}

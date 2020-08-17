using APITienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository.IRepository
{
    //Contiene los métodos que utilizaremos para acceder a la entidad (tabla)
    public interface IProductoRepository
    {
        ICollection<Producto> GetProducto();

        ICollection<Producto> GetProductoPorCategoria(int categoriaId);

        Producto GetProducto(int id);

        bool ExisteProducto(string descripcion);

        bool ExisteProducto(int id);

        bool Crear(Producto producto);

        bool Actualizar(Producto producto);

        bool Borrar(Producto producto);

        bool Guardar();

    }
}

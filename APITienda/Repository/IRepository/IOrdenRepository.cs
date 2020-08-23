using APITienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository.IRepository
{
    public interface IOrdenRepository
    {
        ICollection<Orden> GetOrden();

        Orden GetOrden(int id);

        bool ExisteOrdenPendiente(string estado);

        bool ExisteOrden(int id);

        bool CrearOrden(Orden categoria);

        bool ActualizarOrden(Orden categoria);

        /*bool BorrarOrden(Orden categoria);*/

        bool Guardar();
    }
}

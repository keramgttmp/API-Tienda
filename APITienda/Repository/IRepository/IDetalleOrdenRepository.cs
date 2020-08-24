using APITienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository.IRepository
{
    public interface IDetalleOrdenRepository
    {
        ICollection<DetalleOrden> GetDetalle();

        DetalleOrden GetDetalle(int id);

        bool ExisteDetalle(int id);

        bool CrearDetalle(DetalleOrden detalleOrden);

        bool ActualizarDetalle(DetalleOrden detalleOrden);

        /*bool BorrarOrden(Orden categoria);*/

        bool Guardar();
    }
}

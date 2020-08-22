using APITienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository.IRepository
{
    public interface IClienteRepository
    {
        ICollection<Cliente> GetCliente();

        Cliente GetCliente(int id);

        bool ExisteCliente(string nombrecompleto);

        bool ExisteCliente(int id);

        bool CrearCliente(Cliente cliente);

        bool ActualizarCliente(Cliente cliente);

        bool BorrarCliente(Cliente cliente);

        bool Guardar();

    }
}

using APITienda.Data;
using APITienda.Models;
using APITienda.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        // Para instanciar el contexto de la base de datos
        private readonly ApplicationDbContext _DbContext;

        public ClienteRepository( ApplicationDbContext dbContext)
        {
            //paso el valor del contexto por parámetro y así lo puedo usar en la clase.
            _DbContext = dbContext;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            _DbContext.Cliente.Update(cliente);
            return Guardar();
        }

        public bool BorrarCliente(Cliente cliente)
        {
            _DbContext.Cliente.Remove(cliente);
            return Guardar();
        }

        public bool CrearCliente(Cliente cliente)
        {
            _DbContext.Cliente.Add(cliente);
            return Guardar();
        }

        public bool ExisteCliente(string nombrecompleto)
        {
            bool valor = _DbContext.Cliente.Any(c => (c.Nombre + c.PrimerApellido + c.SegundoApellido).ToLower().Trim() == nombrecompleto.ToLower().Trim());
            return valor;
        }

        public bool ExisteCliente(int id)
        {
            bool valor = _DbContext.Cliente.Any(c => c.Id == id);
            return valor;
        }

        public ICollection<Cliente> GetCliente()
        {
            return _DbContext.Cliente.OrderBy(c => c.PrimerApellido + c.SegundoApellido + c.Nombre).ToList();
        }

        public Cliente GetCliente(int id)
        {
            return _DbContext.Cliente.FirstOrDefault(c => c.Id == id);
            
        }

        public bool Guardar()
        {
            return _DbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}

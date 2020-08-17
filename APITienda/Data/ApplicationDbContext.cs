using APITienda.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Data
{
    public class ApplicationDbContext : DbContext 
    {
        //Se invoca en el startup.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //el IDE sugiere los nombres con una 's' a final pero prefiero trabajar con el mismo nombre.
        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<DetalleOrden> DetalleOrden { get; set; }

        public DbSet<OpinionProducto> OpinionProducto { get; set; }

        public DbSet<Orden> Orden { get; set; }

        public DbSet<Producto> Producto { get; set; }
    }
}

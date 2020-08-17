using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Models.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "La descripción es obligatorio y debe tener mas de 10 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria y debe ser mayor o igual a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio y debe ser mayor o igual a 0")]

        public decimal Precio { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        //FK de Producto a Categoria
        [Required(ErrorMessage = "La categoria es obligatoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}

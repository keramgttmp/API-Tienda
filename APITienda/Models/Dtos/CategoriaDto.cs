using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Models.Dtos
{
    public class CategoriaDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaCreacion{ get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Models
{
    public class Categoria
    {
        [Key] //uso DataAnnotation
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Descripcion { get; set; }

        [Required]

        public DateTime FechaCreacion{ get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}

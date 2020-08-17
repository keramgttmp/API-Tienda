using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace APITienda.Models
{
    public class OpinionProducto
    {
         [Key]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(500)]
        public string Comentario { get; set; }

        [Required]
        [Range(1,5)]
        public int Calificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}

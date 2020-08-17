using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace APITienda.Models
{
    public class DetalleOrden
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int OrdenId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int CantidadDetalle { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioDetalle { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}

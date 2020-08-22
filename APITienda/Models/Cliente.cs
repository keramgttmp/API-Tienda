using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string PrimerApellido { get; set; }

        [Required]
        public string SegundoApellido { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        [StringLength(9)]
        public string Celular { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }

}


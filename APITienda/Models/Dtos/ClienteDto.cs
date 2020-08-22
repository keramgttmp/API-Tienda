using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITienda.Models.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "El nombre es requerido, de tener entre 1 y 50 caracteres.")]
        [StringLength(50, MinimumLength = 1)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es requerido, de tener entre 1 y 50 caracteres.")]
        [StringLength(50, MinimumLength = 1)]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "El segundo apellido es requerido, de tener entre 1 y 50 caracteres.")]
        [StringLength(50, MinimumLength = 1)]
        public string SegundoApellido { get; set; }

        [Required (ErrorMessage ="El correo es requerido")]
        [StringLength(30)]
        [RegularExpression(@"^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El número de celular es requerido y de formato ####-####")]
        [StringLength(9)]
        [RegularExpression(@"^([0-9]{4})+(-)+([0-9]{4})+$")]
        public string Celular { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }

}


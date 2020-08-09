using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SegundoRetoWebAPI.Models
{
    public class Marca
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Estado { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}

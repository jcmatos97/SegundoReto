using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegundoRetoWebAPI.Models
{
    public class Producto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Existencia { get; set; }

        [Required]
        [Column(TypeName = "decimal(16,2)")]
        public decimal Precio { get; set; }

        [Required]
        public string Foto { get; set; }

        [Required]
        public int Estado { get; set; }

        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}

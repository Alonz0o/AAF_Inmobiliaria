using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AAF_Inmobiliaria.Models
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Importe { get; set; }
        public int AlquilerId { get; set; }
        public Alquiler Alquiler { get; set; }
        public Inquilino Inquilino { get; set; }
    }
}

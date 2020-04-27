﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AAF_Inmobiliaria.Models
{
    public class Inmueble
    {
        [Display(Name = "Código")]
        public int InmuebleId { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int Ambientes { get; set; }
        [Required]
        public int Superficie { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public int PropietarioId { get; set; }
        [ForeignKey("PropietarioId")]
        public Propietario propietario { get; set; }
        public Boolean EstaPublicado { get; set; }
        public Boolean EstaHabilitado { get; set; }
    }
}

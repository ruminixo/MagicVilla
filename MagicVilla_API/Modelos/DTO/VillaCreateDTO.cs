﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.DTO
{
    public class VillaCreateDTO
    {
        
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Tarifa  { get; set; }
        public int Ocupantes { get; set;}
        public int MetroCuadrados { get; set;}
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
        
    }
}

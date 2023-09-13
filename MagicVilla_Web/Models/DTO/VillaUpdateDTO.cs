﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Tarifa  { get; set; }
        [Required]
        public int Ocupantes { get; set;}
        [Required]
        public int MetroCuadrados { get; set;}
        [Required]
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
        
    }
}

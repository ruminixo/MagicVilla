﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.DTO
{
    public class NumeroVillaDto
    {
        [Required] 
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
        public VillaDTO Villa { get; set; }

    }
}

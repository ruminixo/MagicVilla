﻿using static MagicVilla_Utilidad.DS;

namespace MagicVilla_Web.Models
{
    public class APIRequrest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;
        public string Url { get; set; }
        public Object Datos { get; set; }
    }
}

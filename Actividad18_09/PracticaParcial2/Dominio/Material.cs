﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial2.Dominio
{
    internal class Material
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }

        public Material(int codigo, string nombre, int stock)
        {
            Codigo = codigo;
            Nombre = nombre;
            Stock = stock;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}

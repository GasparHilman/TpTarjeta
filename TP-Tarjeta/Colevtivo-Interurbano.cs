using System;
using System.Collections.Generic;

namespace Space
{
    public class ColectivoInterurbano : Colectivo
    {
        public ColectivoInterurbano(string linea1) : base(linea1)
        {
            precio = 2500;
        }
    }
}
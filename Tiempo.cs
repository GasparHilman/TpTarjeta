﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Tarjeta
{
    public class Tiempo
     {
        public Tiempo()
        {

        }

        public virtual DateTime Now()
        {
             return DateTime.Now;
        }
    }
}

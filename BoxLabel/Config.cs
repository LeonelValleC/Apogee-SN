﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxLabel
{
    class Config : Conexion
    {
        static string printer;

        public string Printer { get => printer; set => printer = value; }
    }
}

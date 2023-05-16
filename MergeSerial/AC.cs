using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSerial
{
    class AC : Conexion
    {
        int id_ac;
        string ac;
        bool used;
        DateTime date_used;

        public int Id_ac { get => id_ac; set => id_ac = value; }
        public string Ac { get => ac; set => ac = value; }
        public bool Used { get => used; set => used = value; }
        public DateTime Date_used { get => date_used; set => date_used = value; }
    }
}

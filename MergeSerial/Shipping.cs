using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSerial
{
    class Shipping : Conexion
    {
        int id_ship;
        DateTime dateShip;
        int id_wo;
        int id_box;
        DateTime ShippingDate;
        bool isShip;
        string AC;

        public int Id_ship { get => id_ship; set => id_ship = value; }
        public DateTime DateShip { get => dateShip; set => dateShip = value; }
        public int Id_wo { get => id_wo; set => id_wo = value; }
        public int Id_box { get => id_box; set => id_box = value; }
        public DateTime ShippingDate1 { get => ShippingDate; set => ShippingDate = value; }
        public bool IsShip { get => isShip; set => isShip = value; }
        public string AC1 { get => AC; set => AC = value; }
    }
}

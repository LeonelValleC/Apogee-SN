using System;

namespace Portal_Apogee
{
    public class Shipping : Conexion
    {
        int id_shipping;
        int id_inprocess;
        DateTime dateShip;
        int id_wo;
        int id_box;
        int id_user;
        DateTime sgippingDate;
        bool isShip;
        string aC;
        int id_ac;
        DateTime datePair;

        public int Id_shipping { get => id_shipping; set => id_shipping = value; }
        public int Id_inprocess { get => id_inprocess; set => id_inprocess = value; }
        public DateTime DateShip { get => dateShip; set => dateShip = value; }
        public int Id_wo { get => id_wo; set => id_wo = value; }
        public int Id_box { get => id_box; set => id_box = value; }
        public int Id_user { get => id_user; set => id_user = value; }
        public DateTime SgippingDate { get => sgippingDate; set => sgippingDate = value; }
        public bool IsShip { get => isShip; set => isShip = value; }
        public string AC { get => aC; set => aC = value; }
        public int Id_ac { get => id_ac; set => id_ac = value; }
        public DateTime DatePair { get => datePair; set => datePair = value; }
    }
}
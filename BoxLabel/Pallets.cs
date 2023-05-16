using System;

namespace BoxLabel
{
    class Pallets : Conexion
    {
        int id_pallet;
        string pallet;
        int consecutive;
        string nom;
        int count;
        int pallet_count;
        int conse_pallet;


        public int Id_pallet { get => id_pallet; set => id_pallet = value; }
        public string Pallet { get => pallet; set => pallet = value; }
        public int Consecutive { get => consecutive; set => consecutive = value; }
        public string Nom { get => nom; set => nom = value; }
        public int Count { get => count; set => count = value; }
        public int Pallet_count { get => pallet_count; set => pallet_count = value; }

        public int Palleting()
        {

            conse_pallet = int.Parse((ReturnValue("select conse_pallet from config")));
            conse_pallet++;
            nom = ReturnValue("select pallet from config");
            pallet = nom + "-" + DateTime.Now.ToString("yyMMdd") + conse_pallet.ToString().PadLeft(5, '0');
            id_pallet = int.Parse(ReturnID("insert into tb_pallet(pallet) values('" + pallet + "'); SELECT SCOPE_IDENTITY();"));
            Crud("update config set conse_pallet = '" + conse_pallet + "'");



            return id_pallet;
        }
    }
}

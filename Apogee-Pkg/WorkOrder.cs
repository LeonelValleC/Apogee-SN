﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyPro_Project
{
    class WorkOrder : Conexion
    {
		static int id_wo;
		public string wo { get; set; }
		public int qty { get; set; }
		public string rev { get; set; }
		public DateTime datecreated { get; set; }
		public int ubox { get; set; }
		public int id_pn { get; set; }
		public int id_user { get; set; }
		public int id_box { get; set; }
        public int Id_wo { get => id_wo; set => id_wo = value; }
    }
}

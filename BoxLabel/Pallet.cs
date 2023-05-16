using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BoxLabel
{
    public partial class Pallet : Form
    {
        Inprocess inp = new Inprocess();
        Box box = new Box();
        Shipping ship = new Shipping();
        WorkOrder wo = new WorkOrder();

        public Pallet()
        {
            InitializeComponent();
        }

        private void txt_sn_Leave(object sender, EventArgs e)
        {
            if (inp.Existe("select count(*) from tb_Inprocess where Serialnumber ='" + txt_sn.Text.Trim() + "'"))
            {
                //Pallet_Load(sender, e);
                inp.Id_inprocess = int.Parse(inp.ReturnID("select id_inprocess from tb_Inprocess where Serialnumber = '" + txt_sn.Text.Trim() + "'"));
                if (!inp.Existe("select count(*) from tb_shipping where id_pallet is not null and id_inprocess = " + inp.Id_inprocess))
                {
                    //if (!inp.Existe("select count(*) from tb_shipping where datePair is not null and id_inprocess = " + inp.Id_inprocess))
                    //{
                    ship.Crud("update tb_shipping set id_pallet = '" + box.Id_pallet + "', date_pallet = '" + DateTime.Now + "' where id_inprocess = '" + inp.Id_inprocess + "'");
                    txt_sn.Text = "";
                    txt_sn.Focus();
                    Pallet_Load(sender, e);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Serial Number not paring!");
                    //}

                }
                else
                {
                    MessageBox.Show("Serial Numbers already in the pallet!");

                }
            }
            else
            {
                MessageBox.Show("Serial Number not exist!");
                //break;
            }
        }

        private void Pallet_Load(object sender, EventArgs e)
        {
            
            //wo.Wo = "1052021";
            //wo.Id_wo = int.Parse(wo.ReturnID("select id_wo from tb_WoTop where wo = '" + wo.Wo + "'"));
            box.Id_pallet = int.Parse(wo.ReturnID("select pallet from tb_WoTop where id_wo = '" + wo.Id_wo + "'"));

            box.Count = int.Parse(box.ReturnValue("select count(*) from tb_shipping where id_pallet = " + box.Id_pallet));
            //box.Count = int.Parse(box.ReturnValue("select count(*) from tb_shipping where id_pallet = " + box.Id_pallet));

            box.Pallet_count = int.Parse(box.ReturnValue("select Count_Pallet from config"));

            if (box.Id_pallet <= 0)
            {

                box.Id_pallet = box.Palleting();
                wo.Crud("update tb_WoTop set pallet = '" + box.Id_pallet + "' where id_wo = '" + wo.Id_wo + "'");
                box.Count = int.Parse(box.ReturnValue("select count(*) from tb_shipping where id_pallet = " + box.Id_pallet));

            }
            else if ((box.Count) >= box.Pallet_count)
            {
                PrintPallet();
                box.Id_pallet = box.Palleting();
                wo.Crud("update tb_WoTop set pallet = '" + box.Id_pallet + "' where id_wo = '" + wo.Id_wo + "'");
                box.Count = int.Parse(box.ReturnValue("select count(*) from tb_shipping where id_pallet = " + box.Id_pallet));
            }


            lbl_contador.Text = box.Count.ToString();

            dataGridView1.DataSource = ship.LlenarDG("select inp.SerialNumber, p.pallet, ship.date_pallet from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join tb_pallet p on ship.id_pallet = p.id_pallet join tb_WoTop wo on ship.id_wo = wo.id_wo where wo.id_wo = '" + wo.Id_wo + "'").Tables[0];
        
        }

        private void btn_parcial_Click(object sender, EventArgs e)
        {
            PrintPallet();
        }


        private void PrintPallet()
        {
            wo.Crud("delete toprint");

            using (Engine engine = new Engine())
            {
                engine.Start();
                //LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Linea\Box Labels\Masterpack-HMiC.btw");
                LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Linea\Pallet\Pallet.btw");


                //format.PrintSetup.PrinterName = @"Microsoft Print to PDF";
                format.PrintSetup.PrinterName = @"\\mex-bar-001\Apogee_DP03";


                // int Toprint = int.Parse(txt_Qty.Text);
                format.PrintSetup.NumberOfSerializedLabels = 1;
                //format.SubStrings["sn"].Value = txt_SN1.Text;
                DataTable dt = new DataTable();
                dt = ship.LlenarDG("select inp.SerialNumber from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join tb_pallet p on ship.id_pallet = p.id_pallet join tb_WoTop wo on ship.id_wo = wo.id_wo where p.id_pallet = '" + box.Id_pallet + "'").Tables[0];

                inp.ListSN = dt.AsEnumerable().Select(item => string.Format("{0}", item["SerialNumber"])).ToList();


                foreach (var item in inp.ListSN)
                {
                    inp.Crud("insert into toprint values('" + item + "')");
                }

                format.Print();

                engine.Stop();




            }
        }

    }
}

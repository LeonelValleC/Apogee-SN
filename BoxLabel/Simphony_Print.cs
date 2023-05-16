using Seagull.BarTender.Print;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BoxLabel
{
    public partial class Simphony_Print : Form
    {
        Shipping ship = new Shipping();
        Inprocess inp = new Inprocess();
        WorkOrder wo = new WorkOrder();
        //Box box = new Box();
        Pallets pallet = new Pallets();
        Config config = new Config();

        //AccessCode ac = new AccessCode();

        public Simphony_Print()
        {
            InitializeComponent();
        }

        private void Simphony_Print_Load(object sender, EventArgs e)
        {
            //lbl_symunits.Text = "0";
            config.Printer = config.ReturnValue("select printer from config where station = 'Symphony_Packing'");


            lbl_PrinterName.Text = config.Printer;


            //wo.Wo = "1052021";
            //wo.Id_wo = int.Parse(wo.ReturnID("select id_wo from tb_WoTop where wo = '" + wo.Wo + "'"));
            pallet.Id_pallet = int.Parse(wo.ReturnID("select pallet from tb_WoTop where id_wo = '" + wo.Id_wo + "'"));

            pallet.Count = int.Parse(pallet.ReturnValue("select count(*) from tb_shipping where id_pallet = " + pallet.Id_pallet));
            //box.Count = int.Parse(box.ReturnValue("select count(*) from tb_shipping where id_pallet = " + box.Id_pallet));

            pallet.Pallet_count = int.Parse(pallet.ReturnValue("select Count_Pallet from config"));

            if (pallet.Id_pallet <= 0)
            {

                pallet.Id_pallet = pallet.Palleting();
                wo.Crud("update tb_WoTop set pallet = '" + pallet.Id_pallet + "' where id_wo = '" + wo.Id_wo + "'");
                pallet.Count = int.Parse(pallet.ReturnValue("select count(*) from tb_shipping where id_pallet = " + pallet.Id_pallet));

            }
            else if ((pallet.Count) >= pallet.Pallet_count)
            {
                PrintPallet();
                pallet.Id_pallet = pallet.Palleting();
                wo.Crud("update tb_WoTop set pallet = '" + pallet.Id_pallet + "' where id_wo = '" + wo.Id_wo + "'");
                pallet.Count = int.Parse(pallet.ReturnValue("select count(*) from tb_shipping where id_pallet = " + pallet.Id_pallet));
            }


            lbl_symunits.Text = pallet.Count.ToString();

            dataGridView1.DataSource = ship.LlenarDG("select inp.SerialNumber, p.pallet, ship.date_pallet from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join tb_pallet p on ship.id_pallet = p.id_pallet join tb_WoTop wo on ship.id_wo = wo.id_wo where wo.id_wo = '" + wo.Id_wo + "'").Tables[0];


        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_SN1.Text.Trim()) || !string.IsNullOrEmpty(txt_CODE.Text.Trim()))
            {
                if (RecordSNSymphony() == false)
                {
                    PrintSymphony();
                    Reprint();
                    Simphony_Print_Load(sender,e);
                }

            }
            else
            {
                MessageBox.Show("Empty fields!");
            }

        }


        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                        (control as TextBox).BackColor = Color.White;
                    }
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void Reprint()
        {
            DialogResult dialogResult;/* = MessageBox.Show("Print Again", "Label Error!", MessageBoxButtons.YesNo);*/
            do
            {
                dialogResult = MessageBox.Show("Print Again?", "Reprint!", MessageBoxButtons.YesNo);


                if (dialogResult == DialogResult.No)
                {
                    //do something else
                    ClearTextBoxes();
                }
                else if (dialogResult == DialogResult.Yes)
                {
                    PrintSymphony();
                }

            } while (dialogResult == DialogResult.Yes);
        }

        private bool RecordSNSymphony()
        {
            bool exist = false;
            if (!string.IsNullOrEmpty(txt_SN1.Text.Trim()) && !string.IsNullOrEmpty(txt_CODE.Text.Trim()))
            {
                inp.Id_inprocess = int.Parse(ship.ReturnID("select id_inprocess from tb_Inprocess where SerialNumber = '" + txt_SN1.Text.Trim() + "'"));
                ///Check if exist serial 

                //Save in db if not duplicate Serial AND IS PASS TEST
                if (ship.Existe("select count(*) from tb_shipping where id_inprocess = '" + inp.Id_inprocess + "'and AC = '" + txt_CODE.Text.Trim() + "' and dateShip is null"))
                {

                    //pallet.Consecutive = int.Parse((box.ReturnValue("select consecutive from config")));
                    //pallet.Consecutive++;
                    //pallet.Nom = box.ReturnValue("select caracter1 from config");
                    //pallet.Pallet = box.Nom + "-" + DateTime.Now.ToString("yyMMdd") + box.Consecutive.ToString().PadLeft(5, '0');
                    //pallet.Id_pallet = int.Parse(box.ReturnID("insert into tb_Box values('" + box.Boxes + "'); SELECT SCOPE_IDENTITY();"));
                    //pallet.Crud("update config set consecutive = '" + box.Consecutive + "'");


                    if (ship.Existe("select count(*) from tb_shipping where AC = '" + txt_CODE.Text.Trim() + "'"))
                    {
                        if (ship.Existe("select * from accesscode where ac = '" + txt_CODE.Text.Trim() + "'"))
                        {
                            ship.Crud("update tb_shipping set dateShip = '" + DateTime.Now + "', id_wo = '" + wo.Id_wo + "', id_pallet = '" + pallet.Id_pallet + "', date_pallet = '" + DateTime.Now + "' where id_inprocess ='" + inp.Id_inprocess + "'");
                            
                        }
                        else
                        {
                            MessageBox.Show("Access Code not exist!");
                            exist = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Access Code not pairing!");
                        exist = true;
                    }

                }
                else
                {
                    MessageBox.Show("Serial Number not pairing to Access Code!");
                    exist = true;
                }
            }
            else
            {
                MessageBox.Show("Empty Fields!");
                exist = true;
            }
            return exist;

        }

        private void PrintSymphony()
        {
            using (Engine engine = new Engine())
            {
                engine.Start();
                //LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Linea\Box Labels\Masterpack-HMiC.btw");
                LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Linea\Box Labels\Master Carton.btw");


                format.PrintSetup.PrinterName = config.Printer;
                //format.PrintSetup.PrinterName = @"\\mex-bar-001\Apogee_DP03";


                // int Toprint = int.Parse(txt_Qty.Text);
                format.PrintSetup.NumberOfSerializedLabels = 1;
                format.SubStrings["sn"].Value = txt_SN1.Text;

                format.Print();

                engine.Stop();

            }
        }

        private void PrintPallet()
        {
            wo.Crud("delete toprint");

            using (Engine engine = new Engine())
            {
                engine.Start();
                //LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Linea\Box Labels\Masterpack-HMiC.btw");
                LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Linea\Pallet\Pallet.btw");


                format.PrintSetup.PrinterName = config.Printer;
                //format.PrintSetup.PrinterName = @"\\mex-bar-001\Apogee_DP03";


                // int Toprint = int.Parse(txt_Qty.Text);
                format.PrintSetup.NumberOfSerializedLabels = 1;
                //format.SubStrings["sn"].Value = txt_SN1.Text;
                DataTable dt = new DataTable();
                dt = ship.LlenarDG("select inp.SerialNumber from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join tb_pallet p on ship.id_pallet = p.id_pallet join tb_WoTop wo on ship.id_wo = wo.id_wo where p.id_pallet = '" + pallet.Id_pallet + "'").Tables[0];

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

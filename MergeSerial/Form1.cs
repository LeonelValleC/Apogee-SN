using Seagull.BarTender.Print;
using System;
using System.Windows.Forms;

namespace MergeSerial
{
    public partial class Form1 : Form
    {
        readonly AC ac = new AC();
        readonly Shipping ship = new Shipping();
        readonly Inprocess inp = new Inprocess();
        Config config = new Config();


        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            //string txt = txt_SN.Text.Trim().Substring(0, 2);
            if (txt_SN.Text.Trim().Substring(0, 2) == "SD" && PairSerial_AC() == true)
            {
                DefaultPrinter();
                MessageBox.Show("Printed!");
                txt_SN.Text = "";
            }
            else
                MessageBox.Show("Error!/nPlease check Symphony Serial Number!");
        }

        private bool PairSerial_AC()
        {
            bool isValid = false;
            ac.Id_ac = ac.ReturnID("select top 1 id_ac from accesscode where used is null or used = 0 order by id_ac asc");
            ac.Ac = ac.ReturnValue("select ac from accesscode where id_ac = '" + ac.Id_ac + "'");
            try
            {

           
            if (!string.IsNullOrEmpty(txt_SN.Text.Trim()))
            {
                inp.Id_inprocess = ship.ReturnID("select id_inprocess from tb_Inprocess where SerialNumber = '" + txt_SN.Text.Trim() + "'");
                if (ship.Existe("select count(*) from tb_shipping where id_inprocess = '" + inp.Id_inprocess + "'"))
                {
                    MessageBox.Show("Serial Duplicado: " + txt_SN.Text.Trim());


                }
                else if (!ship.Existe("select count(*) from tb_shipping where id_ac = " + ac.Id_ac + " or AC = '" + ac.Ac + "'"))
                {
                    isValid = true;
                    ship.Crud("insert into tb_shipping(id_inprocess, ac, id_ac, datePair) values('" + inp.Id_inprocess + "','" + ac.Ac + "','" + ac.Id_ac + "','" + DateTime.Now + "')");
                    ac.Crud("update accesscode set used = 1, date_used ='" + DateTime.Now + "' where id_ac = " + ac.Id_ac);
                    //MessageBox.Show("Do not Pair Serial & Access Code!");
                }
                else MessageBox.Show("Do not Pair Serial & Access Code!");

            }
            }
            catch (System.Data.SqlClient.SqlException e)
            {

                MessageBox.Show("Serial number not exist!", "Ckeck Serial Number");
            }

            return isValid;
        }

        private void DefaultPrinter()
        {


            using (Engine engine = new Engine())
            {
                engine.Start();



                //LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$
                //\Apogee\Linea\Serial Product\Symphony\Retail_Label.btw");
                LabelFormatDocument format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Linea\SymphonyMerge\Retal_Symphony.btw");


                format.PrintSetup.PrinterName = @"\\mex-bar-001\" + config.Printer;
                //format.PrintSetup.PrinterName = @"Microsoft Print to PDF";

                format.PrintSetup.NumberOfSerializedLabels = 1;

                format.SubStrings["SN"].Value = txt_SN.Text.Trim();
                format.SubStrings["ac"].Value = ac.Ac;



                format.Print();

                engine.Stop();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            config.Printer = config.ReturnValue("select printer from config where station = 'Symphony_Merge'");

            lbl_PrinterName.Text = config.Printer;

        }
    }
}

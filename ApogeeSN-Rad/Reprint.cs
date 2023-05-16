using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Voyager_SN
{
    public partial class Reprint : Form
    {
        WorkOrder wo = new WorkOrder();
        PN pn = new PN();
        Inprocess inprocess = new Inprocess();
        User user = new User();
        Config config = new Config();
        //int count = 0;
        DataTable dt = new DataTable("tb_Inprocess");


        public Reprint()
        {
            InitializeComponent();
        }

        private void Reprint_Load(object sender, EventArgs e)
        {
            wo.Id_wo = int.Parse(wo.ReturnID("select id_wo from tb_WO where wo = '" + wo.Wo + "'"));
            lbl_WO.Text = wo.Wo;
            ////dg_Reprint.DataSource = wo.LlenarDG("select SerialNumber from tb_Inprocess where id_wo = '" + wo.Id_wo + "' and Printed = 1").Tables[0];

            dt = wo.LlenarDG("select id_inprocess, SerialNumber from tb_Inprocess where Printed = 1 and Scrap = 0 or Scrap is null and id_wo = '" + wo.Id_wo + "'").Tables[0];
            dg_Reprint.DataSource = dt; //wo.LlenarDG("select id_inprocess, SerialNumber from tb_Inprocess where Printed = 1 and Scrap = 0 or Scrap is null and id_wo = '" + wo.Id_wo + "'").Tables[0];
            dg_Reprint.Columns[1].Visible = false;

            pn.Pn = wo.ReturnValue("select pn from tb_WO wo join tb_PN pn on wo.id_pn = pn.id_pn where id_wo = '" + wo.Id_wo + "'");
            pn.Rev = wo.ReturnValue("select rev from tb_WO wo join tb_PN pn on wo.id_pn = pn.id_pn where id_wo = '" + wo.Id_wo + "'");


            config.Printer = config.ReturnValue("select printer from config");

        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            btn_Print.Enabled = false;
            List<string> list = new List<string>();
            DefaultPrinter();


            MessageBox.Show("Printed!");
        }

        private void DefaultPrinter()
        {
            inprocess.Crud("delete Temp");
            inprocess.ListSN.Clear();

            using (Engine engine = new Engine())
            {
                engine.Start();

                LabelFormatDocument format = engine.ActiveDocument;

                //LabelFormatDocument format = engine.Documents.Open(@"\\192.168.1.4\labels$\Apogee\Apogee_PCBA.btw");

                if (pn.Pn.Trim() == "3500-0093-0000" || pn.Pn.Trim() == "0000-0445-0000")
                {
                    format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Symphony.btw");
                }
                else
                {
                    format = engine.Documents.Open(@"\\mex-fss-001\labels$\Apogee\Apogee_PCBA.btw");
                }

                format.PrintSetup.PrinterName = @"\\mex-bar-001\" + config.Printer;


                format.PrintSetup.NumberOfSerializedLabels = 1;


                List<string> list = new List<string>();

                foreach (DataGridViewRow row in this.dg_Reprint.Rows)
                {
                    // if a cell has never choosed so it is null 
                    if ((row.Cells[0].Value) == null)
                        continue;

                    if (((bool)row.Cells[0].Value == true))
                    {
                        inprocess.Id_inprocess = int.Parse(row.Cells[1].Value.ToString());
                        inprocess.SerialNumber = row.Cells[2].Value.ToString();

                        //inprocess.Id_inprocess = int.Parse(inprocess.ReturnValue("select top 1 id_inprocess from tb_Inprocess where Printed is not null and id_wo = '" + wo.Id_wo + "' ORDER BY id_inprocess ASC"));


                        inprocess.ListSN.Add(inprocess.SerialNumber);

                        inprocess.Crud("update tb_Inprocess set Printed = 1, Validated = 0  where id_inprocess = '" + inprocess.Id_inprocess + "'");

                        inprocess.Crud("insert into tb_LogReprint values('" + user.Id_user + "','" + DateTime.Now + "','" + inprocess.Id_inprocess + "')");

                    }
                }

                foreach (var item in inprocess.ListSN)
                {
                    inprocess.Crud("insert into Temp values('" + item + "')");
                }

                format.Print();
                engine.Stop();

                btn_Print.Enabled = true;


            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string searchValue = txt_Search.Text;

            dg_Reprint.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dg_Reprint.Rows)
                {
                    if (row.Cells[2].Value.ToString().Equals(searchValue))
                    {
                        //row.Selected = true;
                        dg_Reprint.Rows[row.Index].Selected = true;
                        dg_Reprint.FirstDisplayedScrollingRowIndex = row.Index;
                        dg_Reprint.Focus();
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}

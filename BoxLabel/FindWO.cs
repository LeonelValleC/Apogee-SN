﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BoxLabel
{
    public partial class FindWO : Form
    {
        WorkOrder wo = new WorkOrder();
        Conexion con = new Conexion();
        User user = new User();
        PN pn = new PN();

        public FindWO()
        {
            InitializeComponent();
        }
        public static Form IsFormAlreadyOpen(Type formType)
        {
            return Application.OpenForms.Cast<Form>().FirstOrDefault(openForm => openForm.GetType() == formType);
        }

        private void txt_WO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Submit_Click(this, new EventArgs());

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (wo.Existe("select count(*) from tb_WoTop where wo = '" + txt_WO.Text + "'"))
            {
                //wo.wo = txt_WO.Text;

                wo.Id_wo = int.Parse(wo.ReturnID("select id_wo from tb_WoTop where wo = '" + txt_WO.Text + "'"));
                pn.Pn = pn.ReturnValue("select pn.pn from tb_WoTop wo join tb_PN pn on wo.id_pn = pn.id_pn where wo.id_wo = '" + wo.Id_wo + "'");
                //user.Nemploy = int.Parse(txt_Employ.Text);

                IndentifyEmploy sn = new IndentifyEmploy();
                Form NuevaOrden;
                if ((NuevaOrden = IsFormAlreadyOpen(typeof(IndentifyEmploy))) == null)
                {
                    sn.ShowDialog(this);
                }
                else
                {
                    NuevaOrden.WindowState = FormWindowState.Normal;
                    NuevaOrden.BringToFront();
                }


            }
            else
            {
                MessageBox.Show("Not found!", "ERROR");
            }
        }

        private void FindWO_Load(object sender, EventArgs e)
        {

        }
    }
}

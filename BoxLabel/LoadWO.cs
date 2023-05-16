﻿using System;
using System.Windows.Forms;

namespace BoxLabel
{
    public partial class LoadWO : Form
    {
        WorkOrder wo = new WorkOrder();
        PN pn = new PN();

        public LoadWO()
        {
            InitializeComponent();
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            txt_QTY.Text = wo.ReturnFromROI("SELECT * FROM OPENQUERY([VOYAGER], 'select Qty_To_Mfg from WO_v_1 where Wo_Nbr = ''" + txt_WO.Text + "''')");
            txt_Rev.Text = wo.ReturnFromROI("SELECT * FROM OPENQUERY([VOYAGER], 'select Bom_Rev_Level from WO_v_1 where Wo_Nbr = ''" + txt_WO.Text + "''')");
            cb_PN.Text = wo.ReturnFromROI("SELECT left(Part_Nbr,len(Part_Nbr)-3) FROM OPENQUERY([VOYAGER], 'select Part_Nbr from WO_v_1 where Wo_Nbr = ''" + txt_WO.Text + "''')");
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {

            if (!wo.Existe("select count(*) from tb_WoTop where wo = '" + txt_WO.Text.Trim() + "'"))
            {
                wo.Crud("insert into tb_WoTop(wo,qty,dateReg,id_pn, rev) values('" + txt_WO.Text + "','" + txt_QTY.Text + "','" + DateTime.Now + "','" + cb_PN.SelectedValue + "','" + txt_Rev.Text.Trim() + "')");
                txt_QTY.Text = "";
                txt_Rev.Text = "";
                txt_WO.Text = "";
                cb_PN.SelectedValue = -1;

                MessageBox.Show("WO Saved!");

            }
            else
            {
                MessageBox.Show("Already Exist!");
            }


        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            txt_QTY.Text = "";
            txt_Rev.Text = "";
            txt_WO.Text = "";
            cb_PN.SelectedValue = -1;
        }

        private void LoadWO_Load(object sender, EventArgs e)
        {
            cb_PN.DataSource = pn.LlenarComboBox("select id_pn, CONCAT(description,' | ',pn) as 'pn' from tb_PN");
            cb_PN.DisplayMember = "pn";
            cb_PN.ValueMember = "id_pn";
            cb_PN.SelectedValue = -1;
        }
    }
}

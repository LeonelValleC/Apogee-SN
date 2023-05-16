using System;
using System.Linq;
using System.Windows.Forms;

namespace BoxLabel
{
    public partial class wo_pallet : Form
    {
        WorkOrder wo = new WorkOrder();

        public static Form IsFormAlreadyOpen(Type formType)
        {
            return Application.OpenForms.Cast<Form>().FirstOrDefault(openForm => openForm.GetType() == formType);
        }
        public wo_pallet()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (wo.Existe("select count(*) from tb_WoTop where wo = '" + txt_WO.Text + "'"))
            {
                //wo.wo = txt_WO.Text;

                wo.Id_wo = int.Parse(wo.ReturnID("select id_wo from tb_WoTop where wo = '" + txt_WO.Text + "'"));
                //user.Nemploy = int.Parse(txt_Employ.Text);

                Pallet sn = new Pallet();

                Form NuevaOrden;
                if ((NuevaOrden = IsFormAlreadyOpen(typeof(Pallet))) == null)
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

        private void txt_WO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Submit_Click(this, new EventArgs());

        }
    }
}

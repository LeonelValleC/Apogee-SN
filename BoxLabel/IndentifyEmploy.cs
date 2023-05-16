using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BoxLabel
{
    public partial class IndentifyEmploy : Form
    {
        User operador = new User();
        WorkOrder orden = new WorkOrder();
        Conexion con = new Conexion();
        PN pn = new PN();

        public IndentifyEmploy()
        {
            InitializeComponent();
        }

        public static Form IsFormAlreadyOpen(Type formType)
        {
            return Application.OpenForms.Cast<Form>().FirstOrDefault(openForm => openForm.GetType() == formType);
        }


        private void txt_empleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_aceptar_Click(this, new EventArgs());
            }
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {

            try
            {
                operador.idemp = Convert.ToInt32(txt_empleado.Text);
                if (txt_empleado.Text == "")
                    throw new Exception();

                int regreso = int.Parse(operador.ReturnValue("select id_user from tb_User where nemploy = '" + txt_empleado.Text + "'"));

                if (regreso > 0)
                {
                    operador.Id_user = regreso;
                    //orden.Crud("update tb_Orden set id_user = " + regreso + " where orden = " + orden.wo);
                    
                    //ee.Show();

                    if (pn.Pn.Trim() == "3500-0093-0000" || pn.Pn == "0000-0445-0000")
                    {
                        Simphony_Print ee = new Simphony_Print();
                        Form enet;

                        if ((enet = IsFormAlreadyOpen(typeof(Simphony_Print))) == null)
                        {
                            ee.ShowDialog(this);
                            this.Close();
                        }

                        else
                        {
                            enet.WindowState = FormWindowState.Normal;
                            enet.BringToFront();
                        }
                    }
                    else
                    {
                        Form1 ee = new Form1();
                        Form enet;

                        if ((enet = IsFormAlreadyOpen(typeof(Form1))) == null)
                        {
                            ee.ShowDialog(this);
                            this.Close();
                        }

                        else
                        {
                            enet.WindowState = FormWindowState.Normal;
                            enet.BringToFront();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro");
                    txt_empleado.Text = "";
                }

                con.Cerrar();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No existe ese registro", "ERROR!");
                con.Cerrar();
            }
            catch (Exception)
            {
                MessageBox.Show("Insert an Id!");
            }
        }

        private void IndentifyEmploy_Load(object sender, EventArgs e)
        {

        }
    }
}

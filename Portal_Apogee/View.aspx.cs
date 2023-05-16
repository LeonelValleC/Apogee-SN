using System;

namespace Portal_Apogee
{
    public partial class View : System.Web.UI.Page
    {
        AccessCode ac = new AccessCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            datagrid1.DataSource = ac.LlenarDG("select inp.SerialNumber, ac.ac, ship.datePair, ship.dateShip as 'Date Packing', ship.shippingDate from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join accesscode ac on ship.id_ac = ac.id_ac").Tables[0];
            datagrid1.DataBind();

        }
    }
}
using System;
using System.Web.UI.WebControls;

namespace Portal_Apogee
{
    public partial class Manto : System.Web.UI.Page
    {
        Shipping ship = new Shipping();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_ac.Value.Trim()))
            {
                GridView1.DataSource = ship.LlenarDG("select inp.SerialNumber, box.box, dateShip as 'Packing Label',wo.wo  from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join tb_Box box on ship.id_box = box.id_box join tb_WoTop wo on wo.id_wo = ship.id_wo where inp.SerialNumber = '" + txt_ac.Value.Trim() + "' order by inp.id_inprocess asc").Tables[0];
                GridView1.DataBind();


            }
        }

        protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //((LinkButton)e.Row.Cells[0].Controls[0]).OnClientClick = "return confirm('Are you sure you want to delete?');";
                ((LinkButton)e.Row.Cells[0].Controls[0]).OnClientClick = "Confirm();";
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {

                ship.Id_inprocess = int.Parse(GridView1.SelectedRow.Cells[1].Text);
                ship.Crud("delete tb_Shipping where inp.id_inprocess = " + ship.Id_inprocess);
                //ship.Crud("update accesscode set used = 0, date_used = NULL where id_ac = " + ac.Id);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

            }
            else
            if (confirmValue == "No")
            {

            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_ac.Value.Trim()))
            {
                GridView1.DataSource = ship.LlenarDG("select inp.SerialNumber, box.box, dateShip as 'Packing Label',wo.wo  from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join tb_Box box on ship.id_box = box.id_box join tb_WoTop wo on wo.id_wo = ship.id_wo where inp.SerialNumber = '" + txt_ac.Value.Trim() + "' order by inp.id_inprocess asc").Tables[0];
                GridView1.DataBind();

            }
        }
    }
}
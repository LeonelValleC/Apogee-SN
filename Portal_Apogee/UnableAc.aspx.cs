using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_Apogee
{
    public partial class UnableAc : System.Web.UI.Page
    {
        AccessCode ac = new AccessCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //GridView1.DataSource = ac.LlenarDG("select * from accesscode where used = 1").Tables[0];
                GridView1.DataSource = ac.LlenarDG("select ship.id_ac, ac.ac as 'Access Code', inp.SerialNumber from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join accesscode ac on ac.id_ac = ship.id_ac order by ac.id_ac asc").Tables[0];
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ac.Id = int.Parse(e.Row.Cells[1].Text);
            //}
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_ac.Value.Trim()))
            {
                GridView1.DataSource = ac.LlenarDG("select ship.id_ac, ac.ac as 'Access Code', inp.SerialNumber from tb_Inprocess inp join tb_shipping ship on inp.id_inprocess = ship.id_inprocess join accesscode ac on ac.id_ac = ship.id_ac where inp.SerialNumber = '" + txt_ac.Value.Trim() + "' order by ac.id_ac asc").Tables[0];
                GridView1.DataBind();

            }
            //else
            //Empty field
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //((LinkButton)GridView1.Rows[0].Controls[0].Controls[0]).OnClientClick = "Confirm()";
            //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:Confirm(); ", true);
          

            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {

                ac.Id = int.Parse(GridView1.SelectedRow.Cells[1].Text);
                ac.Crud("delete tb_Shipping where id_ac = " + ac.Id);
                ac.Crud("update accesscode set used = 0, date_used = NULL where id_ac = " + ac.Id);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);

            }
            else
            if (confirmValue == "No")
            {

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
    }
}
using System;
using System.Linq;
using System.Web.UI;

namespace Portal_Apogee
{
    public partial class SiteMaster : MasterPage
    {
        User user = new User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int _displayTimeInMiliSec = (Session.Timeout - 1) * 60000;

                if (Session["id_user"] == null)
                {
                    Session["id_user"] = "New Session";
                    lblUser.Text = Convert.ToString(Session["id_user"]);
                }
                else
                    lblUser.Text = "Old Session";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(),
                    "message",
                    "<script type=\"text/javascript\" language=\"javascript\">Timer('" + _displayTimeInMiliSec + "');</script>",
                    false);
            }
            try
            {


                //int _displayTimeInMiliSec = (Session.Timeout - 1) * 60000;
                if (int.Parse(user.GetUser(int.Parse(Session["id_user"].ToString())).ElementAt(0).Level.ToString()) == 3)
                {
                    inbox.HRef = "";
                    lblUser.Text = user.ReturnValue("select name from users where id_user = '" + Session["id_user"].ToString() + "'");

                }
                else
                {
                    if (Session["id_user"] != null)
                    {
                        lblUser.Text = user.ReturnValue("select name from tb_User where id_user = '" + Session["id_user"].ToString() + "'");

                    }
                    else
                    {
                        string script = "alert(\"Login Error!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Session Expired!", script, true);
                        Response.Redirect("~/Login.aspx", false);

                    }
                }

            }
            catch (Exception)
            {

                //throw;
                string script = "alert(\"Login Error!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "Session Expired!", script, true);
                Response.Redirect("~/Login.aspx", false);
            }
        }
    }
}
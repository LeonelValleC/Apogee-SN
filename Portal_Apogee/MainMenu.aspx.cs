using System;
using System.Linq;
using System.Web.UI;

namespace Portal_Apogee
{
    public partial class MainMenu : System.Web.UI.Page
    {
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //var level = Session["id_user"];
                    ///Level 4 DCC
                    ///Level 5 QC
                    if (int.Parse(user.GetUser(int.Parse(Session["id_user"].ToString())).ElementAt(0).Level.ToString()) == 4)
                    {

                        UnableAC.Visible = true;

                        Upload.Visible = false;

                    }
                    else if (int.Parse(user.GetUser(int.Parse(Session["id_user"].ToString())).ElementAt(0).Level.ToString()) == 5 || int.Parse(user.GetUser(int.Parse(Session["id_user"].ToString())).ElementAt(0).Level.ToString()) == 1)
                    {

                        maintenance.Visible = true;
                        Upload.Visible = true;
                        UnableAC.Visible = false;


                    }
                    else
                    {
                        Upload.Visible = false;
                        UnableAC.Visible = false;
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
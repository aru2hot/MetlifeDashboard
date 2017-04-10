using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }


    #region change
    protected void page_redirect(object sender, EventArgs e)
    {
        string id = (sender as Control).ID;

        Session["Control_ID"] = id;

        Response.Redirect("/View.aspx");
        

    }

    #endregion
}
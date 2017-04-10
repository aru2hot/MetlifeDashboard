using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePanel_WebForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = DateTime.Now.ToString();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = DateTime.Now.ToString();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Quantity");

        dt.Rows.Add("2");

        GridView1.DataSource = dt;

        GridView1.DataBind();
        
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);

        Label2.Text = DateTime.Now.ToString();
    }
}
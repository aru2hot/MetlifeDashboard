using MasterDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mor : System.Web.UI.Page
{
    DAL dal = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DataTable MOR_DETAILS = Get_MOR_DATA();
            DataTable dt;



            fill_mor_details(MOR_DETAILS);

            dt = MOR_DETAILS;

            fill_Add_mor_category(dt);

            fill_Add_mor_portofolio(dt);

            //Excel Download Code 

         //   System.IO.StringWriter stringWriter =
         //new System.IO.StringWriter();
         //   System.Web.UI.HtmlTextWriter htmlWriter =
         //      new System.Web.UI.HtmlTextWriter(stringWriter);
         //   mor_grid.RenderControl(htmlWriter);
         //   string s = stringWriter.ToString();
         //   Response.Write(s);
          //  Response.End();
       }

    }
    public override void   VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    public void fill_mor_filterdropdownlist(DataTable dt)
    {
        fill_mor_portofolio(dt);

        fill_mor_projectname(dt);

        fill_mor_category(dt);


    }
    public void fill_mor_details(DataTable dt)
    {


        //  fill_mor_grid(dt);

        fill_mor_filterdropdownlist(dt);

    }
    protected void MOR_Add_Close_Linke_Click(object sender, EventArgs e)
    {

        lbl_MOR_Add_Status.Text = "";
        MOR_Add_Details.Text = "SAVE";
        ResetMORADDFields();
        ModalPopupExtender_MOR.Hide();
    }

    protected void MOR_Add_Details_Click(object sender, EventArgs e)
    {
        DAL dal = new DAL();
        
        var today = DateTime.Today;
        DateTime weekending = today.AddDays(-(int)today.DayOfWeek).AddDays(5);
        string format = "yyyy-MM-dd HH:mm:ss";
        if (MOR_Add_Details.Text == "ADD")
        {

            int MAX_MOR_ID = GetMaxMORID(dal);
            MAX_MOR_ID = MAX_MOR_ID + 1;
            string sql_insert_smt = "insert into  T_MOR_DETAILS ( MOR_ID,MODIFIED_TIMESTAMP,CREATED_TIMESTAMP,PORTFOLIO,PROJECT_NAME,CATEGORY,DESCRIPTION,LAST_WEEK_COLOR,CURRENT_WEEK_COLOR,NEXT_WEEK_COLOR,OVERALL_STATUS_COLOR,CREATED_BY,LAST_MODIFIED_BY,LAST_MODIFIED_TIMESTAMP,RISKS,WEEK_ENDING,STATUS_LOG,ACTIVE)  Values ";
            //  string sql_insert_smt_values = "";
            string sql_insert_smt_values = "(" + MAX_MOR_ID + ",'" + DateTime.Now.ToString(format) + "','" + DateTime.Now.ToString(format) + "','" + MOR_Add_Portfolio.SelectedValue + "','" + MOR_Add_Project_Name.Text + "','" + MOR_Add_Category.SelectedValue + "','" + MOR_Add_Description.InnerText + "','" + MOR_Add_LastWeekStatus.SelectedValue + "','" + MOR_Add_CurrentWeekStatus.SelectedValue + "','" + MOR_Add_NextWeekStatus.SelectedValue + "','" + "GREEN" + "','" + Environment.UserName + "','" + Environment.UserName + "','" + DateTime.Now.ToString(format) + "','" + MOR_Add_Risks.InnerText + "','" + weekending.ToString(format) + "','" + "Insert Status Loghere " + "','A')";

            sql_insert_smt = sql_insert_smt + sql_insert_smt_values;

            dal.insertintoDB(sql_insert_smt);
            ResetMORADDFields();
            // ModalPopupExtender2.Hide();

            lbl_MOR_Add_Status.Text = "MOR Details Added Successfully!";

        }
        else
        {

            string update_smt = "update T_MOR_DETAILS  set MODIFIED_TIMESTAMP = '" + DateTime.Now.ToString(format) + "', PORTFOLIO = '" + MOR_Add_Portfolio.SelectedValue + "',  DESCRIPTION = '" + MOR_Add_Description.InnerText.Replace("'", "''") + "',"
                                + "CURRENT_WEEK_COLOR ='" + MOR_Add_CurrentWeekStatus.SelectedValue + "',"
                                + "NEXT_WEEK_COLOR = '" + MOR_Add_NextWeekStatus.SelectedValue + "',"
                                + "LAST_MODIFIED_BY ='" + Environment.UserName + "',"
                                + "LAST_MODIFIED_TIMESTAMP ='" + DateTime.Now.ToString(format) + "',"
                                + "RISKS ='" + MOR_Add_Risks.InnerText.Replace("'", "''") + "',"
                                + "PROJECT_NAME ='" + MOR_Add_Project_Name.Text + "',"
                                + "CATEGORY = '" + MOR_Add_Category.SelectedValue + "',"
                                + "WEEK_ENDING = '" + weekending.ToString(format) + "' where MOR_ID = " + Session["updateMORID"];



            dal.UpdateDB(update_smt);
            //ResetMORADDFields();
            //ModalPopupExtender2.Hide();

            lbl_MOR_Add_Status.Text = "MOR Details Updated Successfully!";
            Session["updateMORID"] = null;
            Session["should_proj_name_refresh"] = "No";
            MOR_Search_Button_Click(sender, e);

            //DataTable MOR_DETAILS = Get_MOR_DATA();
            //MOR_DETAILS = MOR_filter_records(MOR_DETAILS);
            //mor_grid.DataSource = MOR_DETAILS;
            //mor_grid.DataBind();

        }

    }
    public void ResetMORADDFields()
    {
        MOR_Add_Portfolio.SelectedValue = "Others";
        MOR_Add_Category.SelectedIndex = 1;
        MOR_Add_Project_Name.Text = "";
        MOR_Add_Project_Name.ToolTip = "Enter Project Name";
        MOR_Add_Description.InnerText = "";

        MOR_Add_LastWeekStatus.SelectedValue = "GREEN";
        MOR_Add_CurrentWeekStatus.SelectedValue = "GREEN";
        MOR_Add_Risks.InnerHtml = "";
        MOR_Add_NextWeekStatus.SelectedValue = "GREEN";
        lbl_MOR_Add_Status.Text = "";

    }
    public int GetMaxMORID(DAL tmp_dal)
    {
        string sql_max_smt = "select Max(MOR_ID) from T_MOR_DETAILS";
        int max_MOR_ID = tmp_dal.SelectMax(sql_max_smt);
        return max_MOR_ID;

    }
    protected void MOR_Add_Close_Button_Click(object sender, EventArgs e)
    {
        Session["should_proj_name_refresh"] = "Yes";
        MOR_Search_Button_Click(sender, e);
        ResetMORADDFields();

        MOR_Add_Details.Text = "ADD";
        MOR_Add_LastWeekStatus.Enabled = true;
        ModalPopupExtender_MOR.Hide();

    }

    public string RemoveHTMLTags(string HTMLCode)
    {
        HTMLCode = HTMLCode.Replace("#", "");
        return System.Text.RegularExpressions.Regex.Replace(
          HTMLCode, "<[^>]*>", "");
    }
    #region MOR

    public DataTable MOR_filter_records(DataTable dt)
    {
        DataView dv = new DataView(dt);

        string query = null;



        if (mor_portofolio.SelectedItem.ToString() != null)
        {
            if (mor_portofolio.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }

                query += "`PORTFOLIO` = '" + mor_portofolio.SelectedItem.ToString() + "'";

            }
        }


        if (mor_projectname.SelectedItem.ToString() != null)
        {
            if (mor_projectname.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }

                query += "`PROJECT_NAME` = '" + mor_projectname.SelectedItem.ToString() + "'";

            }
        }



        if (mor_category.SelectedItem.ToString() != null)
        {
            if (mor_category.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }

                query += "`CATEGORY` = '" + mor_category.SelectedItem.ToString() + "'";

            }
        }

        if (mor_lastweekstatus.SelectedItem.ToString() != null)
        {
            if (mor_lastweekstatus.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }

                query += "`LAST_WEEK_COLOR` = '" + mor_lastweekstatus.SelectedValue.ToString() + "'";

            }
        }
        if (mor_nextweekstatus.SelectedItem.ToString() != null)
        {
            if (mor_nextweekstatus.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }

                query += "`NEXT_WEEK_COLOR` = '" + mor_nextweekstatus.SelectedValue.ToString() + "'";

            }
        }

        if (mor_currentweekstatus.SelectedItem.ToString() != null)
        {
            if (mor_currentweekstatus.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }

                query += "`CURRENT_WEEK_COLOR` = '" + mor_currentweekstatus.SelectedValue.ToString() + "'";

            }
        }



        if (query != null)
        {

            dv.RowFilter = query;
        }


        return dv.ToTable();
    }

    protected void MOR_Search_Button_Click(object sender, EventArgs e)
    {
        DataTable dt = Get_MOR_DATA();
        if ( Session["should_proj_name_refresh"] != null)
        {
            if (Session["should_proj_name_refresh"].ToString() == "Yes")
            {
                fill_mor_projectname(dt);
                Session["should_proj_name_refresh"] = "No";
            }
        }
       

        dt = MOR_filter_records(dt);
        mor_grid.DataSource = dt;
        mor_grid.DataBind();
    }

    private DataTable Get_MOR_DATA()
    {
        DataTable dt = new DataTable();
        dt = dal.SelectDetails("Select * from T_MOR_DETAILS order by Portfolio, project_name");
        return dt;
    }
    #endregion


    protected void mor_grid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAL dal = new DAL();
        string smt = "Select * from T_MOR_DETAILS where MOR_ID =" + mor_grid.SelectedDataKey.Value;
        Session["updateMORID"] = mor_grid.SelectedDataKey.Value;
        DataTable dt = dal.SelectDetails(smt);
        ModalPopupExtender_MOR.Show();
        Set_MOR_Edit_Details(dt);




    }

    private void Set_MOR_Edit_Details(DataTable dt)
    {
        MOR_Add_Portfolio.SelectedValue = dt.Rows[0]["Portfolio"].ToString();
        MOR_Add_Category.SelectedValue = dt.Rows[0]["Category"].ToString();
        MOR_Add_Project_Name.Text = dt.Rows[0]["Project_name"].ToString();

        MOR_Add_Description.InnerText = dt.Rows[0]["description"].ToString().TrimStart();

        MOR_Add_LastWeekStatus.SelectedValue = dt.Rows[0]["LAST_WEEK_COLOR"].ToString();
        MOR_Add_CurrentWeekStatus.SelectedValue = dt.Rows[0]["CURRENT_WEEK_COLOR"].ToString();
   
        MOR_Add_Risks.InnerHtml = dt.Rows[0]["Risks"].ToString().TrimStart();

        //   ((System.Web.UI.HtmlControls.HtmlContainerControl)(MOR_PopUp.FindControl("MOR_Add_Risks"))).InnerHtml = dt.Rows[0]["Risks"].ToString().TrimStart();
        // Request.Form["MOR_Add_Risks"] = RemoveHTMLTags(dt.Rows[0]["Risks"].ToString().TrimStart());
        MOR_Add_LastWeekStatus.Enabled = false;
        MOR_Add_NextWeekStatus.SelectedValue = dt.Rows[0]["NEXT_WEEK_COLOR"].ToString();
        MOR_Add_Details.Text = "UPDATE";
        lbl_MOR_Add_Status.Text = "";
    }

    protected void Select_MOR_ID_Click(object sender, EventArgs e)
    {

        ModalPopupExtender_MOR.Show();
    }
    protected void MOR_Add_Button_Click(object sender, EventArgs e)
    {

        ModalPopupExtender_MOR.Show();
    }
    public void fill_mor_grid(DataTable dt)
    {

        mor_grid.DataSource = dt;

        mor_grid.DataBind();
    }

    public void fill_mor_portofolio(DataTable dt)
    {
        string smt = "SELECT  distinct value as PORTFOLIO FROM [MetLifeDD].[dbo].[T_GENERAL_METDD_DATA] where [Key] = 'Portfolio'";

        //mor_portofolio.DataSource = Distinct_Column_Values(dt, "PORTFOLIO", true);
        mor_portofolio.DataSource = dal.SelectDetails(smt);

        mor_portofolio.DataTextField = "PORTFOLIO";

        mor_portofolio.DataValueField = "PORTFOLIO";

        mor_portofolio.DataBind();


    }

    public void fill_Add_mor_portofolio(DataTable dt)
    {

        string smt = "SELECT  distinct value as PORTFOLIO FROM [MetLifeDD].[dbo].[T_GENERAL_METDD_DATA] where [Key] = 'Portfolio' and [Value] <> 'All' ";

      
        MOR_Add_Portfolio.DataSource = dal.SelectDetails(smt);

    //    MOR_Add_Portfolio.DataSource = Distinct_Column_Values(dt, "PORTFOLIO", false);

        MOR_Add_Portfolio.DataTextField = "PORTFOLIO";

        MOR_Add_Portfolio.DataValueField = "PORTFOLIO";

        MOR_Add_Portfolio.DataBind();


    }

    public void fill_mor_projectname(DataTable dt)
    {


        mor_projectname.DataSource = Distinct_Column_Values(dt, "PROJECT_NAME", true);

        mor_projectname.DataTextField = "PROJECT_NAME";

        mor_projectname.DataValueField = "PROJECT_NAME";

        mor_projectname.DataBind();


    }



    public void fill_mor_category(DataTable dt)
    {



        mor_category.DataSource = Distinct_Column_Values(dt, "CATEGORY", true);

        mor_category.DataTextField = "CATEGORY";

        mor_category.DataValueField = "CATEGORY";

        mor_category.DataBind();


    }

    public void fill_Add_mor_category(DataTable dt)
    {




        MOR_Add_Category.DataSource = Distinct_Column_Values(dt, "CATEGORY", false);

        MOR_Add_Category.DataTextField = "CATEGORY";

        MOR_Add_Category.DataValueField = "CATEGORY";

        MOR_Add_Category.DataBind();


    }
    public DataTable Distinct_Column_Values(DataTable dt, string field_name, Boolean b_should_add_ALL)
    {


        DataView dv = new DataView(dt);
        dv.Sort = field_name + " Asc";



         DataTable d_dt = dv.ToTable(field_name, true, field_name);

        if (b_should_add_ALL)
        {
            DataRow drow = d_dt.NewRow();

            drow[0] = "All";

            d_dt.Rows.InsertAt(drow, 0);
        }



        return d_dt;
    }

    protected void mor_portofolio_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void MOR_Reset_Click(object sender, EventArgs e)
    {
        mor_projectname.SelectedValue = "All";
        mor_lastweekstatus.SelectedValue = "All";
        mor_category.SelectedValue = "All";
        mor_currentweekstatus.SelectedValue = "All";
        mor_nextweekstatus.SelectedValue = "All";
        mor_portofolio.SelectedValue = "All";
     
    }

    protected void MOR_Excel_Export_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        

        Response.AddHeader("content-disposition", "attachment;filename=MOR_REPORT.xls");
       // Response.Charset = "";
        Response.ContentType = "application/ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            mor_grid.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < mor_grid.HeaderRow.Cells.Count; i++)
            {
                mor_grid.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }



            mor_grid.RenderControl(hw);

           
            Response.Output.Write(sw.ToString().Replace("</html>", "").Replace("<html>", "").Replace("<td>\r\n\r\n                                        <p class=\"YELLOW\"><span class=\"fa fa-circle\"></span></p>", "<td style=\"background-color:#ffff00;\" >\r\n\r\n <p>YELLOW</p>").Replace("<html>", "").Replace("<td>\r\n\r\n                                        <p class=\"RED\"><span class=\"fa fa-circle\"></span></p>", "<td style=\"background-color:#fffc5e;\" >\r\n\r\n <p>RED</p>").Replace("<td>\r\n\r\n                                        <p class=\"GREEN\"><span class=\"fa fa-circle\"></span></p>", "<td style=\"background-color:#5eea3f;\" >\r\n\r\n <p>GREEN</p>").Replace("<td>\r\n\r\n                                        <p class=\"NA\"><span class=\"fa fa-circle\"></span></p>", "<td style=\"background-color:#99988e;\" >\r\n\r\n <p>NA</p>"));
   
            Response.Flush();
            Response.End();
          


        }
    }

    protected void mor_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string autoid = mor_grid.DataKeys[e.RowIndex].Value.ToString();

        string smt = "delete from T_MOR_DETAILS where MOR_ID = " + autoid;

        dal.UpdateDB(smt);

        DataTable dt = Get_MOR_DATA();
      


        dt = MOR_filter_records(dt);
        mor_grid.DataSource = dt;
        mor_grid.DataBind();

    }
}
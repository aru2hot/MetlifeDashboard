using MasterDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Releases : System.Web.UI.Page
{
    DAL dal = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataTable dt = Get_UDS_REL_INFO_DATA();    

            DataView dv11 = new DataView(dt);
            dv11.Sort = "RELEASESTARTDATE ASC";
            dt = dv11.ToTable();
            dt = Add_RAG_Column(dt);
            Session["All_DT"] = dt;
            DataView dv = new DataView(dt);

            dv.RowFilter = "BU = '" + ddl_BU.SelectedItem.ToString() + "'";
            dt = dv.ToTable();
            Session["Report_DT"] = dt;
            loading_filtercolumn_values(dt);
        }

    }
  

    protected void Search_Button_Click(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)Session["Report_DT"];

        dt = filter_records(dt);

        Session["Filtered_DataTable"] = dt;
        #region prior_tab


        prior_release_grid.DataSource = Get_Prior_Data();

        prior_release_grid.DataBind();

        #endregion

        #region current_tab


        current_release_grid.DataSource = Get_Current_Data();

        current_release_grid.DataBind();

        #endregion

        #region future_tab


        future_release_grid.DataSource = Get_Future_Data();

        future_release_grid.DataBind();

        #endregion
    }


    private DataTable Get_UDS_REL_INFO_DATA()
    {

        DAL dal = new DAL();
        DataTable dt = new DataTable("UDS_DATA");
        dt = dal.SelectDetails("select U.* , R.*, datename(mm,releasestartdate)  + ' ' + CAST(YEAR(releasestartdate) as varchar) as ReleaseMonth from [UDS] as  U left  join  [T_RELEASE_INFO] as R ON U.UDSPROJECTKEY = R.UDS_Key");
        dal = null;
        return dt;
    }

    //added by Arvind
    private DataTable Fill_release_info_Columns(DataTable dt)
    {
        try
        {
            foreach (DataRow row in dt.Rows)
            {
                DataTable dt_rel_info = new DataTable();
                dt_rel_info = GetReleaseInfo(row["UDSPROJECTKEY"].ToString());
                if (dt_rel_info != null)
                {
                    if (dt_rel_info.Rows.Count > 0)
                    {
                        row["qa_strt_dt"] = dt_rel_info.Rows[0]["QA_Start_dt"];
                        row["uat_strt_dt"] = dt_rel_info.Rows[0]["UAT_Start_dt"];
                        row["uat_end_dt"] = dt_rel_info.Rows[0]["UAT_End_dt"];
                        row["qa_end_dt"] = dt_rel_info.Rows[0]["QA_End_dt"];
                        row["last_modified_by"] = dt_rel_info.Rows[0]["Last_Modified_by"];
                        row["last_modified_dt"] = dt_rel_info.Rows[0]["Last_modified_date"];
                        row["override_rag"] = dt_rel_info.Rows[0]["RAG_Override_ID"];
                        row["comments"] = dt_rel_info.Rows[0]["Comments"];
                        row["qa_progress"] = dt_rel_info.Rows[0]["QA_Progress_PERCENT"];
                        row["uat_progress"] = dt_rel_info.Rows[0]["UAT_Progress_PERCENT"];
                    }
                }
                else
                {

                }



            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }

        return dt;

    }

    private DataTable GetReleaseInfo(string projectkey)
    {
        DataTable dt_tmp_release_info = new DataTable();


        string sql_select_rel_info = "select * from T_RELEASE_INFO where UDS_Key = '" + projectkey.Replace("'", "''") + "'";
        dt_tmp_release_info = dal.SelectDetails(sql_select_rel_info);

        return dt_tmp_release_info;
    }

    private DataTable Add_Release_info_columns(DataTable dt)
    {
        foreach (string rel_info in Release_info())
        {
            dt.Columns.Add(rel_info);
        }

        return dt;
    }
    public static List<string> Release_info()
    {

        List<string> Release_info_list = new List<string>() { "QA_strt_dt",
         "UAT_strt_dt",
         "UAT_end_dt",
         "QA_end_dt",
         "Last_modified_by",
         "Last_modified_dt",
         "override_RAG",
         "Comments",
         "QA_Progress",
         "UAT_Progress"
        };

        return Release_info_list;
    }

    public DataTable Get_Prior_Data()
    {
        DataTable filter_dt = (DataTable)Session["Filtered_DataTable"];



        DataView prior_dv = new DataView(filter_dt);

        #region prior_data_logic

        DateTime CurrentMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        prior_dv.RowFilter = "RELEASESTARTDATE < #" + CurrentMonthDate.ToString("yyyy-MM-dd") + "#";

        #endregion

        return prior_dv.ToTable();
    }

    public DataTable Get_Current_Data()
    {

        DataTable dt = (DataTable)Session["All_DT"];

        //dt=Fill_release_info_Columns(dt);

        DataTable filter_dt = (DataTable)Session["Filtered_DataTable"];

        DataView current_dv = new DataView(filter_dt);

        #region current_data_logic

        DateTime NextMonthdate = new DateTime(DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month, 1);

        DateTime CurrentMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        current_dv.RowFilter = "RELEASESTARTDATE >= #" + CurrentMonthDate.ToString("yyyy-MM-dd") + "# and  RELEASESTARTDATE < #" + NextMonthdate.ToString("yyyy-MM-dd") + "#";

        #endregion

        return current_dv.ToTable();
    }
    // Filter Logic Code 

    public DataTable Get_Future_Data()
    {

        //DataTable dt = (DataTable)Session["All_DT"];

        //dt=Fill_release_info_Columns(dt);

        DataTable filter_dt = (DataTable)Session["Filtered_DataTable"];

        DataView future_dv = new DataView(filter_dt);

        #region current_data_logic

        DateTime NextMonthdate = new DateTime(DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month, 1);

        DateTime CurrentMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        future_dv.RowFilter = "RELEASESTARTDATE >= #" + NextMonthdate.ToString("yyyy-MM-dd") + "#";

        #endregion

        return future_dv.ToTable();
    }

    protected void prior_release_child_grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            GridView gv = (GridView)sender;
            gv.EditIndex = e.NewEditIndex;
            // gv.DataSource = (DataTable)Session["temp_child"];
            // gv.DataBind();
        }
        catch (Exception)
        {

        }

        //gv.DataSource = (DataTable)Session["Prior_Tab"];

        //gv.DataBind();


        //prior_release_child_grid.EditIndex = e.NewEditIndex;

        //prior_release_child_grid.DataSource = (DataTable)Session["prior_tab"];
        //prior_release_child_grid.DataBind();
    }
    protected void prior_release_grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        prior_release_grid.EditIndex = e.NewEditIndex;

        prior_release_grid.DataSource = Get_Prior_Data();

        prior_release_grid.DataBind();
    }
    protected void prior_release_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataTable dt = Get_Prior_Data();

        GridViewRow row = prior_release_grid.Rows[e.RowIndex];

        dt.Rows[row.DataItemIndex]["Comments"] = (row.Cells[18].Controls[1] as TextBox).Text;

        dt.Rows[row.DataItemIndex]["QA_Start_dt"] = (row.Cells[9].Controls[1] as TextBox).Text;

        dt.Rows[row.DataItemIndex]["QA_End_dt"] = (row.Cells[10].Controls[1] as TextBox).Text;

        dt.Rows[row.DataItemIndex]["UAT_start_dt"] = (row.Cells[12].Controls[1] as TextBox).Text;

        dt.Rows[row.DataItemIndex]["UAT_End_dt"] = (row.Cells[13].Controls[1] as TextBox).Text;

        // dt.Rows[row.DataItemIndex]["Last_modified_by"] = (row.Cells[15].Controls[1] as TextBox).Text;

        string uds_project_key = (string)dt.Rows[row.DataItemIndex]["UDSPROJECTKEY"];

        string comments = (string)dt.Rows[row.DataItemIndex]["Comments"];

        DateTime qa_start_date = DateTime.Parse(dt.Rows[row.DataItemIndex]["QA_Start_dt"].ToString());

        DateTime qa_end_date = DateTime.Parse(dt.Rows[row.DataItemIndex]["QA_End_dt"].ToString());

        DateTime uat_start_date = DateTime.Parse(dt.Rows[row.DataItemIndex]["UAT_Start_dt"].ToString());

        DateTime uat_end_date = DateTime.Parse(dt.Rows[row.DataItemIndex]["UAT_End_dt"].ToString());


        update_release_info(uds_project_key, comments, qa_start_date, qa_end_date, uat_start_date, uat_end_date);

        DataTable temp = (DataTable)Session["Report_DT"];

        DataRow[] Rows = temp.Select("UDSPROJECTKEY='" + uds_project_key + "'");

        Rows[0]["Comments"] = comments;

        Rows[0]["QA_Start_dt"] = qa_start_date;

        Session["Report_DT"] = temp;

        Session["Filtered_DataTable"] = dt;

        prior_release_grid.EditIndex = -1;




        prior_release_grid.DataSource = Get_Prior_Data();

        prior_release_grid.DataBind();



    }

    public void update_release_info(string uds_key, string comment, DateTime qa_start_date, DateTime qa_end_date, DateTime uat_start_date, DateTime uat_end_date)
    {
        try
        {
            string name = Environment.UserName;

            string query = "update T_RELEASE_INFO set Comments = '" + comment + "', QA_Start_dt = '" + qa_start_date + "', QA_End_dt = '" + qa_end_date + "', UAT_Start_dt = '" + uat_start_date + "', UAT_End_dt = '" + uat_end_date + "', Last_Modified_by = '" + Environment.UserName + "', Last_modified_date = '" + DateTime.Now + "' where UDS_Key = '" + uds_key + "'";
            dal.UpdateDB(query);

        }
        catch (Exception e)
        {

        }

    }
    protected void prior_release_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        prior_release_grid.EditIndex = -1;

        prior_release_grid.DataSource = Get_Prior_Data();

        prior_release_grid.DataBind();
    }
    protected void current_data(string key)
    {

    }
    protected void current_edit_button_Click(object sender, EventArgs e)
    {

    }

    protected void current_release_grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Determine the RowIndex of the Row whose Button was clicked.
        int rowIndex = Convert.ToInt32(e.CommandArgument);

        //Reference the GridView Row.
        GridViewRow row = current_release_grid.Rows[rowIndex];

        //Fetch value of Name.
        string UDS_Key = (row.FindControl("GV_CF_UDS_Project_Key") as Label).Text;

        //Fetch value of Country
        string RELEASENAME = (current_release_grid.Rows[rowIndex].FindControl("GV_CF_ReleaseName") as Label).Text;
        string Application = (current_release_grid.Rows[rowIndex].FindControl("GV_CF_Application") as Label).Text;
        string PBM1 = (current_release_grid.Rows[rowIndex].FindControl("GV_CF_PBM1") as Label).Text;
        string PBM2 = (current_release_grid.Rows[rowIndex].FindControl("GV_CF_PBM2") as Label).Text;

        PopUp_CF_Release_Name.Text = RELEASENAME + "";
        PopUp_CF_Application.Text = Application;
        PopUp_CF_PBM1.Text = PBM1;
        PopUp_CF_PBM2.Text = PBM1;


        ModalPopupExtender2.Show();



    }
    protected void current_release_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string txt = Get_Description((int)DataBinder.Eval(e.Row.DataItem, "CURRENTPHASE"));

            e.Row.Cells[8].Text = txt;
        }
    }
    protected void prior_release_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string txt = Get_Description((int)DataBinder.Eval(e.Row.DataItem, "CURRENTPHASE"));

            e.Row.Cells[7].Text = txt;
        }

    }
    protected void CF_Close_Click(object sender, EventArgs e)
    {
        Session["REL_INFO_EDIT_KEY"] = "";
        ModalPopupExtender2.Hide();
    }
    protected void F_Close_Click(object sender, EventArgs e)
    {
        Session["REL_INFO_EDIT_KEY"] = "";
        ModalPopupExtender_FUTURE.Hide();
    }

    protected void current_release_grid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAL dal = new DAL();

        string query = "Select *  FROM  [UDS] where UDSPROJECTKEY = '" + current_release_grid.SelectedDataKey.Value + "'";

        DataTable UDS_Data = dal.SelectDetails(query);

        query = "Select *  FROM [T_RELEASE_INFO] where UDS_Key = '" + current_release_grid.SelectedDataKey.Value + "'";

        DataTable ReleaseInfo_Data = dal.SelectDetails(query);

        Set_CFPopUp_Details(UDS_Data, ReleaseInfo_Data, current_release_grid.SelectedDataKey.Value.ToString());

        ModalPopupExtender2.Show();

    }

    public void Set_CFPopUp_Details(DataTable UDS_Data, DataTable ReleaseInfo_Data, string UDSPROJECTKEY)
    {
        Session["REL_INFO_EDIT_KEY"] = UDSPROJECTKEY;
        string RELEASENAME = UDS_Data.Rows[0]["RELEASENAME"].ToString();
        string Application = UDS_Data.Rows[0]["APPLICATION"].ToString();
        string PBM1 = UDS_Data.Rows[0]["PBM1"].ToString();
        string PBM2 = UDS_Data.Rows[0]["PBM2"].ToString();
        int seq_no = int.Parse(UDS_Data.Rows[0]["CURRENTPHASE"].ToString());
        string Current_Phase = Get_Description(seq_no);
        string PSDefects = UDS_Data.Rows[0]["PSDEFECTS"].ToString();


        DataTable dt = (DataTable)Session["Filtered_DataTable"];

        DataView dv = new DataView(dt);

        dv.RowFilter = "UDSPROJECTKEY = '" + UDSPROJECTKEY + "'";

        dt = dv.ToTable();

        PopUp_CF_RAG.Text = dt.Rows[0]["RAG"].ToString();

        PopUp_CF_Release_Name.Text = RELEASENAME;
        PopUp_CF_Application.Text = Application;
        PopUp_CF_PBM1.Text = PBM1;
        PopUp_CF_PBM2.Text = PBM2;
        PopUp_CF_Current_Phase.Text = Current_Phase;

        PopUp_CF_ProductionDefects.Text = PSDefects;
        PopUp_CF_ReleaseDate.Text = UDS_Data.Rows[0]["RELEASESTARTDATE"].ToString();
        if (ReleaseInfo_Data.Rows.Count > 0)
        {

            PopUp_CF_QAStartDate.Text = ReleaseInfo_Data.Rows[0]["QA_Start_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["QA_Start_dt"]).ToString("yyyy-MM-dd") : "";
            PopUp_CF_QAEndDate.Text = ReleaseInfo_Data.Rows[0]["QA_End_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["QA_End_dt"]).ToString("yyyy-MM-dd") : "";
            PopUp_CF_QAProgress.Text = ReleaseInfo_Data.Rows[0]["QA_Progress_PERCENT"].ToString();
            PopUp_CF_UATStartDate.Text = ReleaseInfo_Data.Rows[0]["UAT_Start_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["UAT_Start_dt"].ToString()).ToString("yyyy-MM-dd") : "";
            PopUp_CF_UATEndDate.Text = ReleaseInfo_Data.Rows[0]["UAT_End_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["UAT_End_dt"].ToString()).ToString("yyyy-MM-dd") : "";
            PopUp_CF_UATProgress.Text = ReleaseInfo_Data.Rows[0]["UAT_Progress_PERCENT"].ToString();
            PopUp_CF_ManagerComments.Text = ReleaseInfo_Data.Rows[0]["Comments"].ToString();

        }
        else
        {
            PopUp_CF_QAStartDate.Text = "";
            PopUp_CF_QAEndDate.Text = "";
            PopUp_CF_QAProgress.Text = "";
            PopUp_CF_UATStartDate.Text = "";
            PopUp_CF_UATEndDate.Text = "";
            PopUp_CF_UATProgress.Text = "";
            PopUp_CF_ManagerComments.Text = "";

        }

    }

    public void Set_F_PopUp_Details(DataTable UDS_Data, DataTable ReleaseInfo_Data, string UDSPROJECTKEY)
    {
        Session["REL_INFO_EDIT_KEY"] = UDSPROJECTKEY;
        string RELEASENAME = UDS_Data.Rows[0]["RELEASENAME"].ToString();
        string Application = UDS_Data.Rows[0]["APPLICATION"].ToString();
        string PBM1 = UDS_Data.Rows[0]["PBM1"].ToString();
        string PBM2 = UDS_Data.Rows[0]["PBM2"].ToString();
        int seq_no = int.Parse(UDS_Data.Rows[0]["CURRENTPHASE"].ToString());
        string Current_Phase = Get_Description(seq_no);
        string PSDefects = UDS_Data.Rows[0]["PSDEFECTS"].ToString();


        DataTable dt = (DataTable)Session["Filtered_DataTable"];

        DataView dv = new DataView(dt);

        dv.RowFilter = "UDSPROJECTKEY = '" + UDSPROJECTKEY + "'";

        dt = dv.ToTable();

        PopUp_F_RAG.Text = dt.Rows[0]["RAG"].ToString();

        PopUp_F_Release_Name.Text = RELEASENAME;
        PopUp_F_Application.Text = Application;
        PopUp_F_PBM1.Text = PBM1;
        PopUp_F_PBM2.Text = PBM2;
        PopUp_F_Current_Phase.Text = Current_Phase;

        PopUp_F_ProductionDefects.Text = PSDefects;
        PopUp_F_ReleaseDate.Text = UDS_Data.Rows[0]["RELEASESTARTDATE"].ToString();
        if (ReleaseInfo_Data.Rows.Count > 0)
        {

            PopUp_F_QAStartDate.Text = ReleaseInfo_Data.Rows[0]["QA_Start_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["QA_Start_dt"]).ToString("yyyy-MM-dd") : "";
            PopUp_F_QAEndDate.Text = ReleaseInfo_Data.Rows[0]["QA_End_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["QA_End_dt"]).ToString("yyyy-MM-dd") : "";
            PopUp_F_QAProgress.Text = ReleaseInfo_Data.Rows[0]["QA_Progress_PERCENT"].ToString();
            PopUp_F_UATStartDate.Text = ReleaseInfo_Data.Rows[0]["UAT_Start_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["UAT_Start_dt"].ToString()).ToString("yyyy-MM-dd") : "";
            PopUp_F_UATEndDate.Text = ReleaseInfo_Data.Rows[0]["UAT_End_dt"].ToString() != "" ? Convert.ToDateTime(ReleaseInfo_Data.Rows[0]["UAT_End_dt"].ToString()).ToString("yyyy-MM-dd") : "";
            PopUp_F_UATProgress.Text = ReleaseInfo_Data.Rows[0]["UAT_Progress_PERCENT"].ToString();
            PopUp_F_ManagerComments.Text = ReleaseInfo_Data.Rows[0]["Comments"].ToString();

        }
        else
        {
            PopUp_F_QAStartDate.Text = "";
            PopUp_F_QAEndDate.Text = "";
            PopUp_F_QAProgress.Text = "";
            PopUp_F_UATStartDate.Text = "";
            PopUp_F_UATEndDate.Text = "";
            PopUp_F_UATProgress.Text = "";
            PopUp_F_ManagerComments.Text = "";

        }

    }

    protected void CF_Update_Click(object sender, EventArgs e)
    {
        string sel_rel_info = "select Count(*) from  T_RELEASE_INFO where UDS_Key = '" + Session["REL_INFO_EDIT_KEY"].ToString() + "'";

        int count = dal.GetCountFromDB(sel_rel_info);

        if (count <= 0)
        {
            string get_max_Rel_ID = "select max(Release_Info_ID) from  T_RELEASE_INFO";
            int max_rel_id = dal.SelectMax(get_max_Rel_ID);
            max_rel_id = max_rel_id + 1;
            string insert_Rel_info = "insert into T_RELEASE_INFO(Release_Info_ID,UDS_Key) values ( " + max_rel_id + ",'" + Session["REL_INFO_EDIT_KEY"].ToString() + "')";
            dal.insertintoDB(insert_Rel_info);
        }

        string update_stmt_rel_info = " Update T_Release_info set " + "QA_Start_dt = " + (PopUp_CF_QAStartDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_CF_QAStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",QA_End_dt = " + (PopUp_CF_QAEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_CF_QAEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",UAT_Start_dt = " + (PopUp_CF_UATStartDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_CF_UATStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",UAT_End_dt = " + (PopUp_CF_UATEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_CF_UATEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",QA_Progress_PERCENT = " + (PopUp_CF_QAProgress.Text.Replace(",", "") == "" ? "0" : PopUp_CF_QAProgress.Text.Replace(",", "")) + ",UAT_Progress_PERCENT = " + (PopUp_CF_UATProgress.Text.Replace(",", "") == "" ? "0" : PopUp_CF_UATProgress.Text.Replace(",", "")) + ",Comments = '" + PopUp_CF_ManagerComments.Text.ToString().Replace(",,", "") + "' where UDS_Key = '" + Session["REL_INFO_EDIT_KEY"].ToString() + "'";

        dal.UpdateDB(update_stmt_rel_info);
        reset_CurrentRelease_PopUp();
        refresh_Master_source();
        current_release_grid.DataSource = Get_Current_Data();

        current_release_grid.DataBind();
        Session["REL_INFO_EDIT_KEY"] = "";
        ModalPopupExtender2.Hide();

    }
    public static DataTable Add_RAG_Column(DataTable dt)
    {

        dt.Columns.Add("RAG");

        return dt;
    }
    public void refresh_Master_source()
    {
        DataTable dt = Get_UDS_REL_INFO_DATA();
        // dt = Fill_release_info_Columns(dt);
        dt = Add_RAG_Column(dt);
        Session["All_DT"] = dt;

        DataView dv = new DataView(dt);

        dv.RowFilter = "BU = '" + ddl_BU.SelectedItem.ToString() + "'";

        dt = dv.ToTable();

        Session["Report_DT"] = dt;

        dt = filter_records(dt);

        Session["Filtered_DataTable"] = dt;

        dt = null;
        dv = null;
    }
    private void reset_CurrentRelease_PopUp()
    {
        PopUp_CF_QAStartDate.Text = "";
        PopUp_CF_QAEndDate.Text = "";
        PopUp_CF_QAProgress.Text = "";
        PopUp_CF_UATStartDate.Text = "";
        PopUp_CF_UATEndDate.Text = "";
        PopUp_CF_UATProgress.Text = "";
        PopUp_CF_ManagerComments.Text = "";

    }
    private void reset_FutureRelease_PopUp()
    {
        PopUp_F_QAStartDate.Text = "";
        PopUp_F_QAEndDate.Text = "";
        PopUp_F_QAProgress.Text = "";
        PopUp_F_UATStartDate.Text = "";
        PopUp_F_UATEndDate.Text = "";
        PopUp_F_UATProgress.Text = "";
        PopUp_F_ManagerComments.Text = "";

    }
    protected void future_release_grid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DAL dal = new DAL();

        string query = "Select *  FROM  [UDS] where UDSPROJECTKEY = '" + future_release_grid.SelectedDataKey.Value + "'";

        DataTable UDS_Data = dal.SelectDetails(query);

        query = "Select *  FROM [T_RELEASE_INFO] where UDS_Key = '" + future_release_grid.SelectedDataKey.Value + "'";

        DataTable ReleaseInfo_Data = dal.SelectDetails(query);

        Set_F_PopUp_Details(UDS_Data, ReleaseInfo_Data, future_release_grid.SelectedDataKey.Value.ToString());

        ModalPopupExtender_FUTURE.Show();
    }

    protected void future_release_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string txt = Get_Description((int)DataBinder.Eval(e.Row.DataItem, "CURRENTPHASE"));

            e.Row.Cells[8].Text = txt;
        }
    }

    protected void F_Update_Click(object sender, EventArgs e)
    {
        string sel_rel_info = "select Count(*) from  T_RELEASE_INFO where UDS_Key = '" + Session["REL_INFO_EDIT_KEY"].ToString() + "'";

        int count = dal.GetCountFromDB(sel_rel_info);

        if (count <= 0)
        {
            string get_max_Rel_ID = "select max(Release_Info_ID) from  T_RELEASE_INFO";
            int max_rel_id = dal.SelectMax(get_max_Rel_ID);
            max_rel_id = max_rel_id + 1;
            string insert_Rel_info = "insert into T_RELEASE_INFO(Release_Info_ID,UDS_Key) values ( " + max_rel_id + ",'" + Session["REL_INFO_EDIT_KEY"].ToString() + "')";
            dal.insertintoDB(insert_Rel_info);
        }

        string update_stmt_rel_info = " Update T_Release_info set " + "QA_Start_dt = " + (PopUp_F_QAStartDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_QAStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",QA_End_dt = " + (PopUp_F_QAEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_QAEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",UAT_Start_dt = " + (PopUp_F_UATStartDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_UATStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",UAT_End_dt = " + (PopUp_F_UATEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_UATEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",QA_Progress_PERCENT = " + (PopUp_F_QAProgress.Text.Replace(",", "") == "" ? "0" : PopUp_F_QAProgress.Text.Replace(",", "")) + ",UAT_Progress_PERCENT = " + (PopUp_F_UATProgress.Text.Replace(",", "") == "" ? "0" : PopUp_F_UATProgress.Text.Replace(",", "")) + ",Comments = '" + PopUp_F_ManagerComments.Text.ToString().Replace(",", "") + "' where UDS_Key = '" + Session["REL_INFO_EDIT_KEY"].ToString() + "'";

        dal.UpdateDB(update_stmt_rel_info);
        reset_FutureRelease_PopUp();
        refresh_Master_source();
        future_release_grid.DataSource = Get_Future_Data();

        future_release_grid.DataBind();
        Session["REL_INFO_EDIT_KEY"] = "";
        ModalPopupExtender_FUTURE.Hide();

    }

    public static string Get_Description(int seq)
    {

        switch (seq)
        {
            case 0:
                return "CREATE PROJECT IN UDS";

            case 1:
                return "ANALYSIS";
            case 2:
                return "DESIGN";
            case 3:
                return "CODING AND UNIT TESTING";
            case 4:
                return "FUNCTIONAL AND E2E TESTING";
            case 5:
                return "INTEGRATION TESTING";
            case 6:
                return "QC SUPPORT";
            case 7:
                return "UAT SUPPORT";
            case 8:
                return "IMPLEMENTATION";
            case 9:
                return "WARRANTY";
            default:
                return "Wrong Sequence No";

        }
    }
    public DataTable filter_records(DataTable dt)
    {
        DataView dv = new DataView(dt);

        string query = null;



        if (ddl_ReleaseMonth.SelectedItem.ToString() != null)
        {
            if (ddl_ReleaseMonth.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }

                query += "`ReleaseMonth` = '" + ddl_ReleaseMonth.SelectedItem.ToString() + "'";

            }
        }




        if (ddl_Portfolio.SelectedItem.ToString() != null)
        {

            if (ddl_Portfolio.SelectedItem.ToString() != "All")
            {

                if (query != null)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }


                query += "PORTFOLIO = '" + ddl_Portfolio.SelectedItem.ToString() + "'";

            }
        }


        if (ddl_Application.SelectedItem.ToString() != null)
        {

            if (ddl_Application.SelectedItem.ToString() != "All")
            {
                if (query != null)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }

                query += " `Application` = '" + ddl_Application.SelectedItem.ToString() + "'";
            }

        }


        //if (RAG_status.SelectedItem.ToString() != null)
        //{


        //    if (RAG_status.SelectedItem.ToString() != "All")
        //    {

        //        if (query != null)
        //        {
        //            query += " AND ";
        //        }
        //        else
        //        {
        //            query += " ";
        //        }

        //        query += "RAG = '" + RAG_status.SelectedItem.ToString() + "'";
        //    }


        //}

        if (ddl_Releases.SelectedItem.ToString() != null)
        {

            if (ddl_Releases.SelectedItem.ToString() != "All")
            {

                if (query != null)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }

                query += "`RELEASENAME` = '" + ddl_Releases.SelectedItem.ToString() + "'";
            }

        }


        if (ddl_PBM.SelectedItem.ToString() != null)
        {
            if (ddl_PBM.SelectedItem.ToString() != "All")
            {

                if (query != null)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }

                query += "PBM = '" + ddl_PBM.SelectedItem.ToString() + "'";
            }

        }




        if (ddl_PBM1.SelectedItem.ToString() != null)
        {
            if (ddl_PBM1.SelectedItem.ToString() != "All")
            {

                if (query != null)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }

                query += "PBM1 = '" + ddl_PBM1.SelectedItem.ToString() + "'";
            }

        }



        if (ddl_PBM2.SelectedItem.ToString() != null)
        {
            if (ddl_PBM2.SelectedItem.ToString() != "All")
            {

                if (query != null)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }

                query += "PBM2 = '" + ddl_PBM2.SelectedItem.ToString() + "'";
            }

        }








        if (query != null)
        {

            dv.RowFilter = query;
        }


        return dv.ToTable();
    }
    protected void ddl_Application_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["Report_DT"];

        DataView dv = new DataView(dt);

        if (ddl_ReleaseMonth.SelectedValue.ToString() != "All")
        {

            dv.RowFilter = "`ReleaseMonth` = '" + ddl_ReleaseMonth.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM= '" + ddl_PBM.SelectedValue.ToString() + "'";
        }


        dv = new DataView(dv.ToTable());

        if (ddl_PBM1.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM1 = '" + ddl_PBM1.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM2.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM2 = '" + ddl_PBM2.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_Portfolio.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PORTFOLIO= '" + ddl_Portfolio.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_Application.SelectedValue.ToString() != "All")
        {

            dv.RowFilter = "`Application` = '" + ddl_Application.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        dt = dv.ToTable();

        loading_releases_dropdown(dt);

    }
    protected void ddl_PBM1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["Report_DT"];

        DataView dv = new DataView(dt);

        if (ddl_ReleaseMonth.SelectedValue.ToString() != "All")
        {

            dv.RowFilter = "`ReleaseMonth` = '" + ddl_ReleaseMonth.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM = '" + ddl_PBM.SelectedValue.ToString() + "'";
        }
        dv = new DataView(dv.ToTable());

        if (ddl_PBM1.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM1 = '" + ddl_PBM1.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        dt = dv.ToTable();


        loading_pbm2_dropdown(dt);

        loading_portfolio_dropdown(dt);

        loading_application_dropdown(dt);

        loading_releases_dropdown(dt);


    }
    public void loading_pbm2_dropdown(DataTable dt)
    {

        ddl_PBM2.DataSource = Distinct_Column_Values(dt, "PBM2", true);

        ddl_PBM2.DataTextField = "PBM2";

        ddl_PBM2.DataValueField = "PBM2";

        ddl_PBM2.DataBind();

    }
    protected void ddl_PBM_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["Report_DT"];

        DataView dv = new DataView(dt);

        if (ddl_ReleaseMonth.SelectedValue.ToString() != "All")
        {

            dv.RowFilter = "`ReleaseMonth` = '" + ddl_ReleaseMonth.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM = '" + ddl_PBM.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        dt = dv.ToTable();

        loading_pbm1_dropdown(dt);

        loading_pbm2_dropdown(dt);

        loading_portfolio_dropdown(dt);

        loading_application_dropdown(dt);

        loading_releases_dropdown(dt);


    }
    public void loading_pbm1_dropdown(DataTable dt)
    {

        ddl_PBM1.DataSource = Distinct_Column_Values(dt, "PBM1", true);

        ddl_PBM1.DataTextField = "PBM1";

        ddl_PBM1.DataValueField = "PBM1";

        ddl_PBM1.DataBind();

    }
    
    protected void Reset_Button_Click(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)Session["Report_DT"];

        loading_filtercolumn_values(dt);

       

       

    }
   
    public void loading_filtercolumn_values(DataTable dt)
    {

        DataView dv = new DataView(dt);

        loading_releasemonth_dropdown(dt);

        loading_pbm_dropdown(dt);

        loading_pbm1_dropdown(dt);

        loading_pbm2_dropdown(dt);

        loading_portfolio_dropdown(dt);

        loading_application_dropdown(dt);



        loading_releases_dropdown(dt);



    }
    public void loading_pbm_dropdown(DataTable dt)
    {

        ddl_PBM.DataSource = Distinct_Column_Values(dt, "PBM", true);

        ddl_PBM.DataTextField = "PBM";

        ddl_PBM.DataValueField = "PBM";

        ddl_PBM.DataBind();

    }
    public void loading_releasemonth_dropdown(DataTable dt)
    {
        ddl_ReleaseMonth.DataSource = Distinct_Column_Values(dt, "ReleaseMonth", true);

        //ddl_ReleaseMonth.Items.Insert(0, new ListItem("Select", "NA"));

        ddl_ReleaseMonth.DataTextField = "ReleaseMonth";

        ddl_ReleaseMonth.DataValueField = "ReleaseMonth";

        //ddl_ReleaseMonth.DataTextFormatString = "{0:y}";        

        ddl_ReleaseMonth.DataBind();

    }
    protected void ddl_Portfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["Report_DT"];

        DataView dv = new DataView(dt);

        if (ddl_ReleaseMonth.SelectedValue.ToString() != "All")
        {

            dv.RowFilter = "`ReleaseMonth` = '" + ddl_ReleaseMonth.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM= '" + ddl_PBM.SelectedValue.ToString() + "'";
        }


        dv = new DataView(dv.ToTable());

        if (ddl_PBM1.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM1 = '" + ddl_PBM1.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM2.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM2 = '" + ddl_PBM2.SelectedValue.ToString() + "'";
        }


        dv = new DataView(dv.ToTable());

        if (ddl_Portfolio.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PORTFOLIO= '" + ddl_Portfolio.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        dt = dv.ToTable();

        loading_application_dropdown(dt);

        loading_releases_dropdown(dt);



    }
    public void loading_application_dropdown(DataTable dt)
    {


        ddl_Application.DataSource = Distinct_Column_Values(dt, "Application", true);

        ddl_Application.DataTextField = "Application";

        ddl_Application.DataValueField = "Application";

        ddl_Application.DataBind();

    }
    public void loading_releases_dropdown(DataTable dt)
    {
        ddl_Releases.DataSource = Distinct_Column_Values(dt, "RELEASENAME", true);

        ddl_Releases.DataTextField = "RELEASENAME";

        ddl_Releases.DataValueField = "RELEASENAME";

        ddl_Releases.DataBind();
    }
    public DataTable Distinct_Column_Values(DataTable dt, string field_name, Boolean b_should_add_ALL)
    {


        DataView dv = new DataView(dt);

        DataTable d_dt = dv.ToTable(field_name, true, field_name);

        if (b_should_add_ALL)
        {
            DataRow drow = d_dt.NewRow();

            drow[0] = "All";

            d_dt.Rows.InsertAt(drow, 0);
        }



        return d_dt;
    }

   
    protected void ddl_PBM2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["Report_DT"];

        DataView dv = new DataView(dt);

        if (ddl_ReleaseMonth.SelectedValue.ToString() != "All")
        {

            dv.RowFilter = "`ReleaseMonth` = '" + ddl_ReleaseMonth.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM = '" + ddl_PBM.SelectedValue.ToString() + "'";
        }
        dv = new DataView(dv.ToTable());

        if (ddl_PBM1.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM1 = '" + ddl_PBM1.SelectedValue.ToString() + "'";
        }

        dv = new DataView(dv.ToTable());

        if (ddl_PBM2.SelectedValue.ToString() != "All")
        {
            dv.RowFilter = "PBM2 = '" + ddl_PBM2.SelectedValue.ToString() + "'";
        }


        dv = new DataView(dv.ToTable());

        dt = dv.ToTable();

        loading_portfolio_dropdown(dt);

        loading_application_dropdown(dt);

        loading_releases_dropdown(dt);


    }
    public void loading_portfolio_dropdown(DataTable dt)
    {
        ddl_Portfolio.DataSource = Distinct_Column_Values(dt, "PORTFOLIO", true);

        ddl_Portfolio.DataTextField = "PORTFOLIO";

        ddl_Portfolio.DataValueField = "PORTFOLIO";

        ddl_Portfolio.DataBind();
    }

    protected void ddl_BU_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["All_DT"];

        DataView dv = new DataView(dt);

        dv.RowFilter = "BU = '" + ddl_BU.SelectedItem.ToString() + "'";

        dt = dv.ToTable();

        Session["Report_DT"] = dt;

        loading_filtercolumn_values(dt);

    }

    protected void ddl_ReleaseMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["Report_DT"];

        if (ddl_ReleaseMonth.SelectedValue.ToString() == "All")
        {
            loading_filtercolumn_values(dt);

        }
        else
        {
            DataView dv = new DataView(dt);

            dv.RowFilter = "`ReleaseMonth` = '" + ddl_ReleaseMonth.SelectedValue.ToString() + "'";

            dt = dv.ToTable();

            loading_pbm_dropdown(dt);

            loading_pbm1_dropdown(dt);

            loading_pbm2_dropdown(dt);

            loading_portfolio_dropdown(dt);

            loading_application_dropdown(dt);

            loading_releases_dropdown(dt);

        }
    }

  
  
}
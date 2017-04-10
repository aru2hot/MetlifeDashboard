using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Services;
using MasterDetail;

public partial class View : Page
{


    #region Page_Load
    DAL dal = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            //#region Restrict the direct page load

            //if (Request.UrlReferrer == null || Session["Control_ID"] == null)
            //{
            //    Response.Redirect("/Home.aspx");
            //}
            //else if (Request.UrlReferrer.AbsolutePath.ToLower() != ("/Home.aspx").ToLower() && Request.UrlReferrer.AbsolutePath.ToLower() != ("/Home").ToLower())
            //{
            //    Response.Redirect("/Home.aspx");
            //}


            //string Control_ID = Session["Control_ID"].ToString();


            //#endregion



          //  DataTable dt = Get_UDS_DATA();
            DataTable dt = Get_UDS_REL_INFO_DATA();
            #region ReleaseMonth Sort

            DataView dv11 = new DataView(dt);

            dv11.Sort = "RELEASESTARTDATE ASC";

            dt = dv11.ToTable();

            #endregion

            dt = Add_SLAMetrics_Columns(dt);

            dt = Add_RAG_Column(dt);

            dt = Add_ReleaseMonth_Column(dt);
            // added by Arivind
         //   dt = Add_Release_info_columns(dt);

            //Filling The Values
            dt = Fill_SLAMetrics_Columns(dt);
       //     dt = Fill_release_info_Columns(dt);
            Session["All_DT"] = dt;

            DataView dv = new DataView(dt);


            dv.RowFilter = "BU = '" + ddl_BU.SelectedItem.ToString() + "'";

            dt = dv.ToTable();

            Session["Report_DT"] = dt;

            loading_filtercolumn_values(dt);

            Red_Project_List.Visible = false;

            // DataTable dt1 = grouping_data(dt);
            // TabName.Value = Request.Form[TabName.UniqueID];
           


        }



    }

    private DataTable Get_UDS_REL_INFO_DATA()
    {

        DAL dal = new DAL();
        DataTable dt = new DataTable("UDS_DATA");
        dt = dal.SelectDetails("select U.* , R.* from [UDS] as  U left  join  [T_RELEASE_INFO] as R ON U.UDSPROJECTKEY = R.UDS_Key");
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


        string sql_select_rel_info = "select * from T_RELEASE_INFO where UDS_Key = '" + projectkey.Replace("'","''") + "'";
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

    #endregion

    #region newcode

    public static DataTable Add_SLAMetrics_Columns(DataTable dt)
    {

        foreach (string SLA_Metric in SLAMetrics())
        {
            dt.Columns.Add(SLA_Metric);
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

    public static List<string> SLAMetrics()
    {

        List<string> SLA_Metrics_List = new List<string>() { "req_quality_score",
         "req_quality_value",
         "req_stab_score",
         "req_stab_value",
         "des_rev_cov_score",
         "des_rev_cov_value",
         "code_rev_cov_score",
         "code_rev_cov_value",
         "unit_test_cov_score",
         "unit_test_cov_value",
         "on_time_del_score",
         "on_time_del_value",
         "delivery_defect_density_score",
         "delivery_defect_density_value"
        };

        return SLA_Metrics_List;
    }

    public static DataTable Add_RAG_Column(DataTable dt)
    {

        dt.Columns.Add("RAG");

        return dt;
    }

    public static DataTable Add_ReleaseMonth_Column(DataTable dt)
    {
        dt.Columns.Add("ReleaseMonth");

        return dt;
    }

    public static DataTable Fill_SLAMetrics_Columns(DataTable dt)
    {
        // StreamWriter writer = new StreamWriter(@"D:\DD_Testlogs.txt");

        int i = 0;

        foreach (DataRow row in dt.Rows)
        {
            i++;

            try
            {
                List<int> list_SLA_Metric_Inputs = new List<int>();

                foreach (string SLA_Metric_Input in SLA_Metric_Inputs())
                {
                    if (row[SLA_Metric_Input] == DBNull.Value)
                    {
                        list_SLA_Metric_Inputs.Add(0);

                    }
                    else
                    {

                        int t = int.Parse(row[SLA_Metric_Input].ToString());

                        list_SLA_Metric_Inputs.Add(t);

                    }
                }

                List<string> list_SLA_IsApplicable_Inputs = new List<string>();

                foreach (string SLA_Metric_Input in SLA_IsApplicable_Inputs())
                {

                    if (row[SLA_Metric_Input] == DBNull.Value)
                    {
                        list_SLA_IsApplicable_Inputs.Add(DBNull.Value.ToString());

                    }
                    else
                    {

                        list_SLA_IsApplicable_Inputs.Add(row[SLA_Metric_Input].ToString());

                    }

                }

                SLACalc obj = new SLACalc(list_SLA_Metric_Inputs, list_SLA_IsApplicable_Inputs);

                row["req_quality_score"] = obj.req_quality_score;
                row["req_quality_value"] = obj.req_quality_value;
                row["req_stab_score"] = obj.req_stab_score;
                row["req_stab_value"] = obj.req_stab_value;
                row["des_rev_cov_score"] = obj.des_rev_cov_score;
                row["des_rev_cov_value"] = obj.des_rev_cov_value;
                row["code_rev_cov_score"] = obj.code_rev_cov_score;
                row["code_rev_cov_value"] = obj.code_rev_cov_value;
                row["unit_test_cov_score"] = obj.unit_test_cov_score;
                row["unit_test_cov_value"] = obj.unit_test_cov_value;
                row["on_time_del_score"] = obj.on_time_del_score;
                row["on_time_del_value"] = obj.on_time_del_value;
                row["delivery_defect_density_score"] = obj.delivery_defect_density_score;
                row["delivery_defect_density_value"] = obj.delivery_defect_density_value;

                int RAG_Result = RAG_Status(obj);

                if (RAG_Result == -1)
                {

                    row["RAG"] = "RED";
                }
                else
                {
                    row["RAG"] = "GREEN";
                }

                row["ReleaseMonth"] = string.Format("{0:y}", row["RELEASESTARTDATE"]);

                list_SLA_Metric_Inputs.Clear();

                list_SLA_IsApplicable_Inputs.Clear();

                obj = null;





            }
            catch (Exception e)
            {
                // writer.WriteLine(e.Message);

                Console.WriteLine(e.Message);

            }


        }

        // writer.Flush();

        // writer.Close();


        return dt;

    }

    public static List<string> SLA_Metric_Inputs()
    {

        List<string> SLA_Metrics_Input_List = new List<string>() {



              "REQDEFECTCOUNT",

       "REQUIREMENTCOUNT",

           "CRCOUNT",

       "DESIGNREVIEWED",

 "DESIGNARTIFACTS",

     "CODEREVIEWED",

        "CODECOMPONENTS",

      "UNITTESTED",

       "DELIVERABLESONTIME",

      "DELIVERABLES",

     "UTPASSED",

    "UTCASES",

    "PHASESEQ"




            };

        return SLA_Metrics_Input_List;
    }

    public static List<string> SLA_IsApplicable_Inputs()
    {

        List<string> SLA_Metrics_Input_List = new List<string>() {


        "REQUIREMENTDAYSLATEAPPLICABLE",
"REQUIREMENTDAYSLATEAPPLICABLE",
"DRCAPPLICABLE",
"CRCAPPLICABLE",
"UTCAPPLICABLE",
"OTDAPPLICABLE",
"DDDAPPLICABLE"




            };

        return SLA_Metrics_Input_List;
    }

    public static int RAG_Status(SLACalc obj)
    {

        if (Get_RAG_Result(obj.req_quality_score) || Get_RAG_Result(obj.req_stab_score) || Get_RAG_Result(obj.des_rev_cov_score) || Get_RAG_Result(obj.code_rev_cov_score) || Get_RAG_Result(obj.unit_test_cov_score) || Get_RAG_Result(obj.on_time_del_score) || Get_RAG_Result(obj.delivery_defect_density_score))
        {
            return -1;

        }
        else
        {
            return 0;
        }






    }

    public static bool Get_RAG_Result(int value)
    {
        if (value >= 0 && value < 3)
        {
            return true;

        }

        else
        {

            return false;
        }

    }

    public DataTable grouping_data(DataTable dt, string sel_group_col)
    {
        DataView dv = new DataView(dt);

        string group_column = grouping_column(sel_group_col);


        //  string group_column = "PORTFOLIO";

        var t = from row in dt.AsEnumerable()
                group row by row[group_column] into temp
                select new
                {
                    groupkey = temp.Key,

                    values = temp

                };

        DataTable temp_dt = new DataTable("MyDataTable");

        temp_dt.Columns.Add(group_column);

        temp_dt.Columns.Add("RAG");

        temp_dt.Columns.Add("RedProjectCount");

        //  temp_dt.Columns.Add("RedProjectTotal Projects");

        foreach (string SLA_Metric in SLAMetrics_For_Output())
        {

            temp_dt.Columns.Add(SLA_Metric);
        }

        #region beforechange
        //foreach (string SLA_Metric in SLAMetrics())
        //{

        //    temp_dt.Columns.Add(SLA_Metric);
        //}

        #endregion

        foreach (var key in t)
        {

            string groupkey = key.groupkey.ToString();

            DataTable tmp = key.values.CopyToDataTable();

            int t1 = tmp.Rows.Count;

            int red_project_count = Get_RED_Projects_Count(tmp);

            string red_vs_actual = red_project_count + " / " + t1;

            int[] numerators = new int[7];

            int[] denominators = new int[7];

            foreach (DataRow row in tmp.Rows)
            {
                int temp = Int_Conversation(row["req_quality_score"].ToString());

                if (temp > 0)
                {
                    numerators[0] = numerators[0] + Int_Conversation(row["REQDEFECTCOUNT"].ToString());
                    denominators[0] = denominators[0] + Int_Conversation(row["REQUIREMENTCOUNT"].ToString());
                }

                temp = Int_Conversation(row["req_stab_score"].ToString());

                if (temp > 0)
                {
                    numerators[1] = numerators[1] + Int_Conversation(row["CRCOUNT"].ToString());
                    denominators[1] = denominators[1] + Int_Conversation(row["REQUIREMENTCOUNT"].ToString());

                }

                temp = Int_Conversation(row["des_rev_cov_score"].ToString());

                if (temp > 0)
                {
                    numerators[2] = numerators[2] + Int_Conversation(row["DESIGNREVIEWED"].ToString());
                    denominators[2] = denominators[2] + Int_Conversation(row["DESIGNARTIFACTS"].ToString());

                }

                temp = Int_Conversation(row["code_rev_cov_score"].ToString());

                if (temp > 0)
                {
                    numerators[3] = numerators[3] + Int_Conversation(row["CODEREVIEWED"].ToString());
                    denominators[3] = denominators[3] + Int_Conversation(row["CODECOMPONENTS"].ToString());

                }

                temp = Int_Conversation(row["unit_test_cov_score"].ToString());

                if (temp > 0)
                {
                    numerators[4] = numerators[4] + Int_Conversation(row["UNITTESTED"].ToString());
                    denominators[4] = denominators[4] + Int_Conversation(row["CODECOMPONENTS"].ToString());

                }

                temp = Int_Conversation(row["on_time_del_score"].ToString());

                if (temp > 0)
                {
                    numerators[5] = numerators[5] + Int_Conversation(row["DELIVERABLESONTIME"].ToString());
                    denominators[5] = denominators[5] + Int_Conversation(row["DELIVERABLES"].ToString());

                }

                temp = Int_Conversation(row["delivery_defect_density_score"].ToString());

                if (temp > 0)
                {
                    numerators[6] = numerators[6] + Int_Conversation(row["UTPASSED"].ToString());
                    denominators[6] = denominators[6] + Int_Conversation(row["UTCASES"].ToString());

                }
            }

            List<int> metric_list = new List<int>();

            SLACalc obj1 = new SLACalc();

            metric_list.Add(obj1.CalculateReqQuality(numerators[0], denominators[0], 10, true).Item2);

            metric_list.Add(obj1.CalculateReqStability(numerators[1], denominators[1], 10, true).Item2);

            metric_list.Add(obj1.CalculateDesignReviewCoverage(numerators[2], denominators[2], 10, true).Item2);

            metric_list.Add(obj1.CalculateCodeReviewCoverage(numerators[3], denominators[3], 10, true).Item2);

            metric_list.Add(obj1.CalculateUnitTestCoverage(numerators[4], denominators[4], 10, true).Item2);

            metric_list.Add(obj1.CalculateOnTimeDelivery(numerators[5], denominators[5], 10, true).Item2);

            metric_list.Add(obj1.CalculateDelDefectDensity(numerators[6], denominators[6], 10, true).Item2);


            #region beforechange

            //List<int> list1 = new List<int>();

            //foreach (string SLA_Metric_Input in SLA_Metric_Inputs())
            //{

            //    if (tmp.Compute("SUM(" + SLA_Metric_Input + ")", SLA_Metric_Input + " >= 0") == DBNull.Value)
            //    {

            //           list1.Add(0);            

            //    }
            //    else
            //    {

            //        int sum_value = int.Parse(tmp.Compute("SUM(" + SLA_Metric_Input + ")", SLA_Metric_Input + " >= 0").ToString());

            //        list1.Add(sum_value);
            //    }



            //}




            //List<string> list2 = new List<string>();

            //foreach (string SLA_Metric_Input in SLA_IsApplicable_Inputs())
            //{

            //    list2.Add("Yes");


            //}

            // SLACalc obj = new SLACalc(list1, list2);

            //  int RAG_Result = RAG_Status(m);


            #endregion



            int RAG_Result = RAG_Status(metric_list);



            string RAG = null;

            if (RAG_Result == -1)
            {

                RAG = "RED";
            }
            else
            {
                RAG = "GREEN";
            }



            //temp_dt.Rows.Add(groupkey, RAG, red_project_count, metric_list[0], metric_list[1], metric_list[2], metric_list[3], metric_list[4], metric_list[5], metric_list[6]);
        
            temp_dt.Rows.Add(groupkey.Replace("'","*"), RAG, red_vs_actual, metric_list[0], metric_list[1], metric_list[2], metric_list[3], metric_list[4], metric_list[5], metric_list[6]);



        }

        return temp_dt;





    }

    public static int RAG_Status(List<int> value_list)
    {

        foreach (int value in value_list)
        {
            if (value >= 0 && value < 3)
            {
                return -1;
            }
        }
        return 0;

    }

    public static int Int_Conversation(string content)
    {

        int value = -1;

        try
        {


            value = int.Parse(content);


        }
        catch (Exception)
        {

            value = 0;
        }

        return value;

    }

    public static int Get_RED_Projects_Count(DataTable dt)
    {
        DataView dv = new DataView(dt);

        dv.RowFilter = "RAG = 'RED'";

        return dv.Count;



    }

    public string grouping_column(string group_column)
    {

        switch (group_column)
        {
            case "PORTFOLIO":

                group_column = "PORTFOLIO";

                break;

            case "PBM":

                group_column = "PBM";

                break;

            case "Release Name":

                group_column = "RELEASENAME";

                break;

            case "Application Name":

                group_column = "Application";

                break;

            case "Release Month":

                group_column = "ReleaseMonth";

                break;
        }

        return group_column;
    }

    #endregion

    #region Loading Data


    #region Loading DB Data To DataTable

    public static DataTable Get_UDS_DATA()
    {
        DAL dal = new DAL();
        DataTable dt = new DataTable("UDS_DATA");
        dt = dal.SelectDetails("select distinct * from UDS ");
        dal = null;
        return dt;

    }


    #endregion



    #region Retriving Session DataTables


    #region Filtered_DataTable

    // Retrieving Filtered DataTable

    public DataTable Get_Filtered_DataTable()
    {
        return (DataTable)Session["Filtered_DataTable"];
    }

    #endregion


    #region Report DataTable
    // Retrieving Initial Data Table 

    public DataTable Get_Initial_DataTable()
    {
        return (DataTable)Session["Report_DT"];
    }

    #endregion

    #endregion

    #endregion



    // Code By Karthikeyan

    // Search Button Event - Filtering Records Based On The Condition

    protected void Search_Button_Click(object sender, EventArgs e)
    {


        DataTable dt = (DataTable)Session["Report_DT"];

        dt = filter_records(dt);

        Session["Filtered_DataTable"] = dt;

        DataTable temp = Get_RED_Projects(dt);

        if (temp.Rows.Count == 0)
        {
            Red_Project_List.Visible = false;
        }
        else if (temp.Rows.Count > 0)
        {
            Red_Project_List.Visible = true;
        }

        dt = grouping_data(dt, ddl_grouping_filter.SelectedItem.ToString());

        gvCustomers.DataSource = dt;

        Session["Temp_DS"] = dt;

        string group_column_op = ddl_grouping_filter.SelectedItem.ToString();

        string group_column = grouping_column(ddl_grouping_filter.SelectedItem.ToString());

        ((BoundField)gvCustomers.Columns[1]).DataField = group_column;

        ((BoundField)gvCustomers.Columns[1]).HeaderText = group_column_op;

        //((BoundField)gvCustomers.Columns[2]).DataField = "RAG";

        //((BoundField)gvCustomers.Columns[2]).HeaderText = "RAG";

        ((BoundField)gvCustomers.Columns[3]).DataField = "RedProjectCount";

        //((BoundField)gvCustomers.Columns[3]).HeaderText = "Red Projects Count";

        ((BoundField)gvCustomers.Columns[3]).HeaderText = "Red Projects / Total Projects";

        ((BoundField)gvCustomers.Columns[4]).DataField = "req_quality_score";
        ((BoundField)gvCustomers.Columns[4]).HeaderText = "Requirement Quality Score";

        ((BoundField)gvCustomers.Columns[5]).DataField = "req_stab_score";
        ((BoundField)gvCustomers.Columns[5]).HeaderText = "Requirement Stability Score";

        ((BoundField)gvCustomers.Columns[6]).DataField = "des_rev_cov_score";
        ((BoundField)gvCustomers.Columns[6]).HeaderText = "Design Review Coverage Score";

        ((BoundField)gvCustomers.Columns[7]).DataField = "code_rev_cov_score";
        ((BoundField)gvCustomers.Columns[7]).HeaderText = "Code Review Coverage Score";

        ((BoundField)gvCustomers.Columns[8]).DataField = "unit_test_cov_score";
        ((BoundField)gvCustomers.Columns[8]).HeaderText = "Unit Test Coverage Score";

        ((BoundField)gvCustomers.Columns[9]).DataField = "on_time_del_score";
        ((BoundField)gvCustomers.Columns[9]).HeaderText = "On Time Delivery Score";

        ((BoundField)gvCustomers.Columns[10]).DataField = "delivery_defect_density_score";
        ((BoundField)gvCustomers.Columns[10]).HeaderText = "Delivery Defect Density Score";


        //old gvCustomers.DataSource = dt;

        gvCustomers.PageIndex = 0;
        try
        {

            gvCustomers.DataBind();
        }
        catch (Exception exp)
        {
            throw;
        }
        finally
        {

        }


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





        if (gvCustomers.Rows.Count > 0)
        {
            //This replaces <td> with <th>    
            gvCustomers.UseAccessibleHeader = true;
            //This will add the <thead> and <tbody> elements    
            gvCustomers.HeaderRow.TableSection = TableRowSection.TableHeader;
            //This adds the <tfoot> element. Remove if you don't have a footer row    
            gvCustomers.FooterRow.TableSection = TableRowSection.TableFooter;
        }



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
    // Filter Logic Code 
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


        if (RAG_status.SelectedItem.ToString() != null)
        {


            if (RAG_status.SelectedItem.ToString() != "All")
            {

                if (query != null)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }

                query += "RAG = '" + RAG_status.SelectedItem.ToString() + "'";
            }


        }

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

    // Records Grouping Logic Code

    //public DataTable grouping_data(DataTable dt)
    //{
    //    DataView dv = new DataView(dt);

    //    string group_column = ddl_grouping_filter.SelectedItem.ToString();

    //    var t = from row in dt.AsEnumerable()
    //            group row by row[group_column] into temp
    //            select new
    //            {
    //                groupkey = temp.Key,
    //                values = temp


    //            };

    //    DataTable mytable = new DataTable("MyTable");

    //    mytable.Columns.Add(group_column);

    //    List<string> lst = new List<string>() { "RAG", "Red Projects Count", "Requirement Quality Score", "Requirement Stability Score", "Design Review Coverage Score", "Code Review Coverage Score", "Unit Test Coverage Score", "On Time Delivery Score", "Delivery Defect Density Score" };

    //    foreach (string lstitem in lst)
    //    {

    //        mytable.Columns.Add(lstitem);
    //    }

    //    foreach (var key in t)
    //    {
    //        string Group_RAG = "";

    //        var cols = from value in key.values
    //                   where value["RAG"] == "RED"
    //                   select value["RAG"];
    //        if (cols.Count() == 0)
    //        {
    //            Group_RAG = "GREEN";
    //        }
    //        else
    //        {
    //            Group_RAG = "RED";
    //        }

    //        string groupkey = "";

    //        if (group_column == "Release Month")
    //        {

    //            groupkey = string.Format("{0:y}", key.groupkey);


    //        }

    //        else
    //        {
    //            groupkey = key.groupkey.ToString();

    //        }

    //        //var t1=key.values;

    //        int[] m = new int[7] { 0, 0, 0, 0, 0, 0, 0 };


    //        foreach (var row in key.values)
    //        {

    //            int[] indexes = new int[] { 15, 17, 19, 21, 23, 25 };

    //            int i = 0;
    //            foreach (int index in indexes)
    //            {

    //                var index_content = row[index];

    //                if (index_content == DBNull.Value)
    //                {
    //                    row[index] = 0;
    //                }

    //                m[i] += Convert.ToInt32(row[index].ToString() + "");

    //                i++;

    //            }


    //        }

    //        int count = key.values.Count();

    //        for (int i = 0; i < m.Length; i++)
    //        {

    //            m[i] = m[i] / count;
    //        }


    //        mytable.Rows.Add(groupkey, Group_RAG, cols.Count(), m[0], m[1], m[2], m[3], m[4], m[5], m[6]);

    //    }
    //    return mytable;
    //}




    #region Reset Code

    // Reset Button 

    protected void Reset_Button_Click(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)Session["Report_DT"];

        loading_filtercolumn_values(dt);

        Red_Project_List.Visible = false;

        dt = dt.Clone();

        gvCustomers.DataSource = dt;

        gvCustomers.DataBind();
    }

    #endregion

    #region DropDownList Code

    // Code By Karthikeyan

    #region Filling DropDownList Values


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

    #endregion


    protected void ddl_BU_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["All_DT"];

        DataView dv = new DataView(dt);

        dv.RowFilter = "BU = '" + ddl_BU.SelectedItem.ToString() + "'";

        dt = dv.ToTable();

        Session["Report_DT"] = dt;

        loading_filtercolumn_values(dt);

    }

    #region ReleaseMonth DropDownList

    // Loading ReleaseMonth DropDownList Values

    public void loading_releasemonth_dropdown(DataTable dt)
    {
        ddl_ReleaseMonth.DataSource = Distinct_Column_Values(dt, "ReleaseMonth", true);

        //ddl_ReleaseMonth.Items.Insert(0, new ListItem("Select", "NA"));

        ddl_ReleaseMonth.DataTextField = "ReleaseMonth";

        ddl_ReleaseMonth.DataValueField = "ReleaseMonth";

        //ddl_ReleaseMonth.DataTextFormatString = "{0:y}";        

        ddl_ReleaseMonth.DataBind();

    }

    // ReleaseMonth IndexChanged Event 

    protected void ddl_ReleaseMonth_SelectedIndexChanged1(object sender, EventArgs e)
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

    #endregion

    #region PBM DropDownList

    // Loading PBM DropDownList Values

    public void loading_pbm_dropdown(DataTable dt)
    {

        ddl_PBM.DataSource = Distinct_Column_Values(dt, "PBM",true);

        ddl_PBM.DataTextField = "PBM";

        ddl_PBM.DataValueField = "PBM";

        ddl_PBM.DataBind();

    }

    public void loading_pbm1_dropdown(DataTable dt)
    {

        ddl_PBM1.DataSource = Distinct_Column_Values(dt, "PBM1", true);

        ddl_PBM1.DataTextField = "PBM1";

        ddl_PBM1.DataValueField = "PBM1";

        ddl_PBM1.DataBind();

    }

    public void loading_pbm2_dropdown(DataTable dt)
    {

        ddl_PBM2.DataSource = Distinct_Column_Values(dt, "PBM2", true);

        ddl_PBM2.DataTextField = "PBM2";

        ddl_PBM2.DataValueField = "PBM2";

        ddl_PBM2.DataBind();

    }


    // Index Changed Event 

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




    #endregion

    #region Portfolio DropDownList

    // Loading Portfolio DropDownList Values

    public void loading_portfolio_dropdown(DataTable dt)
    {
        ddl_Portfolio.DataSource = Distinct_Column_Values(dt, "PORTFOLIO", true);

        ddl_Portfolio.DataTextField = "PORTFOLIO";

        ddl_Portfolio.DataValueField = "PORTFOLIO";

        ddl_Portfolio.DataBind();
    }

    // Portfolio IndexChanged Event

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

    #endregion

    #region Application DropDownList

    // Loading Application DropDownList Values

    public void loading_application_dropdown(DataTable dt)
    {


        ddl_Application.DataSource = Distinct_Column_Values(dt, "Application", true);

        ddl_Application.DataTextField = "Application";

        ddl_Application.DataValueField = "Application";

        ddl_Application.DataBind();

    }

    // Application IndexChanged Event 

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

    #endregion

    #region Releases DropDownList

    public void loading_releases_dropdown(DataTable dt)
    {
        ddl_Releases.DataSource = Distinct_Column_Values(dt, "RELEASENAME", true);

        ddl_Releases.DataTextField = "RELEASENAME";

        ddl_Releases.DataValueField = "RELEASENAME";

        ddl_Releases.DataBind();
    }

    #endregion

    #region Retrieving Distinct Column Values

    public DataTable Distinct_Column_Values(DataTable dt, string field_name, Boolean b_should_add_ALL)
    {


        DataView dv = new DataView(dt);

        DataTable d_dt = dv.ToTable(field_name, true, field_name);

        if(b_should_add_ALL)
        {
            DataRow drow = d_dt.NewRow();

            drow[0] = "All";

            d_dt.Rows.InsertAt(drow, 0);
        }
        


        return d_dt;
    }

    #endregion


    #endregion

    #region View RedProjects List


    #region Retrieving Red Projects List

    //Retriving RED Projects

    public DataTable Get_RED_Projects(DataTable dt)
    {

        DataView dv = new DataView(dt);

        dv.RowFilter = "RAG = 'RED'";

        return dv.ToTable();
    }


    #endregion

    #region Binding RedProjects List On GridView And Showing PopUp Window
    // Binding RedProjects List On GridView And Showing PopUp Window

    protected void Bind_RedProjectList_On_GridView(object sender, EventArgs e)
    {


        DataTable dt = Get_Filtered_DataTable();

        if (dt.Rows.Count == 0)
        {
            Modal_GridView.DataSource = null;
        }

        else
        {

            dt = Get_RED_Projects(dt);

            Modal_GridView.DataSource = dt;
        }

        Modal_GridView.DataBind();

        if (Modal_GridView.Rows.Count > 0)
        {
            //This replaces <td> with <th>    
            Modal_GridView.UseAccessibleHeader = true;
            //This will add the <thead> and <tbody> elements    
            Modal_GridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            //This adds the <tfoot> element. Remove if you don't have a footer row    
            Modal_GridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }

        ModalPopupExtender1.Show();
    }

    #endregion

    #region Check or UnCheck All CheckBoxes On GridView

    // Select All CheckBoxes

    //protected void Check_All_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox ChkBoxHeader = (CheckBox)Modal_GridView.HeaderRow.FindControl("Check_All");

    //    for (int i = 0; i < Modal_GridView.Rows.Count; i++)
    //    {
    //        (Modal_GridView.Rows[i].Cells[0].FindControl("chk1") as CheckBox).Checked = ChkBoxHeader.Checked;
    //    }


    //}

    #endregion

    #region Resetting Check Options && Hiding PopUp Window

    // Hide PopUp Window && UnChecking All CheckBoxes

    #region Hiding PopUp Window

    protected void btn_close_Click(object sender, EventArgs e)
    {
        //  UnCheckAll();

        ModalPopupExtender1.Hide();
    }

    #endregion

    #region Unchecking All CheckBoxes
    // Unchecking All CheckBoxes 

    //public void UnCheckAll()
    //{
    //    CheckBox ChkBoxHeader = (CheckBox)Modal_GridView.HeaderRow.FindControl("Check_All");
    //    for (int i = 0; i < Modal_GridView.Rows.Count; i++)
    //    {
    //        (Modal_GridView.Rows[i].Cells[0].FindControl("chk1") as CheckBox).Checked = false;
    //    }

    //    ChkBoxHeader.Checked = false;
    //}

    #endregion

    #endregion

    #region Mail Send Option Logic

    #region Extracting the checked records from gridview

    // Extracting the checked records from gridview

    public DataTable Extracting_Checked_Records()
    {

        DataTable dt = Get_Filtered_DataTable();

        dt = Get_RED_Projects(dt);

        DataTable temp = new DataTable("Email_Table");

        temp = dt.Clone();

        //Modal_GridView.DataSource = dt;


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if ((Modal_GridView.Rows[i].Cells[0].FindControl("chk1") as CheckBox).Checked == true)
            {
                temp.Rows.Add(dt.Rows[i].ItemArray);
            }
        }

        return temp;

    }

    #endregion

    #region Mail Send Logic

    // Mail Send Logic 

    protected void Email_Send(object sender, EventArgs e)
    {

        DataTable email_records = Extracting_Checked_Records();

        DataTable result = grouping_data(email_records, "PORTFOLIO");

        var t = from row in email_records.AsEnumerable()
                group row by row["PORTFOLIO"] into temp
                select new
                {
                    groupkey = temp.Key,

                    values = temp

                };

        List<string> lst1 = new List<string>(){"Application",
"RELEASESTARTDATE",
"RELEASENAME",
"RAG",
"TECHNOLOGY"

        };

        List<string> lst2 = new List<string>(){"PORTFOLIO",

"RAG"
        };

        List<string> lst3 = new List<string>(){

            "req_quality_score",
"req_stab_score",
"des_rev_cov_score",
"code_rev_cov_score",
"unit_test_cov_score",
"on_time_del_score",
"delivery_defect_density_score"

        };


        int i = 0;
        foreach (var key in t)
        {
            string portfolio_name = key.groupkey.ToString();

            DataTable tmp = key.values.CopyToDataTable();

            string parent_table = "";

            string child_table = "";

            parent_table += "<table style='border:1px solid black' border=" + 1 + ">" +
    "<tr ><th style='border:1px solid black'> Portfolio </th><th style='border:1px solid black'> RAG </th> <th style='border:1px solid black'> Requirement Quality Score </th><th style='border:1px solid black'> Requirement Stability Score </th><th style='border:1px solid black'> Design Review Coverage Score </th><th style='border:1px solid black'> Code Review Coverage Score </th><th style='border:1px solid black'> Unit Test Coverage Score </th>" +
    "<th style='border:1px solid black'> On Time Delivery Score </th><th style='border:1px solid black'> Delivery Defect Density Score </th></tr>";

            parent_table += "<tr style=" + "text-align:center;" + ">";

            DataRow result_row = result.Rows[i];

            foreach (string field in lst2)
            {
                if (field == "RAG")
                {
                    if (result_row[field].ToString() == "RED")
                    {
                        parent_table += "<td style='border:1px solid black' bgcolor='red'>" + result_row[field] + "</td>";

                        continue;
                    }

                }

                parent_table += "<td style='border:1px solid black'>" + result_row[field] + "</td>";

            }

            foreach (string field in lst3)
            {
                int val = int.Parse(result_row[field].ToString());

                if (val < 0)
                {
                    if (val == -4)
                    {
                        parent_table += "<td style='border:1px solid black' ><font color='orange'>NA</font></td>";


                    }

                    else
                    {

                        parent_table += "<td style='border:1px solid black' ><font color='gray'>NA</font></td>";

                    }



                }

                else if (val == 1)
                {
                    parent_table += "<td style='border:1px solid black' ><font color='red'>" + result_row[field] + "</font></td>";


                }
                else
                {

                    parent_table += "<td style='border:1px solid black'>" + result_row[field] + "</td>";
                }

            }


            parent_table += "</tr></table>";


            //oMailItem.HTMLBody += "<br /> <br /> <b><u>List of Application in RAG Status RED</u><b>" + "<br /> <br />";

            child_table += "<table border='1' style='border:1px solid black'><tr><th style='border:1px solid black'>Application Name</th><th style='border:1px solid black'> Release Date </th><th style='border:1px solid black'>Project / Release Name </th><th style='border:1px solid black'> RAG Status </th><th style='border:1px solid black'>Technology </th><th style='border:1px solid black'> Requirement Quality Score </th><th style='border:1px solid black'> Requirement Stability Score </th><th style='border:1px solid black'> Design Review Coverage Score </th>" +
    "<th style='border:1px solid black'> Code Review Coverage Score</th><th style='border:1px solid black'> Unit Test Coverage Score</th><th style='border:1px solid black'>On Time Delivery Score</th><th style='border:1px solid black'>Delivery Defect Density Score</th> </tr>";


            foreach (DataRow row in tmp.Rows)
            {
                child_table += "<tr style='text-align:center;'>";
                foreach (string field in lst1)
                {
                    if (field == "RAG")
                    {
                        if (row[field].ToString() == "RED")
                        {
                            child_table += "<td bgcolor='red' style='border:1px solid black'> " + row[field] + "</td>";
                        }
                        else
                        {
                            child_table += "<td style='border:1px solid black'>" + row[field] + "</td>";
                        }
                    }
                    else
                    {
                        child_table += "<td style='border:1px solid black'>" + row[field] + "</td>";

                    }
                }

                foreach (string field in lst3)
                {
                    int val = int.Parse(row[field].ToString());

                    if (val < 0)
                    {
                        if (val == -4)
                        {
                            child_table += "<td style='border:1px solid black' ><font color='orange'>NA</font></td>";


                        }

                        else
                        {

                            child_table += "<td style='border:1px solid black' ><font color='gray'>NA</font></td>";

                        }



                    }

                    else if (val == 1 || val == 2)
                    {
                        child_table += "<td style='border:1px solid black' ><font color='red'>" + row[field] + "</font></td>";


                    }
                    else
                    {

                        child_table += "<td style='border:1px solid black'>" + row[field] + "</td>";
                    }

                }

                child_table += "</tr>";
            }

            child_table += "</table>";

            email_outlook_send(portfolio_name, parent_table, child_table);

            i++;



        }

        //      #region email_part

        //      Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application(); ;


        //        Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        //        oMailItem.To = "KarthikeyanNS.SrinivasanNK@cognizant.com";

        //        oMailItem.CC = "KarthikeyanNS.SrinivasanNK@cognizant.com";

        //      oMailItem.Recipients.Add("Arvindkumar.krishnaswami@cognizant.com");



        //      oMailItem.Recipients.Add("SridharDhanabalan.Sethuraman@cognizant.com");

        //        oMailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;

        //        oMailItem.Subject = "MetLife Delivery Dashboard- RAG Status RED for [[Portfolio]] Portfolio for the month of [[January 2016]]";

        ////        oMailItem.HTMLBody = "<div>" + "Please find the MetLife Delivery Dashboard- status of [[Portfolio]] Portfolio for the month of [[January 2017]] for SLA Metrics." + "<br>" +
        ////"Use the link below to override RAG Status." + "<br/>" +
        ////"https://metlifedd.cognizant.com/OverrideRAG.aspx " + "<br/>" +
        ////"***This is auto generated email please do not reply to this email.****" + "</div> ";

        //        oMailItem.HTMLBody = "Hi," + "<br />" + "<br />" + "Please find the MetLife  Delivery Dashboard- status of <<Portfolio>> Portfolio  for the  month of << January 2017>> for SLA Metrics." + "<br /><br />" ;
        //        oMailItem.HTMLBody+="<u><b>Portfolio Level</b></u>"+"<br /><br />";
        //        oMailItem.HTMLBody += "<table border=" + 1 + ">" +
        //"<tr ><th> Portfolio </th><th> RAG </th><th> Requirement Quality Score </th><th> Requirement Stability Score </th><th> Design Review Coverage Score </th><th> Code Review Coverage Score </th><th> Unit Test Coverage Score </th>" +
        //"<th> On Time Delivery Score </th><th> Delivery Defect Density Score </th></tr><tr style=" + "text-align:center;" + "><td> Policy and Record Keeping & Billing </td><td> RED </td><td> 2 </td><td>2</td><td> 2 </td><td> 2 </td><td> 2 </td><td> 2 </td><td> 2 </td></tr></table>";
        //        oMailItem.HTMLBody += "<br /> <br /> <b><u>List of Application in RAG Status RED</u><b>"+"<br /> <br />";

        //        oMailItem.HTMLBody += "<table border=" + 1 + "><tr><th>Application Name</th><th> Release Date </th><th>Project / Release Name </th><th> RAG Status </th><th>Technology </th><th> Requirement Quality Score </th><th> Requirement Stability Score </th><th> Design Review Coverage Score </th>" +
        //"<th> Code Review Coverage Score</th><th> Unit Test Coverage Score</th><th>On Time Delivery Score</th><th>Delivery Defect Density Score</th> </tr><tr style=" + "text-align:center;" + "><td> CBS</td><td> 10/14/2016 8:00:00 PM </td><td> (10/14) CBS October Install - Production 10/14/2016/DTRC enhancement</td>" +
        //"<td>  RED </td><td> Mainframe</td> <td> 2 </td><td>2</td><td> 2 </td><td> 2 </td><td> 2 </td><td> 2 </td><td> 2 </td></tr></table>";
        //        //oMailItem.SaveSentMessageFolder = oOutboxFolder;
        //        oMailItem.HTMLBody += "<br /><br />" + "Use the link below to override RAG Status." + "<br /> <br />" + "https://metlifedd.cognizant.com/OverrideRAG.aspx"+"<br/><br/>";

        //        oMailItem.HTMLBody += "<font color=" + "red" + ">***This is auto generated email please do not reply to this email.****" + "</font> <br /><br />";

        //        oMailItem.HTMLBody += "<font color=" + "" + "> <b>Thanks <b></font> <br/> Delivery Dashboard Admin Team";
        //        oMailItem.Send();

        //        UnCheckAll();

        //        ModalPopupExtender1.Hide();

        //        //string message = "Email Send Successfully.";
        //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        //sb.Append("alert('");
        //        //sb.Append(message);
        //        //sb.Append("');");
        //        //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
        //      #endregion

        //   UnCheckAll();

        ModalPopupExtender1.Hide();

    }

    public static void email_outlook_send(string portfolio_name, string parent_table, string child_table)
    {


        Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application(); ;


        Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        oMailItem.To = "KarthikeyanNS.SrinivasanNK@cognizant.com";

        // oMailItem.CC = "Indra.V.K@cognizant.com";

        oMailItem.CC = "KarthikeyanNS.SrinivasanNK@cognizant.com";

        ////  oMailItem.CC = "KayedJoher.S@cognizant.com";

        //oMailItem.Recipients.Add("Arvindkumar.krishnaswami@cognizant.com");

        oMailItem.Recipients.Add("SridharDhanabalan.Sethuraman@cognizant.com");

        // oMailItem.Recipients.Add("KayedJoher.S@cognizant.com");

        ////oMailItem.Recipients.Add("Indra.V.K@cognizant.com");



        oMailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;

        oMailItem.Subject = "MetLife Delivery Dashboard- RAG Status RED for '" + portfolio_name + "' Portfolio for the month of January 2016";

        //        oMailItem.HTMLBody = "<div>" + "Please find the MetLife Delivery Dashboard- status of [[Portfolio]] Portfolio for the month of [[January 2017]] for SLA Metrics." + "<br>" +
        //"Use the link below to override RAG Status." + "<br/>" +
        //"https://metlifedd.cognizant.com/OverrideRAG.aspx " + "<br/>" +
        //"***This is auto generated email please do not reply to this email.****" + "</div> ";

        oMailItem.HTMLBody = "Hi," + "<br />" + "<br />" + "Please find the MetLife  Delivery Dashboard- status of '" + portfolio_name + "' Portfolio  for the  month of January 2017 for SLA Metrics." + "<br /><br />";
        oMailItem.HTMLBody += "<u><b>Portfolio Level</b></u>" + "<br /><br />";
        //        oMailItem.HTMLBody += "<table border=" + 1 + ">" +
        //"<tr ><th> Portfolio </th><th> RAG </th><th> Requirement Quality Score </th><th> Requirement Stability Score </th><th> Design Review Coverage Score </th><th> Code Review Coverage Score </th><th> Unit Test Coverage Score </th>" +
        //"<th> On Time Delivery Score </th><th> Delivery Defect Density Score </th></tr><tr style=" + "text-align:center;" + "><td> Policy and Record Keeping & Billing </td><td> RED </td><td> 2 </td><td>2</td><td> 2 </td><td> 2 </td><td> 2 </td><td> 2 </td><td> 2 </td></tr></table>";
        //        oMailItem.HTMLBody += "<br /> <br /> <b><u>List of Application in RAG Status RED</u><b>" + "<br /> <br />";
        //        oMailItem.HTMLBody += html_body;

        oMailItem.HTMLBody += parent_table;

        oMailItem.HTMLBody += "<br /> <br /> <b><u>List of Application in RAG Status RED</u><b>" + "<br /> <br />";

        oMailItem.HTMLBody += child_table;

        oMailItem.HTMLBody += "<br /><br />" + "Use the link below to override RAG Status." + "<br /> <br />" + "https://metlifedd.cognizant.com/OverrideRAG.aspx" + "<br/><br/>";

        oMailItem.HTMLBody += "<font color=" + "red" + ">***This is auto generated email please do not reply to this email.****" + "</font> <br /><br />";

        oMailItem.HTMLBody += "<font color=" + "" + "> <b>Thanks <b></font> <br/> Delivery Dashboard Admin Team";
        oMailItem.Send();
    }

    #endregion




    #endregion


    #endregion

    #region GridView Code


    // Code Updated By Karthikeyan

    protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvCustomers.PageIndex = e.NewPageIndex;

        gvCustomers.DataSource = (DataTable)Session["Temp_DS"];

        gvCustomers.DataBind();

    }

    protected void gvOrders_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvCustomers_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }


    public static System.Drawing.Color Get_Color(string value)
    {
        int val = int.Parse(value);

        if (val < 0)
        {

            if (val == -4)
            {
                return System.Drawing.Color.Orange;
            }
            else if (val == -1 || val == -2 || val == -3)
            {
                return System.Drawing.Color.Gray;
            }

        }
        if (val == 1 || val == 2)
        {
            return System.Drawing.Color.Red;
        }
        return System.Drawing.Color.Black;


    }

    public static Boolean Get_Font_Status(string value)
    {
        int val = int.Parse(value);

        if (val < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void OnRowChildDataBound(object sender, GridViewRowEventArgs e)
    {
        #region codeblock

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string RAGStatus = (string)DataBinder.Eval(e.Row.DataItem, "RAG");

            //if (RAGStatus == "RED")
            //{
            //    e.Row.Cells[1].Text = "";

            //    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
            //}
            //else if (RAGStatus == "GREEN")
            //{
            //    e.Row.Cells[1].Text = "";

            //    e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
            //}

            e.Row.Cells[0].Width = Unit.Pixel(115);

            //e.Row.Cells[0].Width = Unit.Pixel(117);

            e.Row.Cells[1].Width = Unit.Pixel(68);

            e.Row.Cells[2].Width = Unit.Pixel(114);

            e.Row.Cells[3].Width = Unit.Pixel(117);

            e.Row.Cells[4].Width = Unit.Pixel(117);


            e.Row.Cells[5].Text = Get_Description((int)DataBinder.Eval(e.Row.DataItem, "CURRENTPHASE"));

            e.Row.Cells[6].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "req_quality_score"));

            e.Row.Cells[6].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "req_quality_score"));

            e.Row.Cells[6].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "req_quality_score"));



            e.Row.Cells[7].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "req_stab_score"));

            e.Row.Cells[7].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "req_stab_score"));

            e.Row.Cells[7].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "req_stab_score"));


            e.Row.Cells[8].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "des_rev_cov_score"));

            e.Row.Cells[8].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "des_rev_cov_score"));

            e.Row.Cells[8].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "des_rev_cov_score"));



            e.Row.Cells[9].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "code_rev_cov_score"));

            e.Row.Cells[9].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "code_rev_cov_score"));

            e.Row.Cells[9].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "code_rev_cov_score"));


            e.Row.Cells[10].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "unit_test_cov_score"));

            e.Row.Cells[10].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "unit_test_cov_score"));

            e.Row.Cells[10].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "unit_test_cov_score"));



            e.Row.Cells[11].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "on_time_del_score"));

            e.Row.Cells[11].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "on_time_del_score"));

            e.Row.Cells[11].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "on_time_del_score"));



            e.Row.Cells[12].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "delivery_defect_density_score"));

            e.Row.Cells[12].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "delivery_defect_density_score"));

            e.Row.Cells[12].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "delivery_defect_density_score"));




            e.Row.Cells[5].Width = Unit.Pixel(60);

            e.Row.Cells[6].Width = Unit.Pixel(96);

            e.Row.Cells[7].Width = Unit.Pixel(96);

            e.Row.Cells[8].Width = Unit.Pixel(80);

            e.Row.Cells[9].Width = Unit.Pixel(76);

            e.Row.Cells[10].Width = Unit.Pixel(75);

            e.Row.Cells[11].Width = Unit.Pixel(67);

            e.Row.Cells[12].Width = Unit.Pixel(67);

        }

        #endregion
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

    List<string> headers = new List<string>();

    // Code By Karthikeyan

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell header1 in e.Row.Cells)
            {
                headers.Add(header1.Text);
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {





            string group_col = e.Row.Cells[1].Text;

            string group_col_name = headers[1];


            string[] cols = e.Row.Cells[1].AssociatedHeaderCellID;


            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;


            //  DataTable dt = Get_UDS_DATA(); 

            //    string Excelpath = ConfigurationManager.AppSettings["excelpath"];

            //    DataTable dt = new DataTable();

            //  //NOW  dt = ExcelToDataTable(Excelpath);

            //    //gvOrders.DataSource = dt;

            DataTable dt1 = (DataTable)Session["Report_DT"];

            DataView dv = new DataView(dt1);


            //    //string query = "PORTFOLIO = '" + group_col.ToString() + "'";

            string query = "";
            if (group_col_name == "Release Name")
            {
                query = "`" + grouping_column(group_col_name) + "`" + " = '" + group_col.ToString() + "'";
            }
            else
            {
                query = "`" + grouping_column(group_col_name) + "`" + " = \'" + group_col.ToString() + "\'";
            }

            //string query = "`" + grouping_column(group_col_name) + "`" + " = \'" + group_col.ToString() + "\'";

            //    // query = query.Replace("&", "&amp;");

            query = HttpUtility.HtmlDecode(query);


            dv.RowFilter = query.ToString();

            DataTable dt3 = dv.ToTable();

            dt3 = filter_records(dt3);


            gvOrders.DataSource = dt3;

            gvOrders.DataBind();


            //current_release_parent_grid.DataSource = dt3;

            //current_release_parent_grid.DataBind();



            // e.Row.Cells[2].Text = "";


            // string RAGStatus = (string)DataBinder.Eval(e.Row.DataItem, "RAG");

            //if (RAGStatus == "RED")
            //{
            //    e.Row.Cells[2].Text = "";

            //    e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
            //}
            //else if (RAGStatus == "GREEN")
            //{
            //    e.Row.Cells[2].Text = "";

            //    e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
            //}


            e.Row.Cells[4].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "req_quality_score"));

            e.Row.Cells[4].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "req_quality_score"));

            e.Row.Cells[4].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "req_quality_score"));


            e.Row.Cells[5].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "req_stab_score"));

            e.Row.Cells[5].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "req_stab_score"));

            e.Row.Cells[5].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "req_stab_score"));


            e.Row.Cells[6].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "des_rev_cov_score"));

            e.Row.Cells[6].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "des_rev_cov_score"));

            e.Row.Cells[6].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "des_rev_cov_score"));



            e.Row.Cells[7].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "code_rev_cov_score"));

            e.Row.Cells[7].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "code_rev_cov_score"));

            e.Row.Cells[7].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "code_rev_cov_score"));



            e.Row.Cells[8].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "unit_test_cov_score"));

            e.Row.Cells[8].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "unit_test_cov_score"));

            e.Row.Cells[8].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "unit_test_cov_score"));



            e.Row.Cells[9].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "on_time_del_score"));

            e.Row.Cells[9].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "on_time_del_score"));

            e.Row.Cells[9].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "on_time_del_score"));



            e.Row.Cells[10].ForeColor = Get_Color((string)DataBinder.Eval(e.Row.DataItem, "delivery_defect_density_score"));

            e.Row.Cells[10].Font.Bold = Get_Font_Status((string)DataBinder.Eval(e.Row.DataItem, "delivery_defect_density_score"));

            e.Row.Cells[10].Text = Get_Content((string)DataBinder.Eval(e.Row.DataItem, "delivery_defect_density_score"));



        }

    }

    public static string Get_Content(string value)
    {
        int val = int.Parse(value);

        if (val < 0)
        {
            return "NA";
        }

        return value;
    }

    public static List<string> SLAMetrics_For_Output()
    {

        List<string> SLA_Metrics_List = new List<string>() { "req_quality_score",

         "req_stab_score",

         "des_rev_cov_score",

         "code_rev_cov_score",

         "unit_test_cov_score",

         "on_time_del_score",

         "delivery_defect_density_score"

        };

        return SLA_Metrics_List;
    }


    #endregion



    private static DataTable GetData(string query)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }







    protected void Modal_GridView_PreRender(object sender, EventArgs e)
    {
        if (Modal_GridView.Rows.Count > 0)
        {
            //This replaces <td> with <th>    
            Modal_GridView.UseAccessibleHeader = true;
            //This will add the <thead> and <tbody> elements    
            Modal_GridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            //This adds the <tfoot> element. Remove if you don't have a footer row    
            Modal_GridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
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

            e.Row.Cells[5].Text = txt;
        }
    }
    protected void prior_release_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string txt = Get_Description((int)DataBinder.Eval(e.Row.DataItem, "CURRENTPHASE"));

            e.Row.Cells[4].Text = txt;
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

        string update_stmt_rel_info = " Update T_Release_info set " + "QA_Start_dt = " + (PopUp_CF_QAStartDate.Text.ToString().Replace(",","") != "" ? "'" + Convert.ToDateTime(PopUp_CF_QAStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" +    ",QA_End_dt = " + (PopUp_CF_QAEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_CF_QAEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" +    ",UAT_Start_dt = " + (PopUp_CF_UATStartDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_CF_UATStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" +    ",UAT_End_dt = " + (PopUp_CF_UATEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_CF_UATEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" +    ",QA_Progress_PERCENT = " + (PopUp_CF_QAProgress.Text.Replace(",", "") == "" ? "0": PopUp_CF_QAProgress.Text.Replace(",", "")) +    ",UAT_Progress_PERCENT = " + (PopUp_CF_UATProgress.Text.Replace(",", "")  == ""? "0" : PopUp_CF_UATProgress.Text.Replace(",", "")) +    ",Comments = '" + PopUp_CF_ManagerComments.Text.ToString().Replace(",,","") + "' where UDS_Key = '" + Session["REL_INFO_EDIT_KEY"].ToString() + "'";

        dal.UpdateDB(update_stmt_rel_info);
        reset_CurrentRelease_PopUp();
        refresh_Master_source();
        current_release_grid.DataSource = Get_Current_Data();

        current_release_grid.DataBind();
    }
    public void refresh_Master_source ()
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

   
   
    protected void LinkButton5_Click(object sender, EventArgs e)
    {

    }
   

   

    protected void mor_grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            //       HiddenField hdnDataId = (HiddenField)row.FindControl("hdnDataId");
        }


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

            e.Row.Cells[5].Text = txt;
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

        string update_stmt_rel_info = " Update T_Release_info set " + "QA_Start_dt = " + (PopUp_F_QAStartDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_QAStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",QA_End_dt = " + (PopUp_F_QAEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_QAEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",UAT_Start_dt = " + (PopUp_F_UATStartDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_UATStartDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",UAT_End_dt = " + (PopUp_F_UATEndDate.Text.ToString().Replace(",", "") != "" ? "'" + Convert.ToDateTime(PopUp_F_UATEndDate.Text.Replace(",", "")).ToString("yyyy-MM-dd") + "'" : "NULL") + "" + ",QA_Progress_PERCENT = " + (PopUp_F_QAProgress.Text.Replace(",", "") == "" ? "0" : PopUp_F_QAProgress.Text.Replace(",", "")) + ",UAT_Progress_PERCENT = " + (PopUp_F_UATProgress.Text.Replace(",", "") == "" ? "0" : PopUp_F_UATProgress.Text.Replace(",", "")) + ",Comments = '" + PopUp_F_ManagerComments.Text.ToString().Replace(",,", "") + "' where UDS_Key = '" + Session["REL_INFO_EDIT_KEY"].ToString() + "'";

        dal.UpdateDB(update_stmt_rel_info);
        reset_FutureRelease_PopUp();
        refresh_Master_source();
        future_release_grid.DataSource = Get_Future_Data();

        future_release_grid.DataBind();

    }

   
}

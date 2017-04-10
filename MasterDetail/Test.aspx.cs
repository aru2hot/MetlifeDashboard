using MasterDetail;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
    DAL dal = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        mor_grid.DataSource = dal.SelectDetails("SELECT TOP 1000 [MOR_ID]      ,[MODIFIED_TIMESTAMP]      ,[CREATED_TIMESTAMP]      ,[PORTFOLIO]      ,[PROJECT_NAME]      ,[CATEGORY]          ,[LAST_WEEK_COLOR]      ,[CURRENT_WEEK_COLOR]      ,[NEXT_WEEK_COLOR]      ,[OVERALL_STATUS_COLOR]          ,[LAST_MODIFIED_TIMESTAMP]           ,[WEEK_ENDING]       FROM [MetLifeDD].[dbo].[T_MOR_DETAILS]");
        mor_grid.DataBind();
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }


    protected void btn_Click(object sender, EventArgs e)
    {
        txt.Text = ddl.SelectedValue;
    }

    protected void btn_Excel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;


        Response.AddHeader("content-disposition", "attachment;filename=MOR_REPORT.xls");
        // Response.Charset = "";
        Response.ContentType = "application/ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            mor_grid.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in mor_grid.HeaderRow.Cells)
            {
                cell.BackColor = mor_grid.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in mor_grid.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = mor_grid.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = mor_grid.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            //mor_grid.HeaderRow.Style.Add("background-color", "#FFFFFF");
            ////Applying stlye to gridview header cells
            //for (int i = 0; i < mor_grid.HeaderRow.Cells.Count; i++)
            //{
            //    mor_grid.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}

            mor_grid.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }
    }
}
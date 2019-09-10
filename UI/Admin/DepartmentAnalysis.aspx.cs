using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Office.Interop.Owc11;
using BLL;

public partial class Admin_DepartmentAnalysis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"].ToString() != "系统管理员")//防止从地址栏直接输入网址
        {
            Response.Redirect("~\\登录.aspx");
        }

        if (!IsPostBack)
        {  
            gvKJ.DataSource = InitialDataTable("会计系");
            gvKJ.DataBind();

            gvXX.DataSource = InitialDataTable("信息工程系");
            gvXX.DataBind();

            gvJG.DataSource = InitialDataTable("经济管理系");
            gvJG.DataBind();

            gvSP.DataSource = InitialDataTable("食品工程系");
            gvSP.DataBind();

            gvJX.DataSource = InitialDataTable("机械工程系");
            gvJX.DataBind();

            gvWY.DataSource = InitialDataTable("商务外语系");
            gvWY.DataBind();

            gvJZ.DataSource = InitialDataTable("建筑工程系");
            gvJZ.DataBind();

            Clear();
        }
    }


    protected void btnKJClick(object sender, EventArgs e)//会计系图表分析按钮
    {
        lblKJ.Text = "";
        DataTable dt = InitialDataTable("会计系");
        if (dt.Rows.Count > 0)
        {
            //
            string strImageTag = DrawChart(dt);
            if (strImageTag != "")
            {
                this.phKJ.Controls.Add(new LiteralControl(strImageTag));
            }
            else
            {
                lblKJ.Text = "无法生成图表！";
            }
        }
        else
        {
            lblKJ.Text = "会计系目前没有缺勤学生信息，无法生成走势图！";
        }
    }
    protected void btnXXClick(object sender, EventArgs e)//信息系图表分析按钮
    {
        lblXX.Text = "";
        DataTable dt = InitialDataTable("信息工程系");
        if (dt.Rows.Count > 0)
        {
            //
            string strImageTag = DrawChart(dt);
            if (strImageTag != "")
            {
                this.phXX.Controls.Add(new LiteralControl(strImageTag));
            }
            else
            {
                lblXX.Text = "无法生成图表！";
            }
        }
        else
        {
            lblXX.Text = "信息工程系目前没有缺勤学生信息，无法生成走势图！";
        }
    }
    protected void btnJGClick(object sender, EventArgs e)
    {
        lblJG.Text = "";
        DataTable dt = InitialDataTable("经济管理系");
        if (dt.Rows.Count > 0)
        {
            //
            string strImageTag = DrawChart(dt);
            if (strImageTag != "")
            {
                this.phJG.Controls.Add(new LiteralControl(strImageTag));
            }
            else
            {
                lblJG.Text = "无法生成图表！";
            }
        }
        else
        {
            lblJG.Text = "经济管理系目前没有缺勤学生信息，无法生成走势图！";
        }
    }
    protected void btnSPClick(object sender, EventArgs e)
    {
        lblSP.Text = "";
        DataTable dt = InitialDataTable("食品工程系");
        if (dt.Rows.Count > 0)
        {
            //
            string strImageTag = DrawChart(dt);
            if (strImageTag != "")
            {
                this.phSP.Controls.Add(new LiteralControl(strImageTag));
            }
            else
            {
                lblSP.Text = "无法生成图表！";
            }
        }
        else
        {
            lblSP.Text = "食品工程系目前没有缺勤学生信息，无法生成走势图！";
        }
    }
    protected void btnJXClick(object sender, EventArgs e)
    {
        lblJX.Text = "";
        DataTable dt = InitialDataTable("机械工程系");
        if (dt.Rows.Count > 0)
        {
            //
            string strImageTag = DrawChart(dt);
            if (strImageTag != "")
            {
                this.phJX.Controls.Add(new LiteralControl(strImageTag));
            }
            else
            {
                lblJX.Text = "无法生成图表！";
            }
        }
        else
        {
            lblJX.Text = "机械工程系目前没有缺勤学生信息，无法生成走势图！";
        }
    }
    protected void btnWYClick(object sender, EventArgs e)
    {
        lblWY.Text = "";
        DataTable dt = InitialDataTable("商务外语系");
        if (dt.Rows.Count > 0)
        {
            //
            string strImageTag = DrawChart(dt);
            if (strImageTag != "")
            {
                this.phWY.Controls.Add(new LiteralControl(strImageTag));
            }
            else
            {
                lblWY.Text = "无法生成图表！";
            }
        }
        else
        {
            lblWY.Text = "商务外语系目前没有缺勤学生信息，无法生成走势图！";
        }
    }
    protected void btnJZClick(object sender, EventArgs e)
    {
        lblJZ.Text = "";
        DataTable dt = InitialDataTable("建筑工程系");
        if (dt.Rows.Count > 0)
        {
            //
            string strImageTag = DrawChart(dt);
            if (strImageTag != "")
            {
                this.phJZ.Controls.Add(new LiteralControl(strImageTag));
            }
            else
            {
                lblJZ.Text = "无法生成图表！";
            }
        }
        else
        {
            lblJZ.Text = "建筑工程系目前没有缺勤学生信息，无法生成走势图！";
        }
    }



    private DataTable InitialDataTable(string Department)
    {
        DataTable dt = DataAnalysis.CreateDataTable();
        string cWeek = Session["CurrentWeek"].ToString();
        if (cWeek == "0")//没有校历的情况
        {
            return dt;
        }
        else
        {
            for (int i = Convert.ToInt32(cWeek); i > 0; i--)
            {
                string str1 = i.ToString();
                if (str1.Length == 1)
                {
                    str1 = "0" + str1;  //周次
                }
                int Late = DataAnalysis.GetEveryAttendanceNumber(Department, str1, "迟到");
                int Early = DataAnalysis.GetEveryAttendanceNumber(Department, str1, "早退");
                int Attandance = DataAnalysis.GetEveryAttendanceNumber(Department, str1, "旷课");
                int Leave = DataAnalysis.GetEveryAttendanceNumber(Department, str1, "请假");

                DataRow dr = DataAnalysis.CreatDataRow(dt, str1, Department, Late, Early, Attandance, Leave);
                dt.Rows.Add(dr);
            }
            DataRow drLast = DataAnalysis.InsertLastRow(dt);
            dt.Rows.Add(drLast);
            return dt;
        }
    }  
    private string DrawChart(DataTable dtTemp)
    {
        int zz = dtTemp.Rows.Count - 1;
        if (zz > 0)
        {
            string[] AllWeek = new string[zz];
            string[] AllAttendanceNum = new string[zz];
            for (int i = 0; i < dtTemp.Rows.Count - 1; i++)
            {
                zz--;
                AllWeek[i] = dtTemp.Rows[zz]["周次"].ToString();
                AllAttendanceNum[i] = dtTemp.Rows[zz]["合计"].ToString();
            }

            string strXdata = string.Empty;
            foreach (string strData in AllWeek)
            {
                strXdata += strData + "\t";
            }

            string strYdata = string.Empty;
            foreach (string strValue in AllAttendanceNum)
            {
                strYdata += strValue + "\t";
            }

            ChartSpace laySpace = new ChartSpaceClass();
            ChChart InsertChart = laySpace.Charts.Add(0);

            InsertChart.Type = ChartChartTypeEnum.chChartTypeLineStacked;
            InsertChart.HasLegend = false;

            InsertChart.HasTitle = true;
            InsertChart.Title.Caption = dtTemp.Rows[0]["系部"] + "学生" + AllWeek[0] + "-" + AllWeek[AllWeek.Length - 1] + "周缺勤情况走势图";
            InsertChart.Axes[0].HasTitle = true;
            InsertChart.Axes[0].Title.Caption = "周次";
            InsertChart.Axes[1].HasTitle = true;
            InsertChart.Axes[1].Title.Caption = "缺勤人数";

            InsertChart.SeriesCollection.Add(0);

            InsertChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames, (int)ChartSpecialDataSourcesEnum.chDataLiteral, "图例1");
            InsertChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories, (int)ChartSpecialDataSourcesEnum.chDataLiteral, strXdata);
            InsertChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral, strYdata);

            string strAbsolutePath = (Server.MapPath(".")) + "\\ShowData.gif";
            laySpace.ExportPicture(strAbsolutePath, "GIF",1450, 250);

            string strRelativePath = "./ShowData.gif";
            Random rd = new Random();
            string strImageTag = "<IMG SRC='" + strRelativePath + "?id=" + rd.Next(65500) + "'/>";

            return strImageTag;
        }
        else
        {
            return "";
        }
    } 
    protected void btnDetailsClick(object sender, EventArgs e)//详情
    {
        Button btn = sender as Button;
        GridViewRow dvr = btn.Parent.Parent as GridViewRow;
        string queryWeek = dvr.Cells[0].Text.ToString();
        string queryDepartment = dvr.Cells[1].Text.ToString();
        string url = "AttendanceStudentDetails.aspx?queryWeek="+queryWeek+"&queryDepartment="+Server.UrlEncode(queryDepartment);
        Page.RegisterStartupScript("ServiceManHistoryButtonClick", "<script>window.open('" + url + "','_blank')</script>");
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ServiceManHistoryButtonClick", "<script>window.open('" + url + "','_blank')</script>");    
    }
    
    protected void gvKJ_RowDataBound(object sender, GridViewRowEventArgs e)  //详情不显示
    {
        if (e.Row.Cells[6].Text == "0")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Enabled = false;
        }

        if (e.Row.Cells[0].Text == "缺勤人数总计")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Visible = false;
        }
    }
    protected void gvXX_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[6].Text == "0")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Enabled = false;
        }
        if (e.Row.Cells[0].Text == "缺勤人数总计")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Visible = false;
        }
    }
    protected void gvJG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[6].Text == "0")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Enabled = false;
        }
        if (e.Row.Cells[0].Text == "缺勤人数总计")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Visible = false;
        }
    }
    protected void gvSP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[6].Text == "0")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Enabled = false;
        }
        if (e.Row.Cells[0].Text == "缺勤人数总计")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Visible = false;
        }
    }
    protected void gvJX_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[6].Text == "0")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Enabled = false;
        }
        if (e.Row.Cells[0].Text == "缺勤人数总计")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Visible = false;
        }
    }
    protected void gvWY_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[6].Text == "0")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Enabled = false;
        }
        if (e.Row.Cells[0].Text == "缺勤人数总计")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Visible = false;
        }
    }
    protected void gvJZ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[6].Text == "0")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Enabled = false;
        }
        if (e.Row.Cells[0].Text == "缺勤人数总计")
        {
            Control ctl = e.Row.FindControl("btnDetail");
            (ctl as Button).Visible = false;
        }
    }

    private void Clear()
    {
        lblJG.Visible = false;
        lblJX.Visible = false;
        lblJZ.Visible = false;
        lblKJ.Visible = false;
        lblSP.Visible = false;
        lblWY.Visible = false;
        lblXX.Visible = false;
    }
   
}
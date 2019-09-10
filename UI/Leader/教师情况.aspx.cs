using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

public partial class Leader_教师情况 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() != "院系领导")
            {
                Response.Redirect("~\\登录.aspx");
            }
            else
            {
                Label1.Visible = false;//多出来的没用
                //填充DDL
                DataTable DTWeek = AddSQLStringToDAL.GetDataTableBysql("select distinct CurrentWeek from TabTeacherAttendance where [IsAttendance] = '未考勤' and CONVERT(int,CurrentWeek) < '" + Session["CurrentWeek"].ToString() + "' order by CurrentWeek");//查找小于本周的周次并排序
                ddlWeek.Items.Add(Session["CurrentWeek"].ToString());
                for (int i = 0; i < DTWeek.Rows.Count; i++)
                {
                    ddlWeek.Items.Add(DTWeek.Rows[i]["CurrentWeek"].ToString());
                }
                DataTable DTDepartment = AddSQLStringToDAL.GetDataTableBysql("select distinct TeacherDepartment from TabTeacherAttendance");
                ddlDepartment.Items.Add("所有系部");
                for (int i = 0; i < DTDepartment.Rows.Count; i++)
                {
                    ddlDepartment.Items.Add(DTDepartment.Rows[i]["TeacherDepartment"].ToString());
                }

                Clear();

                //填充GridView
                DataTable dt = GetTeacherDt(Session["CurrentWeek"].ToString(), "所有系部");
                if (dt.Rows.Count == 0)
                {
                    Teacherlbl.Visible = true;
                }
                else
                {
                    gvTeacher.Visible = true;
                    btnTeacher.Visible = true;
                    gvTeacher.DataSource = dt;
                    gvTeacher.DataBind();
                }
            }
        }
    }



    private DataTable GetTeacherDt(string Week, string Department)
    {
        DataTable dt;
        if (Department == "所有系部")
        {
            dt = AddSQLStringToDAL.GetDataTableBysql("SELECT [TeacherDepartment] as '所属部门',[TeacherID] as '教师工号',[TeacherName] as '教师姓名',[CurrentWeek] as '周次',[Week] as '星期',[Time] as '节次' ,[Course] as '课程名称' ,[Area] as '上课地点' ,[IsAttendance] as '考勤情况',[Count] as '作业情况',[StudentIDList] as '课程信息' FROM [TabTeacherAttendance]where [IsAttendance] = '未考勤' and  CONVERT(int,CurrentWeek) <= '" + Week + "'");
        }
        else
        {
            dt = AddSQLStringToDAL.GetDataTableBysql("SELECT [TeacherDepartment] as '所属部门',[TeacherID] as '教师工号',[TeacherName] as '教师姓名',[CurrentWeek] as '周次',[Week] as '星期',[Time] as '节次' ,[Course] as '课程名称' ,[Area] as '上课地点' ,[IsAttendance] as '考勤情况',[Count] as '作业情况',[StudentIDList] as '课程信息' FROM [TabTeacherAttendance] where [IsAttendance] = '未考勤' and [TeacherDepartment] = '" + Department + "' and  CONVERT(int,CurrentWeek) <= '" + Week + "'");
        }
        return dt;
    }

    protected void btnTeacher_Click(object sender, EventArgs e)
    {
        ExportToSpreadsheet(GetTeacherDt(ddlWeek.SelectedItem.Text.ToString(), ddlDepartment.SelectedItem.Text.ToString()),  ddlDepartment.SelectedItem.Text.ToString()+"教师漏报信息");
    }
    /// <summary>  
    /// 将DataTable导出为Excel  
    /// </summary>  
    /// <param name="table">DataTable数据源</param>  
    /// <param name="name">文件名</param>  
    public static void ExportToSpreadsheet(DataTable table, string name)
    {
        Random r = new Random();
        string rf = "";
        for (int j = 0; j < 10; j++)
        {
            rf = r.Next(int.MaxValue).ToString();
        }

        HttpContext context = HttpContext.Current;
        context.Response.Clear();

        context.Response.ContentType = "text/csv";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + rf + ".xls");
        context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

        foreach (DataColumn column in table.Columns)
        {
            context.Response.Write(column.ColumnName + ",");
            //context.Response.Write(column.ColumnName + "(" + column.DataType + "),");  
        }

        context.Response.Write(Environment.NewLine);
        double test;

        foreach (DataRow row in table.Rows)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                switch (table.Columns[i].DataType.ToString())
                {
                    case "System.String":
                        if (double.TryParse(row[i].ToString(), out test)) context.Response.Write("=");
                        context.Response.Write("\"" + row[i].ToString().Replace("\"", "\"\"") + "\",");
                        break;
                    case "System.DateTime":
                        if (row[i].ToString() != "")
                            context.Response.Write("\"" + ((DateTime)row[i]).ToString("yyyy-MM-dd hh:mm:ss") + "\",");
                        else
                            context.Response.Write("\"" + row[i].ToString().Replace("\"", "\"\"") + "\",");
                        break;
                    default:
                        context.Response.Write("\"" + row[i].ToString().Replace("\"", "\"\"") + "\",");
                        break;
                }
            }
            context.Response.Write(Environment.NewLine);
        }
        context.Response.End();
    }


    protected void btnOK_Click(object sender, EventArgs e)//查询
    {
        Clear();
        DataTable dt = GetTeacherDt(ddlWeek.SelectedItem.Text.ToString(), ddlDepartment.SelectedItem.Text.ToString());
        if (dt.Rows.Count == 0)
        {
            Teacherlbl.Visible = true;
        }
        else
        {
            gvTeacher.Visible = true;
            btnTeacher.Visible = true;
            gvTeacher.DataSource = dt;
            gvTeacher.DataBind();
        }
    }

    private void Clear()
    {
        Teacherlbl.Visible = false;
        gvTeacher.Visible = false;
        btnTeacher.Visible = false;
    }
}
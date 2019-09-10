using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

public partial class Secretary_本系学生情况 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() != "辅导员")
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

                Clear();

                DataTable dt1 = GetStudentDt(Session["CurrentWeek"].ToString(), Session["Department"].ToString());
                if (dt1.Rows.Count == 0)
                {
                    lblStudent.Visible = true;
                }
                else
                {
                    btnStudent.Visible = true;
                    gvStudent.Visible = true;
                    gvStudent.DataSource = dt1;
                    gvStudent.DataBind();
                }

                DataTable dt2 = GetHomeworkDt(Session["CurrentWeek"].ToString(), Session["Department"].ToString());
                if (dt2.Rows.Count == 0)
                {
                    lblHomework.Visible = true;
                }
                else
                {
                    btnHomework.Visible = true;
                    gvHomework.Visible = true;
                    gvHomework.DataSource = dt2;
                    gvHomework.DataBind();
                }
            }
        }
    }

    private DataTable GetStudentDt(string Week, string Department)
    {
        DataTable dt;
   
        dt = AddSQLStringToDAL.GetDataTableBysql("SELECT [TeacherID] as '教师工号',[TeacherName] as '教师姓名',[Course] as '课程名称',[CurrentWeek] as '周次',[Week] as '星期',[Time] as '节次',[StudentDepartment] as '学生系部',[StudentClass] as '课程信息',[StudentID] as '学生学号',[StudentName] as '学生姓名',[AttendanceType] as '异常类型' FROM [TabStudentAttendance] where StudentDepartment = '" + Department + "' and CONVERT(int,CurrentWeek) <= '" + Week + "'");
        
        return dt;
    }

    private DataTable GetHomeworkDt(string Week, string Department)
    {
        DataTable dt;

        dt = AddSQLStringToDAL.GetDataTableBysql("SELECT [TeacherID] as '教师工号',[TeacherName] as '教师姓名',[Course] as '课程名称',[CurrentWeek] as '周次',[Week] as '星期',[Time] as '节次',[StudentDepartment] as '学生系部',[StudentClass] as '课程信息',[StudentID] as '学生学号',[StudentName] as '学生姓名',[HomeworkType] as '异常类型' FROM [TabStudentHomework] where StudentDepartment = '" + Department + "' and CONVERT(int,CurrentWeek) <= '" + Week + "'");

        return dt;
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

    protected void btnStudent_Click(object sender, EventArgs e)
    {
        ExportToSpreadsheet(GetStudentDt(ddlWeek.SelectedItem.Text.ToString(), Session["Department"].ToString()),  Session["Department"].ToString()+"学生缺勤信息");
    }

    protected void btnOK_Click(object sender, EventArgs e)//查询
    {
        Clear();

        DataTable dt1 = GetStudentDt(ddlWeek.SelectedItem.Text.ToString(), Session["Department"].ToString());
        if (dt1.Rows.Count == 0)
        {
            lblStudent.Visible = true;
        }
        else
        {
            btnStudent.Visible = true;
            gvStudent.Visible = true;
            gvStudent.DataSource = dt1;
            gvStudent.DataBind();
        }

        DataTable dt2 = GetHomeworkDt(ddlWeek.SelectedItem.Text.ToString(), Session["Department"].ToString());
        if (dt2.Rows.Count == 0)
        {
            lblHomework.Visible = true;
        }
        else
        {
            btnHomework.Visible = true;
            gvHomework.Visible = true;
            gvHomework.DataSource = dt2;
            gvHomework.DataBind();
        }
    }

    private void Clear()
    {
        btnStudent.Visible = false;
        lblStudent.Visible = false;
        gvStudent.Visible = false;
    }
    protected void btnHomework_Click(object sender, EventArgs e)
    {
        ExportToSpreadsheet(GetHomeworkDt(ddlWeek.SelectedItem.Text.ToString(), Session["Department"].ToString()),  Session["Department"].ToString() + "学生作业信息");
    }
}
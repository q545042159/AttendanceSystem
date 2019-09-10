using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BLL;

public partial class Admin_录入考勤 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"].ToString() == "")
            {
                Response.Redirect("~\\登录.aspx");
            }
            else
            {
                lblTeacherName.Text = Session["UserName"].ToString() + "您好！请录入本周考勤信息";
                Bind();
            }
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
 
    }

    protected void BtnHomeworkSubmit_Click(object sender, EventArgs e)
    {

    }

    private void Bind()
    {
        DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabTeacherAttendance where TeacherID = '"+Session["UserID"].ToString()+"' and CurrentWeek = '"+Session["CurrentWeek"].ToString()+"'");
        if (dt.Rows.Count == 0)
        {
            lblTeacherName.Text = Session["UserName"].ToString() + "您好,您本周无授课安排！";
        }
        else
        {
            rptCourse.DataSource = dt;
            rptCourse.DataBind();
        }

        int ICurrentWeek = Convert.ToInt32(Session["CurrentWeek"].ToString());
        int ILastWeek = 0;
        string SLastWeek = "";
        if (ICurrentWeek > 1)
        {
            ILastWeek = ICurrentWeek - 1;
            if (ILastWeek <= 9)
            {
                SLastWeek = "0" + ILastWeek.ToString();
            }
            else
            {
                SLastWeek = ILastWeek.ToString();
            }
        }

        if (SLastWeek != "")
        {
            Session["HomeWorkWeek"] = SLastWeek;
            DataTable dt2 = AddSQLStringToDAL.GetDataTableBysql("select * from TabTeacherAttendance where TeacherID = '"+Session["UserID"].ToString()+"' and CurrentWeek = '"+SLastWeek+"' and Count = '已布置作业'");
            if (dt2.Rows.Count == 0)
            {
                lblTest.Text = Session["UserName"].ToString() + "您好,上周作业没有未批改情况！";
            }
            else
            {
                rptHomework.DataSource = dt2;
                rptHomework.DataBind();
            }
        }
    }

    protected void rptHomework_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        TextBox txt = e.Item.FindControl("txtCourse1") as TextBox;
        Session["CurrentCourse"] = txt.Text.Trim();

        TextBox txt1 = e.Item.FindControl("txtWeek1") as TextBox;
        Session["Week"] = txt1.Text.Trim();

        TextBox txt2 = e.Item.FindControl("txtTime1") as TextBox;
        Session["Time"] = txt2.Text.Trim();

        TextBox txt3 = e.Item.FindControl("txtWeekRange1") as TextBox;
        Session["WeekRange"] = txt3.Text.Trim();

        Response.Redirect("~\\Admin\\HomeworkDetails.aspx");
    }

    protected void rptCourse_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        CheckBox chk = e.Item.FindControl("chkHomework") as CheckBox;
        if (chk.Checked == true)
        {
            Session["Homework"] = "已布置作业";
        }
        else
        {
            Session["Homework"] = "未布置作业";
        }

        TextBox txt = e.Item.FindControl("txtCourse") as TextBox;
        Session["CurrentCourse"] = txt.Text.Trim();

        TextBox txt1 = e.Item.FindControl("txtWeek") as TextBox;
        Session["Week"] = txt1.Text.Trim();

        TextBox txt2 = e.Item.FindControl("txtTime") as TextBox;
        Session["Time"] = txt2.Text.Trim();

        TextBox txt3 = e.Item.FindControl("txtWeekRange") as TextBox;
        Session["WeekRange"] = txt3.Text.Trim();

        Response.Redirect("~\\Admin\\AttendanceDetails.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.Text;


public partial class Leader_HomeworkDetails : System.Web.UI.Page
{
    System.Drawing.Color c;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CurrentWeek"].ToString() != "")
            {
                btnClose.Visible = true;
                Label1.Visible = false;
            }
            else
            {
                Response.Redirect("~\\登录.aspx");
            }

            string LastWeek = (Convert.ToInt32(Session["CurrentWeek"].ToString()) - 1).ToString();
            DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from [TabTeacherAttendance] where TeacherID = '" + Session["UserID"].ToString() + "' and CurrentWeek = '" + LastWeek + "' and Course = '" + Session["CurrentCourse"].ToString() + "' and Week = '" + Session["Week"].ToString() + "' and Time = '" + Session["Time"].ToString() + "' and Count = '已布置作业'");

            if (dt.Rows.Count == 0)
            {
                lblResultMessage.Text = "您已经批改本次作业！";
                lblHomeWorkMessage.Visible = false;
                gvHomeworkDetails.Visible = false;
                btnAttdance.Visible = false;
            }
            else
            {
                string strCourse = Session["CurrentCourse"].ToString();
                lblMessage.Text = Session["Week"].ToString() + Session["Time"].ToString() + "|" + strCourse.Substring(8, strCourse.Length - 11) + "|" + this.gvHomeworkDetails.Rows.Count.ToString() + "人";
                c = this.gvHomeworkDetails.BackColor;
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)//返回
    {
        Response.Redirect("~\\Leader\\录入考勤.aspx");
    }
    protected void btnAttdance_Click(object sender, EventArgs e)//上报
    {
        StringBuilder strNO = new StringBuilder("未完成作业名单:");
        int sum = 0;
        foreach (GridViewRow row in this.gvHomeworkDetails.Rows)
        {
            Control ctl2 = row.FindControl("rdoNO");
            TableCellCollection cell = row.Cells;
            if ((ctl2 as RadioButton).Checked)
            {
                if (AddSQLStringToDAL.InsertTabTeachers("TabStudentHomework", Session["UserID"].ToString(), Session["UserName"].ToString(), Session["CurrentCourse"].ToString(), Session["CurrentWeek"].ToString(), Session["Week"].ToString(), Session["Time"].ToString(), cell[0].Text.ToString(), cell[1].Text.ToString(), cell[2].Text.ToString(), cell[3].Text.ToString(), "未完成", ""))
                {
                    sum++;
                    strNO.Append(cell[3].Text.ToString() + ";");//添加上姓名
                }
            }
        }

        if (strNO.ToString() == "未完成作业名单:")
        {
            strNO.Append("无");
        }

        if (AddSQLStringToDAL.UpdateTabTeachers("TabTeacherAttendance", "Count", "已批改作业", "IsAttendance", "已考勤", "TeacherID", Session["UserID"].ToString(), "Course", Session["CurrentCourse"].ToString(), "CurrentWeek", (Convert.ToInt32(Session["CurrentWeek"].ToString()) - 1).ToString(), "Week", Session["Week"].ToString(), "Time", Session["Time"].ToString()))
        {

            lblHomeWorkMessage.Text = strNO.ToString();
            lblResultMessage.Text = "本次作业记录已经上报成功！请返回主页面！";
            btnClose.Visible = true;
        }
        gvHomeworkDetails.Visible = false;
    }

    protected void rdo_CheckChange(object sender, EventArgs e)//点击radioButton变色
    {
        foreach (GridViewRow row in this.gvHomeworkDetails.Rows)
        {
            Control ctl1 = row.FindControl("rdoOK");
            Control ctl2 = row.FindControl("rdoNO");
            TableCellCollection cell = row.Cells;

            if ((ctl1 as RadioButton).Checked)
            {
                this.gvHomeworkDetails.Rows[row.RowIndex].BackColor = c;
            }
            if ((ctl2 as RadioButton).Checked)
            {
                this.gvHomeworkDetails.Rows[row.RowIndex].BackColor = System.Drawing.Color.SkyBlue;
            }
        }
    }
}
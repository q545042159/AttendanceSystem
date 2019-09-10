using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.Text;

public partial class Leader_AttendanceDetails : System.Web.UI.Page
{
    System.Drawing.Color c;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CurrentCourse"].ToString() != "")
            {
                VisibleNO();
                btnClose.Visible = true;
                Label1.Visible = false;
            }
            else
            {
                Response.Redirect("~\\登录.aspx");
            }

            if (CompareWeek())
            {
                if (CheckIsRecord())
                {
                    SetControlsVisibleFalse();
                    lblResultMessage.Text = "您已经录入本次考勤记录！";
                    btnClose.Visible = true;
                }
                else
                {
                    string strCourse = Session["CurrentCourse"].ToString();
                    lblMessage.Text = Session["Week"].ToString() + Session["Time"].ToString() + "|" + strCourse.Substring(8, strCourse.Length - 11) + "|" + this.gvAttendanceDetails.Rows.Count.ToString() + "人";
                    c = this.gvAttendanceDetails.BackColor;
                }
            }
            else
            {
                SetControlsVisibleFalse();
                lblResultMessage.Text = "本门课程尚未结束，请于课程后录入！";
                btnClose.Visible = true;
            }
        }
    }

    private bool CompareWeek()
    {
        int Week = 0;
        int CurrentWeek = 0;
        switch (DateTime.Now.DayOfWeek.ToString())
        {
            case "Monday":
                CurrentWeek = 1;
                break;
            case "Tuesday":
                CurrentWeek = 2;
                break;
            case "Wednesday":
                CurrentWeek = 3;
                break;
            case "Thursday":
                CurrentWeek = 4;
                break;
            case "Firday":
                CurrentWeek = 5;
                break;
            case "Saturday":
                CurrentWeek = 6;
                break;
            case "Sunday":
                CurrentWeek = 7;
                break;
            default:
                CurrentWeek = 0;
                break;
        }

        switch (Session["Week"].ToString())
        {
            case "星期一":
                Week = 1;
                break;
            case "星期二":
                Week = 2;
                break;
            case "星期三":
                Week = 3;
                break;
            case "星期四":
                Week = 4;
                break;
            case "星期五":
                Week = 5;
                break;
            case "星期六":
                Week = 6;
                break;
            case "星期七":
                Week = 7;
                break;
            default:
                Week = 0;
                break;
        }

        if (CurrentWeek > Week)
        {
            return true;
        }
        else if (CurrentWeek == Week)
        {
            int tt = 0;
            switch (Session["Time"].ToString())
            {
                case "1-2节":
                    tt = 10;
                    break;
                case "3-4节":
                    tt = 12;
                    break;
                case "5-6节":
                    tt = 16;
                    break;
                case "7-8节":
                    tt = 18;
                    break;
                case "9-10节":
                    tt = 20;
                    break;
                default:
                    tt = 0;
                    break;
            }

            if (DateTime.Now.Hour >= tt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool CheckIsRecord()
    {
        string strSQL = "select * from [TabTeacherAttendance] where TeacherID = '" + Session["UserID"].ToString() + "' and CurrentWeek = '" + Session["CurrentWeek"].ToString() + "' and Course = '" + Session["CurrentCourse"].ToString() + "' and Week = '" + Session["Week"].ToString() + "' and Time = '" + Session["Time"].ToString() + "'";
        DataTable dt = AddSQLStringToDAL.GetDataTableBysql(strSQL);

        if (dt.Rows[0]["IsAttendance"].ToString().Trim() == "未考勤")
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private void SetControlsVisibleFalse()
    {
        lblMessage.Visible = false;
        btnAttdance.Visible = false;
        btnUnNormal.Visible = false;
        chkNormal.Visible = false;
        ddlUnNormal.Visible = false;
        gvAttendanceDetails.Visible = false;
    }



    protected void rdo_CheckChange(object sender, EventArgs e)//点击radioButton变色
    {
        foreach (GridViewRow row in this.gvAttendanceDetails.Rows)
        {
            Control ctl1 = row.FindControl("rdoNormal");
            Control ctl2 = row.FindControl("rdoLate");
            Control ctl3 = row.FindControl("rdoAbsence");
            Control ctl4 = row.FindControl("rdoEarly");
            Control ctl5 = row.FindControl("rdoLeave");
            TableCellCollection cell = row.Cells;

            if ((ctl1 as RadioButton).Checked)
            {
                //this.gvAttendanceDetails.Rows[row.DataItemIndex].BackColor = c;
                this.gvAttendanceDetails.Rows[row.RowIndex].BackColor = c;
            }
            if ((ctl2 as RadioButton).Checked)
            {
                this.gvAttendanceDetails.Rows[row.RowIndex].BackColor = System.Drawing.Color.LightBlue;
            }
            if ((ctl3 as RadioButton).Checked)
            {
                this.gvAttendanceDetails.Rows[row.RowIndex].BackColor = System.Drawing.Color.Pink;
            }
            if ((ctl4 as RadioButton).Checked)
            {
                this.gvAttendanceDetails.Rows[row.RowIndex].BackColor = System.Drawing.Color.Yellow;
            }
            if ((ctl5 as RadioButton).Checked)
            {
                this.gvAttendanceDetails.Rows[row.RowIndex].BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }

    protected void gvAttendanceDetails_RowDataBond(object sender, GridViewRowEventArgs e)//“正常”时变回原色
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor = this.style.backgroundColor;this.style.backColor = '#6699ff'");//鼠标移到时
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = currentcolor");//鼠标离开时
        }
    }

    protected void btnAttdance_Click(object sender, EventArgs e)//将非正常学生名单录入数据库
    {
        StringBuilder strLate = new StringBuilder("迟到名单:");
        StringBuilder strAbsence = new StringBuilder("旷课名单:");
        StringBuilder strEarly = new StringBuilder("早退名单:");
        StringBuilder strLeave = new StringBuilder("请假名单:");

        int sum = 0;
        foreach (GridViewRow row in this.gvAttendanceDetails.Rows)
        {
            Control ctl2 = row.FindControl("rdoLate");
            Control ctl3 = row.FindControl("rdoAbsence");
            Control ctl4 = row.FindControl("rdoEarly");
            Control ctl5 = row.FindControl("rdoLeave");
            TableCellCollection cell = row.Cells;
            if ((ctl2 as RadioButton).Checked)
            {
                if (AddSQLStringToDAL.InsertTabTeachers("TabStudentAttendance", Session["UserID"].ToString(), Session["UserName"].ToString(), Session["CurrentCourse"].ToString(), Session["CurrentWeek"].ToString(), Session["Week"].ToString(), Session["Time"].ToString(), cell[0].Text.ToString(), cell[1].Text.ToString(), cell[2].Text.ToString(), cell[3].Text.ToString(), "迟到", ""))
                {
                    sum++;
                    strLate.Append(cell[3].Text.ToString() + ";");
                }
            }
            if ((ctl3 as RadioButton).Checked)
            {
                if (AddSQLStringToDAL.InsertTabTeachers("TabStudentAttendance", Session["UserID"].ToString(), Session["UserName"].ToString(), Session["CurrentCourse"].ToString(), Session["CurrentWeek"].ToString(), Session["Week"].ToString(), Session["Time"].ToString(), cell[0].Text.ToString(), cell[1].Text.ToString(), cell[2].Text.ToString(), cell[3].Text.ToString(), "旷课", ""))
                {
                    sum++;
                    strAbsence.Append(cell[3].Text.ToString() + ";");
                }
            }
            if ((ctl4 as RadioButton).Checked)
            {
                if (AddSQLStringToDAL.InsertTabTeachers("TabStudentAttendance", Session["UserID"].ToString(), Session["UserName"].ToString(), Session["CurrentCourse"].ToString(), Session["CurrentWeek"].ToString(), Session["Week"].ToString(), Session["Time"].ToString(), cell[0].Text.ToString(), cell[1].Text.ToString(), cell[2].Text.ToString(), cell[3].Text.ToString(), "早退", ""))
                {
                    sum++;
                    strEarly.Append(cell[3].Text.ToString() + ";");
                }
            }
            if ((ctl5 as RadioButton).Checked)
            {
                if (AddSQLStringToDAL.InsertTabTeachers("TabStudentAttendance", Session["UserID"].ToString(), Session["UserName"].ToString(), Session["CurrentCourse"].ToString(), Session["CurrentWeek"].ToString(), Session["Week"].ToString(), Session["Time"].ToString(), cell[0].Text.ToString(), cell[1].Text.ToString(), cell[2].Text.ToString(), cell[3].Text.ToString(), "请假", ""))
                {
                    sum++;
                    strLeave.Append(cell[3].Text.ToString() + ";");
                }
            }
        }

        if (strLate.ToString() == "迟到名单:")
        {
            strLate.Append("无");
        }
        if (strAbsence.ToString() == "旷课名单:")
        {
            strAbsence.Append("无");
        }
        if (strEarly.ToString() == "早退名单:")
        {
            strEarly.Append("无");
        }
        if (strLeave.ToString() == "请假名单:")
        {
            strLeave.Append("无");
        }

        VisibleYES();

        if (AddSQLStringToDAL.UpdateTabTeachers("TabTeacherAttendance", "IsAttendance", "已考勤", "Count", Session["Homework"].ToString(), "TeacherID", Session["UserID"].ToString(), "Course", Session["CurrentCourse"].ToString(), "CurrentWeek", Session["CurrentWeek"].ToString(), "Week", Session["Week"].ToString(), "Time", Session["Time"].ToString()))
        {
            lblAttendanceMessage.Text = strAbsence.ToString();
            lblLateMessage.Text = strLate.ToString();
            lblEarlyMessage.Text = strEarly.ToString();
            lblLeaveMessage.Text = strLeave.ToString();
            strLate.Remove(0, strLate.Length);
            strAbsence.Remove(0, strAbsence.Length);
            strEarly.Remove(0, strEarly.Length);
            strLeave.Remove(0, strLeave.Length);

            SetControlsVisibleFalse();

            lblResultMessage.Text = "本次考勤记录已经上报成功！本次课您" + Session["Homework"].ToString() + ",请返回主页面！";
            btnClose.Visible = true;
        }
    }

    private void VisibleNO()
    {
        lblAttendanceMessage.Visible = false;
        lblEarlyMessage.Visible = false;
        lblLateMessage.Visible = false;
        lblLeaveMessage.Visible = false;
    }

    private void VisibleYES()
    {
        lblAttendanceMessage.Visible = true;
        lblEarlyMessage.Visible = true;
        lblLateMessage.Visible = true;
        lblLeaveMessage.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\Leader\\录入考勤.aspx");
    }
}
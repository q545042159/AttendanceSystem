using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

public partial class Admin_SendMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() != "系统管理员")
            {
                Response.Redirect("~\\登录.aspx");
            }
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (chkToLeader.Checked == false && chkToSecretary.Checked == false && chkToTeacher.Checked == false)
        {
            lblMessage.Text = "请选中要发布通知的对象！";
        }
        else
        {
            if (txtxMessage.Text == "")
            {
                lblMessage.Text = "通知不能为空！";
            }
            else
            {
                List<string> strSum = new List<string>();
                List<string> strID1 = new List<string>();
                List<string> strID2 = new List<string>();
                List<string> strID3 = new List<string>();
                List<string> strID4 = new List<string>();

                if (chkToLeader.Checked)//找出校领导ID
                {
                    strID1 = AddSQLStringToDAL.GetDistinctStrings("TabTeachers", "UserID", "Role", 2);
                }

                if (chkToSecretary.Checked)//找出辅导员ID
                {
                    strID2 = AddSQLStringToDAL.GetDistinctStrings("TabTeachers", "UserID", "Role", 3);
                }

                if (chkToTeacher.Checked)//找出所有有课教师的ID
                {
                    strID3 = AddSQLStringToDAL.GetDistinctStrings("Tabteachers", "UserID");
                }

                //将这三个找出来的全部放到strSum里
                strSum.AddRange(strID1);
                strSum.AddRange(strID2);
                strSum.AddRange(strID3);

                //因为有课的教师也可能是辅导员或者校领导，所以移除相同的ID
                for (int i = 0; i < strSum.Count; i++)
                {
                    for (int j = 0; j < strSum.Count; j++)
                    {
                        if (i != j)
                        {
                            if (strSum[i] == strSum[j])
                            {
                                strSum.RemoveAt(j);
                            }
                        }
                    }
                }


                if (strSum.Count > 0)
                {
                    for (int i = 0; i < strSum.Count; i++)
                    {
                        if (AddSQLStringToDAL.InsertTabTeachers("TabMessage", System.DateTime.Now.ToString(), txtxMessage.Text.ToString(), strSum[i].ToString(), "false", "", ""))
                        {

                        }
                    }
                    lblMessage.Text = "消息发送成功！";
                    txtxMessage.Text = "";
                }
            }
        }
    }

}
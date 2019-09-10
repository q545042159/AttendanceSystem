using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Web.Security;//MD5加密要用到

public partial class Secretary_修改密码 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() != "")
            {
                txtUserID.Text = Session["UserID"].ToString();
                txtUserName.Text = Session["UserName"].ToString();
            }
            else
            {
                Response.Redirect("~\\登录.aspx");
            }
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (txtUserPWD.Text == "")
        {
            txtUserEnterPWD.Text = "";
            lblMessage.Text = "修改失败！密码不能为空！";
        }
        else
        {
            if (txtUserPWD.Text != txtUserEnterPWD.Text)
            {
                txtUserPWD.Text = "";
                txtUserEnterPWD.Text = "";
                lblMessage.Text = "修改失败！两次密码不一致！";
            }
            //防止SQL注入式攻击
            else if (txtUserPWD.Text.ToString().ToLower().IndexOf("select") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf(";") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("delete") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("insert") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("update") != -1 || txtUserEnterPWD.Text.ToString().ToLower().IndexOf("select") != -1 || txtUserEnterPWD.Text.ToString().ToLower().IndexOf("update") != -1 || txtUserEnterPWD.Text.ToString().ToLower().IndexOf("delete") != -1 || txtUserEnterPWD.Text.ToString().ToLower().IndexOf("insert") != -1 || txtUserEnterPWD.Text.ToString().ToLower().IndexOf(";") != -1)
            {
                txtUserPWD.Text = "";
                txtUserEnterPWD.Text = "";
                lblMessage.Text = "含有关键词，请重新输入！";
            }
            else
            {
                DataTable dt = AddSQLStringToDAL.GetOldPWD(Session["UserID"].ToString());
                //查询原密码,是加密后的
                string OldPWD = "";
                if (dt.Rows.Count != 0)
                {
                    OldPWD = dt.Rows[0]["UserPWD"].ToString();
                }
                //将原密码插入到TabPassword表
                if (AddSQLStringToDAL.InsertTabPassword(Session["UserID"].ToString(), OldPWD, System.DateTime.Now.ToString()))
                {

                }
                AddSQLStringToDAL.Update("TabTeachers", "UserPWD", FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserPWD.Text, "MD5").ToString(), "UserID", txtUserID.Text);
                lblMessage.Text = "修改成功!";
            }
        }
    }
}
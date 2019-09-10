using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BLL;

public partial class Admin_AddNewTeacher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() == "系统管理员")
            {
                Bind();
            }
            else
            {
                Response.Redirect("~\\登录.aspx");  
            }
        }
    }

    protected void Bind()
    {
        DataTable dt = AddSQLStringToDAL.GetDistinctColums("TabTeachers", "Department");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ddlDepartment.Items.Add(dt.Rows[i][0].ToString());
        }

        DataTable dt1 = AddSQLStringToDAL.GetDistinctColums("TabTeachers", "Role");
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            ddlRole.Items.Add(dt1.Rows[i][0].ToString());
        }

        DataTable dt2 = AddSQLStringToDAL.GetDistinctColums("TabTeachers", "Type");
        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            ddlTeachersType.Items.Add(dt2.Rows[i][0].ToString());
        }
    }

    private void Clear()
    {
        lblMessage.Text = "";
        txtUserID.Text = "";
        txtUserName.Text = "";
        txtUserPWD.Text = "";
    }

    protected void btnEnter_Click(object sender, EventArgs e)
    {
        if (txtUserID.Text.ToString().ToLower().IndexOf("delete") != -1 || txtUserID.Text.ToString().ToLower().IndexOf(";") != -1 || txtUserID.Text.ToString().ToLower().IndexOf("select") != -1 || txtUserID.Text.ToString().ToLower().IndexOf("insert") != -1 || txtUserID.Text.ToString().ToLower().IndexOf("update") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("select") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf(";") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("delete") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("insert") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("update") != -1 || txtUserName.Text.ToString().ToLower().IndexOf("update") != -1 || txtUserName.Text.ToString().ToLower().IndexOf("insert") != -1 || txtUserName.Text.ToString().ToLower().IndexOf("select") != -1 || txtUserName.Text.ToString().ToLower().IndexOf("delete") != -1 || txtUserName.Text.ToString().ToLower().IndexOf(";") != -1)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "(含有关键词！请重新输入！)";
        }
        else
        {
            try//和数据库UserID不能为空且唯一对应，try catch  异常处理
            {
                if (txtUserID.Text != "" && txtUserPWD.Text != "" && txtUserName.Text != "")
                {
                    AddSQLStringToDAL.Insert("TabTeachers", ddlTeachersType.SelectedItem.ToString(), ddlDepartment.SelectedItem.ToString(), txtUserID.Text, txtUserName.Text, FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserPWD.Text, "MD5").ToString(), ddlRole.SelectedItem.ToString());
                    Clear();
                    lblMessage.Visible = true;
                    lblMessage.Text = "添加成功";
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "教师工号、姓名或密码不能为空";
                }
            }
            catch
            {
                Clear();
                lblMessage.Visible = true;
                lblMessage.Text = "输入有误！请核对教师工号等信息！教师工号不能重复！";
            }
        }
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Clear();
    }
}
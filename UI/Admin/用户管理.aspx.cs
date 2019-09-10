using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using BLL;

public partial class Admin_用户管理 : System.Web.UI.Page
{
    string GongHao = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"].ToString() != "系统管理员")
        {
            Response.Redirect("~\\登录.aspx");
        }
        else
        {
            DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabTeachers");
            Label3.Visible = false;
            txtLimit.Visible = false;
            ddlDepartment.Visible = false;
            ddlRole.Visible = false;
            lblMessage.Visible = false;
        }
    }

    protected void gvTeachers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=e;");//光标移除时
            e.Row.Attributes.Add("OnMouseOver", "e=this.style.backgroundColor;this.style.backgroundColor='#5CCCCC'");//光标移到时
            e.Row.Attributes["style"] = "Cursor.hand";//变成小手形状  
        } 
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e) //部门选择
    {
        Label3.Visible = true;
        ddlDepartment.Visible = true;
        txtLimit.Visible = false;
        ddlRole.Visible = false;
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e) //权限选择
    {
        Label3.Visible = true;
        ddlRole.Visible = true;
        txtLimit.Visible = false;
        ddlDepartment.Visible = false;
    }

    protected void ddlLimit_SelectedIndexChanged(object sender, EventArgs e) //可以在这里 将 textbox 改为 dropdownlist  这是选择范围选择
    {
        gvTeachers.Visible = false;


        if (ddlLimit.SelectedItem.ToString() == "所有记录")
        {
            Label3.Visible = false;
            txtLimit.Visible = false;
            ddlRole.Visible = false;
            ddlDepartment.Visible = false;
        }
        else if(ddlLimit.SelectedItem.ToString() == "按部门查询")
        {
            Label3.Visible = true;
            ddlDepartment.Visible = true;
            txtLimit.Visible = false;
            ddlRole.Visible = false;
        }
        else if (ddlLimit.SelectedItem.ToString() == "按权限查询")
        {
            Label3.Visible = true;
            ddlRole.Visible = true;
            txtLimit.Visible = false;
            ddlDepartment.Visible = false;
        }
        else if (ddlLimit.SelectedItem.ToString() == "按教师工号查询" || ddlLimit.SelectedItem.ToString() == "按教师工号查询" || ddlLimit.SelectedItem.ToString() == "按教师姓名查询")
        {
            Label3.Visible = true;
            txtLimit.Visible = true;
            ddlDepartment.Visible = false;
            ddlRole.Visible = false;
        }
    }

    protected void Bind()  //查询
    {
        if (ddlLimit.SelectedItem.ToString() == "所有记录")
        {
            DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabTeachers");
            BindToGridView(dt);
        }
        else if (ddlLimit.SelectedItem.ToString() != "所有记录")  //按条件查询
        {
            Label3.Visible = true;
            DataTable dt;
            string str = "";
            if (ddlLimit.SelectedItem.ToString() == "按权限查询")
            {
                ddlRole.Visible = true;
                str = "Role";
                dt = AddSQLStringToDAL.GetDataTableBysql("TabTeachers", str, Convert.ToInt32(ddlRole.SelectedItem.ToString()));
            }
            else if(ddlLimit.SelectedItem.ToString() == "按部门查询")
            {
                ddlDepartment.Visible = true;
                str = "Department";
                dt = AddSQLStringToDAL.GetDataTableBysql("TabTeachers", str, ddlDepartment.SelectedItem.ToString().Trim());
            }
            else if (ddlLimit.SelectedItem.ToString() == "按教师工号查询")
            {
                txtLimit.Visible = true;
                str = "UserID";
                dt = AddSQLStringToDAL.GetDataTableBysql("TabTeachers", str, txtLimit.Text.ToString().Trim());
            }
            else //按教师姓名查询
            {
                txtLimit.Visible = true;
                str = "UserName";
                dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabTeachers where "+str+" like '%"+txtLimit.Text.ToString().Trim()+"%'");
            }
            if (dt.Rows.Count == 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "暂无查询结果！";
            }
            else
            {
                BindToGridView(dt);
                gvTeachers.Visible = true;
                lblMessage.Visible = false;
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)//查询按钮
    {
        Bind();
    }

    protected void BindToGridView(DataTable dt)
    {
        gvTeachers.DataSource = dt;
        gvTeachers.DataKeyNames = new string[] { "UserID" };
        gvTeachers.DataBind();
    }


    protected void gvTeachers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTeachers.PageIndex = e.NewPageIndex;
        Bind();
    }

    protected void gvTeachers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTeachers.EditIndex = e.NewEditIndex;
        Bind();
    }

    protected void gvTeachers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)  //取消
    {
        gvTeachers.EditIndex = -1;
        Bind();
    }
    protected void gvTeachers_RowUpdating(object sender, GridViewUpdateEventArgs e)  //更新
    {
        string strUserID = gvTeachers.DataKeys[e.RowIndex].Value.ToString();
        string strDepartment = ((DropDownList)(gvTeachers.Rows[e.RowIndex].FindControl("ddlDepartmentEdit"))).SelectedItem.ToString();
        string strRole = ((DropDownList)(gvTeachers.Rows[e.RowIndex].FindControl("ddlRoleEdit"))).SelectedItem.ToString();
        int Role = Convert.ToInt32(strRole);//数据库的Role是int型的，要是在数据库改的话还要改别的增删改查，但我都忘了在哪了，所以不改了，就用int
        if (AddSQLStringToDAL.UpdateTabTeachers("TabTeachers", strDepartment, Role, strUserID))
        {
            gvTeachers.EditIndex = -1;
            Bind();
        } 
    }
    protected void gvTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)   //删除 
    {
        if(AddSQLStringToDAL.DeleteTabTeachers("TabTeachers",gvTeachers.DataKeys[e.RowIndex].Value.ToString()))
        {
            Bind();
        }
    }
}
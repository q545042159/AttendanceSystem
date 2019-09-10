using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_管理员首页 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"].ToString() != "系统管理员")//防止从地址栏直接输入网址
        {
            Response.Redirect("~\\登录.aspx");
        }
        if (!IsPostBack)
        {
            
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Configuration;
using System.Collections;
using BLL;

public partial class Teacher_GetMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() != "教师")
            {
                Response.Redirect("~\\登录.aspx");
            }
            else
            {
                Bind();
            }
        }
    }

    private void Bind()
    {
        DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabMessage where UserID = '" + Session["UserID"].ToString() + "' and MessageStatus = 'false'");
        if (dt.Rows.Count == 0)
        {

        }
        else
        {
            rptMessage.DataSource = dt;
            rptMessage.DataBind();
        }
    }

    protected void rptCourse_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (AddSQLStringToDAL.UpdateMessage(Session["UserID"].ToString()))
        {
            Response.Redirect("教师首页.aspx");
        }
    }
}
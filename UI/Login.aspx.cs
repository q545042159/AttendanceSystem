using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using BLL;
using System.IO;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /*txtCode.Visible = false;//验证码和文本框先不显示
            labCode.Visible = false;
            imgCode.Visible = false;*/
            //如果想点一下登录再显示验证码，去掉注释就好
            lblMessage.Visible = false;
        }
        txtUserPWD.Attributes.Add("value", txtUserPWD.Text);//不随着点击确定而清空密码框，提高用户体验
    }

    protected void btnlogin_Click(object sender, EventArgs e)
    {
        //防止SQL注入式攻击
        if (txtUserID.Text.ToString().ToLower().IndexOf("delete") != -1 || txtUserID.Text.ToString().ToLower().IndexOf(";") != -1 || txtUserID.Text.ToString().ToLower().IndexOf("select") != -1 || txtUserID.Text.ToString().ToLower().IndexOf("insert") != -1 || txtUserID.Text.ToString().ToLower().IndexOf("update") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("select") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf(";") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("delete") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("insert") != -1 || txtUserPWD.Text.ToString().ToLower().IndexOf("update") != -1)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "(含有关键词)请正确输入账号或密码！";
        }
        else
        {
            if (txtUserID.Text == "")//没有输入账户
            {
                lblMessage.Visible = true;
                lblMessage.Text = "账户为空！";
            }
            else//账号不为空
            {
                if (txtUserPWD.Text == "")//没有输入密码
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "密码为空！";
                }
                else//输入了密码
                {
                    if (txtCode.Visible == false)//还没显示出来验证码
                    {
                        //labCode.Visible = true;
                        //txtCode.Visible = true;
                        //imgCode.Visible = true;
                    }
                    else//显示出来了验证码
                    {
                        if (txtCode.Text == "")//验证码为空
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "验证码为空！";
                        }
                        else//验证码不为空
                        {
                            if (txtCode.Text.ToUpper() != Session["Code"].ToString().ToUpper())//验证码输入错误,并且忽略大小写
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "验证码错误！请重新输入！";
                                txtCode.Text = "";
                            }
                            else//验证码输入正确
                            {
                                DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabTeachers where UserID = '" + txtUserID.Text + "'");

                                if (dt.Rows.Count == 1)//账户存在
                                {
                                    if (dt.Rows[0]["Time"] == DBNull.Value || dt.Rows[0]["Count"] == DBNull.Value)
                                    {
                                        dt.Rows[0]["Time"] = "1997-04-12 00:00:00";
                                        dt.Rows[0]["Count"] = 5;
                                    }
                                    DateTime LastTime = Convert.ToDateTime(dt.Rows[0]["Time"]);
                                    DateTime CurrentTime = Convert.ToDateTime(System.DateTime.Now.ToString());
                                    TimeSpan ShiJian = CurrentTime - LastTime;

                                    if (Convert.ToInt32(ShiJian.TotalMinutes) > 15)//大于15分钟
                                    {
                                        AddSQLStringToDAL.Update("update TabTeachers set Count = 5 where UserID = '" + txtUserID.Text + "'");//次数还原为5

                                        if (FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserPWD.Text, "MD5").ToString() == dt.Rows[0]["UserPWD"].ToString())//密码正确
                                        {
                                            AddSQLStringToDAL.Update("update TabTeachers set Count = 5,Time = '" + CurrentTime.ToString("yyyy-MM-dd HH:mm:ss") + "' where UserID = '" + txtUserID.Text + "'");
                                            CurrentWeek();
                                            lblMessage.Visible = true;
                                            lblMessage.Text = "登陆成功！";
                                            Loginaspx(dt);
                                        }
                                        else//密码错误
                                        {
                                            AddSQLStringToDAL.Update("update TabTeachers set Count = 4,Time = '" + CurrentTime.ToString("yyyy-MM-dd HH:mm:ss") + "' where UserID = '" + txtUserID.Text + "'");
                                            lblMessage.Visible = true;
                                            txtCode.Text = "";
                                            txtUserPWD.Text = "";
                                            lblMessage.Text = "密码错误！您还剩4次尝试机会";
                                        }
                                    }
                                    else//小于等于15分钟
                                    {
                                        if (Convert.ToInt32(dt.Rows[0]["Count"]) > 1)//次数大于0
                                        {
                                            if (dt.Rows[0]["UserPWD"].ToString() == FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserPWD.Text, "MD5").ToString())//密码正确
                                            {
                                                AddSQLStringToDAL.Update("update TabTeachers set Count = 5,Time = '" + CurrentTime.ToString("yyyy-MM-dd HH:mm:ss") + "' where UserID = '" + txtUserID.Text + "'");//次数还原为5，时间改为这次的
                                                CurrentWeek();
                                                lblMessage.Visible = true;
                                                lblMessage.Text = "登陆成功！";
                                                Loginaspx(dt);
                                            }
                                            else//密码不正确
                                            {
                                                AddSQLStringToDAL.Update("update Tabteachers set Count = '" + (Convert.ToInt32(dt.Rows[0]["Count"]) - 1).ToString() + "',Time = '" + CurrentTime.ToString("yyyy-MM-dd HH:mm:ss") + "' where UserID = '" + txtUserID.Text + "'");//次数-1，时间改为这次的
                                                lblMessage.Visible = true;
                                                txtUserPWD.Text = "";
                                                txtCode.Text = "";
                                                lblMessage.Text = "密码错误！您还有" + (Convert.ToInt32(dt.Rows[0]["Count"]) - 1).ToString() + "次尝试机会";
                                            }
                                        }
                                        else//次数小于等于0
                                        {
                                            lblMessage.Visible = true;
                                            lblMessage.Text = "请" + (15 - Convert.ToInt32(ShiJian.TotalMinutes)) + "分钟后重试";
                                        }
                                    }
                                }
                                else//账户不存在
                                {
                                    lblMessage.Visible = true;
                                    lblMessage.Text = "账户不存在！";
                                    txtCode.Text = "";
                                    txtUserID.Text = "";
                                    txtUserPWD.Text = "";
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void Loginaspx(DataTable dt)//判断权限
    {
        if (dt.Rows.Count == 1)
        {
            Session["UserName"] = dt.Rows[0]["UserName"].ToString();
            Session["UserID"] = txtUserID.Text.Trim();
            string Role = dt.Rows[0]["Role"].ToString();
            switch (Role)
            {
                case "1":
                    Session["Role"] = "系统管理员";
                    Response.Redirect("Admin\\GetMessage.aspx");
                    break;
                case "2":
                    Session["Role"] = "院系领导";
                    Response.Redirect("Leader\\GetMessage.aspx");
                    break;
                case "3":
                    Session["Role"] = "辅导员";
                    Session["Department"] = dt.Rows[0]["Department"].ToString();
                    Response.Redirect("Secretary\\GetMessage.aspx");
                    break;
                case "4":
                    Session["Role"] = "教师";
                    Response.Redirect("Teacher\\GetMessage.aspx");
                    break;
                default:
                    break;
            }
        }
    }

    public void CurrentWeek()
    {
        Session["CurrentWeek"] = "0";
        try
        {
            DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from [校历] ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dt.Rows[i]["StartWeek"]) < DateTime.Now && Convert.ToDateTime(dt.Rows[i]["EndWeek"]) > DateTime.Now)
                {
                    string strWeekNumber = dt.Rows[i]["WeekNumber"].ToString();
                    if (strWeekNumber.Length == 1)
                    {
                        strWeekNumber = "0" + strWeekNumber;
                    }
                    Session["CurrentWeek"] = strWeekNumber;
                    break;
                }
                else
                {
                    Session["CurrentWeek"] = "0";//不满足所有周次
                }
            }
        }
        catch
        {
            Session["CurrentWeek"] = "0";
        }
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Secretary_SecretaryMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "当前在线" + Application["online"].ToString() + "人";
        toplbl2.Text = "[" + Session["UserName"].ToString() + "]";
        toplbl3.Text = "[" + Session["Role"].ToString() + "]";
        lblcount.Text = "[第" + Session["CurrentWeek"].ToString() + "周]";
        topbtn1.Text = "修改密码";
        topbtn2.Text = "退出";

        if (!Directory.Exists(Server.MapPath("../") + "\\loginimages\\头像\\" + Session["UserID"].ToString()))//不存在文件夹
        {
            Directory.CreateDirectory(Server.MapPath("../") + "\\loginimages\\头像\\" + Session["UserID"].ToString());//创建该文件夹
        }
        else//存在文件夹
        {

        }


        if (Directory.GetFiles(Server.MapPath("../") + "\\loginimages\\头像\\" + Session["UserID"].ToString()).Length == 0)//该文件夹为空
        {
            //不复制了，直接用默认的
            //File.Copy(Server.MapPath(".") + "\\loginimages\\头像\\默认.png", Server.MapPath(".") + "\\loginimages\\头像\\" + Session["UserID"] + "\\默认.png");//将默认的图片复制到工号文件夹下
            ImageButton1.ImageUrl = "~\\loginimages\\头像\\默认.png";
        }
        else//文件夹有文件,不是默认的，我没有将默认的复制到这里
        {
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("../") + "\\loginimages\\头像\\" + Session["UserID"]);
            FileInfo[] fis = di.GetFiles();//所有文件的名字
            int a = fis.Length;
            string[] FileName = new string[a];//只能这么定义数组，不然会出错
            string[] KuoZhanMing = new string[a];

            for (int i = 0; i < fis.Length; i++)
            {
                FileName[i] = fis[i].Name.Substring(0, fis[i].Name.IndexOf("."));//截取扩展名之前的时间格式的数字
                KuoZhanMing[i] = fis[i].Name.Substring(fis[i].Name.IndexOf(".") + 1);//获取扩展名,不带"."
            }
            long max = Convert.ToInt64(FileName[0]);
            string finalKuoZhanMing = KuoZhanMing[0]; ;
            for (int i = 0; i < FileName.Length; i++)//获取最晚修改的头像
            {
                if (Convert.ToInt64(FileName[i]) > max)
                {
                    max = Convert.ToInt64(FileName[i]);
                    finalKuoZhanMing = KuoZhanMing[i];
                }
            }
            string finalFileName = max.ToString() + "." + finalKuoZhanMing;//最终的带扩展名的文件名
            ImageButton1.ImageUrl = "~\\loginimages\\头像\\" + Session["UserID"].ToString() + "\\" + finalFileName;
        }
        Session["imageURL"] = ImageButton1.ImageUrl;
    }
    protected void topbtn1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~\\Secretary\\修改密码.aspx");  
    }
    protected void topbtn2_Click1(object sender, EventArgs e)
    {
        Session["Role"] = null;
        Session["UserID"] = null;
        Session["UserName"] = null;
        Session["UserPWD"] = null;
        Session["Code"] = null;
        Response.Redirect("~\\登录.aspx");
    }
    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~\\Secretary\\个人资料.aspx"); 
    }
}

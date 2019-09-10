using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


public partial class Secretary_个人资料 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() != "")
            {
                Label1.Text = Session["UserID"].ToString();
                label2.Text = Session["UserName"].ToString();
                Label3.Text = Session["Role"].ToString();
                /*Label4.Text = "男";
                Label5.Text = "商务外语系";*/
                btnImage.ImageUrl = Session["imageURL"].ToString();
                FileUpload1.Visible = true;
                btnOK.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                Response.Redirect("~\\登录.aspx");
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        FileUpload1.Visible = true;
        btnOK.Visible = true;
        btnCancel.Visible = true;
        lblMessage.Visible = false;
    }

    private string UpLoad()//先上传到服务器指定的文件夹下
    {
        HttpPostedFile file = FileUpload1.PostedFile;
        string fileName = file.FileName;
        DateTime CurrentTime = Convert.ToDateTime(System.DateTime.Now.ToString());
        string tempPath = Server.MapPath("../") + "loginimages\\头像\\" + Session["UserID"] + "\\";//放在头像文件夹下
        fileName = CurrentTime.ToString("yyyyMMddHHmmss") + "." + FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf(".") + 1);//不带路径的文件名
        string FinalPathAndName = tempPath + fileName;//最终的路径加名字
        file.SaveAs(FinalPathAndName);
        return fileName;//返回最终的路径加文件名     
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Image();
    }

    private void Image()//确定上传的头像
    {
        if (FileUpload1.HasFile == true)//有文件
        {
            string KuoZhanName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf(".") + 1);//扩展名
            if (KuoZhanName == "jpg" || KuoZhanName == "icon" || KuoZhanName == "png" || KuoZhanName == "jpeg")
            {
                string filename = UpLoad();//最终的文件名
                btnImage.ImageUrl = "~//loginimages//头像//" + Session["UserID"] + "//" + filename;
                ImageButton IB = Master.FindControl("ImageButton1") as ImageButton;
                IB.ImageUrl = "~//loginimages//头像//" + Session["UserID"] + "//" + filename;
                FileUpload1.Visible = false;
                btnOK.Visible = false;
                btnCancel.Visible = false;
                lblMessage.Visible = false;
            }
            else//图片格式不对
            {
                lblMessage.Visible = true;
                lblMessage.Text = "图片格式不对！";
            }
        }
        else//没有文件
        {
            lblMessage.Visible = true;
            lblMessage.Text = "没有文件！";
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using BLL;
using System.Text;
using DAL;//为了方便  直接调用的DAL层  因为还要ddl的name，有些麻烦
using System.Security.Cryptography;

public partial class Admin_导入数据 : System.Web.UI.Page
{
    string KuoZhanName="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"].ToString() == "系统管理员")
            {
                label6.Visible = false;
                Clear();
                ddlDepartmentName.Items.Add("");
                ddlDepartmentName.Items.Add("教务处");
                ddlDepartmentName.Items.Add("会计系");
                ddlDepartmentName.Items.Add("机械工程系");
                ddlDepartmentName.Items.Add("基础教学部");
                ddlDepartmentName.Items.Add("建筑工程系");
                ddlDepartmentName.Items.Add("经济管理系");
                ddlDepartmentName.Items.Add("商务外语系");
                ddlDepartmentName.Items.Add("食品工程系");
                ddlDepartmentName.Items.Add("信息工程系");

                Button2.Attributes.Add("onClick","return confirm('本操作将覆盖原有数据，确定要执行这个操作吗？')");
                Button3.Attributes.Add("onClick", "return confirm('本操作将覆盖原有数据，确定要执行这个操作吗？')");
                Button4.Attributes.Add("onClick", "return confirm('本操作将清空所有数据，确定要执行这个操作吗？')");
            }
            else
            {
                Response.Redirect("~\\登录.aspx");
            }
        }    
    }
    protected void btnImportTeachers_Click(object sender, EventArgs e)//导入教师基本信息按钮
    {
        Clear();
        string identity = "";
        if (rdoOthers.Checked == false && rdoTeachers.Checked == false)//选择教师类型
        {
            lblMessage1.Text = "请先选择导入的数据是“本校教师”或者“外聘教师”";
        }
        else//选择完了教师类型
        {      
            if (rdoTeachers.Checked)//本校教师单选
            {
                identity = "TabTeachers";          
            }
            else//外聘教师单选
            {
                identity = "TabOtherTeachers";
            }

            if (fileexcel.FileName != string.Empty)
            {
                string AllFileName = UpLoad("Teachers\\", fileexcel);
                lblMessage1.Text = ExcelToDatabase.CheckFile(AllFileName, identity);//获取完整路径
            }
            else
            {
                lblMessage1.Text = "导入的Excel文件不能为空！";
            }
        }
    }
    protected void btnImportCourse_Click(object sender, EventArgs e)//导入原始授课信息  导入到  原始表
    {
        Clear();
        string department = "";//和表名称是一致的
        department = ddlDepartmentName.SelectedItem.ToString();
        if (department == "" || department == null)
        {
            lblMessage2.Text = "部门不能为空！";
        }
        else
        {
            if (filecourse.HasFile == true)//有文件，即大小不为0
            {
                this.KuoZhanName = filecourse.PostedFile.FileName.Substring(filecourse.PostedFile.FileName.LastIndexOf(".") + 1);//扩展名
                if (KuoZhanName == "xls" || KuoZhanName == "xlsx")
                {
                    string FinalPathAndName = UpLoad("TeacherInfo\\", filecourse);//传到服务器上
                    
                    DataTable dt = InsertToSQLServer(FinalPathAndName,department);//获得Excel表，放在dt里，下一步进行
                    //DBHelper.SQLBulkCopy(dt, "原始表");//参数获得表然后用BulkCopy不做任何字符处理直接导入到数据库的  原始表  中
                    

                    //拆分 任课教师  拆为 教师工号+教师姓名
                    //是 " ",不是NULL
                    DataTable dt1 = InsertToSQLServer(FinalPathAndName, department);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        sb.Append(dt1.Rows[i]["任课教师"].ToString());//所有的 任课教师 列的拼接 [001]张三[002]李四
                    }
                    string[] ChaiFen = sb.ToString().Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);//拆分完了，放在了ChaiFen数组里  偶数工号，奇数姓名
                    StringBuilder sb1 = new StringBuilder();
                    for (int i = 0; i < dt1.Rows.Count; i++)//因为偶数（包括0）是ID，奇数是Name，所以可以用 2*i  和  2*i+1  表示
                    {
                        sb1.Append("insert into [TabAllCourses] values('" + dt1.Rows[i][0].ToString() + "','" + ChaiFen[2 * i].ToString() + "','" + ChaiFen[2 * i + 1].ToString() + "','" + dt1.Rows[i][2].ToString() + "','" + dt1.Rows[i][3].ToString() + "','" + dt1.Rows[i][4].ToString() + "','" + dt1.Rows[i][5].ToString() + "','" + dt1.Rows[i][6].ToString() + "','" + dt1.Rows[i][7].ToString() + "','" + dt1.Rows[i][8].ToString() + "','" + dt1.Rows[i][9].ToString() + "','" + dt1.Rows[i][10].ToString() + "','" + dt1.Rows[i][11].ToString() + "','" + dt1.Rows[i][12].ToString() + "','" + dt1.Rows[i][13].ToString() + "','" + dt1.Rows[i][14].ToString() + "');");
                    }
                    ConnHelper.ZSGC(sb1.ToString());//连接数据库导入
                    lblMessage2.Text = "导入SQLServer数据库成功!";
            }
                else
                {
                    lblMessage2.Text = "只能上传Excel文件！";
                }
            }
            else
            {
                lblMessage2.Text = "请先选择Excel文件！";
            }
        }
    }
    protected void ddlDepartmentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
    }
    protected void rdo_CheckedChanged(object sender,EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        lblMessage1.Text = "";
        lblMessage2.Text = "";
        lblMessage3.Text = "";
        lblMessage4.Text = "";
        lblMessage5.Text = "";
        lblMessage7.Text = "";
    }

    private string UpLoad(string file_path,FileUpload fileUploadName)//先上传到服务器指定的文件夹下
    {
        HttpPostedFile file = fileUploadName.PostedFile;
        string fileName = file.FileName;
        //string tempPath = Server.MapPath(".")+"\\Teachers\\"+file_path+"\\";  //放到文件夹里
        string tempPath = Server.MapPath(".")+"\\"+ file_path +"\\";
        fileName = System.IO.Path.GetFileName(fileName);//不带路径的文件名
        string FinalPathAndName = tempPath + fileName;//最终的路径加名字
        file.SaveAs(FinalPathAndName);
        return FinalPathAndName;//返回最终的路径加文件名     
    }

    private DataTable InsertToSQLServer(string PathAndName,string BiaoMing)//文件路径+名称   Excel表的Sheet
    {
        string strConn = "provider=microsoft.ace.oledb.12.0;data source=" + PathAndName + ";extended properties='excel 8.0;HDR=YES;IMEX=1;';";
        OleDbConnection conn = new OleDbConnection(strConn);
        conn.Open();

        string strSQL = "select * from ["+BiaoMing+"$]";
        OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);//表在dt里了
        conn.Close();

        return dt;
    }

    private void InsertTeacherStatus()
    {
        Clear();
        List<string> str = new List<string>();
        str = AddSQLStringToDAL.GetDistinctStrings  ("TabAllCourses", "TeacherID");
        lblMessage3.Text = "第一步：教师信息对比完成！正在进行第二步...";
        InsertCoursesSimpleMap(str);
        lblMessage3.Text = "所有信息核对无误！请对数据进行处理";
    }

    private void InsertCoursesSimpleMap(List<string> strDistinctTeacherID)
    {
        for (int i = 0; i < strDistinctTeacherID.Count; i++)
        {
            List<string> strDD = new List<string>();
            strDD = AddSQLStringToDAL.GetDistinctStrings("TabAllCourses","TimeAndArea","TeacherID",strDistinctTeacherID[i].ToString());//获取TimeAndArea 
            for (int k = 0; k < strDD.Count; k++)
            {
                List<string> strResult = new List<string>();
                strResult = SplitString.GetSplitCountAndDetails(strDD[k]);
                DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabAllCourses where TeacherID = '"+strDistinctTeacherID[i].ToString()+"' and TimeAndArea = '"+strDD[k].ToString()+"'");
                for (int j = 0; j < (strResult.Count / 4); j++)
                {
                    string WeekRange = SplitString.GetWithoutWeek(strResult[j * 4 + 0].ToString());//如果想用空格隔开，只是在最后加一个空格（以，作为例子试的）
                    string Week = strResult[j * 4 + 1].ToString();
                    string Time = strResult[j * 4 + 2].ToString();
                    string Area = strResult[j * 4 + 3].ToString();
                    string Course = dt.Rows[0]["Course"].ToString().Trim();
                    if (AddSQLStringToDAL.InsertTabTeachers("TabTeacherCourseSimpleMap", strDistinctTeacherID[i].ToString(), dt.Rows[0]["TeacherName"].ToString(), Course, WeekRange, Week, Time, strDD[k].ToString(), dt.Rows[0]["Class"].ToString(), dt.Rows.Count.ToString(), dt.Rows[0]["TeacherDepartment"].ToString(), dt.Rows[0]["StudentDepartment"].ToString(), Area))
                    {
                        
                    }
                }
                dt.Clear();
            }
        }
    }

    private void GetTeacherCourseSimpleMap()
    {
        DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabTeacherCourseSimpleMap");
        foreach (DataRow dr in dt.Rows)
        {
            string[] strT = dr["WeekRange"].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strT.Length; i++)
            {
                string WeekNumber = "";
                string TeacherDepartment = dr["TeacherDepartment"].ToString();
                string TeacherID = dr["TeacherID"].ToString();
                string TeacherName = dr["TeacherName"].ToString();
                string Week = dr["Week"].ToString();
                switch (Week)
                {
                    case "星期一":
                        WeekNumber = "1";
                        break;
                    case "星期二":
                        WeekNumber = "2";
                        break;
                    case "星期三":
                        WeekNumber = "3";
                        break;
                    case "星期四":
                        WeekNumber = "4";
                        break;
                    case "星期五":
                        WeekNumber = "5";
                        break;
                    case "星期六":
                        WeekNumber = "6";
                        break;
                    default:
                        WeekNumber = "7";
                        break;
                }
                string Time = dr["Time"].ToString();
                string Course = dr["Course"].ToString();
                string Area = dr["Area"].ToString();
                if (strT[i].Length == 1)
                {
                    strT[i] = "0" + strT[i];
                }
                if (AddSQLStringToDAL.InsertTabTeachers("TabTeacherAttendance", WeekNumber, TeacherDepartment, TeacherID, TeacherName, strT[i].ToString(), Week, Time, Course, Area, "未考勤", "", dr["WithoutWeek"].ToString(), "", ""))
                {

                }
            }
            lblMessage7.Text = "数据处理完毕！";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)//分析入库信息，给密码加密
    {
        DataTable dt = AddSQLStringToDAL.GetDataTableBysql("select * from TabAllTeachers");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["UserID"].ToString() == dt.Rows[0]["UserPWD"].ToString())
            {
                InitialPWD();
            }
        }
        InsertTeacherStatus();
    }
    private void InitialPWD()//密码加密
    {
        List<string> str = new List<string>();
        str = AddSQLStringToDAL.GetDistinctStrings("TabAllTeachers","UserID");
        for (int i = 0; i < str.Count; i++)
        {
            if (AddSQLStringToDAL.UpdateTabTeachers("TabAllTeachers", FormsAuthentication.HashPasswordForStoringInConfigFile(str[i].ToString(), "MD5").ToString(), str[i].ToString()))
            {
                lblMessage3.Text = "正在初始化密码...";
            }
        }

        
        List<string> str1 = new List<string>();
        str1 = AddSQLStringToDAL.GetDistinctStrings("TabAllTeachers", "UserID");
        for (int i = 0; i < str1.Count; i++)
        {
            if (AddSQLStringToDAL.UpdateTabTeachers("TabTeachers", FormsAuthentication.HashPasswordForStoringInConfigFile(str[i].ToString(), "MD5").ToString(), str[i].ToString()))
            {
                lblMessage3.Text = "正在初始化密码...";
            }
        }
        List<string> str2 = new List<string>();
        str2 = AddSQLStringToDAL.GetDistinctStrings("TabAllTeachers", "UserID");
        for (int i = 0; i < str2.Count; i++)
        {
            if (AddSQLStringToDAL.UpdateTabTeachers("TabOtherTeachers", PWDProcess.MD5Encrypt(str2[i].ToString(), PWDProcess.CreateKey(str2[i].ToString())), str2[i].ToString()))
            {
                lblMessage3.Text = "正在初始化密码...";
            }
        }   
    }
    protected void Button3_Click(object sender, EventArgs e)//处理入库数据,TabTeacherAttendance
    {
        Clear();
        GetTeacherCourseSimpleMap();
    }
    protected void Button4_Click(object sender, EventArgs e)//清空入库数据
    {
        Clear();
        if (AddSQLStringToDAL.DeleteTabTeachers("TabTeacherStatus") && AddSQLStringToDAL.DeleteTabTeachers("TabTeacherCourseSimpleMap") && AddSQLStringToDAL.DeleteTabTeachers("TabTeacherAttendance") && AddSQLStringToDAL.DeleteTabTeachers("TabStudentAttendance") && AddSQLStringToDAL.DeleteTabTeachers("TabStudentHomework") && AddSQLStringToDAL.DeleteTabTeachers("校历"))
        {
            lblMessage4.Text="异常数据清空完毕！请对数据进行分析和处理！";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//校历导入
    {
        if (FileUpload1.HasFile == true)
        {
            this.KuoZhanName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf(".") + 1);//扩展名
            if (KuoZhanName == "xls" || KuoZhanName == "xlsx")
            {
                string Final = UpLoad("校历", FileUpload1);//上传到服务器的校历文件夹下
                DataTable dt = InsertToSQLServer(Final, "Sheet1");//最终的名字+Sheet
                DBHelper.SQLBulkCopyXiaoLi(dt, "校历");//插入到SQLServer表里
                lblMessage5.Text = "导入校历成功！";
                lblMessage5.Visible = true;
            }
            else
            {
                lblMessage5.Text = "请选择正确的Excel文件！";
                lblMessage5.Visible = true;
            }
        }
        else
        {
            lblMessage5.Text = "请先选择Excel文件！";
            lblMessage5.Visible = true;
        }
    }
    protected void Button5_Click(object sender, EventArgs e)//导入系部人数按钮
    {
        if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "" && TextBox4.Text != "" && TextBox5.Text != "" && TextBox6.Text != "" && TextBox7.Text != "")
        {
            string[] str = { "会计系", "信息工程系", "经济管理系", "食品工程系", "机械工程系", "商务外语系", "建筑工程系"};
            int[] sum = new int[str.Length];
            sum[0] = Convert.ToInt32(TextBox1.Text.Trim());
            sum[1] = Convert.ToInt32(TextBox2.Text.Trim());
            sum[2] = Convert.ToInt32(TextBox3.Text.Trim());
            sum[3] = Convert.ToInt32(TextBox4.Text.Trim());
            sum[4] = Convert.ToInt32(TextBox5.Text.Trim());
            sum[5] = Convert.ToInt32(TextBox6.Text.Trim());
            sum[6] = Convert.ToInt32(TextBox7.Text.Trim());

            if (AddSQLStringToDAL.DeleteTabTeachers("TabDepartmentSum"))
            {
 
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (AddSQLStringToDAL.InsertTabTeachers("TabDepartmentSum", str[i], sum[i].ToString()))
                {
                    label6.Visible = true;
                    label6.Text = "各系人数设置完毕！";
                }
            }
        }
        else
        {
            label6.Visible = true;
            label6.Text = "部分系部人数未设置，请全部设置！";
        }
    }
}
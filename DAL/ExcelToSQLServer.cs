using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;

namespace DAL
{
   public class ExcelToSQLServer
    {
       public static DataSet ds;
       private static bool CheckExcelTableTeachers()
       {
           try
           {
               string[] str = { "部门","工号","密码","姓名","性别","权限"};
               for (int i = 0; i <= 5; i++)
               {
                   if (ds.Tables["ExcelInfo"].Columns[i].ColumnName.ToString() != str[i])
                   {
                       return false;
                   }
               }
               return true;
           }
           catch 
           {
               return false;
           }
       }

       private static bool CheckExcelTableCalendar()
       {
           try
           {
               string[] str = { "周次","起","止"};
               for (int i = 0; i <= 2; i++)
               {
                   if (ds.Tables["ExcelInfo"].Columns[i].ColumnName.ToString() != str[i])
                   {
                       return false;
                   }
               }
               return true;
           }
           catch
           {
               return false;
           }
       }

       private static bool CheckExcelTableCourses()
       {
           try
           {
               string[] str = { "承担单位","任课教师","上课时间/地点","课程","所属部门"};
               for (int i = 0; i <= 4; i++)
               {
                   if (ds.Tables["ExcelInfo"].Columns[i].ColumnName.ToString() != str[i])
                   {
                       return false;
                   }
               }
               return true;
           }
           catch
           {
               return false;
           }
       }

       private static void ExcelToDatabaseByDataReader(string fileName, string strSQL)
       {
           System.GC.Collect();
           //string oleStr1 = @"Provider = Microsoft.Jet.OLEDB.4.0;Data Source = '"+fileName+"';Extended Properties=Excel 8.0";
           string oleStr1 = @"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = '" + fileName + "';Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
           OleDbConnection oleConn = new OleDbConnection(oleStr1);
           oleConn.Open();

           OleDbCommand oleCmd = new OleDbCommand(strSQL, oleConn);
           OleDbDataReader oleDr = oleCmd.ExecuteReader();

           string sqlStr1 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           SqlConnection sqlConn = new SqlConnection(sqlStr1);
           sqlConn.Open();

           SqlCommand sqlCmd = new SqlCommand();
           sqlCmd.Connection = sqlConn;
           StringBuilder strBder = new StringBuilder();
           List<string>str=new List<string> ();
           while (oleDr.Read())
           {
               str = SplitTecherIDAndTeacherName(oleDr[1].ToString());
               strBder.Append("insert into TabAllCourses(TeacherDepartment,TeacherID,TeacherName,TimeAndArea)");//还没写完 P73
               strBder.Append("'"+oleDr[0].ToString()+"','"+str[0]+"','"+str[1]+"'");
               for(int j = 2;j<=14;j++)
               {
                   strBder.Append(",'"+oleDr[j].ToString()+"'");
               }
               strBder.Append(")");
               sqlCmd.CommandText = strBder.ToString();
               sqlCmd.ExecuteNonQuery();
               strBder.Remove(0, strBder.Length);
           }

           sqlConn.Close();
           oleDr.Close();
           oleConn.Close();
       }

       private static List<string> GetSheetName(string fileName)
       {
           //string str1 = @"Provider = Microsoft.Jet.OLEDB.4.0;Data Source = '"+fileName+"';Extended Properties=Excel 8.0";
           string str1 = @"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = '" + fileName + "';Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
           OleDbConnection conn = new OleDbConnection(str1);
           conn.Open();

           List<string> SheetNameList = new List<string>();
           DataTable dtExcelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
           string SheetName = "";
           for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
           {
               SheetName = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
               SheetNameList.Add(SheetName);
           }
           conn.Close();
           conn.Dispose();
           return SheetNameList;
       }

       public static void ReadExcelToDataSet(string fileName, string strSQL)
       {
           string str1 = @"Provider = Microsoft.Jet.OLEDB.4.0;Data Source = '"+fileName+"';Extended Properties=Excel 8.0";
           OleDbConnection conn = new OleDbConnection(str1);
           conn.Open();

           OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
           da.SelectCommand.CommandTimeout = 600;
           ds = new DataSet();
           da.Fill(ds,"ExcelInfo");
           conn.Close();
           conn.Dispose();
       }

       public static string ReadCoursesExcel(string fileName, string identity)
       {
           List<string> SheetName = new List<string>();
           SheetName = GetSheetName(fileName);
           string strSQL = "";
           if (SheetName[0] != identity + "$")
           {
               return "指定的Excel文件的工作表名不为"+identity+",当前的表名为"+SheetName[0]+"";
           }
           strSQL = "select * from ["+SheetName[0]+"]";
           ReadExcelToDataSet(fileName, strSQL);
           if (CheckExcelTableCourses())
           {
               CoursesToSQLServer(identity);
               return "文件导入成功";
           }
           else
           {
               return "选择的Excel文件中的内容与数据库要求不匹配，请确认!";
           }
       }

       public static string ReadCalendarExcel(string fileName, string identity)
       {
           List<string> SheetName = new List<string>();
           SheetName = GetSheetName(fileName);
           string strSQL = "";

           if (SheetName[0] != "Sheet1$")
           {
               return "指定的Excel文件的工作表名不为Sheet1，当前的表名为"+SheetName[0]+"";
           }

           strSQL = "select * from [Sheet1$]";
           ReadExcelToDataSet(fileName, strSQL);

           if (CheckExcelTableCalendar())
           {
               CalendarToSQLServer(identity);
               return "文件导入成功";
           }
           else
           {
               return "选择的Excel文件中的内容与数据库要求不匹配，请确认！";
           }
       }

       public static string ReadTeachersExcel(string fileName, string identity)
       {
           List<string> SheetName = new List<string>();
           SheetName = GetSheetName(fileName); //簿名
           string strSQL = "";

           if (SheetName[0] != "Sheet1$")
           {
               return "指定的Excel文件的工作表名不为Sheet1，当前的表名为"+SheetName[0]+"";
           }

           strSQL = "select * from [Sheet1$]";
           ReadExcelToDataSet(fileName, strSQL);

           if (CheckExcelTableTeachers())
           {
               TeachersToSQLServer(identity);
               return "文件导入成功";
           }
           else
           {
               return "选择的Excel文件中的内容与数据库要求不匹配，请确认！";
           }
       }

       public static void CoursesToSQLServer(string identity)
       {
           string str1=ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           SqlConnection conn=new SqlConnection (str1);
           conn.Open();

           SqlCommand cmd=new SqlCommand ();
           cmd.Connection=conn;
           StringBuilder strBder=new StringBuilder ();
           List<string>str=new List<string>();
           for(int i =0;i<ds.Tables["ExcelInfo"].Rows.Count;i++)
           {
               strBder.Append("'"+ds.Tables["ExcelInfo"].Rows[i].ItemArray[0].ToString()+"','"+str[0]+"','"+str[1]+"'");
               for(int j=2;j<=14;j++)
               {
                   strBder.Append(",'"+ds.Tables["ExcelInfo"].Rows[i].ItemArray[j].ToString()+"'");
               }
               strBder.Append(")");
               string str2=strBder.ToString();
               str2=string.Empty;
               cmd.ExecuteNonQuery();
               strBder.Remove(0,strBder.Length);
               System.GC.Collect();
           }
       }

       private static List<string> SplitTecherIDAndTeacherName(string str)
       {
           List<string> strSplit = new List<string>();
           string[] newStr = str.Split(new char[]{'[',']'},StringSplitOptions.RemoveEmptyEntries);
           for (int i = 0; i < newStr.Length; i++)
           {
               strSplit.Add(newStr[i]);
           }
           return strSplit;
       }

       public static void CalendarToSQLServer(string identity)
       {
           string str1=ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           SqlConnection conn=new SqlConnection (str1);
           conn.Open();

           SqlCommand cmd=new SqlCommand ();
           cmd.Connection=conn;
           StringBuilder strBder=new StringBuilder ();
           for (int i = 0; i < ds.Tables["ExcelInfo"].Rows.Count; i++)
           {
               strBder.Append("insert into " + identity + "(WeekNumber,StartWeek,EndWeek)values(");
               for (int j = 0; j <= 1; j++)
               {
                   strBder.Append("'" + ds.Tables["ExcelInfo"].Rows[i].ItemArray[j].ToString() + "',");
               }
               strBder.Append("'"+ds.Tables["ExcelInfo"].Rows[i].ItemArray[2]+"')");
               string str2 = strBder.ToString();
               cmd.CommandText = str2;
               cmd.ExecuteNonQuery();
               strBder.Remove(0, strBder.Length);
           }

           conn.Close();
           conn.Dispose();
       }

       public static void TeachersToSQLServer(string identity)//在这里写只导入不存在用户
       {
           string str1 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           SqlConnection conn = new SqlConnection(str1);
           conn.Open();

           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           StringBuilder strBder = new StringBuilder();
           for (int i = 0; i < ds.Tables["ExcelInfo"].Rows.Count; i++)
           {
               if (!IsHas(ds.Tables["ExcelInfo"].Rows[i]["工号"].ToString(),identity))//如果原来的表没有该老师
               {
                   strBder.Append("insert into " + identity + "(Type,Department,UserID,UserPWD,UserName,Sex,Role)values(");
                   if (identity == "TabTeachers")
                   {
                       strBder.Append("'本校教师',");
                   }
                   else
                   {
                       strBder.Append("'外聘教师',");
                   }
                   for (int j = 0; j <= 4; j++)
                   {
                       strBder.Append("'" + ds.Tables["ExcelInfo"].Rows[i].ItemArray[j].ToString() + "',");
                   }
                   strBder.Append("'" + ds.Tables["ExcelInfo"].Rows[i].ItemArray[5] + "')");//Role是int型的
                   string str2 = strBder.ToString();
                   cmd.CommandText = str2;
                   cmd.ExecuteNonQuery();
                   strBder.Remove(0, strBder.Length);
                   conn.Close();
                   conn.Dispose();
               }
               else
               {
                  
               }
           }
       }

       public static bool IsHas(string UserID,string identity)//判断原表有没有该老师，有的话就是True，没有是False
       {
           DataTable dt = ConnHelper.GetDataTable("select UserID from "+identity+"");
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               if (UserID == dt.Rows[i]["UserID"].ToString())
               {
                   return true;
               }
           }
           return false;
       }
    }
}

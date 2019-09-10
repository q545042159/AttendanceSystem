using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace DAL
{
   public class ConnHelper
    {

       public static List<string> GetDistinceColoum(string strSQL, string str1)
       {
           DataTable dt = GetDataTable(strSQL);
           List<string> strList = new List<string>();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               string str = dt.Rows[i][0].ToString();
               strList.Add(str);
           }
               return strList;
       }

       public static DataTable GetDistinceColoum(string strSQL)  //返回不同行
       {
           DataTable dt = GetDataTable(strSQL);
           return dt;
       }

       public static DataSet GetDataSet(string strSQL)
       {
           string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           //要想引出ConfigurationManager类，要在DAL层中添加引用 using System.Configuration;
           //这句意思是  从web.config配置文件中获取数据库连接字符串
           SqlConnection conn = new SqlConnection(ConnectionString);//实例化SqlConnection对象
           conn.Open();
           SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
           DataSet ds = new DataSet();
           da.Fill(ds);
           conn.Close();
           return ds;
       }

       public static int GetRecordCount(string strSQL)
       {
           string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           SqlConnection conn = new SqlConnection(ConnectionString);
           conn.Open();
           SqlCommand cmd = new SqlCommand(strSQL, conn);
           string count = cmd.ExecuteScalar().ToString().Trim();
           if (count == "")
           {
               count = "0";
           }
           conn.Close();
           return Convert.ToInt32(count);
       }

       public static bool ExecuteNoneQueryOperation(string strSQL)
       {
           string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           SqlConnection conn = new SqlConnection(ConnectionString);
           conn.Open();
           SqlCommand cmd = new SqlCommand(strSQL, conn);
           try               //try catch:处理异常
           {
               cmd.ExecuteNonQuery();
           }
           catch
           {
               int i = 0;
               i++;
           }
           conn.Close();
           return true;
       }

       public static DataTable GetDataTable(string strSQL)
       {
           DataSet ds = GetDataSet(strSQL);
           ds.CaseSensitive = false;//不区分大小写   如果是true就是区分大小写
           return ds.Tables[0];
       }

       public static void ZSGC(string strsql)
       {
           string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
           SqlConnection conn = new SqlConnection(ConnectionString);
           conn.Open();

           SqlCommand cmd = new SqlCommand(strsql, conn);
           cmd.ExecuteNonQuery();
           conn.Close();
       }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

namespace BLL
{
   public class AddSQLStringToDAL
    {
       public static DataTable GetDataTableBysql(string strsql)
       {
           return ConnHelper.GetDataTable(strsql);
       }

       public static DataTable GetDataTableBysql(string TabName, string str1, string str2)
       {
           return ConnHelper.GetDataTable("select * from "+TabName+" where "+str1+" = '"+str2+"' ");
       }

       public static DataTable GetDataTableBysql(string TabName, string str1, int str2)
       {
           return ConnHelper.GetDataTable("select * from " + TabName + " where " + str1 + " = " + str2 + " ");
       }

       public static DataTable GetDataTableBysql(string TabName, string str1, string str2,string str3,string str4)
       {
           return ConnHelper.GetDataTable("select * from " + TabName + " where " + str1 + " = '" + str2 + "' and " + str3 + " =  '"+str4+"'");
       }

       //获取原密码
       public static DataTable GetOldPWD(string UserID)
       {
           return ConnHelper.GetDataTable("select UserPWD from TabTeachers where UserID = '"+UserID+"'");
       }

       public static void Insert(string BiaoMing,string Type, string Department, string UserID, string UserName, string UserPWD, string Role)//新增教师，插入数据
       {
           ConnHelper.ZSGC("insert into "+BiaoMing+"(Type,Department,UserID,UserName,UserPWD,Role) values('"+Type.Trim()+"','"+Department.Trim()+"','"+UserID.Trim()+"','"+UserName.Trim()+"','"+UserPWD.Trim()+"','"+Role.Trim()+"')");
       }

       public static bool InsertTabTeachers(string TabName, string str1, string str2)
       {
           try
           {
               ConnHelper.ZSGC("insert into " + TabName + " values('" + str1 + "','" + str2 + "')");
               return true;
           }
           catch
           {
               return false;
           }
       }

       public static bool InsertTabPassword(string UserID, string UserPWD, string Time)
       {
           try
           {
               ConnHelper.ZSGC("insert into [TabPassword] values('"+UserID+"','"+UserPWD+"','"+Time+"')");
               return true;
           }
           catch
           {
               return false;
           }
       }

       public static bool InsertTabTeachers(string TabName, string str1, string str2,string str3,string str4,string str5,string str6)
       {
           try
           {
               ConnHelper.ZSGC("insert into " + TabName + " values('" + str1 + "','" + str2 + "','" + str3 + "','" + str4 + "','" + str5 + "','" + str6 + "')");
               return true;
           }
           catch
           {
               return false;
           }
       }

       public static bool InsertTabTeachers(string TabName, string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9, string str10, string str11, string str12)
       {
           try
           {
               ConnHelper.ZSGC("insert into " + TabName + " values('" + str1 + "','" + str2 + "','" + str3 + "','" + str4 + "','" + str5 + "','" + str6 + "','" + str7 + "','" + str8 + "','" + str9 + "','" + str10 + "','" + str11 + "','" + str12 + "')");
               return true;
           }
           catch
           {
               return false;
           }
       }

       public static bool InsertTabTeachers(string TabName, string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9, string str10, string str11, string str12,string str13,string str14)
       {
           try
           {
               ConnHelper.ZSGC("insert into " + TabName + " values('" + str1 + "','" + str2 + "','" + str3 + "','" + str4 + "','" + str5 + "','" + str6 + "','" + str7 + "','" + str8 + "','" + str9 + "','" + str10 + "','" + str11 + "','" + str12 + "','" + str13 + "','" + str14 + "')");
               return true;
           }
           catch
           {
               return false;
           }
       }

       public static DataTable GetDistinctColums(string BiaoMing,string LieMing)//获取不同行
       {
           DataTable dt=ConnHelper.GetDataTable("select distinct "+LieMing+" from "+BiaoMing+"");
           return dt;
       }

       public static List<string> GetDistinctStrings(string strTable, string str1)
       {
           string strSQL = "select distinct " + str1 + " from " + strTable + " ";
           return ConnHelper.GetDistinceColoum(strSQL, str1);
       }

       public static List<string> GetDistinctStrings(string strTable, string str1,string str2,string str3)
       {
           string strSQL = "select distinct " + str1 + " from " + strTable + " where "+str2+" = '"+str3+"'" ;
           return ConnHelper.GetDistinceColoum(strSQL, str1);
       }

       public static List<string> GetDistinctStrings(string strTable, string str1, string str2, int str3)
       {
           string strSQL = "select distinct " + str1 + " from " + strTable + " where " + str2 + " = "+str3+" ";
           return ConnHelper.GetDistinceColoum(strSQL, str1);
       }

       public static void Update(string BiaoMing,string WantUpdateLieMing,string WantValue,string LieMing,string value1)
       {
           ConnHelper.ZSGC("update "+BiaoMing+" set "+WantUpdateLieMing+" = '"+WantValue.Trim()+"' where "+LieMing+" = '"+value1.Trim()+"'");
       }

       public static void Update(string strsql)
       {
           ConnHelper.ZSGC(strsql);
       }

       public static void Delete(string UserID,string BiaoMing)
       {
           ConnHelper.ZSGC("delete * from "+BiaoMing+" where UserID = '"+UserID+"'");
       }

       public static bool UpdateTabTeachers(string TableName, string UserPWD, string UserID)
       {
           string strSQL = BuildSQLUpdateString(TableName, UserPWD, UserID);
           return ConnHelper.ExecuteNoneQueryOperation(strSQL);
       }

       public static bool UpdateMessage( string UserID)
       {
           ConnHelper.ZSGC("update [TabMessage] set [MessageStatus] = 'true' where UserID = '"+UserID+"'");
           return true;
       }

       public static bool UpdateTabTeachers(string TableName , string DepartmentValue , int RoleValue ,string UserIDValue)
       {
           ConnHelper.ZSGC("update "+TableName+" set Department = '"+DepartmentValue+"', Role = "+RoleValue+" where UserID = '"+UserIDValue+"'");
           return true;
       }

       public static bool UpdateTabTeachers(string TabName,string str1,string str2,string str3,string str4,string str5,string str6,string str7,string str8,string str9,string str10,string str11,string str12,string str13,string str14)
       {
           ConnHelper.ZSGC("update "+TabName+" set "+str1+" = '"+str2+"' , "+str3+" = '"+str4+"' where "+str5+" = '"+str6+"' and "+str7+" = '"+str8+"' and "+str9+" = '"+str10+"' and "+str11+" = '"+str12+"' and "+str13+" = '"+str14+"'");
           return true;
       }

       public static string BuildSQLUpdateString(string strTableName, string UserPWD, string UserID)
       {
           return "update "+strTableName+" set UserPWD = '"+UserPWD+"' where UserID = '"+UserID+"'";
       }

       public static bool DeleteTabTeachers(string TabName)
       {
           try
           {
               ConnHelper.ZSGC("delete from " + TabName + "");
               return true;
           }
           catch
           {
               return false;
           }
       }

       public static bool DeleteTabTeachers(string TabName, string UserID)
       {
           ConnHelper.ZSGC("delete from "+TabName+" where UserID = '"+UserID+"'");
           return true;
       }

       public static int GetRecordCount(string TabName, string str1, string str2)
       {
           int i = ConnHelper.GetRecordCount("select count(*) from "+TabName+" where "+str1+" = '"+str2+"'");
           return i;
       }
    }
}

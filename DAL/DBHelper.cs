using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
   public class DBHelper
    {
       public static void SQLBulkCopy(DataTable dt,string BiaoMing)//授课信息
       {
           using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
           {
               conn.Open();
               using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
               {
                   bulkcopy.DestinationTableName = BiaoMing;
                   bulkcopy.ColumnMappings.Add("承担单位", "承担单位");
                   bulkcopy.ColumnMappings.Add("任课教师", "任课教师");
                   bulkcopy.ColumnMappings.Add("上课时间/地点", "[上课时间/地点]");
                   bulkcopy.ColumnMappings.Add("课程", "课程");
                   bulkcopy.ColumnMappings.Add("所属部门", "所属部门");
                   bulkcopy.ColumnMappings.Add("学分", "学分");
                   bulkcopy.ColumnMappings.Add("总学时", "总学时");
                   bulkcopy.ColumnMappings.Add("上课班级名称", "上课班级名称");
                   bulkcopy.ColumnMappings.Add("院(系)/部", "[院(系)/部]");
                   bulkcopy.ColumnMappings.Add("学号", "学号");
                   bulkcopy.ColumnMappings.Add("姓名", "姓名");
                   bulkcopy.ColumnMappings.Add("行政班级", "行政班级");
                   bulkcopy.ColumnMappings.Add("性别", "性别");
                   bulkcopy.ColumnMappings.Add("课程类别1", "课程类别1");
                   bulkcopy.ColumnMappings.Add("课程类别2", "课程类别2");
                   bulkcopy.WriteToServer(dt);
               }
           }
       }

       public static void SQLBulkCopyAllCourses(DataTable dt)
       {
           using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
           {
               conn.Open();
               using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
               {
                   bulkcopy.DestinationTableName = "TabAllCourses";
                   bulkcopy.ColumnMappings.Add("承担单位", "承担单位");
                   bulkcopy.ColumnMappings.Add("上课时间/地点", "[上课时间/地点]");
                   //bulkcopy.ColumnMappings.Add("教师姓名", "教师姓名");
                   //bulkcopy.ColumnMappings.Add("教师工号", "教师工号");
                   bulkcopy.ColumnMappings.Add("课程", "课程");
                   bulkcopy.ColumnMappings.Add("所属部门", "所属部门");
                   bulkcopy.ColumnMappings.Add("学分", "学分");
                   bulkcopy.ColumnMappings.Add("总学时", "总学时");
                   bulkcopy.ColumnMappings.Add("上课班级名称", "上课班级名称");
                   bulkcopy.ColumnMappings.Add("院(系)/部", "[院(系)/部]");
                   bulkcopy.ColumnMappings.Add("学号", "学号");
                   bulkcopy.ColumnMappings.Add("姓名", "姓名");
                   bulkcopy.ColumnMappings.Add("行政班级", "行政班级");
                   bulkcopy.ColumnMappings.Add("性别", "性别");
                   bulkcopy.ColumnMappings.Add("课程类别1", "课程类别1");
                   bulkcopy.ColumnMappings.Add("课程类别2", "课程类别2");
                   bulkcopy.WriteToServer(dt);
               }
           }
       }

       public static void SQLBulkCopyXiaoLi(DataTable dt, string BiaoMing)
       {
           using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
           {
               conn.Open();
               using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
               {
                   bulkcopy.DestinationTableName = BiaoMing;
                   bulkcopy.ColumnMappings.Add("周次", "WeekNumber");
                   bulkcopy.ColumnMappings.Add("起", "StartWeek");
                   bulkcopy.ColumnMappings.Add("止", "EndWeek");
                   bulkcopy.WriteToServer(dt);
               }
           }
       }
    }
}

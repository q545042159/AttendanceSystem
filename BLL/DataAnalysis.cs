using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
   public class DataAnalysis
    {
       public static DataTable CreateDataTable()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add("周次");
           dt.Columns.Add("系部");
           dt.Columns.Add("请假人数",typeof(int));
           dt.Columns.Add("旷课人数", typeof(int));
           dt.Columns.Add("早退人数", typeof(int));
           dt.Columns.Add("迟到人数", typeof(int));
           dt.Columns.Add("合计", typeof(int));
           return dt;
       }

       public static DataTable CreateDataTableReplaceChart(string[] Department,string[] Count,string[] Attendance,string[] Late,string[] Early,string[] Leave,string[] Data)
       {
           DataTable dt = new DataTable();
           dt.Columns.Add("系部");                //0
           dt.Columns.Add("在校生人数");          //1
           dt.Columns.Add("旷课人次");            //2
           dt.Columns.Add("旷课率");              //3
           dt.Columns.Add("迟到人次");            //4
           dt.Columns.Add("迟到率");              //5
           dt.Columns.Add("早退人次");            //6
           dt.Columns.Add("早退率");              //7
           dt.Columns.Add("请假人次");            //8
           dt.Columns.Add("请假率");              //9
           dt.Columns.Add("总缺勤数");            //10
           dt.Columns.Add("总缺勤率");            //11


           for (int i = 0; i < Department.Length; i++)
           {
               int Sum = Convert.ToInt32(Count[i]);
               DataRow dr = dt.NewRow();
               dr[0] = Department[i];  //系部
               dr[1] = Count[i];  //在校生人数
               dr[2] = Attendance[i];    //旷课人数
               dr[3] = Convert.ToInt32(Attendance[i]) / (float)Sum;
               dr[4] = Late[i];     //迟到人数
               dr[5] = Convert.ToInt32(Late[i]) / (float)Sum;
               dr[6] = Early[i];   //早退人数
               dr[7] = Convert.ToInt32(Early[i]) / (float)Sum;
               dr[8] = Leave[i];    //请假人数
               dr[9] = Convert.ToInt32(Leave[i]) / (float)Sum;
               dr[10] = Data[i];     //总缺勤数
               dr[11] = Convert.ToInt32(Data[i]) / (float)Sum;
               dt.Rows.Add(dr);
           }

           return dt;
       }

       public static int GetEveryAttendanceNumber(string department,string str1,string str2)
       {
           DataTable dt = ConnHelper.GetDataTable("select * from TabStudentAttendance where [StudentDepartment] = '" + department + "' and [CurrentWeek] = '" + str1 + "' and [AttendanceType] = '" + str2 + "' ");
           int i = dt.Rows.Count;
           return i;
       }

       public static DataRow CreatDataRow(DataTable dt, string str1, string Department, int late, int early, int attendance, int leave)
       {
           DataRow dr = dt.NewRow();
           dr["周次"] = str1;
           dr["系部"] = Department;
           dr["迟到人数"] = late;
           dr["早退人数"] = early;
           dr["旷课人数"] = attendance;
           dr["请假人数"] = leave;
           dr["合计"] = late + early + attendance + leave;
           return dr;
       }

       public static DataRow InsertLastRow(DataTable dt)
       {
           int Late = 0;
           int Early = 0;
           int Attendance = 0;
           int Leave = 0;
           int HeJi = 0;
           for(int i =0;i<dt.Rows.Count;i++)
           {
               int late = Convert.ToInt32(dt.Rows[i]["迟到人数"].ToString());
               Late += late;
               int early = Convert.ToInt32(dt.Rows[i]["早退人数"].ToString());
               Early += early;
               int attendance = Convert.ToInt32(dt.Rows[i]["旷课人数"].ToString());
               Attendance += attendance;
               int leave = Convert.ToInt32(dt.Rows[i]["请假人数"].ToString());
               Leave += leave;
               int heji = Convert.ToInt32(dt.Rows[i]["合计"].ToString());
               HeJi += heji;
           }
           DataRow dr = dt.NewRow();
           dr["系部"] = dt.Rows[0]["系部"].ToString();
           dr["周次"] = "缺勤人数总计";
           dr["请假人数"] = Leave;
           dr["迟到人数"] = Late;
           dr["早退人数"] = Early;
           dr["旷课人数"] = Attendance;
           dr["合计"] = HeJi;

           return dr;
       }
    }
}

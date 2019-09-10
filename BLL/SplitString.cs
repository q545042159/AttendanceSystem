using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public class SplitString
    {
      public static List<string> GetSplitCountAndDetails(string str)
      {
          List<string> str1 = new List<string>();
          List<string> strResult = new List<string>();

          str1 = SplitTimeAndAreaStrinng(str);//以@拆分
          //strResult.Add(str1.Count.ToString());
          for (int i = 0; i < str1.Count; i++)
          {
              List<string> str0 = new List<string>();
              str0 = GetTimeAndAreaDetails(str1[i]);//将CAD/CAM换为CAD与CAM  以[ ] / 拆分
              for (int j = 0; j < str0.Count; j++)
              {
                  strResult.Add(str0[j]);
              }
          }
          return strResult;
      }

      public static List<string> SplitTimeAndAreaStrinng(string str)
      {
          List<string> strList = new List<string>();
          string[] strTemp = str.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
          for (int i = 0; i < strTemp.Length; i++)
          {
              strList.Add(strTemp[i]);
          }
          return strList;
      }

      public static List<string> GetTimeAndAreaDetails(string str)
      {
          List<string> strList = new List<string>();
          if (str.IndexOf("CAD/CAM一体化室") != -1)
          {
              str = str.Replace("CAD/CAM一体化室", "CAD与CAM一体化室");
          }
          string[] strTemp = str.Split(new char[] { '[', ']', '/' }, StringSplitOptions.RemoveEmptyEntries);
          if (strTemp.Length % 4 == 0)
          {
              for (int i = 0; i < strTemp.Length; i++)
              {
                  strList.Add(strTemp[i]);
              }
          }
          else
          {
              for (int i = 0; i < strTemp.Length; i++)
              {
                  strList.Add(strTemp[i]);
              }
              strList.Add("体育馆或操场"); //没找到还有别的没地点的
          }
          return strList; 
      }

      public static string GetWithoutWeek(string str)
      {
          int v = 0;
          if (str.IndexOf("单周") != -1)//包含  这两个字  
          {
              v = 1;//单周
          }
          if (str.IndexOf("双周") != -1)
          {
              v = 2;//双周
          }

          StringBuilder sb = new StringBuilder();
          if (sb.Length > 0)
          {
              sb.Remove(0, sb.Length);
          }
          string[] strTemp = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
          for (int i = 0; i < strTemp.Length; i++)
          {
              string[] s1 = strTemp[i].Split(new char[] { '-', '周', '单', '双' }, StringSplitOptions.RemoveEmptyEntries);
              if (s1.Length == 1)
              {
                  sb.Append("" + s1[0].ToString() + ""+" ");//（1,3,6,8,9   分隔这个的）
              }
              else
              {
                  for (int j = Convert.ToInt32(s1[0]); j < Convert.ToInt32(s1[1]); j++)
                  {
                      sb.Append(""+j.ToString()+""+" ");//在这里加上，用空格隔开  OK！！  这个是里面的空格（就是1-4周  1 2 3 4）
                  }
                  sb.Append(Convert.ToInt32(s1[1]).ToString()+" ");//这个空格是外面的  （1-4周，6-10周   4 6）
              }
          }
          if (v == 1)
          {
              string[] ss1 = sb.ToString().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
              sb.Remove(0, sb.Length);
              for (int i = 0; i < ss1.Length; i++)
              {
                  int t = Convert.ToInt32(ss1[i]);
                  if (t % 2 != 0)
                  {
                      sb.Append(ss1[i]+" ");//单周加空格
                  }
              }
          }
          if (v == 2)
          {
              string[] ss1 = sb.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
              sb.Remove(0, sb.Length);
              for (int i = 0; i < ss1.Length; i++)
              {
                  int t = Convert.ToInt32(ss1[i]);
                  if (t % 2 == 0)
                  {
                      sb.Append(ss1[i]+" ");//双周加空格
                  }
              }
          }
          return sb.ToString();
      }

    }
}

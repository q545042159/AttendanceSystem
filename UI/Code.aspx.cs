using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

public partial class ValidateImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Random rd = new Random();
        string[] YSF = { "+", "-","*" };
        Color[] colors = { Color.AliceBlue, Color.Aqua, Color.Aquamarine, Color.Azure, Color.Beige, Color.Bisque, Color.Blue, Color.DarkGray, Color.DarkRed, Color.DeepPink };
        string[] fonts = { "宋体", "新宋体", "楷体_GB2312", "Times New Roman" };
        String ysf = YSF[rd.Next(YSF.Length)];
        String num = "";
        string Code = "";
        string strCode = "";

        Bitmap bmp = new Bitmap(100, 30);
        for (int i = 0; i < 50; i++)//控制生成的噪点数量
        {
            int x = rd.Next(90);
            int y = rd.Next(30);
            bmp.SetPixel(x, y, Color.Yellow);
        }
        Graphics g = Graphics.FromImage(bmp);
        for (int i = 0; i < 3; i++)
        {
            Font fnt = new Font(fonts[rd.Next(fonts.Length)], rd.Next(18, 24), FontStyle.Bold);

            if (i == 1)//运算符
            {
                g.DrawString(ysf, fnt, new SolidBrush(colors[rd.Next(colors.Length)]), i * rd.Next(15, 18), rd.Next(5));
            }
            else//数字
            {
                num = rd.Next(10).ToString();
                g.DrawString(num, fnt, new SolidBrush(colors[rd.Next(colors.Length)]), i * rd.Next(15, 18), rd.Next(5));
                Code += (num + ".");//将数字放在这里，用.分隔开
            }
        }
       
        string[] a = Code.Split('.');//将数字拆分到a数组里s
        switch (ysf)//判断运算符
        {
            case "+":
                strCode = (int.Parse(a[0]) + int.Parse(a[1])).ToString();//先转化为数字再进行运算
                break;
            case "-":
                strCode = (int.Parse(a[0]) - int.Parse(a[1])).ToString();
                break;
            case "*":
                strCode = (int.Parse(a[0]) * int.Parse(a[1])).ToString();
                break;
            default:
                break;
        }

        Session["Code"] = strCode;

        for (int i = 0; i < 2; i++)//控制生成的干扰线数量
        {
            int x1 = rd.Next(70);
            int x2 = rd.Next(30);
            int y1 = rd.Next(70);
            int y2 = rd.Next(30);
            g.DrawLine(new Pen(Color.Red), x1, y1, x2, y2);
        }
        bmp.Save(Response.OutputStream, ImageFormat.Gif);//作为输出流保存
    }
}
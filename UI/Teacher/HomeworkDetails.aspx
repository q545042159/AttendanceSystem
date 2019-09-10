<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="HomeworkDetails.aspx.cs" Inherits="Teacher_HomeworkDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
    #teacher-homework
    {
        width:100%;height:100%;background-image:url(../loginimages/背景图/bgimg4.jpg);
            background-repeat:no-repeat;
            background-size:100% 100%;}
        ul {
            margin:0; padding:0;
            background-color:darkorange;
        }
        ul li {
            list-style:none; margin:2px 0;
            background-color:#52478c;
        }
        .lbl {
            color:white;
        }
        .btnClose,.btnAttdance,.btnUnNormal {
            border:0;
        }
        .btnClose {
            height:20px;
            border-radius:5px;
            background-color:chocolate;
            margin-right:10px;
            cursor:pointer;
            color:white;
        }
        .btnAttdance {
            height:20px;
            border-radius:5px;
            background-color:coral;
            margin-right:10px;
            cursor:pointer;
            color:white;
        }
       .btnUnNormal {
            height:20px;
            border-radius:5px;
            background-color:crimson;
            margin-left:10px;
            cursor:pointer;
            color:white;
        }
    </style>
    <div id="teacher-homework">
    <ul>
        <li><asp:Label ID="Label1" CssClass="lbl" runat="server" Text="Label"></asp:Label></li><!--没用  你们懂得！！！-->
        <li><asp:Label ID="lblHomeWorkMessage" CssClass="lbl"  runat="server" Text="未完成作业名单："></asp:Label></li>
        <li><asp:Label ID="lblResultMessage" CssClass="lbl"  runat="server" Text="考勤情况"></asp:Label></li>
        <li><asp:Label ID="lblMessage" CssClass="lbl"  runat="server" Text="授课信息"></asp:Label></li>
        <li>
            <table style="border-collapse:collapse; width:100%; text-align:center;">
                <tr>
                    <td><asp:Button ID="btnClose" CssClass="btnClose" runat="server" Text="返回主页面" onclick="btnClose_Click" /></td>
                    <td> <asp:Button ID="btnAttdance" CssClass="btnAttdance"  runat="server" Text="上报作业结果"  onclick="btnAttdance_Click" /></td>
                </tr>
            </table>     
        </li>
    </ul>
    <div style="overflow-y: scroll; height: 88%">   <!-- 控制滚动条的 -->
    <asp:GridView ID="gvHomeworkDetails" runat="server" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="StudentID"
                  DataSourceID="SqlDataSourceHomeworkDetails" 
        GridLines="Horizontal" 
        Font-Size="12px" PageSize="20" Width="100%" Height="141%" 
            style="margin-bottom: 0px">
                  <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <Columns>
            <asp:BoundField HeaderText="所属系部" ItemStyle-Width="100px" 
                SortExpression="StudentDepartment" DataField="StudentDepartment" >
            <ItemStyle Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="班级" DataField="TimeAndArea" SortExpression="TimeAndArea" >
            <ItemStyle Width="100px" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField HeaderText="学号" DataField="StudentID" ReadOnly="True" 
                SortExpression="StudentID" >
            <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="姓名" DataField="StudentName" 
                SortExpression="StudentName" >
            <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="作业情况">
            <ItemTemplate>
            <asp:RadioButton ID="rdoOK" runat="server" GroupName="g1" Text="完成" Checked="true" AutoPostBack="true" OnCheckedChanged="rdo_CheckChange" />
            <asp:RadioButton ID="rdoNO" runat="server" GroupName="g1" Text="未完成"  AutoPostBack="true" OnCheckedChanged="rdo_CheckChange" />
      
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <FooterStyle  BackColor="#B5C7DE" ForeColor="#4A3C8C"/>
                  <PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" 
                      PreviousPageText="上一页" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="true" ForeColor="#F7F7F7" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="true" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
    </asp:GridView>
    </div>

    
    <asp:SqlDataSource ID="SqlDataSourceHomeworkDetails" runat="server"
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT DISTINCT [StudentDepartment],[StudentID],[StudentName],[TimeAndArea] FROM [TabAllCourses] WHERE (([TeacherID] = @TeacherID) AND ([Course] = @Course) AND ([TimeAndArea] = @TimeAndArea ))">
    
    <SelectParameters>
    <asp:SessionParameter Name="TeacherID" SessionField="UserID" Type="String" />
    <asp:SessionParameter Name="Course" SessionField="CurrentCourse" Type="String" />
    <asp:SessionParameter Name="TimeAndArea" SessionField="WeekRange" Type="String" />
    </SelectParameters>

    </asp:SqlDataSource>
   </div> 
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Leader/LeaderMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceStudentDetails.aspx.cs" Inherits="Leader_AttendanceStudentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="lblMessage" runat="server" Text="lblMessage"></asp:Label>
    <asp:Button ID="btnBackDetails" runat="server" 
    onclick="btnBackDetails_Click" Text="关闭页面" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="gvStudentAttendance" runat="server" 
    AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="12px" 
    GridLines="Horizontal">
         <Columns>
         <asp:BoundField DataField="TeacherName" HeaderText="任课教师">
         <ItemStyle Width="80px"/>
         </asp:BoundField>
         <asp:BoundField DataField="StudentDepartment" HeaderText="学生系部">
         <ItemStyle Width="100px"/>
         </asp:BoundField>
         <asp:BoundField DataField="StudentClass" HeaderText="班级">
         <ItemStyle Width="100px"/>
         </asp:BoundField>
         <asp:BoundField DataField="StudentID" HeaderText="学号">
         <ItemStyle Width="80px"/>
         </asp:BoundField>
         <asp:BoundField DataField="StudentName" HeaderText="姓名">
         <ItemStyle Width="80px"/>
         </asp:BoundField>
         <asp:BoundField DataField="Course" HeaderText="缺勤课程">
         <ItemStyle Width="150px"/>
         </asp:BoundField>
         <asp:BoundField DataField="AttendanceType" HeaderText="缺勤类型">
         <ItemStyle Width="80px"/>
         </asp:BoundField>
         <asp:BoundField DataField="Week" HeaderText="缺勤时间">
         <ItemStyle Width="80px"/>
         </asp:BoundField>
         <asp:BoundField DataField="Time" HeaderText="缺课节次">
         <ItemStyle Width="80px"/>
         </asp:BoundField>
         <asp:BoundField HeaderText="累计缺勤次数" DataField="SumAttendance">
         <ItemStyle Width="60px"/>
         </asp:BoundField>
         </Columns>
         <RowStyle BackColor="#7E7FF" ForeColor="#4A3C8C" />
         <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
         <PagerStyle  BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"/>
         <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
         <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
         <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
</asp:Content>


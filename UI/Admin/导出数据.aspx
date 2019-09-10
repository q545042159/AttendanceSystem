<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="导出数据.aspx.cs" Inherits="Admin_导出数据" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <style type="text/css">
        #daochu {  width:100%;height:100%; }
          .daochu-table,.daochu-table  tr,.daochu-table tr td {
              border-collapse:collapse;height:10px;
          }
          .daochu-table {
              width:100%; 
          }
          #daochu-table1 tr td {
              width:16%;
          }
          #daochu-table2 tr td {
              width:33%;
          }
          #daochu-table3 tr td {
              width:33%;
          }
        .daochu-table .lbl {
            float:left; width:20px; height:10px; margin-left:20px; font:16px;
        }
        .daochu-btndownload {
            float:right; width:100px; height:25px; border:0; background-color:#b23d2b; color:white; cursor:pointer; border-radius:4px;
            margin-right:30px;
        } 
        #daochu-btndownload:hover{
            background-color:cornflowerblue;
        }
        .gridview1,.gridview2,.gridview3 { width:100%;text-align:center; }
        .btnselect {
             border:0; width:100px; height:25px; border-radius:4px; background-color:mediumvioletred;color:white;cursor:pointer;
         }
         .btnselect:hover {
              background-color:coral;
          }     
    </style>

<div id="daochu">
    <div id="daochhu-top">

        <table class="daochu-table" id="daochu-table1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" CssClass="lbl"></asp:Label>
                    <asp:Label ID="lblTeacher" runat="server" Text="教师漏报信息"   ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:Label ID="Teacherlbl" runat="server" Text="暂无教师漏报信息！" ForeColor="White"></asp:Label>
                </td>

                <td>
                    <asp:Label ID="lblWeek" runat="server" Text="按周次查询：" ForeColor="#FF6666"></asp:Label>
                    <asp:DropDownList ID="ddlWeek" runat="server" Width="100px"></asp:DropDownList>
                </td>

                <td>
                    <asp:Label ID="lblDepartment" runat="server" Text="按系部查询：" ForeColor="#FF6666"></asp:Label>
                    <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>
                </td>

                <td>
                    <asp:Button ID="btnOK" CssClass="btnselect" runat="server" Text="查询" onclick="btnOK_Click" />
                </td>
               
                <td>
                    <asp:Button ID="btnTeacher" runat="server" Text="下载导出数据" onclick="btnTeacher_Click" CssClass="daochu-btndownload"/>
                </td>              
            </tr>
        </table>

        <div id="daochu-top-gridview">
            <div style="overflow-y: scroll; height:240px;">   <!-- 控制滚动条的 -->
                <asp:GridView ID="gvTeacher" runat="server"  
                     ForeColor="White" CssClass="gridview1">
                </asp:GridView>
            </div>
        </div>
    </div>

    <hr />

    <div id="daochu-middle">
        <table class="daochu-table" id="daochu-table2">
            <tr>
                <td>
                    <asp:Label ID="Students" runat="server" Text="学生缺勤信息" CssClass="lbl" Width="200px" ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:Label ID="lblStudent" runat="server" Text="暂无学生缺勤信息！" Width="200px" ForeColor="White"></asp:Label>
                </td>

                <td>
                    <asp:Button ID="btnStudent" runat="server" Text="下载导出数据" onclick="btnStudent_Click" CssClass="daochu-btndownload" /> 
                </td>
            </tr>  
        </table>

        <div id="daochu-bottom-gridview">
            <div style="overflow-y: scroll; height:240px;">   <!-- 控制滚动条的 -->
                <asp:GridView ID="gvStudent" runat="server" 
                     ForeColor="White" CssClass="gridview2">
                </asp:GridView>
            </div>

        </div>
    </div>

    <hr />

    <div id="daochu-bottom">
        <table class="daochu-table" id="daochu-table3">
            <tr>
                <td>
                    <asp:Label ID="lblHomework" runat="server" Text="学生作业信息" CssClass="lbl" Width="200px" ForeColor="Red"></asp:Label>
                </td>

                <td>
                    <asp:Label ID="lblHomework2" runat="server" Text="暂无学生未完成作业信息！" Width="200px" ForeColor="White"></asp:Label>
                </td>

                <td>
                    <asp:Button ID="btnHomework" runat="server" Text="下载导出数据" onclick="btnHomework_Click" CssClass="daochu-btndownload" />
                </td>
            </tr>     
        </table>

        <div id="daochu-bottom2-gridview">
            <div style="overflow-y: scroll; height:230px;">   <!-- 控制滚动条的 -->
                <asp:GridView ID="gvHomework" runat="server" 
                     ForeColor="White" CssClass="gridview3">
                </asp:GridView>
            </div>

        </div>
    </div>
 </div>

</asp:Content>


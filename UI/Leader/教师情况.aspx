<%@ Page Title="" Language="C#" MasterPageFile="~/Leader/LeaderMasterPage.master" AutoEventWireup="true" CodeFile="教师情况.aspx.cs" Inherits="Leader_教师情况" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
         #jiaoshi-daochu {  width:100%;height:100%;background-image:url(../loginimages/背景图/bgimg4.jpg);
            background-repeat:no-repeat;
            background-size:100% 100%; }
          .jiaoshi-daochu-table,.jiaoshi-daochu-table  tr,.jiasohi-daochu-table tr td {
              border-collapse:collapse;height:10px;
          }
          .jiaoshi-daochu-table {
              width:100%; 
          }
          .jiaoshi-daochu-table tr td {
              width:16%;
          }
       
        .jiaoshi-daochu-table .lbl {
            float:left; width:20px; height:10px; margin-left:20px; font:16px;
        }
        .daochu-btndownload {
            float:right; width:100px; height:25px; border:0; background-color:#b23d2b; color:white; cursor:pointer; border-radius:4px;
            margin-right:30px;
        } 
        #daochu-btndownload:hover{
            background-color:cornflowerblue;
        }
        .gridview1 { width:100%;text-align:center; }
        .btnselect {
             border:0; width:100px; height:25px; border-radius:4px; background-color:mediumvioletred;color:white;cursor:pointer;
         }
         .btnselect:hover {
              background-color:coral;
          }     
    </style>

<div id="jiaoshi-daochu">
    <div id="jiaoshi-daochhu-top">
        <table class="jiaoshi-daochu-table">
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
            <div style="overflow-y: scroll; height:770px;">   <!-- 控制滚动条的 -->
                <asp:GridView ID="gvTeacher" runat="server"  
                     ForeColor="White" CssClass="gridview1" Height="100%" Width="100%">
                </asp:GridView>
            </div>
        </div>
    </div>
    <hr />
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Secretary/SecretaryMasterPage.master" AutoEventWireup="true" CodeFile="本系学生情况.aspx.cs" Inherits="Secretary_本系学生情况" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
        #daochu {  width:100%;height:100%;background-image:url(../loginimages/背景图/bgimg4.jpg);
            background-repeat:no-repeat;
            background-size:100% 100%; }
        .xuesheng-table,.xuesheng-table  tr,.xuesheng-table tr td {
              border-collapse:collapse;height:10px;
          }
          .xuesheng-table {
              width:100%; 
          }
          .xuesheng-table1 tr td {
              width:20%;
          }

        .xuesheng-table .lbl {
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

        <table class="xuesheng-table xuesheng-table1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" CssClass="lbl"></asp:Label>
                    <asp:Label ID="Students" runat="server" Text="学生缺勤信息" ForeColor="Red" CssClass="lbl" Width="200px"></asp:Label>
                </td>                   

                <td>
                    <asp:Label ID="lblStudent" runat="server" Text="暂无学生缺勤信息！"  Width="200px"  ForeColor="White" ></asp:Label>
                </td>

                <td>
                    <asp:Label ID="lblWeek" runat="server" Text="按周次查询" ForeColor="White"></asp:Label>
                    <asp:DropDownList ID="ddlWeek" runat="server"></asp:DropDownList>
                </td>

                <td>
                    <asp:Button ID="btnOK" runat="server" Text="查询" onclick="btnOK_Click" CssClass="btnselect" />
                </td>

                <td>
                    <asp:Button ID="btnStudent" runat="server" Text="下载导出数据" onclick="btnStudent_Click" CssClass="daochu-btndownload" />
                </td>

            </tr>      
        </table>
       <div id="daochu-bottom-gridview">
            <div style="overflow-y: scroll; height:375px;">   <!-- 控制滚动条的 -->
                <asp:GridView ID="gvStudent" runat="server" 
                     ForeColor="White" CssClass="gridview2">
                </asp:GridView>
            </div>
        </div>
    </div>

    <hr />

    <div id="daochu-bottom2">
        
        <table class="xuesheng-table xuesheng-table1">
            <tr>
                <td>
                    <asp:Label ID="Homework" runat="server" Text="学生作业信息" ForeColor="Red" CssClass="lbl" Width="200px"></asp:Label> 
                </td>

                <td>
                    <asp:Label ID="lblHomework" runat="server" Text="暂无学生未完成作业！"  Width="200px"  ForeColor="White" ></asp:Label>
                </td>

                <td>
                    <asp:Button ID="btnHomework" runat="server" Text="下载导出数据" onclick="btnHomework_Click" CssClass="daochu-btndownload" />
                </td>
            </tr> 
        </table>

        <div id="daochu-bottom-gridview2">
            <div style="overflow-y: scroll; height:375px;">   <!-- 控制滚动条的 -->
                <asp:GridView ID="gvHomework" runat="server" 
                     ForeColor="White" CssClass="gridview2">
                </asp:GridView>
            </div>
        </div>
     </div>
</div>
</asp:Content>


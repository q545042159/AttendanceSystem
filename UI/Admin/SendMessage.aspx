<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="Admin_SendMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style type="text/css">
        table {
            width:100%;
            height:100%;
            margin:0 auto; background-color:white;
        }
        table, table tr, table tr td {
            border-collapse:collapse;
        }
        .text {
            width:99%;
            height:100%;
            margin-left:5px;
            background- color:rgba(255,255,255,0.2);
            border:2px solid #808080;
        }
        #tr1 {
            width:100%;
            height:30px;
            line-height:30px;
            background-color:lightsalmon;
        }
        #tr2 {
            width:100%;
            height:30px;
            line-height:30px;
            background-color:lightpink;
        }
         #tr3,#tr4 {
             background-color:cornsilk;
         }
        .btnOK {
            width:120px;
            height:30px;
            border:0;
            border-radius:8px;
            background-color:crimson;
            cursor:pointer;
            font:18px bold;
            color:white;
            letter-spacing:2em;
            padding-left:25px;
        }
        .btnOK:hover {
            background-color:#ed3a5f;
        }
    </style>
    <table>
        <tr id="tr1">    
            <td colspan="3">
                <div>
                    <asp:Label ID="lblMessage" runat="server" Text="通知" ForeColor="White" CssClass="title" Font-Bold="True" Font-Size="18pt"></asp:Label>
                </div>
            </td>
        </tr>

        <tr id="tr2">
            <td><div><asp:CheckBox ID="chkToLeader" Text="院系领导" runat="server" ForeColor="Black"/></div></td>
            <td><div><asp:CheckBox ID="chkToSecretary" Text="各系辅导员" runat="server" ForeColor="Black"/></div></td>
            <td><div><asp:CheckBox ID="chkToTeacher" Text="本学期所有有课教师" runat="server" ForeColor="Black"/></div></td>
        </tr>

        <tr id="tr3">
            <td colspan="3">          
                <asp:TextBox ID="txtxMessage" runat="server" Height="630px"
                    TextMode="MultiLine" CssClass="text" ForeColor="Black" Font-Size="20px" BackColor="White" Font-Overline="False" Font-Underline="False"></asp:TextBox>
            </td>
        </tr>

        <tr id="tr4">
            <td colspan="3" align="center"><asp:Button ID="btnOK" runat="server" Text="确定" 
                    onclick="btnOK_Click" CssClass="btnOK"/></td>
        </tr>
    </table>
</asp:Content>


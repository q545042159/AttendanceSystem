<%@ Page Title="" Language="C#" MasterPageFile="~/Secretary/SecretaryMasterPage.master" AutoEventWireup="true" CodeFile="修改密码.aspx.cs" Inherits="Secretary_修改密码" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
        table {
            width:800px;
            height:100%;
            margin:0 auto;
        }
        table th,table td {
            text-align:center;
            color:white;
            height:140px;
        }
        table th div {
            float:right;
            width:400px;
            height:55px;
            line-height:55px;
            border-radius:0 50px 0 50px;
            border-radius:0 50px 0 50px;
            background-color:rgba(255,255,255,0.4);
        }
        table td div {
            text-align:center;
            float:left;
            width:400px;
            height:40px;
            padding-top:15px;
            border-radius: 50px 0 50px 0;
            border-radius: 50px 0 50px 0;
            background-color:coral;
        }
        .textBox {
            width:240px;
            height:25px;
            margin:0 auto;
            border-radius:5px;
            background-color:rgba(255,255,255,0.2);
            border:1px solid white;
        }
        .btnOK {
            width:120px;
            height:35px;
            margin:0 auto;
            font:16px bold;
            border:0;
            border-radius:8px;
            background-color:brown;
            color:white;
            cursor:pointer;
        }
        .auto-style1 {
            height: 159px;
        }
        .auto-style3 {
            height: 100%;
        }
        .auto-style4 {
            height: 117px;
        }
        .auto-style6 {
            height: 156px;
        }
        .auto-style7 {
            height: 81px;
        }
        .auto-style8 {
            height: 118px;
        }
    </style>
<div>
    <table class="auto-style3">
        <tr>
            <th class="auto-style6"><div>工号:</div></th>
            <td class="auto-style6">
                <div>
                <asp:TextBox ID="txtUserID" runat="server" CssClass="textBox" ReadOnly="True"></asp:TextBox>
                </div>
            </td>
        </tr>

        <tr>
            <th class="auto-style8"><div>姓名:</div></th>
            <td class="auto-style8">
                <div>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="textBox" ReadOnly="True"></asp:TextBox>
                </div>
            </td>
        </tr>

        <tr>
            <th class="auto-style1"><div>密码:</div></th>
            <td class="auto-style1">
                <div>
                <asp:TextBox ID="txtUserPWD" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                </div>
            </td>
        </tr>

        <tr>
            <th class="auto-style4"><div>确认密码:</div></th>
            <td class="auto-style4">
                <div>
                <asp:TextBox ID="txtUserEnterPWD" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="2" class="auto-style7">
                <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="确定" CssClass="btnOK" />
            </td>
        </tr>

        <tr>
            <td colspan="2"><asp:Label id="lblMessage" runat="server"></asp:Label></td>
        </tr>
    </table>
</div>
</asp:Content>


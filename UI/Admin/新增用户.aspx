<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="新增用户.aspx.cs" Inherits="Admin_AddNewTeacher" Debug="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        #addmain {
            width:100%;
            height:100%;
            backgroun d-image:url(../loginimages/背景图/bgimg6.jpg);
            background-repeat:no-repeat;
            background-size:100% 100%;
        }
        table {
            width:850px;
            margin:0 auto;
        }
        caption h2{
            letter-spacing:1em;
            color:white;
            font:weight;
            text-shadow:4px 4px rgba(255,255,255,0.3);
        }
        table tr th {
            height:80px;
        }
        .font div {
            text-align:center;
            background-color:#ff6a00;
            width:400px;
            height:50px;
            margin:0 auto;
            line-height:50px;
            box-shadow:5px 5px rgba(255,255,255,0.3); 
            border-radius:10px;
        }
        .text div {
            text-align:center;
            background-color:#fbcd93;
            width:400px;
            height:32px;
            margin:0 auto;
            padding-top:13px;
            box-shadow:5px 5px rgba(0,0,0,0.8);
            border-radius:10px;
        }
         .dropdown, .textBox {
            width:240px;
            margin:0 auto;
            border-radius:3px;
            background-color:rgba(255,255,255,0.2);
            border:1px solid white;
         }
        .btnEnter,.btnCancel {
            float:left;
            width:90px;
            height:30px;
            border:0;
            font-family:"楷体";
            font-size:20px;
            border-radius:5px;
            margin-left:200px;
            background:deeppink;
            color:white;
            cursor:pointer;
        }
        .btnEnter:hover, .btnCancel:hover {
            background:#fc9347;
        }
    </style>
<div id="addmain">
    <table> 
    <caption><h2>添加用户</h2></caption>

    <tr>
        <th class="font"><div class="left-top">教师类型:</div></th>
        <th class="text">
            <div class="right-top">
            <asp:DropDownList ID="ddlTeachersType" runat="server" Height="20px" Width="192px" CssClass="dropdown" >
            </asp:DropDownList>
            </div>
        </th>
    </tr>

    <tr>
        <th class="font"><div class="left-top">所属部门:</div></th>
        <th class="text">
            <div class="right-top">
            <asp:DropDownList ID="ddlDepartment" runat="server" Height="20px" Width="192px" CssClass="dropdown">
            </asp:DropDownList>
            </div>
        </th>
    </tr>

    <tr>
        <th class="font"><div class="left-top">教师工号:</div></th>   
        <th class="text">
            <div class="right-top">
            <asp:TextBox ID="txtUserID" runat="server" Width="192px" CssClass="textBox"></asp:TextBox>
            </div>
        </th>
    </tr>
    
    <tr>
        <th class="font"><div class="left-bottom">教师姓名:</div></th>   
        <th class="text">
            <div class="right-bottom">
            <asp:TextBox ID="txtUserName" runat="server" Width="192px" CssClass="textBox"></asp:TextBox>
            </div>
        </th>
    </tr>
    
    <tr>
        <th class="font"><div class="left-bottom">密码:</div></th>
        <th class="text">
            <div class="right-bottom">
            <asp:TextBox ID="txtUserPWD" runat="server" TextMode="Password" Width="192px" CssClass="textBox"></asp:TextBox>
            </div>
        </th>
    </tr>
    
    <tr>
        <th class="font"><div class="left-bottom">权限:</div></th>
        <th class="text">
    	    <div class="right-bottom">
            <asp:DropDownList ID="ddlRole" runat="server" Height="20px" Width="192px" CssClass="dropdown">
            </asp:DropDownList>
    	    </div>
        </th>
    </tr>

	<tr>
        <th colspan="2"><asp:Button ID="btnEnter" runat="server"  Text="确定" onclick="btnEnter_Click" CssClass="btnEnter" />
            <asp:Button ID="btnCancel" runat="server"  Text="取消" 
                onclick="btnCancel_Click1" CssClass="btnCancel" />
        </th>
    </tr>
    

	<tr>
        <th colspan="2"><asp:Label id="lblMessage" runat="server" ForeColor="Red"></asp:Label></th>
    </tr>

    </table>
</div>
</asp:Content>


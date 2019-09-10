<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="用户管理.aspx.cs" Inherits="Admin_用户管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <style type="text/css">
        #user-management {
            width:100%; height:100%; background-color:white;
        }
#user-top-table,#user-top-table tr,#user-top-table tr td{ width:100%; border-collapse:collapse;text-align:center;
     background-color:#4a6c95;}
 #user-top-table tr td {
     width:33%; height:10px;
        }
.user-margin-top,.user-management-select {
    margin-top:4px;
        }
#Label2,#ddlLimit,#Label3,#txtLimit,#ddlDepartment,#ddlRole,#btnQuery{ float:left; margin-top:3px;}
.user-management-select {
            border:0; width:100px; height:25px; background-color:cornflowerblue; color:white; border-radius:4px; cursor:pointer;
        }
.user-management-select:hover {
    background-color:deepskyblue;
        }
</style>
<div id="user-management">
    <table id="user-top-table">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="查询范围：" Font-Size="16px" ForeColor="White" CssClass="user-margin-top"></asp:Label>
     
                <asp:DropDownList ID="ddlLimit" runat="server" Font-Bold="false" 
                    AutoPostBack="true" Font-Size="12px" 
                    onselectedindexchanged="ddlLimit_SelectedIndexChanged" CssClass="user-margin-top">
                <asp:ListItem>所有记录</asp:ListItem>
                <asp:ListItem>按部门查询</asp:ListItem>
                <asp:ListItem>按教师工号查询</asp:ListItem>
                <asp:ListItem>按教师姓名查询</asp:ListItem>
                <asp:ListItem>按权限查询</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="查询条件：" Font-Size="16px" ForeColor="White" CssClass="user-margin-top"></asp:Label>
        
                <asp:TextBox ID="txtLimit" runat="server" Font-Size="12px" Width="100px" CssClass="user-margin-top"></asp:TextBox>
       
                <asp:DropDownList ID="ddlDepartment" runat="server" Font-Bold="True" 
                    AutoPostBack="True" Font-Size="12px" 
                    DataSourceID="Department" DataTextField="Department" 
                    onselectedindexchanged="ddlDepartment_SelectedIndexChanged"
                    DataValueField="Department">
                </asp:DropDownList>
                <asp:SqlDataSource ID="Department" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="select distinct Department from TabTeachers">
                </asp:SqlDataSource>
        
                <asp:DropDownList ID="ddlRole" runat="server" Font-Bold="True" 
                    AutoPostBack="True" Font-Size="12px" 
                    DataSourceID="Role" DataTextField="Role" 
                    onselectedindexchanged="ddlRole_SelectedIndexChanged"
                    DataValueField="Role">
                </asp:DropDownList>
                <asp:SqlDataSource ID="Role" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="select distinct Role from TabTeachers"></asp:SqlDataSource>
            </td>
            <td>
                <asp:Button ID="btnQuery" CssClass="user-management-select" runat="server" Text="查询" Width="100px" Font-Size="12px" OnClick="btnQuery_Click"/>
            </td>   
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Size="18px"></asp:Label>   
            </td>
        </tr>
    </table>
        
    
    <asp:GridView ID="gvTeachers" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="UserID" CellPadding="4" ForeColor="#333333" 
        GridLines="None" Height="99%" onrowdatabound="gvTeachers_RowDataBound" 
        PageSize="25" Width="100%" HorizontalAlign="Center" 
        onpageindexchanging="gvTeachers_PageIndexChanging" 
        onrowcancelingedit="gvTeachers_RowCancelingEdit" 
        onrowediting="gvTeachers_RowEditing" 
        onrowdeleting="gvTeachers_RowDeleting" onrowupdating="gvTeachers_RowUpdating">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="所属部门" SortExpression="Department">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlDepartmentEdit" runat="server" 
                        DataSourceID="SqlDataSource2" DataTextField="Department" 
                        DataValueField="Department" SelectedValue='<%# Bind("Department") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="select distinct Department from TabTeachers">
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Department") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserID" HeaderText="教师工号" SortExpression="UserID" />
            <asp:BoundField DataField="UserName" HeaderText="教师姓名" 
                SortExpression="UserName" />
            <asp:TemplateField HeaderText="教师权限" SortExpression="Role">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlRoleEdit" runat="server" 
                        DataSourceID="SqlDataSource3" DataTextField="Role" DataValueField="Role" 
                        SelectedValue='<%# Bind("Role") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="select distinct Role from TabTeachers"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
            <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

<SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
    </asp:GridView>
</div>
</asp:Content>


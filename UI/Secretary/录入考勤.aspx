<%@ Page Title="" Language="C#" MasterPageFile="~/Secretary/SecretaryMasterPage.master" AutoEventWireup="true" CodeFile="录入考勤.aspx.cs" Inherits="Secretary_录入考勤" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <style type="text/css">
        * {
            color:white;
        }
        #secretary-kaoqin{
            width:100%;height:100%;background-image:url(../loginimages/背景图/bgimg4.jpg);
            background-repeat:no-repeat;
            background-size:100% 100%;
        }
        .kaoqin-btn {
            width:110px; height:25px; border:0; cursor:pointer; border-radius:4px; background-color:orangered;
        }
        .kaoqin-btn:hover {
                background-color:darkorange;
            }
        #lblTeacherName {
            margin-right:10px;
        }
    </style>
<div id="secretary-kaoqin">
    <asp:Label ID="lblTeacherName" runat="server" Text="Label"></asp:Label>
<div style="overflow-y: scroll; height: 450px">   <!-- 控制滚动条的 -->
   
    <asp:Repeater ID="rptCourse" runat="server" OnItemCommand="rptCourse_ItemCommand">
        <HeaderTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%" >
                <tr>
                    <td colspan="4" style="width:100%;height:30px" class="STYLE1">
                        <asp:Label ID="Label2" runat="server" Text="本周授课信息"></asp:Label>
                    </td>
                </tr>
 
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td style="width:10%;height:30px" align="center" class="STYLE1">
                    <%#Container.ItemIndex+1 %>&nbsp;
                </td>

                <td style="width:40%;height:25px" class="STYLE1">
                    <%#DataBinder.Eval(Container.DataItem,"Week") %>
                    <%#DataBinder.Eval(Container.DataItem,"Time") %>
                    <%#(DataBinder.Eval(Container.DataItem,"Course")).ToString().Substring(0,(DataBinder.Eval(Container.DataItem,"Course")).ToString().Length-3) %>
                    <asp:TextBox ID="txtCourse" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Course") %>'/>
                    <asp:TextBox ID="txtWeek" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Week") %>'/>
                    <asp:TextBox ID="txtTime" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Time") %>'/>
                    <asp:TextBox ID="txtWeekRange" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"StudentIDList") %>'/>
                </td>

                <td style="width:10%;height:25px" class="STYLE1">
                    <asp:CheckBox ID="chkHomework" Text="布置作业" runat="server" />                    
                </td>

                <td style="width:20%;height:25px" class="STYLE1">
                    <asp:Button ID="btnClick" CssClass="kaoqin-btn" Text="考勤" Width="100px" runat="server" OnClick="BtnSubmit_Click" />
                </td>

                <td style="width:10%;height:25px">&nbsp;</td>
            </tr>

            <tr>
                <td colspan="5">
                    <hr />
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
   </div>

        
        <asp:Label ID="lblTest" runat="server" Text="Label" Visible="false"></asp:Label>
   <div style="overflow-y: scroll; height: 320px">   <!-- 控制滚动条的 -->
    <asp:Repeater ID="rptHomework" runat="server" OnItemCommand="rptHomework_ItemCommand">
        <HeaderTemplate>
            <table border="0" style="width:100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="4" style="width:100%;height:30px" class="STYLE1">
                        <asp:Label ID="Label1" runat="server" Text="上周作业未批改情况：" Font-Bold="true" />
                    </td>
                </tr>
            
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td style="width:10%;height:25px" align="center" class="STYLE1">
                    <%#Container.ItemIndex+1 %>&nbsp;
                </td>
                <td style="width:50%;height:25px" class="STYLE1">
                    <%#DataBinder.Eval(Container.DataItem,"Week") %>
                    <%#DataBinder.Eval(Container.DataItem,"Time") %>
                    <%#(DataBinder.Eval(Container.DataItem,"Course")).ToString().Substring(0,(DataBinder.Eval(Container.DataItem,"Course")).ToString().Length-3) %>
                    <asp:TextBox ID="txtCourse1" runat="server" Visible="false"
                        Text='<%#DataBinder.Eval(Container.DataItem,"Course") %>'>
                    </asp:TextBox>
                    <asp:TextBox ID="txtWeek1" runat="server" Visible="false"
                        Text='<%#DataBinder.Eval(Container.DataItem,"Week") %>'>
                    </asp:TextBox>
                    <asp:TextBox ID="txtTime1" runat="server" Visible="false"
                        Text='<%#DataBinder.Eval(Container.DataItem,"Time") %>'>
                    </asp:TextBox>
                    <asp:TextBox ID="txtWeekRange1" runat="server" Visible="false"
                        Text='<%#DataBinder.Eval(Container.DataItem,"StudentIDList") %>'>
                    </asp:TextBox>
                </td>

                <td style="width:20%;height:25px" class="STYLE1">
                    <asp:Button ID="btnHomeworkClick" CssClass="kaoqin-btn" Text="批改" Width="100px" runat="server" OnClick="BtnHomeworkSubmit_Click" />
                </td>

                <td style="width:10%;height:25px">&nbsp;</td>
            </tr>

            <tr>
                <td colspan="5"><hr /></td>
            </tr>
        </ItemTemplate>
        
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    </div>
</div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Leader/LeaderMasterPage.master" AutoEventWireup="true" CodeFile="GetMessage.aspx.cs" Inherits="Leader_GetMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        * {
            color:white;
        }
       #getmes {
           width:100%;
           height:100%;
            background-image:url(../loginimages/背景图/bgimg4.jpg);
            background-repeat:no-repeat;
            background-size:100% 100%;
       }
        .Label2 {
            color:orangered;
        }
        .btnOK{
            border:0;
            width:120px;
            height:30px;
            border-radius:5px;
            cursor:pointer;
            background-color:darkviolet;
        }
    </style>
<div id="getmes">
<asp:Repeater ID="rptMessage" runat="server" OnItemCommand="rptCourse_ItemCommand">

        <HeaderTemplate>
            <table border="0" style="width:100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="4" style="width:100%;height:30px" class="STYLE1" >
                        &nbsp;&nbsp;&nbsp;&nbsp<asp:Label ID="Label2" CssClass="Label2" runat="server" Text="您有未读消息通知：" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td style="width:10%;height:25px" align="center" class="STYLE1" >
                    <%#Container.ItemIndex+1 %>&nbsp;
                </td>

                <td style="width:15%;height:25px" class="STYLE1" >&nbsp;&nbsp;
                    <%#DataBinder.Eval(Container.DataItem,"MessageTime")%>
                </td>

                <td style="width:70%;height:25px" class="STYLE1" >&nbsp;&nbsp;
                    <%#DataBinder.Eval(Container.DataItem,"Message")%>
                </td>
            </tr>

            <tr>
                <td colspan="3"><hr />
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            <tr>
                <td colspan="3" class="STYLE1" align="center" >
                    <asp:Button ID="btnOK" CssClass="btnOK" runat="server" Text="已读所有通知" />
                </td>
            </tr>
        </table>
        </FooterTemplate>

    </asp:Repeater>

</div>
</asp:Content>


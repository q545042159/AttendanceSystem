<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="DepartmentAnalysis.aspx.cs" Inherits="Admin_DepartmentAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">   
    #student-analysis-main{
	    width:100%;
	    height:100%;
	    margin:0 auto; background-color:white;}
   #student-analysis-main .lbl-btn {
       display:block;
       margin:0;
       width:100%;
       padding:5px 0;
    }
   #student-analysis-main .gridview {
       width:100%;
       height:90%;
    }
   #student-analysis-main .lbl {
       float:left; margin-left:20px; font:16px;
    }
    #student-analysis-main .gridview {
        text-align:center;
    }
.btn-small,.btn-big
{
    border:0px;
    }
.btn-small
{
    width:60px;
    height:20px;
    border-radius:3px;
    background:#e9629a;
    color:White;
    cursor:pointer;
    }
.btn-small:hover
{
    background:#f37bad;
    }
.btn-big
{
    float:right;
    width:100px;
    height:28px;
    margin-right:30px;
    margin-bottom:4px;
    border-radius:5px;
    color:White;
    background:#5584f4;
    cursor:pointer;
    }
.btn-big:hover
{
    background:#6e95f2;
    }


#student-analysis-main #headline{
	margin:0;
	padding:0;
	width:100%;
	height:30px;
	font-size:15px;
    color:white;
    background-color:indianred;
	border-top:2px solid #999;}
#student-analysis-main #headline li{
	list-style:none;
    padding:0 30px;
	font:"宋体" 14px;
	float:left;
	width:60px;
	height:30px;
	text-align:center;
	line-height:30px;
	cursor:pointer;
	}
#student-analysis-main a{
	text-decoration:none;
	color:black;}
#student-analysis-main .content{
    background-color:rgba(255,255,255,0.45);
	display:none;}
#student-analysis-main .content a:hover{
	cursor:pointer;
	text-decoration:underline;
	color:#F00;}
#student-analysis-main .selectcolor{
    color:black;
	background-color:rgba(255,255,255,0.5);
	border-top:2px solid red;
	margin-top:-2px;
	}
</style>
<script type="text/javascript" src="../loginimages/script/jquery-3.2.1.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#d1").addClass("selectcolor");
        $("#cont-d1").show();
        $("#headline li").hover(function () {
            var index = $(this).index();
            $("#headline li").eq(index).addClass("selectcolor").siblings().removeClass("selectcolor");
            $("#cont-" + this.id).show().siblings("div").hide();
            $(".btn-big").click(function () {
                var index = $(this).index();
                $(this).parent().siblings().show();
            });
        });

    });
</script>

<div id="student-analysis-main">
	<ul id="headline">
    	<li id="d1">会计系</li>
        <li id="d2">信息系</li>
        <li id="d3">经管系</li>
        <li id="d4">食品系</li>
        <li id="d5">机械系</li>
        <li id="d6">外语系</li>
        <li id="d7">建筑系</li>
    </ul>

    <div class="content" id="cont-d1">
        <div class="lbl-btn">
                <asp:Label ID="lblKJ" CssClass="lbl" runat="server" Text="lblKJ" ForeColor="Red"></asp:Label>            
                <asp:Button ID="btnKJ" runat="server" CssClass="btn-big" Text="会计系图表分析" OnClick="btnKJClick" Font-Size="12px"/>
        </div>

        <div class="gridview">
                <asp:GridView ID="gvKJ" CssClass="gridview" runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                    CellPadding="2" ForeColor="Black" GridLines="None" 
                                    OnRowDataBound="gvKJ_RowDataBound" Font-Size="12px" style="margin-top: 0px" 
                                    Width="100%">
                                    <Columns>
                                       <asp:BoundField HeaderText="周次" ItemStyle-Width="60px" DataField="周次" >
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="系部" ItemStyle-Width="80px" DataField="系部">
                <ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="迟到人数" ItemStyle-Width="60px" DataField="迟到人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="早退人数" ItemStyle-Width="60px" DataField="早退人数">
                                        <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="旷课人数" ItemStyle-Width="60px" DataField="旷课人数">
                                        <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="请假人数" ItemStyle-Width="60px" DataField="请假人数">
                                        <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="合计" ItemStyle-Width="60px" DataField="合计">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="详情" ItemStyle-Width="60px" >
                                       <ItemTemplate>
                                            <asp:Button ID="btnDetail" CssClass="btn-small" runat="server" Text="详情" Font-Size="12px" OnClick="btnDetailsClick" />
                                       </ItemTemplate>

                <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                   </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="Tan" Font-Bold="true" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="true" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="phKJ" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="content" id="cont-d2">
        <div class="lbl-btn">
                <asp:Label ID="lblXX" runat="server" Text="lblXX" ForeColor="Red" CssClass="lbl"></asp:Label>
                <asp:Button ID="btnXX" runat="server" CssClass="btn-big" Text="信息系图表分析" OnClick="btnXXClick" Font-Size="12px"/>
        </div>

        <div class="gridview">
                <asp:GridView ID="gvXX" CssClass="gridview"  runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                    CellPadding="2" ForeColor="Black" GridLines="None" 
                                    OnRowDataBound="gvXX_RowDataBound" Font-Size="12px" style="margin-top: 0px" 
                                    Width="100%">
                                    <Columns>
                                       <asp:BoundField HeaderText="周次" ItemStyle-Width="60px" DataField="周次" >
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="系部" ItemStyle-Width="80px" DataField="系部">
                <ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="迟到人数" ItemStyle-Width="60px" DataField="迟到人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="早退人数" ItemStyle-Width="60px" DataField="早退人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="旷课人数" ItemStyle-Width="60px" DataField="旷课人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="请假人数" ItemStyle-Width="60px" DataField="请假人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="合计" ItemStyle-Width="60px" DataField="合计">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="详情" ItemStyle-Width="60px" >
                                       <ItemTemplate>
                                            <asp:Button ID="btnDetail" CssClass="btn-small" runat="server" Text="详情" Font-Size="12px" OnClick="btnDetailsClick" />
                                       </ItemTemplate>

                <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                   </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="Tan" Font-Bold="true" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="true" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="phXX" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="content" id="cont-d3">
        <div class="lbl-btn">
                 <asp:Label ID="lblJG" CssClass="lbl" runat="server" Text="lblJG" ForeColor="Red"></asp:Label>
                 <asp:Button ID="btnJG" runat="server" CssClass="btn-big" Text="经管系图表分析" OnClick="btnJGClick" Font-Size="12px"/>
        </div>

        <div class="gridview">
                                <asp:GridView ID="gvJG" CssClass="gridview"  runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                    CellPadding="2" ForeColor="Black" GridLines="None" 
                                    OnRowDataBound="gvJG_RowDataBound" Font-Size="12px" style="margin-top: 0px" 
                                    Width="100%">
                                    <Columns>
                                       <asp:BoundField HeaderText="周次" ItemStyle-Width="60px" DataField="周次" >
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="系部" ItemStyle-Width="80px" DataField="系部">
                <ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="迟到人数" ItemStyle-Width="60px" DataField="迟到人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="早退人数" ItemStyle-Width="60px" DataField="早退人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="旷课人数" ItemStyle-Width="60px" DataField="旷课人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="请假人数" ItemStyle-Width="60px" DataField="请假人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="合计" ItemStyle-Width="60px" DataField="合计">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="详情" ItemStyle-Width="60px" >
                                       <ItemTemplate>
                                            <asp:Button ID="btnDetail" CssClass="btn-small" runat="server" Text="详情" Font-Size="12px" OnClick="btnDetailsClick" />
                                       </ItemTemplate>

                <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                   </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="Tan" Font-Bold="true" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="true" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="phJG" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="content" id="cont-d4">
        <div class="lbl-btn">
                  <asp:Label ID="lblSP" CssClass="lbl" runat="server" Text="lblSP" ForeColor="Red"></asp:Label>
                  <asp:Button ID="btnSP" runat="server" CssClass="btn-big" Text="食品系图表分析" OnClick="btnSPClick" Font-Size="12px"/>
        </div>

        <div class="gridview">
                                <asp:GridView ID="gvSP" CssClass="gridview"  runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                    CellPadding="2" ForeColor="Black" GridLines="None" 
                                    OnRowDataBound="gvSP_RowDataBound" Font-Size="12px" style="margin-top: 0px" 
                                    Width="100%">
                                    <Columns>
                                       <asp:BoundField HeaderText="周次" ItemStyle-Width="60px" DataField="周次" >
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="系部" ItemStyle-Width="80px" DataField="系部">
                <ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="迟到人数" ItemStyle-Width="60px" DataField="迟到人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="早退人数" ItemStyle-Width="60px" DataField="早退人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="旷课人数" ItemStyle-Width="60px" DataField="旷课人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="请假人数" ItemStyle-Width="60px" DataField="请假人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="合计" ItemStyle-Width="60px" DataField="合计">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="详情" ItemStyle-Width="60px" >
                                       <ItemTemplate>
                                            <asp:Button ID="btnDetail" CssClass="btn-small" runat="server" Text="详情" Font-Size="12px" OnClick="btnDetailsClick" />
                                       </ItemTemplate>

                <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                   </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="Tan" Font-Bold="true" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="true" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="phSP" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="content" id="cont-d5">
        <div class="lbl-btn">
                <asp:Label ID="lblJX" CssClass="lbl" runat="server" Text="lblJX" ForeColor="Red"></asp:Label>
                <asp:Button ID="btnJX" runat="server" CssClass="btn-big" Text="机械系图表分析" OnClick="btnJXClick" Font-Size="12px"/>
        </div>

        <div class="gridview">

                                <asp:GridView ID="gvJX" CssClass="gridview"  runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                    CellPadding="2" ForeColor="Black" GridLines="None" 
                                    OnRowDataBound="gvJX_RowDataBound" Font-Size="12px" style="margin-top: 0px" 
                                    Width="100%">
                                    <Columns>
                                       <asp:BoundField HeaderText="周次" ItemStyle-Width="60px" DataField="周次" >
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="系部" ItemStyle-Width="80px" DataField="系部">
                <ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="迟到人数" ItemStyle-Width="60px" DataField="迟到人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="早退人数" ItemStyle-Width="60px" DataField="早退人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="旷课人数" ItemStyle-Width="60px" DataField="旷课人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="请假人数" ItemStyle-Width="60px" DataField="请假人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="合计" ItemStyle-Width="60px" DataField="合计">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="详情" ItemStyle-Width="60px" >
                                       <ItemTemplate>
                                            <asp:Button ID="btnDetail" CssClass="btn-small" runat="server" Text="详情" Font-Size="12px" OnClick="btnDetailsClick" />
                                       </ItemTemplate>

                <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                   </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="Tan" Font-Bold="true" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="true" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="phJX" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="content" id="cont-d6">
        <div class="lbl-btn">
                 <asp:Label ID="lblWY" CssClass="lbl" runat="server" Text="lblWY" ForeColor="Red"></asp:Label>
                 <asp:Button ID="btnWY" runat="server" CssClass="btn-big" Text="外语系图表分析" OnClick="btnWYClick" Font-Size="12px"/>
        </div>

        <div class="gridview">

                                <asp:GridView ID="gvWY" CssClass="gridview"  runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                    CellPadding="2" ForeColor="Black" GridLines="None" 
                                    OnRowDataBound="gvWY_RowDataBound" Font-Size="12px" style="margin-top: 0px" 
                                    Width="100%">
                                    <Columns>
                                       <asp:BoundField HeaderText="周次" ItemStyle-Width="60px" DataField="周次" >
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="系部" ItemStyle-Width="80px" DataField="系部">
                <ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="迟到人数" ItemStyle-Width="60px" DataField="迟到人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="早退人数" ItemStyle-Width="60px" DataField="早退人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="旷课人数" ItemStyle-Width="60px" DataField="旷课人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="请假人数" ItemStyle-Width="60px" DataField="请假人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="合计" ItemStyle-Width="60px" DataField="合计">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="详情" ItemStyle-Width="60px" >
                                       <ItemTemplate>
                                            <asp:Button ID="btnDetail" CssClass="btn-small" runat="server" Text="详情" Font-Size="12px" OnClick="btnDetailsClick" />
                                       </ItemTemplate>

                <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                   </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="Tan" Font-Bold="true" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="true" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="phWY" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="content" id="cont-d7">
        <div class="lbl-btn">
                 <asp:Label ID="lblJZ" CssClass="lbl" runat="server" Text="lblJZ" ForeColor="Red"></asp:Label>
                 <asp:Button ID="btnJZ" runat="server" CssClass="btn-big" Text="建筑系图表分析" OnClick="btnJZClick" Font-Size="12px"/>
        </div>

        <div class="gridview">

                                <asp:GridView ID="gvJZ" CssClass="gridview"  runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                                    CellPadding="2" ForeColor="Black" GridLines="None" 
                                    OnRowDataBound="gvJZ_RowDataBound" Font-Size="12px" style="margin-top: 0px" 
                                    Width="100%">
                                    <Columns>
                                       <asp:BoundField HeaderText="周次" ItemStyle-Width="60px" DataField="周次" >
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="系部" ItemStyle-Width="80px" DataField="系部">
                <ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="迟到人数" ItemStyle-Width="60px" DataField="迟到人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="早退人数" ItemStyle-Width="60px" DataField="早退人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="旷课人数" ItemStyle-Width="60px" DataField="旷课人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="请假人数" ItemStyle-Width="60px" DataField="请假人数">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:BoundField HeaderText="合计" ItemStyle-Width="60px" DataField="合计">
                <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="详情" ItemStyle-Width="60px" >
                                       <ItemTemplate>
                                            <asp:Button ID="btnDetail" CssClass="btn-small" runat="server" Text="详情" Font-Size="12px" OnClick="btnDetailsClick" />
                                       </ItemTemplate>

                <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                   </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="Tan" Font-Bold="true" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="true" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="phJZ" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</div>

</asp:Content>


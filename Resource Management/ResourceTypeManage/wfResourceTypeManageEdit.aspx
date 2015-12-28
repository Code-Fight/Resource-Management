<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="wfResourceTypeManageEdit.aspx.cs"
Inherits="Resource_Management.ResourceTypeManage.wfResourceTypeManageEdit" Title="备品添加、编辑" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="list_all">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br/>
                <br/>
                <div class="caption" align="right">资源类型管理</div>
                <br/>
                <hr/>
                <br/>
                <div align="center">
                    类型 ：<%
                            if (_excutetype == EnExcuteType.Search)
                            { %><asp:Label ID="lblname"
                                                                           runat="server"/><% }
                            else
                            { %><input type="text" ID="txtname" runat="server" tabindex="1"
                                  style="width: 180px"/>
                    <% } %>
                </div>
                <p></p>
                <div style="text-align: center">
                    <asp:Label ID="UiLabErr" runat="server"
                               ForeColor="Red"/>
                </div>
                <p></p>
                <div align="center">
                    <% if (_excutetype != EnExcuteType.Search)
                       { %>

                        <asp:Button ID="btnSubmit" runat="server"
                                    onclick="btnSubmit_Click" style="width: 70px" tabindex="5" Text="确定"
                                    ValidationGroup="vg" Width="121px"/>
                    <% } %>
                    <asp:Button ID="btnReturn" runat="server"
                                onclick="btnReturn_Click" style="width: 70px" tabindex="7" Text="返回"
                                Width="150px"/>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
CodeBehind="wfResourceManageSearch.aspx.cs" Inherits="Resource_Management.ResourceDetailManage.wfResourceManageSearch"
Title="" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list_all">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br/>
        <br/>
        <div class="caption" align="right">资源列表</div>
        <div align="right">

            &nbsp;<asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click"/>

        </div>
        <div class="caption" align="right">

        </div>
        <br/>
        <hr/>
        <br/>
        <div>
            时间：<asp:TextBox ID="txt_start_time" runat="server" class="Wdate" Width="150" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true, isShowClear:true,readOnly:true});"></asp:TextBox>
            至
            <asp:TextBox ID="txt_end_time" runat="server" class="Wdate" Width="150" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true, isShowClear:true,readOnly:true});"></asp:TextBox>&nbsp;
            名称 :
            <asp:TextBox ID="txtname" runat="server" CssClass="text_one" TabIndex="1"></asp:TextBox>
            &nbsp;
            类别：<asp:DropDownList ID="DDL_type" Width="180px" DataTextField="name" DataValueField="id" runat="server"></asp:DropDownList>&nbsp;
            备注 :
            <asp:TextBox ID="txtmemo" runat="server" CssClass="text_one" TabIndex="1"></asp:TextBox>&nbsp;
            <asp:Button ID="btnSeach" runat="server" Text="查询" OnClick="btnSeach_Click" TabIndex="2"/>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

            <ContentTemplate>
                <div style="line-height: 15px; text-align: center;">&nbsp;<asp:Label runat="server" ID="errMsg" ForeColor="red"></asp:Label>
                </div>
                <asp:Panel ID="PanHaveRecords" runat="server">
                    <asp:Repeater ID="Rept" runat="server" OnItemCommand="Rept_ItemCommand">
                        <HeaderTemplate>
                            <table id="tbid" class="mytable" width="100%">
                            <tr>
                                <th class="bj_first" style="width: 3%">
                                    序号
                                </th>
                                <th class="bj_first" style="width: 10%">
                                    上传时间
                                </th>
                                <th class="bj_first" style="width: 25%">
                                    名称
                                </th>
                                <th class="bj_first" style="width: 10%">
                                    类型
                                </th>
                                <th class="bj_first" style="width: 5%">
                                    上传者
                                </th>
                                <th class="bj_first" style="width: 20%">
                                    备注
                                </th>


                                <th class="wid_xgsc bj_first"></th>

                                <th class="wid_xgsc bj_first"></th>

                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>

                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "RowNumber") %>
                                    <asp:TextBox ID="id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id") %>' Style="display: none;"/>

                                </td>
                                <td style="white-space: normal">
                                    <%#DataBinder.Eval(Container.DataItem, "insert_time") %>
                                </td>
                                <td style="white-space: normal">

                                    <asp:LinkButton ID="txtname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "name") %>' CommandName="Search" CssClass="link_blue"/>

                                </td>
                                <td style="white-space: normal">
                                    <%#DataBinder.Eval(Container.DataItem, "type") %>
                                </td>
                                <td style="white-space: normal">
                                    <%#DataBinder.Eval(Container.DataItem, "upload_people") %>
                                </td>
                                <td style="white-space: normal">
                                    <%#DataBinder.Eval(Container.DataItem, "memo") %>
                                </td>


                                <td>
                                    <asp:LinkButton ID="lbtnupdate" runat="server" Text="修改" CommandName="Update" CssClass="link_blue"/>

                                </td>

                                <td>
                                    <asp:LinkButton ID="lbdelete" runat="server" Text="删除" CommandName="Delete" OnClientClick=" return confirm('确定删除吗?'); "
                                                    CssClass="link_blue"/>
                                </td>

                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="Panel1" runat="server">
                        <table width="100%" align="center">
                            <tr>
                                <td class="tex_left">
                                    总记录:<asp:Label ID="lblallrecord" runat="server" Text="Label"></asp:Label>
                                    页码:<asp:Label ID="lblpageindex" runat="server" Text="Label"></asp:Label>
                                    /
                                    <asp:Label ID="lblallpagerecord" runat="server" Text="Label"></asp:Label>
                                    每页:<asp:Label ID="lblrecord" runat="server"/>条记录
                                </td>
                                <td class="tex_right">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pages" CurrentPageButtonClass="cpb"
                                                          CustomInfoSectionWidth="1%" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
                                                          PrevPageText="上一页" OnPageChanging="AspNetPager_PageChanging">
                                    </webdiyer:AspNetPager>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="PanNoRecords" runat="server">
                    <asp:Label ID="LabText" runat="server" Text="您的查询没有记录"></asp:Label>
                </asp:Panel>
                </table>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSeach"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <asp:Label runat="server" ID="download_id" Visible="False"></asp:Label>
</asp:Content>
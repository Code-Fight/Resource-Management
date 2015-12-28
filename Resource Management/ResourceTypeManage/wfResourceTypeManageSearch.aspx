<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="wfResourceTypeManageSearch.aspx.cs" Inherits="Resource_Management.ResourceTypeManage.wfResourceTypeManageSearch"
    Title="" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       <br/>
                <br/>
      <div class="caption" align="right">资源类型管理</div>
        <div align="right">
              &nbsp;<asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" />
        </div>
        <div class="caption" align="right">
        <br />
        <hr />
        <br />
        <div style="text-align: left">
            类型 :
            <asp:TextBox ID="txtname" runat="server" CssClass="text_one" TabIndex="1"></asp:TextBox>
            &nbsp;<asp:Button ID="btnSeach" runat="server" Text="查询" OnClick="btnSeach_Click" TabIndex="2"
                 />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           
            <ContentTemplate>
                  <div style="text-align: center;line-height: 15px">&nbsp;<asp:Label runat="server" ID="errMsg" ForeColor="red"></asp:Label></div>
                <asp:Panel ID="PanHaveRecords" runat="server">
                    <asp:Repeater ID="Rept" runat="server" OnItemCommand="Rept_ItemCommand">
                        <HeaderTemplate>
                            <table id="tbid" class="mytable" width="100%">
                                <tr>
                                    <th class="bj_first" style="width: 10%">
                                        序号
                                    </th>
                                    <th class="bj_first" style="width: 80%">
                                    类型
                                </th>
                                    
                                <th class="wid_xgsc bj_first">
                                </th>
                                <th class="wid_xgsc bj_first">
                                </th>
                                    
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                            
                                <td>
                                <%#DataBinder.Eval(Container.DataItem, "RowNumber")%>
                                     <asp:TextBox ID="id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>' Style="display: none;" />
                                   
                                </td>
                                
                                <td>
                                  
                                    <asp:LinkButton id="txtname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "name")%>' CommandName="Search" CssClass="link_blue"/>
                                     <asp:TextBox ID="txtdir" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "directory")%>' Style="display: none;" />
                                    
                                </td>
                                
                               
                                <td>
                                    <asp:LinkButton id="lbtnupdate" runat="server" Text="修改" CommandName="Update" CssClass="link_blue"/>
                        
                                </td>
                                
                                <td>
                                    <asp:LinkButton ID="lbdelete" runat="server" Text="删除" CommandName="Delete" OnClientClick="return confirm('确定删除吗?');"
                                        CssClass="link_blue" />
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
                                    每页:<asp:Label ID="lblrecord" runat="server" />条记录
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
                </table> </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSeach" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

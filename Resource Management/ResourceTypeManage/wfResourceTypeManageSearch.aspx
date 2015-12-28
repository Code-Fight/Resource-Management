<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="wfResourceTypeManageSearch.aspx.cs" Inherits="Resource_Management.ResourceTypeManage.wfResourceTypeManageSearch"
    Title="" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       <br/>
                <br/>
      <div class="caption" align="right">��Դ���͹���</div>
        <div align="right">
              &nbsp;<asp:Button ID="btnAdd" runat="server" Text="���" OnClick="btnAdd_Click" />
        </div>
        <div class="caption" align="right">
        <br />
        <hr />
        <br />
        <div style="text-align: left">
            ���� :
            <asp:TextBox ID="txtname" runat="server" CssClass="text_one" TabIndex="1"></asp:TextBox>
            &nbsp;<asp:Button ID="btnSeach" runat="server" Text="��ѯ" OnClick="btnSeach_Click" TabIndex="2"
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
                                        ���
                                    </th>
                                    <th class="bj_first" style="width: 80%">
                                    ����
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
                                    <asp:LinkButton id="lbtnupdate" runat="server" Text="�޸�" CommandName="Update" CssClass="link_blue"/>
                        
                                </td>
                                
                                <td>
                                    <asp:LinkButton ID="lbdelete" runat="server" Text="ɾ��" CommandName="Delete" OnClientClick="return confirm('ȷ��ɾ����?');"
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
                                    �ܼ�¼:<asp:Label ID="lblallrecord" runat="server" Text="Label"></asp:Label>
                                    ҳ��:<asp:Label ID="lblpageindex" runat="server" Text="Label"></asp:Label>
                                    /
                                    <asp:Label ID="lblallpagerecord" runat="server" Text="Label"></asp:Label>
                                    ÿҳ:<asp:Label ID="lblrecord" runat="server" />����¼
                                </td>
                                <td class="tex_right">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pages" CurrentPageButtonClass="cpb"
                                        CustomInfoSectionWidth="1%" FirstPageText="��ҳ" LastPageText="βҳ" NextPageText="��һҳ"
                                        PrevPageText="��һҳ" OnPageChanging="AspNetPager_PageChanging">
                                    </webdiyer:AspNetPager>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="PanNoRecords" runat="server">
                    <asp:Label ID="LabText" runat="server" Text="���Ĳ�ѯû�м�¼"></asp:Label>
                </asp:Panel>
                </table> </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSeach" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

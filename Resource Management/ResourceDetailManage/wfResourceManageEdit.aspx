<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="wfResourceManageEdit.aspx.cs" Inherits="Resource_Management.ResourceDetailManage.wfResourceManageEdit" Title="备品添加、编辑" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="list_all">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br/>
    <br/>
    <div class="caption" align="right">资源列表</div>
    <br/>
    <hr/>
    <br/>

    <table id="tb_spares" cellpadding="5">

        <tr>
            <td class="td_spares_l">名称</td>
            <td class="td_spares_r">
                <% if (_excutetype == EnExcuteType.Search)
                   { %><asp:Label ID="lb_name"
                                     runat="server"/><% }
                   else
                   { %>
                    <input type="text" id="tb_name" runat="server" tabindex="1" style="width: 220px"/>

                <% } %>
            </td>
        </tr>
        <tr>
            <td class="td_spares_l">类型</td>
            <td class="td_spares_r">

                <% if (_excutetype == EnExcuteType.Search)
                   { %><asp:Label ID="lb_type"
                                     runat="server"/><% }
                   else
                   { %><asp:DropDownList ID="DDL_type" Width="220px" DataTextField="name" DataValueField="id" runat="server" OnSelectedIndexChanged="DDL_type_SelectedIndexChanged"></asp:DropDownList>

                <% } %>
            </td>
        </tr>


        <tr>
            <td class="td_spares_l">上传人</td>
            <td class="td_spares_r">

                <% if (_excutetype == EnExcuteType.Search)
                   { %><asp:Label ID="lb_upload_people"
                                     runat="server"/><% }
                   else
                   { %>
                    <input type="text" id="tb_upload_people" runat="server" tabindex="1" style="width: 220px"/>

                <% } %>
            </td>
        </tr>

        <tr>
            <td class="td_spares_l">文件简介</td>
            <td class="td_spares_r">

                <% if (_excutetype == EnExcuteType.Search)
                   { %><asp:Label ID="lb_memo"
                                     runat="server"/><% }
                   else
                   { %>
                    <input type="text" id="tb_memo" runat="server" tabindex="1" style="width: 220px"/>

                <% } %>
            </td>
        </tr>
        <tr>
            <td class="td_spares_l">文件</td>
            <td style="background: #DFECF8">


                <%-- <% if (_excutetype == EnExcuteType.Search)
                       {%>--%>
                <asp:Repeater ID="Rept" runat="server" OnItemCommand="Rept_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%">
                        <tr>
                            <th></th>
                            <% if (_excutetype != EnExcuteType.Search)
                               { %>
                                <th></th>
                            <% } %>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id") %>' Style="display: none;"/>
                                <asp:TextBox ID="url" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "url") %>' Style="display: none;"/>
                                <asp:LinkButton ID="txtname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "name") %>' CommandName="download" CssClass="link_blue"/>
                            </td>
                            <% if (_excutetype != EnExcuteType.Search)
                               { %>
                                <td>
                                    <asp:LinkButton ID="btn_delete" runat="server" Text="删除" CssClass="link_blue" OnClientClick=" return confirm('确定删除吗?'); " CommandName="delete"></asp:LinkButton>
                                </td>
                            <% } %>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

                <% if (_excutetype != EnExcuteType.Search)
                   { %>
                    <br/>
                    <input type="file" name="uploadify" id="uploadify1"/>
                    <div id="fileQueue" style="margin-left: auto; margin-right: auto"></div>
                <% } %>
            </td>
        </tr>

    </table>
    <p></p>
    <div style="text-align: center">
        <asp:Label ID="UiLabErr" runat="server"
                   ForeColor="Red"/>
    </div>
    <p></p>
    <div align="center">
        <% if (_excutetype != EnExcuteType.Search)
           { %>
            <input type="button" id="btn_ok" Style="width: 70px" onclick=" submitData() " value="确定"/>
            <asp:Button ID="btnSubmit" runat="server" Visible="False"
                        OnClick="btnSubmit_Click" Style="width: 70px" TabIndex="5" Text="确定"
                        Width="121px"/>
        <% } %>
        <asp:Button ID="btnReturn" runat="server"
                    OnClick="btnReturn_Click" Style="width: 70px" TabIndex="7" Text="返回"
                    Width="150px"/>
    </div>

</div>
<script type="text/javascript">
    //var acct = document.getElementById('<%= DDL_type.ClientID %>')
    var errCount = 0;
    var fileCount = 0;
    var fileCancel = 0;
    var fileSuccess = 0;
    var AddIndex;
    var id = GetQueryString("id");

    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]);
        return null;
    };


    $(function() {
        $("#uploadify1").uploadify({
            'swf': "/JS/uploadify/uploadify.swf",
            'uploader': "UploadHandler.ashx?",
            'buttonText': "选择文件",
            'cancelImg': "/JS/uploadify/uploadify-cancel.png",

            'queueID': "fileQueue",
            'auto': false,
            'multi': true,
            'removeCompleted': false,
            'onUploadStart': function(file) {


                $("#uploadify1").uploadify("settings", "formData",
                {
                    'id': AddIndex,
                    'dir': $("#<%= DDL_type.ClientID %>").val(),
                    'name': $("#<%= tb_name.ClientID %>").val(),
                    'peo': $("#<%= tb_upload_people.ClientID %>").val(),
                    'memo': $("#<%= tb_memo.ClientID %>").val(),
                    'user': "<%= _user.Name %>"
                });
            },
            'onQueueComplete': function(queueData) {
                if (errCount != fileCancel && errCount > fileCancel) {
                    alert("有文件上传失败");
                    fileCount = 0;
                    $("#btn_ok").removeAttr("disabled");
                } else {
                    alert("所有文件上传成功");

                    //window.history.back(-1);
                    window.location.href = "wfResourceManageSearch.aspx";
                }

            },
            'onUploadError': function(file, errorCode, errorMsg, errorString) {
                errCount++;
            },
            'onSelect': function(file) {
                errCount = 0;
                fileCount++;
            },
            'onCancel': function(file) {
                fileCancel++;
                //alert('The file ' + file.name + ' was cancelled.');
            },
            'onUploadSuccess': function(file, data, response) {
                fileSuccess++;
            }


        });
        $("#uploadify1").css("margin-left", "auto");
        $("#uploadify1").css("margin-right", "auto");
    });

    function submitData() {
        //校验     
        var dir = document.getElementById("<%= DDL_type.ClientID %>").value;
        //var dir ='<%= _oldname %>';// $('#<%= DDL_type.ClientID %>').val();
        var name = $("#<%= tb_name.ClientID %>").val();
        var peo = $("#<%= tb_upload_people.ClientID %>").val();
        var memo = $("#<%= tb_memo.ClientID %>").val();

        if (name.length <= 0) {
            alert("请填写文件名称");
            return;
        }
        if (dir.length <= 0) {
            alert("请先选择文件类型");
            return;
        }
        if (peo.length <= 0) {
            alert("请填写文件上传者");
            return;
        }
        if ("<%= _excutetype %>" == "Add") {
            if (fileCount == 0) {
                alert("清先选择文件");
                return;
            }
        }
        var mode = "<%= _excutetype %>";
        $("#btn_ok").attr("disabled", "disabled");

        $.ajax({
            url: "AjaxResourceDetailHandler.ashx",
            type: "post",
            data: { 'name': name, 'peo': peo, 'memo': memo, 'dir': dir, 'mode': mode, 'id': id },
            success: function(msg) {
                if (msg == "error") {
                    alert("上传软件失败！请重试");
                    $("#btn_ok").removeAttr("disabled");
                } else {
                    if (fileCount > 0) {


                        AddIndex = msg;

                        $("#uploadify1").uploadify("upload", "*"); //后面的‘*’ 代表全部上传，如果不加则点击一次上传按钮，上传一个
                    } else {
                        window.location.href = "wfResourceManageSearch.aspx";
                    }
                }

            }

        });
    }
</script>

</asp:Content>
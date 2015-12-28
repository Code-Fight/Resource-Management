using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using BLL.ResourceType;
using Entity;

namespace Resource_Management.ResourceTypeManage
{
    public partial class wfResourceTypeManageSearch : wfmain
    {
        private string _searchname
        {
            get
            {
                if (ViewState["searchname"] == null)
                {
                    return string.Empty;
                }
                return ViewState["searchname"].ToString();
            }
            set
            {
                ViewState["searchname"] = value;
            }
        }
        private string _searchtype
        {
            get
            {
                if (ViewState["searchtype"] == null)
                {
                    return string.Empty;
                }
                return ViewState["searchtype"].ToString();
            }
            set
            {
                ViewState["searchtype"] = value;
            }
        }

        /// <summary>
        /// 列车时刻表信息
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                GetParam();
                FillForm();
            }
        }

        public override void GetParam()
        {
            _searchname  = this.GetQueryString("searchname");
            txtname.Text = _searchname;
       }


        


        /// <summary>
        /// 页面数据绑定
        /// </summary>
        protected override void FillForm()
        {
            int TotalCount = Pager.TotalCount;
            string ErrMsg = string.Empty;
            Pager.StrParm = GetSeachWhere();
            ResourceTypeManager dep = new ResourceTypeManager();
            DataTable dt = dep.GetList(Pager, out TotalCount);
            if (TotalCount > 0)
            {
                PanHaveRecords.Visible = true;
                PanNoRecords.Visible = false;
                Rept.DataSource = dt;
                Pager.TotalCount = TotalCount;
                this.AspNetPager1.RecordCount = TotalCount;
                this.AspNetPager1.PageSize = Pager.PageSize;
                AspNetPager1.CurrentPageIndex = Pager.PageIndex;
                lblallrecord.Text = TotalCount.ToString();
                lblpageindex.Text = Pager.PageIndex.ToString();
                lblallpagerecord.Text = AspNetPager1.PageCount.ToString();
                lblrecord.Text = Pager.PageSize.ToString();

                Rept.DataBind();
            }
            else
            {
                PanHaveRecords.Visible = false;
                PanNoRecords.Visible = true;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeach_Click(object sender, EventArgs e)
        {
            try
            {
                _searchname = txtname.Text.Trim();
                InitPage();
                FillForm();
                RefreshForm();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得检索条件
        /// </summary>
        /// <returns></returns>
        private string GetSeachWhere()
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                string name = string.Empty;
                if (_searchname.Trim().Length > 0)
                {
                    strSQL.Append(" and name like '%" + _searchname.Trim() + "%'");
                }
                 return strSQL.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Rept_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            TextBox tbx = (TextBox)e.Item.FindControl("id");
            LinkButton link = (LinkButton) e.Item.FindControl("txtname");
            TextBox txtdir = (TextBox) e.Item.FindControl("txtdir");
            if (e.CommandName == "Search")
            {
                Response.Redirect("wfResourceTypeManageEdit.aspx?mode=" + (int) EnExcuteType.Search + "&id=" + tbx.Text +
                                  "&" + GetParamInfo());
            }
            if (e.CommandName == "Update")
            {
                Response.Redirect("wfResourceTypeManageEdit.aspx?mode=" + (int) EnExcuteType.Update + "&id=" + tbx.Text +
                                  "&" + GetParamInfo());
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    SpareResourceTypeEntity info = new SpareResourceTypeEntity();
                    info.delete_user = _user;
                    info.Id = int.Parse(tbx.Text);

                    ResourceTypeManager dep = new ResourceTypeManager();
                    dep.Delete(info.Id);
                    DeleteDir(txtdir.Text);
                    FillForm();
                    RefreshForm();
                }
                catch (Exception ex)
                {
                    errMsg.Text = ex.Message;
                    RefreshForm();
                    //throw;
                }
            }
        }

        /// <summary>
        /// 删除对应目录
        /// </summary>
        /// <param name="name"></param>
        private void DeleteDir(string name)
        {
            try
            {
                //获取文件夹
                string dir = Server.MapPath("~/Window/ResourceDetailManage/upload/" + name);
                if (Directory.GetDirectories(dir).Length == 0 && Directory.GetFiles(dir).Length == 0)
                {
                    Directory.Delete(dir); //删除文件夹，若不删除文件夹则不需要 Directory.Delete(dir)
                    return;
                }
                foreach (string var in Directory.GetDirectories(dir))
                {
                    DeleteDir(var);
                }
                foreach (string var in Directory.GetFiles(dir))
                {
                    File.Delete(var);
                }
                Directory.Delete(dir); //删除文件夹，若不删除文件夹则不需要 Directory.Delete(dir)
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            Response.Redirect("wfResourceTypeManageEdit.aspx?mode=" + (int)EnExcuteType.Add + "&" + GetParamInfo());
        }
        
        protected override string GetParamInfo()
        {
            return base.GetParamInfo() + "&searchname=" + _searchname +  "&searchtype=" + _searchtype;
        }

        /// <summary>
        /// 翻页
        /// </summary>
        protected override void InitPage()
        {
            Pager.PageIndex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            _pagerflag = false;
        }


    }
}

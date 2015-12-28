using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using BLL.ResourceDetail;
using BLL.ResourceType;
using DAL;
using Entity;

namespace Resource_Management.ResourceDetailManage
{
    public partial class wfResourceManageSearch : wfmain
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
            _searchname = this.GetQueryString("searchname");
            txtname.Text = _searchname;
            ResourceTypeManager resourceTypeManager = new ResourceTypeManager();
            DDL_type.DataSource = resourceTypeManager.GetAllResourceType();
            DDL_type.DataBind();
            DDL_type.Items.Insert(0, "");
        }





        /// <summary>
        /// 页面数据绑定
        /// </summary>
        protected override void FillForm()
        {
            int TotalCount = Pager.TotalCount;
            string ErrMsg = string.Empty;
            Pager.StrParm = GetSeachWhere();
            ResourceDetailManager dep = new ResourceDetailManager();
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
                    strSQL.Append(" and t_resource_detail.name like '%" + _searchname.Trim() + "%'");
                }
                if (DDL_type.SelectedItem.Text.Trim().Length > 0)
                {
                    strSQL.Append(" and type_id ='" + DDL_type.SelectedValue + "'");
                }
                if (txtmemo.Text.Trim().Length > 0)
                {
                    strSQL.Append(" and memo like '%" + txtmemo.Text.Trim() + "%'");
                }
                if (txt_start_time.Text.Trim().Length > 0)
                {
                    strSQL.Append(" and t_resource_detail.insert_time >='" + txt_start_time.Text.Trim() + "'");
                }
                if (txt_end_time.Text.Trim().Length > 0)
                {
                    strSQL.Append(" and t_resource_detail.insert_time <='" + txt_end_time.Text.Trim() + "'");
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
            if (e.CommandName == "Search")
            {
                Response.Redirect("wfResourceManageEdit.aspx?mode=" + (int)EnExcuteType.Search + "&id=" + tbx.Text +
                                  "&" + GetParamInfo());
            }
            if (e.CommandName == "Update")
            {
                Response.Redirect("wfResourceManageEdit.aspx?mode=" + (int)EnExcuteType.Update + "&id=" + tbx.Text + "&" + GetParamInfo());
            }
            if (e.CommandName == "download")
            {
               
                //download_id.Text = tbx.Text;
               // download_Click(null, null);
                ResourceDetailManager bll = new ResourceDetailManager();
                ResourceDetailEntity entity = bll.GetInfo(CommonDBCheck.ToInt(tbx.Text));
                string hz = entity.Url.Substring(entity.Url.LastIndexOf('.'), entity.Url.Length - entity.Url.LastIndexOf('.'));
                string fileName = entity.Name + hz;//客户端保存的文件名
                string filePath = Server.MapPath(entity.Url);//路径
                FileSaveAs(fileName, filePath);
            }
            if (e.CommandName == "Delete")
            {
                try
                {
                    ResourceDetailEntity info = new ResourceDetailEntity();
                    info.delete_user = _user;
                    info.Id = int.Parse(tbx.Text);
                    ResourceDetailManager dep = new ResourceDetailManager();
                    dep.Delete(info.Id);
                    dep.DeleteUpload(0, info.Id);
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
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            Response.Redirect("wfResourceManageEdit.aspx?mode=" + (int)EnExcuteType.Add + "&" + GetParamInfo());
        }

        protected override string GetParamInfo()
        {
            return base.GetParamInfo() + "&searchname=" + _searchname + "&searchtype=" + _searchtype;
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
